using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// WidgetCartitemAddResponse.
    /// </summary>
    public class WidgetCartitemAddResponse : TopResponse
    {
        /// <summary>
        /// 商品是否添加成功。同一个商品同一个sku添加多次购买记录不增加，单挑的购买数量增加
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }
    }
}
