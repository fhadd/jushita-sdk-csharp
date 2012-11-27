using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Top.Api.Domain;

namespace Top.Api.Response
{
    /// <summary>
    /// JuCityitemsGetResponse.
    /// </summary>
    public class JuCityitemsGetResponse : TopResponse
    {
        /// <summary>
        /// 聚划算商品对象列表
        /// </summary>
        [XmlArray("item_list")]
        [XmlArrayItem("item_data")]
        public List<ItemData> ItemList { get; set; }
    }
}
