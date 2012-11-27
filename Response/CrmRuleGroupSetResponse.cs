using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// CrmRuleGroupSetResponse.
    /// </summary>
    public class CrmRuleGroupSetResponse : TopResponse
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}
