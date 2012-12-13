using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ju.cities.get
    /// </summary>
    public class JuCitiesGetRequest : ITopRequest<JuCitiesGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ju.cities.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            return parameters;
        }

        public void Validate()
        {
        }

        #endregion
    }
}
