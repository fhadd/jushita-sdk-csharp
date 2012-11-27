using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.wlb.item.consignment.page.get
    /// </summary>
    public class WlbItemConsignmentPageGetRequest : ITopRequest<WlbItemConsignmentPageGetResponse>
    {
        /// <summary>
        /// 代销商宝贝id
        /// </summary>
        public Nullable<long> IcItemId { get; set; }

        /// <summary>
        /// 供应商商品id
        /// </summary>
        public Nullable<long> OwnerItemId { get; set; }

        /// <summary>
        /// 供应商用户昵称
        /// </summary>
        public string OwnerUserNick { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.wlb.item.consignment.page.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("ic_item_id", this.IcItemId);
            parameters.Add("owner_item_id", this.OwnerItemId);
            parameters.Add("owner_user_nick", this.OwnerUserNick);
            return parameters;
        }

        public void Validate()
        {
        }

        #endregion
    }
}
