using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.activity.add
    /// </summary>
    public class UmpActivityAddRequest : ITopRequest<UmpActivityAddResponse>
    {
        /// <summary>
        /// 活动内容，通过ump sdk里面的MarkeitngTool来生成
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 工具id
        /// </summary>
        public Nullable<long> ToolId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.activity.add";
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
