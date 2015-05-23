namespace Util.Webs.EasyUi.Forms.TextBoxs {
    /// <summary>
    /// 组合控件
    /// </summary>
    /// <typeparam name="T">组合控件类型</typeparam>
    public abstract class Combo<T> : TextBox<T>,ICombo<T> where T : ICombo<T> {
        /// <summary>
        /// 设置面板高度，即下拉列表的高度
        /// </summary>
        /// <param name="height">面板高度，值为"auto"为自适应，也可以指定高度，范例"100"</param>
        public T PanelHeight( string height = "auto" ) {
            AddDataOption( "panelHeight", height,true );
            return This();
        }
    }
}
