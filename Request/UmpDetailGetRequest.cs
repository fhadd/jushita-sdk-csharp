using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.detail.get
    /// </summary>
    public class UmpDetailGetRequest : ITopRequest<UmpDetailGetResponse>
    {
        /// <summary>
        /// 活动详情的id
        /// </summary>
        public Nullable<long> DetailId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.detail.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("detail_id", this.DetailId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("detail_id", this.DetailId);
        }

        #endregion
    }
}
