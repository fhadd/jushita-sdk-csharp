using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.topats.task.delete
    /// </summary>
    public class TopatsTaskDeleteRequest : ITopRequest<TopatsTaskDeleteResponse>
    {
        /// <summary>
        /// 需要取消的任务ID
        /// </summary>
        public Nullable<long> TaskId { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.topats.task.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("task_id", this.TaskId);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("task_id", this.TaskId);
        }

        #endregion
    }
}
