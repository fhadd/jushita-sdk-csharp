using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.tool.update
    /// </summary>
    public class UmpToolUpdateRequest : ITopRequest<UmpToolUpdateResponse>
    {
        /// <summary>
        /// 工具的内容，由sdk的marketingBuilder生成
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 工具id
        /// </summary>
        public Nullable<long> ToolId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.tool.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("content", this.Content);
            parameters.Add("tool_id", this.ToolId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("content", this.Content);
            RequestValidator.ValidateRequired("tool_id", this.ToolId);
        }

        #endregion
    }
}
