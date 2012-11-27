using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Top.Api.Jushita;


namespace Top.Api.Jushita.stream
{
    /// <summary>
    /// 消息队列的driver
    /// </summary>
    public class MessageDriver
    {
        private string _reportUrl;
        public string reportUrl
        {
            get { return _reportUrl; }
            set { _reportUrl = value; }
        }

        private int _reportCount = 100;
        public int reportCount
        {
            get { return _reportCount; }
            set { _reportCount = value; }
        }

        private long _reportInterval = 5 * 60 * 1000L;
        public long reportInterval
        {
            get { return _reportInterval; }
            set { _reportInterval = value; }
        }

        private int _consumerThreadCount = 10;
        public int consumerThreadCount
        {
            get { return _consumerThreadCount; }
            set { _consumerThreadCount = value; }
        }

        private int _timeoutSeconds = 10;
        public int timeoutSeconds
        {
            get { return _timeoutSeconds; }
            set { _timeoutSeconds = value; }
        }


        private IMessageHandler _messageHandler;
        public IMessageHandler messageHandler
        {
            get { return _messageHandler; }
            set { _messageHandler = value; }
        }


        private MessageCircleQueue _queue;
        public MessageCircleQueue queue
        {
            get { return _queue; }
            set { _queue = value; }
        }

        private string _appKey;
        public string appKey
        {
            get { return _appKey; }
            set { _appKey = value; }
        }

        private string _secret;
        public string secret
        {
            get { return _secret; }
            set { _secret = value; }
        }

        private Timer _timerTask;
        public Timer timerTask
        {
            get { return _timerTask; }
            set { _timerTask = value; }
        }

        public MessageDriver(string appKey, string secret)
        {
            if (appKey == null || secret == null)
                throw new NullReferenceException();
            this.appKey = appKey;
            this.secret = secret;
        }

        private ReportTask reportTask;
        private consumeThreadTask consumeTask;
        private Thread[] consumerThreads;

        public void start()
        {
            if (this.messageHandler == null)
                throw new NullReferenceException("messageHandler must not be null");
            if (this.reportUrl == null)
                throw new NullReferenceException("reportUrl must not be null");
            int queueSizeMultiple = 20;
            long reportTimerInterval = 50;
            //初始化消息队列
            queue = new MessageCircleQueue(reportCount * queueSizeMultiple, timeoutSeconds);
            //初始化消费线程数组
            consumerThreads = new Thread[consumerThreadCount];
            //构建消费者线程的Task任务
            consumeTask = new consumeThreadTask(messageHandler, queue);
            //构建线程委托任务
            ThreadStart threadTask = new ThreadStart(consumeTask.doTask);
            for (int i = 0; i < consumerThreads.Length; i++)
            {
                consumerThreads[i] = new Thread(threadTask);
                consumerThreads[i].Start();
            }
            reportTask = new ReportTask(queue, reportUrl, appKey, secret);
            reportTask.reportCount = reportCount;
            reportTask.reportInterval = reportInterval;
            TimerCallback tcb = reportTask.run;
            timerTask = new Timer(tcb, null, reportTimerInterval, reportTimerInterval);
        }

        public void setReportCount(int count)
        {
            this.reportCount = count;
        }

        public void setReportInterval(long interval)
        {
            this.reportInterval = interval;
        }

        /// <summary>
        /// 让MessageDriver的服务停下来
        /// </summary>
        public void stop()
        {
            for (int i = 0; i < consumerThreads.Length; i++)
            {
                consumeTask.shutdown();
            }
            timerTask.Dispose();
        }

        /// <summary>
        /// 往message队列中放消息
        /// </summary>
        /// <param name="message"></param>
        public void pushMessage(string message)
        {
            try
            {
                //Console.WriteLine("MessageDriver putting messge to queue");
                queue.put(new Message(message));
            }
            catch (ThreadInterruptedException )
            {
                //无视中断
                ;
            }
        }


        /// <summary>
        /// 定时器任务类，给主线程每隔reportTimerInterval检查一次要不要report
        /// </summary>
        private class ReportTask
        {
            private MessageCircleQueue queue;
            private Report lastFailedReport;
            private long lastReportTime;
            private string reportUrl;
            private string appKey;
            private string secret;
            private JushitaTopClient client;
            private int _reportCount = 100;
            public int reportCount
            {
                get { return _reportCount; }
                set { _reportCount = value; }
            }

            private long _reportInterval = 5 * 60 * 1000L;
            public long reportInterval
            {
                get { return _reportInterval; }
                set { _reportInterval = value; }
            }

            public ReportTask(MessageCircleQueue queue, string reportUrl, string appKey, string secret)
            {
                this.queue = queue;
                this.reportUrl = reportUrl;
                this.appKey = appKey;
                this.secret = secret;
                client = new JushitaTopClient(reportUrl, appKey, secret);
                lastReportTime = Message.currentTimeMills();
            }

            public void run(Object obj)
            {
                if (lastFailedReport != null)
                {
                    report(lastFailedReport);
                }
                else
                {
                    int checkCount = queue.check();
                    Console.WriteLine("checkCount: " + checkCount);
                    Thread.Sleep(2000);
                    if (checkCount >= reportCount || (checkCount > 0 && (Message.currentTimeMills() - lastReportTime) > reportInterval))
                    {
                        report(queue.report());
                        lastReportTime = Message.currentTimeMills();
                    }
                }
            }

            public void report(Report report)
            {
                IDictionary<string, string> param = new Dictionary<string, string>();
                param.Add("report", report.asJson());
                param.Add("user_id", "1");
                param.Add("topic", "topic");
                try
                {
                    client.Execute("report_message", param, "session");
                    lastFailedReport = null;
                }
                catch (TopException)
                {
                    lastFailedReport = report;
                }
            }
        }



        /// <summary>
        /// 消费者的线程任务，主要就是从message队列中取一个消息消费，然后记录下来消费的状态
        /// </summary>
        private class consumeThreadTask
        {
            private IMessageHandler messageHandler;
            private MessageCircleQueue queue;
            private volatile Boolean _run = true;
            private Boolean run
            {
                get { return _run; }
                set { _run = value; }
            }

            public consumeThreadTask(IMessageHandler messageHandler, MessageCircleQueue queue)
            {
                this.messageHandler = messageHandler;
                this.queue = queue;
            }

            public void doTask()
            {
                while (run)
                {
                    try
                    {
                        //Console.WriteLine("taking a message from queue by consumerTaskThread");
                        Message message = queue.take();
                        Boolean isSuccess = messageHandler.handle(message);
                        if (isSuccess)
                        {
                            //Console.WriteLine("message consumed successful");
                            message.state = Message.State.SUCCESS;
                        }
                        else
                        {
                            //Console.WriteLine("message consumed failed");
                            message.state = Message.State.FAILED;
                        }
                    }
                    catch (ThreadInterruptedException )
                    {
                        //Console.WriteLine("consumeTaskThread is interrupted");
                        //被中断就跳出
                        break;
                    }
                    catch (Exception )
                    {
                        //别的错都无视
                        ;//do nothing
                    }
                }
            }

            public void shutdown()
            {
                run = false;
            }
        }
    }
}
