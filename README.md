聚石塔三方集成（JIP）客户端SDK（V2）文档
==
业务场景概述
--
- 消息推动是三方集成平台（JIP）数据互通的两种方式的一种，通过发布-订阅的方式为三方建立数据通道。消息发布方提供发布的消息类型（Topic），消息订阅方订阅需要的Topic，从而建立一个发布-订阅关系，在用户对通道授权之后，数据就可以在这个通道上被传输。
 
- 消息发布者通过HTTP请求提交消息给JIP,消息订阅者通过HTTP长连接和JIP保持通信，一旦在该通道上有数据产生，JIP就会把数据通过长连接推送给消息订阅者。

- 因为JIP的长连接使用了Top的长连接管理工具，所以SDK也是基于Top的长连接SDK改造而来。SDK的作用不仅是用来发起长连接，而且还要负责从JIP接受消息并对消息消费的可靠性做出保证。使用SDK可以大大简化ISV的工作。

代码组织
---
聚石塔相关C#代码主要在以下包中：

 * Top.Api.Jushita
 * Top.Api.Jushita.stream

SDK的实现概述
---
SDK使用长连接的方式同服务器保持通信，并通过长连接收取消息和报告数据消费信息。

SDK内部使用生产者/消费者模型，对象是一个循环消息队列，该队列从服务器端收到消息并存放到队列中提供给ISV消费。该队列会维护四个指针，生产指针、消费指针、确认指针、报告指针。

  * 生产指针：从服务器端收到消息之后，放到队列中，生产指针用来标记当前最新被放置的消息在队列中的位置
  * 消费指针：当前可供ISV消费的消息在队列的位置
  * 确认指针：当前被ISV确认消费状态的消息所在队列的位置
  * 报告指针：上一次发送消费信息给服务器端时，被统计的消息所在队列的位置

消费部分实现了一个线程池，线程池都跑一个任务：从消息队列中取出消息提交给ISV消费，并期待ISV返回一个消费的状态：`success`或者`failed`。如果消费超时就算失败。每次ISV返回一个消息的消费状态，就会立即更新到消息队列中对应消息的属性中.系统每隔一段时间会检查report机制是否被触发，并移动确认指针；每次report之后，都会更新报告指针。

可靠性
---

可靠性的保证主要是指消息的消费状态问题、比如丢消息。这个通过report机制来保证。

- SDK负责记录下ISV消费消息的状态，无论是成功还是失败，并每隔一段时间检查一次看是否满足report的条件。report有两种触发方式：
    -  时间触发：是指如果超过一定的时间还没有report，那就report一次。
    -  数量触发：是指确认ISV成功处理了一定量的消息就会report一次。任何一个触发方式满足都会触发report机制。
- report的内容包含了最新消费成功消息的下一条消息的offset，和消费失败消息的offset列表
- 服务器收到report过来的信息就可以为该订阅关系保存消费的状态，以及对消费失败消息的重发

API说明（C#）
---

`JushitaConfigurationV2`

是SDK的参数配置类，控制SDK的参数配置。它有一个构造函数

        JushitaConfigurationV2（string appkey, stirng secret, string connectId, List<string> topics）
构造函数接受的参数有:

* `appkey`：预先分配的第三方应用ID
* `secret`: 认证密钥
* `connecId`: 连接的ID标识，由调用者指定
* `topics`: 订阅的topic列表

除此之外还有一些主要的setter方法：

        public void setCometMessageListener(ITopCometMessageListener listener)
设置SDK的消息监听类，用来接收服务器推送的消息。SDK会接收来自服务器推送的消息，放到本地的一个队列中并提交给ISV处理，ISV通过实现`ITopCometMessageListern`的`OnReceiveMsg(string message)`方法来自行处理消息。

        public void setReportCount(int count)
设置report的数量触发阈值。report有两种机制：时间触发和数量触发。时间是指如果超过一定的时间还没有report，那就report一次;数量是确认ISV成功处理了一定量的消息就会reort一次。
    
        public void setReportInterval(long interval)
设置Report的时间触发阈值。

        public void setConnectUrl(string url)
