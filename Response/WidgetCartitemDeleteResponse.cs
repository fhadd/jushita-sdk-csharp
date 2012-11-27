using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// WidgetCartitemDeleteResponse.
    /// </summary>
    public class WidgetCartitemDeleteResponse : TopResponse
    {
        /// <summary>
        /// 被成功删除的购物车id号
        /// </summary>
        [XmlElement("cart_id")]
        public long CartId { get; set; }

        /// <summary>
        /// 删除是否成功，true表示成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}
