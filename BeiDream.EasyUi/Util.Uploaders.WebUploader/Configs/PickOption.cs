using Json.Net;

namespace Util.Uploaders.WebUploader.Configs {
    /// <summary>
    /// 选择文件的按钮容器配置项
    /// </summary>
    public class PickOption {
        /// <summary>
        /// 指定选择文件的按钮容器Id
        /// </summary>
        [Json( PropertyName = "id", NullValueHandling = NullValueHandling.Ignore )]
        public string Id { get; set; }
        /// <summary>
        /// 指定选择文件的按钮Html
        /// </summary>
        [Json( PropertyName = "innerHTML", NullValueHandling = NullValueHandling.Ignore )]
        public string InnerHtml { get; set; }
        /// <summary>
        /// 是否允许多选
        /// </summary>
        [Json( PropertyName = "multiple", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Multiple { get; set; }
    }
}
