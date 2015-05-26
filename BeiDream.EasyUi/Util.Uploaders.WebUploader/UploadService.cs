using System.Web.Mvc;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传服务
    /// </summary>
    public class UploadService : IUploadService {
        /// <summary>
        /// 初始化上传服务
        /// </summary>
        public UploadService( HtmlHelper helper ) {
            _helper = helper;
        }

        /// <summary>
        /// HtmlHelper
        /// </summary>
        private readonly HtmlHelper _helper;

        /// <summary>
        /// 上传图片集合
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        public IUploadImagesService Images( string url ) {
            return new UploadImagesService( _helper ).Url( url );
        }
    }
}
