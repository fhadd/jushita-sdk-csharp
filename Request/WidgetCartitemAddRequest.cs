using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.widget.cartitem.add
    /// </summary>
    public class WidgetCartitemAddRequest : ITopRequest<WidgetCartitemAddResponse>
    {
        /// <summary>
        /// 要购买的商品的数字id，同Item的num_iid字段
        /// </summary>
        public Nullable<long> ItemId { get; set; }

        /// <summary>
        /// 需要购买的数量，至少购买1件
        /// </summary>
        public Nullable<long> Quantity { get; set; }

        /// <summary>
        /// 要购买的sku的id，如果是无sku的商品此字段不传，如果是有sku的商品必需指定sku_id。同sku的sku_id字段
        /// </summary>
        public Nullable<long> SkuId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.widget.cartitem.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("item_id", this.ItemId);
            parameters.Add("quantity", this.Quantity);
            parameters.Add("sku_id", this.SkuId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("item_id", this.ItemId);
            RequestValidator.ValidateRequired("quantity", this.Quantity);
            RequestValidator.ValidateMaxValue("quantity", this.Quantity, 999999);
            RequestValidator.ValidateMinValue("quantity", this.Quantity, 1);
            RequestValidator.ValidateMinValue("sku_id", this.SkuId, 1);
        }

        #endregion
    }
}
