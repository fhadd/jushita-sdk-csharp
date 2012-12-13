using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// TaohuaItemresurlGetResponse.
    /// </summary>
    public class TaohuaItemresurlGetResponse : TopResponse
    {
        /// <summary>
        /// 下载链接地址.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }
    }
}
