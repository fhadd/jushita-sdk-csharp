using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.tools.get
    /// </summary>
    public class UmpToolsGetRequest : ITopRequest<UmpToolsGetResponse>
    {
        /// <summary>
        /// 工具编码
        /// </summary>
        public string ToolCode { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.tools.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("tool_code", this.ToolCode);
            return parameters;
        }

        public void Validate()
        {
        }

        #endregion
    }
}
