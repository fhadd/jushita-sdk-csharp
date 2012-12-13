using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Stream.Message;

namespace Top.Api.Jushita.stream
{

    /// <summary>
    /// 测试用的类，正常使用不要调用
    /// </summary>
    public class MockTopCometMessageListener : ITopCometMessageListener
    {
        private MessageDriver driver;
        private ITopCometMessageListener topCometMessageListener;
        private Random random = new Random(237592648);

        public MockTopCometMessageListener(MessageDriver driver, ITopCometMessageListener topCometMessageListener)
        {
            this.driver = driver;
            this.topCometMessageListener = topCometMessageListener;
        }

        public void OnConnectMsg(string message)
        {
            if (topCometMessageListener != null)
                topCometMessageListener.OnConnectMsg(message);
        }

        public void OnHeartBeat()
        {
            Console.WriteLine("heart beat");
        }

        public void OnReceiveMsg(string message)
        {
            Console.WriteLine(message);
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
}
