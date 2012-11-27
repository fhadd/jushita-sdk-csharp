using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.details.get
    /// </summary>
    public class UmpDetailsGetRequest : ITopRequest<UmpDetailsGetResponse>
    {
        /// <summary>
        /// 营销活动id
        /// </summary>
        public Nullable<long> ActId { get; set; }

        /// <summary>
        /// 分页的页码
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 每页的最大条数
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.details.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("act_id", this.ActId);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("act_id", this.ActId);
            RequestValidator.ValidateRequired("page_no", this.PageNo);
            RequestValidator.ValidateMinValue("page_no", this.PageNo, 0);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 50);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1);
        }

        #endregion
    }
}
