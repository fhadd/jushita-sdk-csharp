using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alipay.user.account.get
    /// </summary>
    public class AlipayUserAccountGetRequest : ITopRequest<AlipayUserAccountGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "alipay.user.account.get";
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
