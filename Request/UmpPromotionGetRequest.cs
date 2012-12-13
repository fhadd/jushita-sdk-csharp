using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.promotion.get
    /// </summary>
    public class UmpPromotionGetRequest : ITopRequest<UmpPromotionGetResponse>
    {
        /// <summary>
        /// 渠道来源，第三方站点
        /// </summary>
        public string ChannelKey { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public Nullable<long> ItemId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.promotion.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("channel_key", this.ChannelKey);
            parameters.Add("item_id", this.ItemId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("item_id", this.ItemId);
        }

        #endregion
    }
}
