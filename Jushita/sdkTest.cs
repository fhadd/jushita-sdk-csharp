using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api.Jushita;
using Top.Api.Jushita.stream;
using System.Net;
using System.IO;

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
            //string pub_secret = "d41ee217895de8b64ec44653edce9263";
            //string sub_secret = "7398570d7bb0c4473bc0962b61df321c";
            //string session = "6c4631d7debcbdf2f3555ac51f4a88a703d6b57ef2364992bfec6ad07bf5df78";
            string session = "00d54d4b2a53d86b6678033d656f9e557373a434bd592b6bf7f8dc1b7cbfa295";
            //string topic = "guichen.test1";
            //string user_id = "2010907121";
            String secret = "e890f61b79599f625498538c5b253032";
            String appKey = "488973";
            string serverUrl = "http://10.235.144.91:8080/message/pub";
            string connectUrl = "http://10.235.144.91:8080/message/sub";
            string connectId = "liubing_test_connect";
            string format_xml = "xml";
            string format_json = "json";
            String onLine_url = "http://eai.tmall.com/message/pub";
            String user_id = "2011220128";

            String online_appkey = "21166802";
            String online_session = "39c20ffd696cec3d5ef7e23da1ce0180c8e122662c48a7d846fc763d563a21c7";
            String online_secret = "2021b9532ddefccde4ec0841b31b92f3";
            String online_id = "274927542";

            String osa_topic = "osa-msg-deliverynotice-test";
            String osa_secret = "d41ee217895de8b64ec44653edce9263";
            String osa_session = "a017a3ccecfe5ab40151a2e36ed7a52abdb452d17c0bb4d3b7858127a785429c";
            String osa_usrID = "2011220128";
            String osa_appkey = "483936";

            String message = "";
            String line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("d:\\111111.txt");
            while ((line = file.ReadLine()) != null)
            {
                message += line;
            }
            file.Close();
            int timeout = 10000;
            JushitaTopClient client = new JushitaTopClient(serverUrl, osa_appkey, osa_secret, "xml", timeout);
            IDictionary<string, string> param = new Dictionary<string, string>();
            //String user_id = "2011220128";
            String topic = "cf-topic-3";
            //param.Add("biz_id", "101280000000680001");
            //param.Add("sc_item_name", "TOpitemName123");
            //param.Add("outer_code", "topouterCode123");
            //param.Add("inventorys", "[{\"storeCode\":\"yanqiu_002\",\"quantity\":100,\"inventoryType\":1}]");
            //param.Add("items", "{\"simple_sc_item_maps\":{\"simple_sc_item_map\":[{\"item_id\":\"1500009650483\"}]}}");
            param.Add("topic", osa_topic);
            param.Add("user_id", osa_usrID);
            param.Add("message", "liubing.tk");
            //Console.WriteLine("message: " + message);
            try
            {
                string rsp = client.Execute("put_data", param, osa_session);
                Console.WriteLine(rsp);
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                            Console.WriteLine(streamReader.ReadToEnd());
                    }
            }
            //for (; ; )
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
