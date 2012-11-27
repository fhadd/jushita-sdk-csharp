﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Jushita;
using Top.Api.Jushita.stream;

namespace Top.Api.Jushita
{
    class sdkTest
    {
        public static void Main(string[] args)
        {
            //string app_key = "487524";
            //string app_secret = "c7001ff65ea2feb229b52eec7c585f2d";
            //string session = "cf4914500c8ece1f445dc604a9c3e9a80c500df07ad7e27071234d7c5a6b769c";
            //string topic = "cfapptest.guichen.Msg.get";
            //string user_id = "2011220128";

            string pub_key = "483936";
            string sub_key = "483937";
            string pub_secret = "d41ee217895de8b64ec44653edce9263";
            string sub_secret = "7398570d7bb0c4473bc0962b61df321c";
            string session = "6c4631d7debcbdf2f3555ac51f4a88a703d6b57ef2364992bfec6ad07bf5df78";
            string topic = "guichen.test1";
            string user_id = "2010907121";
            string serverUrl = "http://10.235.144.91:8080/message/pub";
            string connectUrl = "http://10.235.144.91:8080/message/sub";
            string connectId = "liubing_test_connect";

            JushitaTopClient client = new JushitaTopClient(serverUrl, pub_key, pub_secret);
            IDictionary<string, string> param = new Dictionary<string, string>();
            param.Add("topic", topic);
            param.Add("user_id", user_id);
            param.Add("message", "*****hellotk*******");
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    string rsp = client.Execute("put_data", param, session);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }
            }
            Console.WriteLine("pub done");
            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("ready to receve");
            List<string> topics = new List<string>();
            topics.Add("guichen.test1");
            JushitaConfigurationV2 config = new JushitaConfigurationV2(sub_key, sub_secret, connectId, topics);
            config.SetConnectUrl(connectUrl);
            config.setCometMessageListener(new messageListener());
            config.setReportCount(10);
            JushitaTopCometStreamImpl stream = new JushitaTopCometStreamImpl(config);
            stream.Start();
            for (; ; )
                System.Threading.Thread.Sleep(5000);
        }

        public class messageListener : Top.Api.Stream.Message.ITopCometMessageListener
        {

            #region ITopCometMessageListener 成员

            public void OnConnectMsg(string message)
            {
                //throw new NotImplementedException();
            }

            public void OnHeartBeat()
            {
                //throw new NotImplementedException();
            }

            public void OnReceiveMsg(string message)
            {
                if (message.Length < 30)
                    Console.WriteLine("message: " + message);
                else
                    Console.WriteLine("message" + message.Length);
                //throw new NotImplementedException();
            }

            public void OnConnectReachMaxTime()
            {
                //throw new NotImplementedException();
            }

            public void OnDiscardMsg(string message)
            {
                //throw new NotImplementedException();
            }

            public void OnServerUpgrade(string message)
            {
                //throw new NotImplementedException();
            }

            public void OnServerRehash()
            {
                //throw new NotImplementedException();
            }

            public void OnServerKickOff()
            {
                //throw new NotImplementedException();
            }

            public void OnClientKickOff()
            {
                //throw new NotImplementedException();
            }

            public void OnOtherMsg(string message)
            {
                //throw new NotImplementedException();
            }

            public void OnException(Exception ex)
            {
                //throw new NotImplementedException();
            }

            #endregion
        }
    }
}