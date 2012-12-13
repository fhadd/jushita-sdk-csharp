using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.activities.list.get
    /// </summary>
    public class UmpActivitiesListGetRequest : ITopRequest<UmpActivitiesListGetResponse>
    {
        /// <summary>
        /// 营销活动id列表。
        /// </summary>
        public string Ids { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.activities.list.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("ids", this.Ids);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("ids", this.Ids);
            RequestValidator.ValidateMaxListSize("ids", this.Ids, 40);
        }

        #endregion
    }
}
