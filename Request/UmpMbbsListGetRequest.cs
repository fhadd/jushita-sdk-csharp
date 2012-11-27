using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.mbbs.list.get
    /// </summary>
    public class UmpMbbsListGetRequest : ITopRequest<UmpMbbsListGetResponse>
    {
        /// <summary>
        /// 营销积木块id组成的字符串。
        /// </summary>
        public string Ids { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.mbbs.list.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("ids", this.Ids);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ids", this.Ids);
            RequestValidator.ValidateMaxListSize("ids", this.Ids, 20);
        }

        #endregion
    }
}
