using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.tool.get
    /// </summary>
    public class UmpToolGetRequest : ITopRequest<UmpToolGetResponse>
    {
        /// <summary>
        /// 工具的id
        /// </summary>
        public Nullable<long> ToolId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.tool.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("tool_id", this.ToolId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("tool_id", this.ToolId);
        }

        #endregion
    }
}
