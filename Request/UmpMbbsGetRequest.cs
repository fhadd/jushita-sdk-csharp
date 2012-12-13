using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.mbbs.get
    /// </summary>
    public class UmpMbbsGetRequest : ITopRequest<UmpMbbsGetResponse>
    {
        /// <summary>
        /// 积木块类型。如果该字段为空表示查出所有类型的  现在有且仅有如下几种：resource,condition,action,target
        /// </summary>
        public string Type { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.mbbs.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("type", this.Type);
            return parameters;
        }

        public void Validate()
        {
        }

        #endregion
    }
}
