using System.Web;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传控件
    /// </summary>
    public interface IWebUploader : IHtmlString {
        /// <summary>
        /// 指定控件名称
        /// </summary>
        /// <param name="name">控件名称，用于标识控件</param>
        IWebUploader Name( string name );
        /// <summary>
        /// 设置控件容器内部Html
        /// </summary>
        /// <param name="html">控件容器内部Html</param>
        IWebUploader Html( string html );
        /// <summary>
        /// 指定选择文件的按钮容器
        /// </summary>
        /// <param name="id">选择文件按钮容器Id</param>
        /// <param name="btnHtml">选择文件按钮的Html</param>
        /// <param name="multiple">是否允许多选</param>
        IWebUploader Pick( string id, string btnHtml = "", bool? multiple = null );
        /// <summary>
        /// 设置文件接收服务端Url
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        IWebUploader Server( string url );
        /// <summary>
        /// 设置文件加入队列事件处理函数
        /// </summary>
        /// <param name="handler">文件加入队列事件处理函数</param>
        IWebUploader FileQueued( string handler );
    }
}
