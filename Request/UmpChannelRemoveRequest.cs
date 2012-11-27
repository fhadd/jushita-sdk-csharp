using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.ump.channel.remove
    /// </summary>
    public class UmpChannelRemoveRequest : ITopRequest<UmpChannelRemoveResponse>
    {
        /// <summary>
        /// 标示某个渠道的代码（由新增渠道时添加）。
        /// </summary>
        public string ChannelKey { get; set; }

        /// <summary>
        /// 当前渠道中，需要删除的referer地址。  referers为空，删除当前渠道信息，同时清空当前渠道已关联的所有referer。
        /// </summary>
        public string Referers { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.ump.channel.remove";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("channel_key", this.ChannelKey);
            parameters.Add("referers", this.Referers);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("channel_key", this.ChannelKey);
        }

        #endregion
    }
}
