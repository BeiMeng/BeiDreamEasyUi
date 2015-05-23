using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 选项卡面板
    /// </summary>
    public interface ITabPanel : IContainer<ITabPanel> {
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        ITabPanel Id( string id );
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        ITabPanel Title( string title );
        /// <summary>
        /// 设置允许折叠
        /// </summary>
        ITabPanel Collapsible();
        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="content">内容</param>
        ITabPanel Content( string content );
        /// <summary>
        /// 设置远程加载url
        /// </summary>
        /// <param name="url">远程加载url</param>
        ITabPanel Url( string url );
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cache">是否缓存</param>
        ITabPanel Cache( bool cache );
        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        ITabPanel Icon( string iconClass );
        /// <summary>
        /// 设置允许关闭
        /// </summary>
        /// <param name="closable">是否允许关闭</param>
        ITabPanel Closable( bool closable = true );
        /// <summary>
        /// 设置选中
        /// </summary>
        ITabPanel Select();
    }
}
