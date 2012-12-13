using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// TaobaokeToolRelationResponse.
    /// </summary>
    public class TaobaokeToolRelationResponse : TopResponse
    {
        /// <summary>
        /// 返回true或false表示是否关联用户
        /// </summary>
        [XmlElement("tools_user")]
        public bool ToolsUser { get; set; }
    }
}
