using System.Web;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传图片集合服务
    /// </summary>
    public interface IUploadImagesService : IHtmlString {
        /// <summary>
        /// 设置上传限制
        /// </summary>
        /// <param name="totalFileNumber">上传的最大文件个数，默认100</param>
        /// <param name="totalFileSizeM">上传的最大总容量，单位：M,默认50M</param>
        /// <param name="singleFileSizeK">上传单个文件的最大容量，单位：K,默认600K</param>
        IUploadImagesService Limit( int totalFileNumber = 100, int totalFileSizeM = 50, int singleFileSizeK = 600 );
        /// <summary>
        /// 设置文件接收服务端
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        IUploadImagesService Url( string url );
        /// <summary>
        /// 设置缩略图尺寸
        /// </summary>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        IUploadImagesService Thumb( int width = 110, int height = 110 );
        /// <summary>
        /// 设置发送的表单数据
        /// </summary>
        /// <param name="data">表单数据，一般为匿名对象，范例：{a=1}</param>
        IUploadImagesService FormData( object data );
        /// <summary>
        /// 设置所有文件上传结束事件处理函数
        /// </summary>
        /// <param name="handler">所有文件上传结束事件处理函数</param>
        IUploadImagesService OnFinish( string handler );
    }
}
