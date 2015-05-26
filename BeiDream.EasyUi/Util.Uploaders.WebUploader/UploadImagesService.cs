using System.Web.Mvc;
using System.Web.Mvc.Html;
using Util.Uploaders.WebUploader.Configs;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传图片集合服务
    /// </summary>
    public class UploadImagesService : IUploadImagesService{
        /// <summary>
        /// 初始化上传图片集合服务
        /// </summary>
        public UploadImagesService( HtmlHelper helper ) {
            _helper = helper;
            _option = new Option();
        }

        /// <summary>
        /// HtmlHelper
        /// </summary>
        private readonly HtmlHelper _helper;
        /// <summary>
        /// 上传图片集合配置项
        /// </summary>
        private readonly Option _option;

        /// <summary>
        /// 设置上传限制
        /// </summary>
        /// <param name="totalFileNumber">上传的最大文件个数，默认100</param>
        /// <param name="totalFileSizeM">上传的最大总容量，单位：M,默认50M</param>
        /// <param name="singleFileSizeK">上传单个文件的最大容量，单位：K,默认600K</param>
        public IUploadImagesService Limit( int totalFileNumber = 100, int totalFileSizeM = 50, int singleFileSizeK = 600 ) {
            _option.TotalFileNumber = totalFileNumber;
            _option.TotalFileSize = totalFileSizeM * 1024 * 1024;
            _option.SingleFileSize = singleFileSizeK * 1024;
            return this;
        }

        /// <summary>
        /// 设置文件接收服务端
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        public IUploadImagesService Url( string url ) {
            _option.Server = url;
            return this;
        }

        /// <summary>
        /// 设置缩略图尺寸
        /// </summary>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        public IUploadImagesService Thumb( int width = 110, int height = 110 ) {
            _option.ThumbnailWidth = width;
            _option.ThumbnailHeight = height;
            return this;
        }

        /// <summary>
        /// 设置发送的表单数据
        /// </summary>
        /// <param name="data">表单数据，一般为匿名对象，范例：{a=1}</param>
        public IUploadImagesService FormData( object data ) {
            _option.FormData = data;
            return this;
        }

        /// <summary>
        /// 设置所有文件上传结束事件处理函数
        /// </summary>
        /// <param name="handler">所有文件上传结束事件处理函数</param>
        public IUploadImagesService OnFinish( string handler ) {
            _option.UploadFinished = handler;
            return this;
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public string ToHtmlString() {
            return _helper.Partial( "Uploaders/Images", _option ).ToHtmlString();
        }
    }
}