设置服务器的url。

`JushitaTopCometStreamImpl`

是SDK向服务器发起长连接的驱动类,它有一个构造函数和两个方法

        JushitaTopCometStreamImpl(JushitaConfigurationV2 configuration)
    
构造函数接受一个`JushitaConfigurationV2`类型的参数初始化自己。

        public void Start()
    
这个方法启动驱动类，发起长连接

        public void Stop()

这个方法停止驱动类，SDK停止工作

`JushitaTopClient`

聚石塔用客户端

        JushitaTopClient(string serverUrl, string appKey, string appSecret)
构造函数，`serverUrl`是服务器的url，`appKey`和`appSecret`分别是第三方应用的appkey和secret。

        public string Execute(string apiName, IDictionary<string, string>param, string session)

执行具体的发送请求。`apiName`是本次请求的名称，`param`是请求的参数集，`session`是请求的access_token值。

SDK应用逻辑
---
SDK的应用的主要逻辑分两个步骤：

* 配置`JushitaConfigurationV2`配置类，以参数形式传递给`JushitaTopCometStreamImpl`。
* `JushitaTopCometStreamImpl`根据配置参数，实例化自身并发起连接。


一些问题的说明：
---
1. 消息消费超时的问题：
 > 关于消息消费超时的问题，现在SDK是这样的，我们的SDK主线程会定时的check消息的消费状态，我们的消息也都是设置了消费最大超时时间的，也就是说，对与check的动作来说，消息超时意味着失败，它会在report信息的组织过程中，把超时的消息认定为失败，记录下来这条消息的offset，传回给JIP server，下次会重发这条消息。
 > 但是对SDK的消费回调函数来说，我们是希望由ISV来完成自己的函数调用超时逻辑的，也就是他的函数要有超时返回的能力，否则它会一直占用我们的消费线程。在极端的情况下，也就是它的回调函数都调不通，也不设置超时返回。那么我们的线程一直在等待调用结果，会导致SDK耗尽消费线程无法工作（BTW，当然，因为他的函数调不通，程序本身就无法工作，和SDK无关）

2. to be continued...


发布消息的示例代码（C#）
---

            //准备必要的参数
            string pub_key = "483936";
            string pub_secret = "d41ee217895de8b64ec44653edce9263";
            string session = "6c4631d7debcbdf2f3555ac51f4a88a703d6b57ef2364992bfec6ad07bf5df78";
            string topic = "guichen.test1";
            string user_id = "2010907121";
            string serverUrl = "http://localhost:8080/message/pub";
            
            //初始化聚石塔Top客户端
             JushitaTopClient client = new JushitaTopClient(serverUrl, pub_key, pub_secret);
            //添加必要的参数
            IDictionary<string, string> param = new Dictionary<string, string>();
            //发布消息的Topic
            param.Add("topic", topic);
            //发布消息的身份id
            param.Add("user_id", user_id);
            //发布消息的消息体
            param.Add("message", "test");
            //这里测试连续发送了50次
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
            

订阅消息的示例代码（C#）：
---

            //准备必要的参数
            string sub_key = "483937";
            string sub_secret = "7398570d7bb0c4473bc0962b61df321c";
            string connectId = "liubing_test_connect";
            string connectUrl = "http://localhost:8080/message/sub";
            //准备要订阅的topic列表，这里之订阅一个topic：“guichen.test1”
            List<string> topics = new List<string>();
            topics.Add("guichen.test1");
            //初始化订阅参数配置类
            JushitaConfigurationV2 config = new JushitaConfigurationV2(sub_key, sub_secret, connectId, topics);
            //设置订阅的服务器Url
            config.SetConnectUrl(connectUrl);
            //设置消息回调处理类
            config.setCometMessageListener(new messageListener());
            //初始化长连接驱动类
            JushitaTopCometStreamImpl stream = new JushitaTopCometStreamImpl(config);
            //发起长连接，订阅消息
            stream.Start();

消息回调处理类：

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
                //ISV自行决定收到消息该怎么处理。这里测试仅仅打印出消息来
                Console.WriteLine("message: " + message);      
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




