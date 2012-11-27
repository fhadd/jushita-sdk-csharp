using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.range.delete
    /// </summary>
    public class UmpRangeDeleteRequest : ITopRequest<UmpRangeDeleteResponse>
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public Nullable<long> ActId { get; set; }

        /// <summary>
        /// id列表，当范围类型为商品时，该id为商品id；当范围类型为类目时，该id为类目id
        /// </summary>
        public string Ids { get; set; }

        /// <summary>
        /// 范围的类型，比如是全店，商品，类目见：MarketingConstants.PARTICIPATE_TYPE_*
        /// </summary>
        public Nullable<long> Type { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.range.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("act_id", this.ActId);
            parameters.Add("ids", this.Ids);
            parameters.Add("type", this.Type);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("act_id", this.ActId);
            RequestValidator.ValidateRequired("ids", this.Ids);
            RequestValidator.ValidateRequired("type", this.Type);
        }

        #endregion
    }
}
