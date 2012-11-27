using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.activity.delete
    /// </summary>
    public class UmpActivityDeleteRequest : ITopRequest<UmpActivityDeleteResponse>
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public Nullable<long> ActId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.activity.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("act_id", this.ActId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("act_id", this.ActId);
        }

        #endregion
    }
}
