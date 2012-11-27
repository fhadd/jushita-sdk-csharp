using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.widget.cartitem.delete
    /// </summary>
    public class WidgetCartitemDeleteRequest : ITopRequest<WidgetCartitemDeleteResponse>
    {
        /// <summary>
        /// 要删除的购物车记录id号
        /// </summary>
        public Nullable<long> CartId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.widget.cartitem.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cart_id", this.CartId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cart_id", this.CartId);
            RequestValidator.ValidateMinValue("cart_id", this.CartId, 1);
        }

        #endregion
    }
}
