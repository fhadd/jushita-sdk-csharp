using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.mbb.getbycode
    /// </summary>
    public class UmpMbbGetbycodeRequest : ITopRequest<UmpMbbGetbycodeResponse>
    {
        /// <summary>
        /// 营销积木块code
        /// </summary>
        public string Code { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.mbb.getbycode";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("code", this.Code);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("code", this.Code);
        }

        #endregion
    }
}
