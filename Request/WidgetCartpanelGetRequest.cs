using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.widget.cartpanel.get
    /// </summary>
    public class WidgetCartpanelGetRequest : ITopRequest<WidgetCartpanelGetResponse>
    {
        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.widget.cartpanel.get";
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
