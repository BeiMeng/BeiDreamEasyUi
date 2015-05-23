namespace Util.Webs.EasyUi.Forms.Comboxs {
    /// <summary>
    /// 组合框选项
    /// </summary>
    public class ComboxItem {
        /// <summary>
        /// 初始化组合框选项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="group">组</param>
        public ComboxItem( string text, object value = null, string group = "" ) {
            Text = text;
            Value = value;
            Group = group;
        }

        /// <summary>
        /// 值
        /// </summary>
        [Json( PropertyName = "value" )]
        public object Value { get; private set; }

        /// <summary>
        /// 文本
        /// </summary>
        [Json( PropertyName = "text" )]
        public string Text { get; private set; }
        
        /// <summary>
        /// 组
        /// </summary>
        [Json( PropertyName = "group" )]
        public string Group { get; private set; }
    }
}
