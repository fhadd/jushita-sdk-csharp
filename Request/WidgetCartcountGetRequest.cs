using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.widget.cartcount.get
    /// </summary>
    public class WidgetCartcountGetRequest : ITopRequest<WidgetCartcountGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.widget.cartcount.get";
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
