using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Stream;
using Top.Api.Stream.Message;
using Top.Api.Stream.Connect;
using Top.Api.Jushita.stream;


namespace Top.Api.Jushita
{
    public class JushitaConfigurationV2 : Configuration
    {
        private String url = "http://eai.tmall.com/message/sub";
        private String reportUrl = "http://eai.tmall.com/message/report";
        private List<String> topics = null;

        private ITopCometMessageListener _topCometMessageListener;
        public ITopCometMessageListener topCometMessageListener
        {
            get { return _topCometMessageListener; }
            set { _topCometMessageListener = value;}
        }

        private MessageDriver _driver;
        public MessageDriver driver
        {
            get { return _driver; }
            set { _driver = value; }
        }


        public JushitaConfigurationV2(String appKey, String secret, String connectId, List<String> topics)
            : base(appKey, secret, connectId, topics, new Dictionary<string, string>())
        {
            this.topics = topics;
            //保持单线程 多线程交给MessageDriver去处理 这是为了确保顺序的把消息提交给MessageDriver
            base.SetMaxThreads(1);
            base.SetMinThreads(1);

            //实例化消息driver
            driver = new MessageDriver(appKey, secret);

            //设置url。包括连接url和report的url
            this.SetConnectUrl(url);

            //虽然只有一个，因为是set，所以还要遍历一下
            foreach (TopCometStreamRequest cometReq in this.GetConnectReqParam())
            {
                cometReq.SetConnectListener(new InnerConnectionLifeCycleListener(null));
                cometReq.SetMsgListener(new InnerMessageListener(driver, null));
                cometReq.GetOtherParam().Add("ver", "2");
            }
        }

        public void setCometMessageListener(ITopCometMessageListener topCometMessageListener)
        {
            this.topCometMessageListener = topCometMessageListener;
            MessageHandler handler = new MessageHandler(this.topCometMessageListener);
            driver.messageHandler = handler;
        }

        public void setReportCount(int count)
        {
            driver.setReportCount(count);
        }

        public void setReportInterval(long interval)
        {
            driver.setReportInterval(interval);
        }

        public new void SetConnectUrl(string url)
        {
            this.url = url;
            base.SetConnectUrl(url);
            this.reportUrl = url.Substring(0, url.LastIndexOf('/') + 1) + "report";
            this.driver.reportUrl = this.reportUrl;
        }

        /// <summary>
        /// 消息处理类，调用消息监听函数
        /// </summary>
        class MessageHandler : IMessageHandler
        {
            private ITopCometMessageListener _topCometMessageListener;
            public ITopCometMessageListener topCometMessageListener
            {
                get { return _topCometMessageListener; }
                set { _topCometMessageListener = value; }
            }
            public MessageHandler(ITopCometMessageListener topCometMessageListener)
            {
                this._topCometMessageListener = topCometMessageListener;
            }

            public Boolean handle(Message message)
            {
                try
                {
                    topCometMessageListener.OnReceiveMsg(message.message);
                }
                catch (Exception)
                {
                    //只要有异常就算失败
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 消息监听类，收到消息的回调实现往messageDriver中塞数据
        /// </summary>
        private class InnerMessageListener : ITopCometMessageListener
        {
            private MessageDriver driver;
            private ITopCometMessageListener topCometMessageListener;

            public InnerMessageListener(MessageDriver driver, ITopCometMessageListener topCometMessageListener)
            {
                this.topCometMessageListener = topCometMessageListener;
                this.driver = driver;
            }

            public void OnConnectMsg(string message)
            {
                if (topCometMessageListener != null)
                    topCometMessageListener.OnConnectMsg(message);
            }

            public void OnHeartBeat()
            {
                if (topCometMessageListener != null)
                    topCometMessageListener.OnHeartBeat();
            }

            public void OnReceiveMsg(string message)
            {
                // 收到消息时push到driver里去
                driver.pushMessage(message);
            }

            public void OnConnectReachMaxTime()
            {
                ;
            }

            public void OnDiscardMsg(string message)
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnDiscardMsg(message);
                }
            }

            public void OnServerUpgrade(string message)
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnServerUpgrade(message);
                }
            }

            public void OnServerRehash()
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnServerRehash();
                }
            }

            public void OnServerKickOff()
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnServerKickOff();
                }
            }

            public void OnClientKickOff()
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnClientKickOff();
                }
            }

            public void OnOtherMsg(string message)
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnOtherMsg(message);
                }
            }

            public void OnException(Exception ex)
            {
                if (topCometMessageListener != null)
                {
                    topCometMessageListener.OnException(ex);
                }
            }
        }

        private class InnerConnectionLifeCycleListener : IConnectionLifeCycleListener
        {
            private InnerConnectionLifeCycleListener topCometConnectionListener;

            public InnerConnectionLifeCycleListener(InnerConnectionLifeCycleListener topCometConnectionListener)
            {
                this.topCometConnectionListener = topCometConnectionListener;
            }

            public void OnBeforeConnect()
            {
                if (topCometConnectionListener != null)
                    topCometConnectionListener.OnBeforeConnect();
            }

            public void OnConnect()
            {
                //if (topCometConnectionListener != null)
                //    topCometConnectionListener.OnConnect();
                Console.WriteLine("connect successful");
            }

            public void OnException(Exception throwable)
            {
                //if (topCometConnectionListener != null)
                //    topCometConnectionListener.OnException();
                Console.WriteLine("exception : " + throwable.StackTrace);
            }

            public void OnConnectError(Exception e)
            {
                //if (topCometConnectionListener != null)
                //    topCometConnectionListener.OnConnectError();
                Console.WriteLine("connect error :" + e.StackTrace);
            }

            public void OnReadTimeout()
            {
                if (topCometConnectionListener != null)
                    topCometConnectionListener.OnReadTimeout();
            }

            public void OnMaxReadTimeoutException()
            {
                if (topCometConnectionListener != null)
                    topCometConnectionListener.OnMaxReadTimeoutException();
            }

            public void OnSysErrorException(Exception e)
            {
                if (topCometConnectionListener != null)
                    topCometConnectionListener.OnSysErrorException(e);
            }
        }
                    
    }
}






