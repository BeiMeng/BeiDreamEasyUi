using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Menus {
    /// <summary>
    /// 菜单项
    /// </summary>
    public interface IMenuItem : IComponent<IMenuItem> {
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        IMenuItem Text( string text );
        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        IMenuItem Icon( string iconClass );
        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        IMenuItem Href( string url );
        /// <summary>
        /// 禁用
        /// </summary>
        IMenuItem Disable();
        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        IMenuItem Click( string handler );
    }
}
