using System.Text;
using Util.Uploaders.WebUploader.Configs;

namespace Util.Uploaders.WebUploader {
    /// <summary>
    /// 上传控件
    /// </summary>
    public class Uploader : IWebUploader {
        /// <summary>
        /// 初始化
        /// </summary>
        public Uploader() {
            _option = new Option();
        }

        /// <summary>
        /// Html
        /// </summary>
        private string _html;
        /// <summary>
        /// 配置项
        /// </summary>
        private readonly Option _option;

        /// <summary>
        /// 指定控件名称
        /// </summary>
        /// <param name="name">控件名称，用于标识控件</param>
        public IWebUploader Name( string name ) {
            _option.Name = name;
            return this;
        }

        /// <summary>
        /// 设置控件容器内部Html
        /// </summary>
        /// <param name="html">控件容器内部Html</param>
        public IWebUploader Html( string html ) {
            _html = html;
            return this;
        }

        /// <summary>
        /// 指定选择文件的按钮容器
        /// </summary>
        /// <param name="id">选择文件按钮容器Id</param>
        /// <param name="btnHtml">选择文件按钮的Html</param>
        /// <param name="multiple">是否允许多选</param>
        public IWebUploader Pick( string id, string btnHtml = "", bool? multiple = null ) {
            id = "#" + id;
            _option.Pick = new PickOption() { Id = id, InnerHtml = btnHtml, Multiple = multiple };
            return this;
        }

        /// <summary>
        /// 设置文件接收服务端Url
        /// </summary>
        /// <param name="url">文件接收服务端Url</param>
        public IWebUploader Server( string url ) {
            _option.Server = url;
            return this;
        }

        /// <summary>
        /// 设置文件加入队列事件处理函数
        /// </summary>
        /// <param name="handler">文件加入队列事件处理函数</param>
        public IWebUploader FileQueued( string handler ) {
            _option.FileQueued = handler;
            return this;
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return ToHtmlString();
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public string ToHtmlString() {
            var result = new StringBuilder();
            result.AppendFormat( "<div class=\"web-uploader\" data-options=\"{0}\">", _option );
            result.Append( _html );
            result.Append( "</div>" );
            return result.ToString();
        }
    }
}
