using System;
using System.Xml.Serialization;

namespace Top.Api.Response
{
    /// <summary>
    /// WidgetCartcountGetResponse.
    /// </summary>
    public class WidgetCartcountGetResponse : TopResponse
    {
        /// <summary>
        /// 当前用户通过当前app加入购物车的商品记录条数。
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}
