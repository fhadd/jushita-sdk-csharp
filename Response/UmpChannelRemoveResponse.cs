using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// UmpChannelRemoveResponse.
    /// </summary>
    public class UmpChannelRemoveResponse : TopResponse
    {
        /// <summary>
        /// 本次操作所影响到的referer个数。
        /// </summary>
        [XmlElement("effect_referer_number")]
        public long EffectRefererNumber { get; set; }
    }
}
