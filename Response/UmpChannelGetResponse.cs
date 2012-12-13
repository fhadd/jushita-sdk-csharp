using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Top.Api.Domain;

namespace Top.Api.Response
{
    /// <summary>
    /// UmpChannelGetResponse.
    /// </summary>
    public class UmpChannelGetResponse : TopResponse
    {
        /// <summary>
        /// 渠道信息。
        /// </summary>
        [XmlArray("channels")]
        [XmlArrayItem("channel_info")]
        public List<ChannelInfo> Channels { get; set; }
    }
}
