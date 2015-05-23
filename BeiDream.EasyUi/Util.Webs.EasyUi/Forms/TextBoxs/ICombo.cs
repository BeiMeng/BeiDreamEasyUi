namespace Util.Webs.EasyUi.Forms.TextBoxs {
    /// <summary>
    /// 组合控件
    /// </summary>
    /// <typeparam name="T">组合控件</typeparam>
    public interface ICombo<out T> : ITextBox<T> where T : ICombo<T> {
        /// <summary>
        /// 设置面板高度，即下拉列表的高度
        /// </summary>
        /// <param name="height">面板高度，值为"auto"为自适应，也可以指定高度，范例"100"</param>
        T PanelHeight( string height = "auto" );
    }
}
