using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.widget.appapirule.check
    /// </summary>
    public class WidgetAppapiruleCheckRequest : ITopRequest<WidgetAppapiruleCheckResponse>
    {
        /// <summary>
        /// 要判断权限的api名称，如果指定的api不存在，报错invalid method
        /// </summary>
        public string ApiName { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.widget.appapirule.check";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("api_name", this.ApiName);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("api_name", this.ApiName);
        }

        #endregion
    }
}
