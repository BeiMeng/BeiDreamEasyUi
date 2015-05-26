using System.Web.Mvc;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// HtmlHelper扩展 - 上传扩展
    /// </summary>
    public static class HtmlHelperExtentions {
        /// <summary>
        /// 上传
        /// </summary>
        public static IUploadService Uploader( this HtmlHelper helper ) {
            return new UploadService( helper );
        }
    }
}
