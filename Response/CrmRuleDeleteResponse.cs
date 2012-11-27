using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// CrmRuleDeleteResponse.
    /// </summary>
    public class CrmRuleDeleteResponse : TopResponse
    {
        /// <summary>
        /// 删除是否成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}
