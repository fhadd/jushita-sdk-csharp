using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.sellercenter.subuser.permissions.roles.get
    /// </summary>
    public class SellercenterSubuserPermissionsRolesGetRequest : ITopRequest<SellercenterSubuserPermissionsRolesGetResponse>
    {
        /// <summary>
        /// 子账号昵称(子账号标识)
        /// </summary>
        public string Nick { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.sellercenter.subuser.permissions.roles.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("nick", this.Nick);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("nick", this.Nick);
            RequestValidator.ValidateMaxLength("nick", this.Nick, 100);
        }

        #endregion
    }
}
