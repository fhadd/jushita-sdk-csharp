using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Top.Api.Jushita.stream
{
    /// <summary>
    /// message处理接口
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// 处理message的具体方法
        /// </summary>
        /// <param name="message">收下来的message</param>
        /// <returns>返回一个处理message的结果，true代表该消息被成功处理；false代表该消息处理失败</returns>
        Boolean handle(Message message);
    }
}
