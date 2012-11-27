using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.detail.list.add
    /// </summary>
    public class UmpDetailListAddRequest : ITopRequest<UmpDetailListAddResponse>
    {
        /// <summary>
        /// 营销活动id。
        /// </summary>
        public Nullable<long> ActId { get; set; }

        /// <summary>
        /// 营销详情的列表。此列表由detail的json字符串组成。最多插入为10个。
        /// </summary>
        public string Details { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.detail.list.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("act_id", this.ActId);
            parameters.Add("details", this.Details);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("act_id", this.ActId);
            RequestValidator.ValidateRequired("details", this.Details);
        }

        #endregion
    }
}
