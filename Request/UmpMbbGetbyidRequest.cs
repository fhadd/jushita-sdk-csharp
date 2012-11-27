using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.mbb.getbyid
    /// </summary>
    public class UmpMbbGetbyidRequest : ITopRequest<UmpMbbGetbyidResponse>
    {
        /// <summary>
        /// 积木块的id
        /// </summary>
        public Nullable<long> Id { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.mbb.getbyid";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("id", this.Id);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("id", this.Id);
        }

        #endregion
    }
}
