namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传服务
    /// </summary>
    public interface IUploadService {
        /// <summary>
        /// 上传图片集合
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        IUploadImagesService Images( string url );
    }
}
