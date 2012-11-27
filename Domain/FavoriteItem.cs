using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// FavoriteItem Data Structure.
    /// </summary>
    [Serializable]
    public class FavoriteItem : TopObject
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [XmlElement("item_id")]
        public long ItemId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [XmlElement("item_name")]
        public string ItemName { get; set; }

        /// <summary>
        /// 商品图片地址
        /// </summary>
        [XmlElement("item_pictrue")]
        public string ItemPictrue { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [XmlElement("item_price")]
        public string ItemPrice { get; set; }

        /// <summary>
        /// 商品的详情页面地址
        /// </summary>
        [XmlElement("item_url")]
        public string ItemUrl { get; set; }

        /// <summary>
        /// 促销价格
        /// </summary>
        [XmlElement("promotion_price")]
        public string PromotionPrice { get; set; }

        /// <summary>
        /// 商品销售次数
        /// </summary>
        [XmlElement("sell_count")]
        public long SellCount { get; set; }
    }
}
