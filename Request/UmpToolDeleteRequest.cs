using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.tool.delete
    /// </summary>
    public class UmpToolDeleteRequest : ITopRequest<UmpToolDeleteResponse>
    {
        /// <summary>
        /// 营销工具id
        /// </summary>
        public Nullable<long> ToolId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.tool.delete";
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
