using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.tool.add
    /// </summary>
    public class UmpToolAddRequest : ITopRequest<UmpToolAddResponse>
    {
        /// <summary>
        /// 工具内容，由sdk里面的MarketingTool生成的json字符串
        /// </summary>
        public string Content { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.tool.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("content", this.Content);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("content", this.Content);
        }

        #endregion
    }
}
