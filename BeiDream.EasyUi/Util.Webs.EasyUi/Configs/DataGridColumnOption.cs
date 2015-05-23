using Json.Net;

namespace Util.Webs.EasyUi.Configs {
    /// <summary>
    /// 表格列配置项
    /// </summary>
    public class DataGridColumnOption {
        /// <summary>
        /// 字段
        /// </summary>
        [Json( PropertyName = "field", NullValueHandling = NullValueHandling.Ignore )]
        public string Field { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Json( PropertyName = "title", NullValueHandling = NullValueHandling.Ignore )]
        public string Title { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [Json( PropertyName = "width", NullValueHandling = NullValueHandling.Ignore )]
        public string Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [Json( PropertyName = "height", NullValueHandling = NullValueHandling.Ignore )]
        public int? Height { get; set; }
        /// <summary>
        /// 标题对齐方式
        /// </summary>
        [Json( PropertyName = "halign", NullValueHandling = NullValueHandling.Ignore )]
        public string HeaderAlign { get; set; }
        /// <summary>
        /// 内容对齐方式
        /// </summary>
        [Json( PropertyName = "align", NullValueHandling = NullValueHandling.Ignore )]
        public string Align { get; set; }
        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [Json( PropertyName = "checkbox", NullValueHandling = NullValueHandling.Ignore )]
        public bool? CheckBox { get; set; }
        /// <summary>
        /// 格式化函数
        /// </summary>
        [Json( false, PropertyName = "formatter", NullValueHandling = NullValueHandling.Ignore )]
        public string Formatter { get; set; }

        /// <summary>
        /// 输出Json结果
        /// </summary>
        public override string ToString() {
            return Json.ToJson( this, true );
        }
    }
}
