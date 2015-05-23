namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 按钮
    /// </summary>
    public class Button : Button<IButton>, IButton {
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public Button( string text )
            : base( text ) {
        }
    }
}
