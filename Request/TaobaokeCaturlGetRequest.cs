using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.taobaoke.caturl.get
    /// </summary>
    public class TaobaokeCaturlGetRequest : ITopRequest<TaobaokeCaturlGetResponse>
    {
        /// <summary>
        /// 类目id.注意：这里的类目id是淘宝后台发布商品的类目id.
        /// </summary>
        public Nullable<long> Cid { get; set; }

        /// <summary>
        /// 推广者的淘宝会员昵称.注：这里指的是淘宝的登录会员名
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 自定义输入串.格式:英文和数字组成;长度不能大于12个字符,区分不同的推广渠道,如:bbs,表示bbs为推广渠道;blog,表示blog为推广渠道.
        /// </summary>
        public string OuterCode { get; set; }

        /// <summary>
        /// 淘客用户的pid,用于生成点击串.nick和pid都传入的话,以pid为准
        /// </summary>
        public Nullable<long> Pid { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Q { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.taobaoke.caturl.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("cid", this.Cid);
            parameters.Add("nick", this.Nick);
            parameters.Add("outer_code", this.OuterCode);
            parameters.Add("pid", this.Pid);
            parameters.Add("q", this.Q);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cid", this.Cid);
            RequestValidator.ValidateMinValue("cid", this.Cid, 0);
        }

        #endregion
    }
}
