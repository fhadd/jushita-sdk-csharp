using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.wlb.orderschedulerule.delete
    /// </summary>
    public class WlbOrderscheduleruleDeleteRequest : ITopRequest<WlbOrderscheduleruleDeleteResponse>
    {
        /// <summary>
        /// 订单调度规则ID
        /// </summary>
        public Nullable<long> Id { get; set; }

        /// <summary>
        /// 商品userNick
        /// </summary>
        public string UserNick { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.wlb.orderschedulerule.delete";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("id", this.Id);
            parameters.Add("user_nick", this.UserNick);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("id", this.Id);
            RequestValidator.ValidateRequired("user_nick", this.UserNick);
            RequestValidator.ValidateMaxLength("user_nick", this.UserNick, 64);
        }

        #endregion
    }
}
