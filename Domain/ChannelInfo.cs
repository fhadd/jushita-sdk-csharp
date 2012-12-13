using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// ChannelInfo Data Structure.
    /// </summary>
    [Serializable]
    public class ChannelInfo : TopObject
    {
        /// <summary>
        /// 渠道展示名称
        /// </summary>
        [XmlElement("channel_display_name")]
        public string ChannelDisplayName { get; set; }

        /// <summary>
        /// 渠道标识代码
        /// </summary>
        [XmlElement("channel_key")]
        public string ChannelKey { get; set; }

        /// <summary>
        /// 当前渠道所包含的来源referer地址。
        /// </summary>
        [XmlArray("referers")]
        [XmlArrayItem("string")]
        public List<string> Referers { get; set; }
    }
}
