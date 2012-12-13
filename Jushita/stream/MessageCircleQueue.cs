using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using Top.Api.Jushita.stream;

namespace Top.Api.Jushita.stream
{
    public class MessageCircleQueue
    {
        private readonly Object thisLock = new Object();
        private Message[] messageContent;
        private int producerP = 0;
        private int consumerP = 0;
        private int checkP = 0;
        private int reportP = 0;
        private int size;
        private int count = 0;
        private int unreportedCount = 0;
        private long messageTimeout = 10000L;
        private int checkedCount = 0;

        public MessageCircleQueue(int size)
        {
            this.messageContent = new Message[size];
            this.size = size;
        }

        public MessageCircleQueue(int size, int timeoutSecond)
        {
            this.size = size;
            this.messageTimeout = timeoutSecond * 1000L;
            this.messageContent = new Message[size];
        }

        /// <summary>
        /// 此方法压入元素(message)并循环递增生产指针，当遇到报告指针时hold住直到报告指针被往前移动。
        /// </summary>
        /// <param name="message">需要压入队列的message</param>
        /// <returns>put进去的元素在队列中的位置;如果由于异常而调用失败，函数返回-1</returns>
        public int put(Message message)
        {
            Monitor.Enter(thisLock);
            try
            {
                while (unreportedCount == size)
                {
                    //Console.WriteLine("unreportedCount == size waiting report");
                    Monitor.Wait(thisLock);
                }
                //放入消息的时候带上自己的位置
                message.index = producerP;
                messageContent[producerP] = message;
                int tempP = producerP;
                if (++producerP == size)
                    producerP = 0;
                ++unreportedCount;
                ++count;
                Monitor.Pulse(thisLock);
                return tempP;
            }
            catch(ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
                return -1;
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
        }

        /// <summary>
        /// 此方法弹出元素并递增消费指针，当遇到生产指针时hold住直到生产指针被往前移动
        /// </summary>
        /// <returns>返回队列中的message;如果因为异常函数调用失败，函数返回null</returns>
        public Message take()
        {
            Monitor.Enter(thisLock);
            try
            {
                while (consumerP == producerP)
                {
                    //Console.WriteLine("consumeP==producerP waiting put message");
                    Monitor.Wait(thisLock);
                }
                Message message = messageContent[consumerP];
                message.taken();
                if (++consumerP == size)
                    consumerP = 0;
                --count;
                return message;
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
        }

        /// <summary>
        /// 此方法移动确认指针尽可能长的距离，直到遇到未确认也未超时的消息
        /// 此方法不是线程安全的，应当仅在单个线程中调用它或者在外部同步
        /// </summary>
        /// <returns>这次确认的消息数量</returns>
        public int check()
        {
            do
            {
                Message message = messageContent[checkP];
                if (message == null || (message.state == Message.State.UNKNOWN && (Message.currentTimeMills() - message.takeTime) < messageTimeout))
                {
                    break;
                }
                ++checkedCount;
                if (++checkP == size)
                    checkP = 0;
            } while (checkP != consumerP);
            return checkedCount;
        }

        /// <summary>
        /// 此方法统计上一次报告位置到现在确认位置之间的消息并生成报告
        /// 此方法本身是线程安全的，但是会影响check方法的状态，所以应该只在单线程中顺序的调用check方法和此方法，或者在外部保持同步。
        /// 一般来说应该成对的调用check和report方法
        /// </summary>
        /// <returns>报告</returns>
        public Report report()
        {
            Monitor.Enter(thisLock);
            try
            {
                if (checkedCount == 0)
                    return null;
                Report report = new Report();
                Dictionary<long,string> successMap = new Dictionary<long, string>();
                while (reportP != checkP || checkedCount == size)
                {
                    Message message = messageContent[reportP];
                    messageContent[reportP] = null;
                    switch (message.state)
                    {
                        case Message.State.SUCCESS:
                            if (successMap.ContainsKey(message.nextOffsetDO.id))
                                successMap.Remove(message.nextOffsetDO.id);
                            successMap.Add(message.nextOffsetDO.id, message.nextOffset);
                            break;
                        case Message.State.FAILED:
                        case Message.State.UNKNOWN:
                            report.fOffset.Add(message.offset);
                            break;
                    }
                    if (++reportP == size)
                        reportP = 0;
                    --unreportedCount;
                    --checkedCount;
                }
                report.sOffset.AddRange(successMap.Values);
                Monitor.PulseAll(thisLock);
                return report;
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
        }
    }
}

