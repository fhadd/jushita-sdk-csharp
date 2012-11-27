using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.fenxiao.product.image.upload
    /// </summary>
    public class FenxiaoProductImageUploadRequest : ITopUploadRequest<FenxiaoProductImageUploadResponse>
    {
        /// <summary>
        /// 产品图片
        /// </summary>
        public FileItem Image { get; set; }

        /// <summary>
        /// 产品主图图片空间相对路径或绝对路径
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 图片位置，0-5之间。0表示操作sku属性图片
        /// </summary>
        public Nullable<long> Position { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Nullable<long> ProductId { get; set; }

        /// <summary>
        /// properties表示sku图片的属性。key:value形式，key是pid，value是vid。如果position是0的话，则properties需要是必传项
        /// </summary>
        public string Properties { get; set; }

        #region ITopRequest Members

        public string GetApiName()
        {
            return "taobao.fenxiao.product.image.upload";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("pic_path", this.PicPath);
            parameters.Add("position", this.Position);
            parameters.Add("product_id", this.ProductId);
            parameters.Add("properties", this.Properties);
            return parameters;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("position", this.Position);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion
    }
}
