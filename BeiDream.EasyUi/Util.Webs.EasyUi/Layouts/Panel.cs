using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 面板
    /// </summary>
    public class Panel : LayoutRegion<IPanel>, IPanel {
        /// <summary>
        /// 初始化面板
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public Panel( ITextWriter textWriter ) : base( textWriter ){
            AddClass( "easyui-panel" );
        }

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public IPanel Id( string id ) {
            return UpdateAttribute( "id", id );
        }

        /// <summary>
        /// 设置自适应
        /// </summary>
        public IPanel Fit() {
            return AddDataOption( "fit","true" );
        }

        /// <summary>
        /// 设置页脚
        /// </summary>
        /// <param name="id">页脚div的id</param>
        public IPanel Footer( string id ) {
            return AddDataOption( "footer", "#" + id, true );
        }
    }
}
