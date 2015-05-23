using Json.Net;

namespace Util.Webs.EasyUi.Configs {
    /// <summary>
    /// 查找带回配置选项
    /// </summary>
    public class LookupOption {
        /// <summary>
        /// 远程Url
        /// </summary>
        [Json( PropertyName = "url", NullValueHandling = NullValueHandling.Ignore )]
        public string Url { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Json( PropertyName = "title", NullValueHandling = NullValueHandling.Ignore )]
        public string Title { get; set; }
        /// <summary>
        /// 图标class
        /// </summary>
        [Json( PropertyName = "icon", NullValueHandling = NullValueHandling.Ignore )]
        public string Icon { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [Json( PropertyName = "dialogWidth", NullValueHandling = NullValueHandling.Ignore )]
        public int? Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [Json( PropertyName = "dialogHeight", NullValueHandling = NullValueHandling.Ignore )]
        public int? Height { get; set; }
        /// <summary>
        /// 弹出窗口按钮区域div Id
        /// </summary>
        [Json( PropertyName = "buttons", NullValueHandling = NullValueHandling.Ignore )]
        public string ButtonsDivId { get; set; }
        /// <summary>
        /// 初始化事件处理函数
        /// </summary>
        [Json( false, PropertyName = "onInit", NullValueHandling = NullValueHandling.Ignore )]
        public string OnInit { get; set; }
        /// <summary>
        /// 是否允许编辑文本框
        /// </summary>
        [Json( PropertyName = "editable", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Editable { get; set; }
    }
}
