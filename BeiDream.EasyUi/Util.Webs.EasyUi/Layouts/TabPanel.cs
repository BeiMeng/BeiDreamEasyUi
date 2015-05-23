using Util.Webs.EasyUi.Base;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 选项卡面板
    /// </summary>
    public class TabPanel : ContainerBase<ITabPanel>, ITabPanel {
        /// <summary>
        /// 初始化选项卡面板
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public TabPanel( ITextWriter textWriter )
            : base( textWriter ) {
        }

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public ITabPanel Id( string id ) {
            return AddDataOption( "id", id, true );
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        public ITabPanel Title( string title ) {
            return AddDataOption( "title", title,true );
        }

        /// <summary>
        /// 允许折叠
        /// </summary>
        public ITabPanel Collapsible() {
            return AddDataOption( "collapsible", true );
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="content">内容</param>
        public ITabPanel Content( string content ) {
            return AddDataOption( "content", content,true );
        }

        /// <summary>
        /// 设置远程加载url
        /// </summary>
        /// <param name="url">远程加载url</param>
        public ITabPanel Url( string url ) {
            return AddDataOption( "href", url, true );
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cache">是否缓存</param>
        public ITabPanel Cache( bool cache ) {
            return AddDataOption( "cache", cache );
        }

        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public ITabPanel Icon( string iconClass ) {
            return AddDataOption( "iconCls", iconClass,true );
        }

        /// <summary>
        /// 设置允许关闭
        /// </summary>
        /// <param name="closable">是否允许关闭</param>
        public ITabPanel Closable( bool closable = true ) {
            return AddDataOption( "closable", closable );
        }

        /// <summary>
        /// 设置选中
        /// </summary>
        public ITabPanel Select() {
            return AddDataOption( "selected", true );
        }
    }
}
