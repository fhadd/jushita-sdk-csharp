using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alipay.user.contract.get
    /// </summary>
    public class AlipayUserContractGetRequest : ITopRequest<AlipayUserContractGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "alipay.user.contract.get";
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
