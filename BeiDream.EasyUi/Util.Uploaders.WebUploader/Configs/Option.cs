using Json.Net;

namespace Util.Uploaders.WebUploader.Configs {
    /// <summary>
    /// WebUploader初始化配置
    /// </summary>
    public class Option {
        /// <summary>
        /// 指定控件名称
        /// </summary>
        [Json( PropertyName = "name", NullValueHandling = NullValueHandling.Ignore )]
        public string Name { get; set; }
        /// <summary>
        /// 指定拖拽的容器选择器
        /// </summary>
        [Json( PropertyName = "dnd", NullValueHandling = NullValueHandling.Ignore )]
        public string Dnd { get; set; }
        /// <summary>
        /// 是否禁掉整个页面的拖拽功能，如果不禁用，图片拖进来的时候会默认被浏览器打开
        /// </summary>
        [Json( PropertyName = "disableGlobalDnd", NullValueHandling = NullValueHandling.Ignore )]
        public bool? DisableGlobalDnd { get; set; }
        /// <summary>
        /// 选择文件的按钮容器
        /// </summary>
        [Json( PropertyName = "pick", NullValueHandling = NullValueHandling.Ignore )]
        public PickOption Pick { get; set; }
        /// <summary>
        /// 自动上传
        /// </summary>
        [Json( PropertyName = "auto", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Auto { get; set; }
        /// <summary>
        /// 文件接收服务端
        /// </summary>
        [Json( PropertyName = "server", NullValueHandling = NullValueHandling.Ignore )]
        public string Server { get; set; }
        /// <summary>
        /// 文件加入队列事件
        /// </summary>
        [Json( false, PropertyName = "fileQueued", NullValueHandling = NullValueHandling.Ignore )]
        public string FileQueued { get; set; }
        /// <summary>
        /// 上传进度事件
        /// </summary>
        [Json( false, PropertyName = "uploadProgress", NullValueHandling = NullValueHandling.Ignore )]
        public string UploadProgress { get; set; }
        /// <summary>
        /// 上传的最大文件个数
        /// </summary>
        [Json( PropertyName = "fileNumLimit", NullValueHandling = NullValueHandling.Ignore )]
        public int? TotalFileNumber { get; set; }
        /// <summary>
        /// 上传的最大总容量
        /// </summary>
        [Json( PropertyName = "fileSizeLimit", NullValueHandling = NullValueHandling.Ignore )]
        public int? TotalFileSize { get; set; }
        /// <summary>
        /// 上传单个文件的最大容量
        /// </summary>
        [Json( PropertyName = "fileSingleSizeLimit", NullValueHandling = NullValueHandling.Ignore )]
        public int? SingleFileSize { get; set; }
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        [Json( PropertyName = "thumbnailWidth", NullValueHandling = NullValueHandling.Ignore )]
        public int? ThumbnailWidth { get; set; }
        /// <summary>
        /// 缩略图高度
        /// </summary>
        [Json( PropertyName = "thumbnailHeight", NullValueHandling = NullValueHandling.Ignore )]
        public int? ThumbnailHeight { get; set; }
        /// <summary>
        /// 表单数据
        /// </summary>
        [Json( PropertyName = "formData", NullValueHandling = NullValueHandling.Ignore )]
        public object FormData { get; set; }
        /// <summary>
        /// 所有文件上传结束事件
        /// </summary>
        [Json(false, PropertyName = "uploadFinished", NullValueHandling = NullValueHandling.Ignore )]
        public string UploadFinished { get; set; }

        /// <summary>
        /// 输出Json结果
        /// </summary>
        public override string ToString() {
            var result = Json.ToJson( this, true );
            if ( result == "{}" )
                return string.Empty;
            return result;
        }
    }
}
