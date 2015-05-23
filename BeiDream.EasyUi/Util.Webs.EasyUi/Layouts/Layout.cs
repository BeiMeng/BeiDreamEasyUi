using Util.Webs.EasyUi.Base;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局
    /// </summary>
    public class Layout : ContainerBase<ILayout>, ILayout {
        /// <summary>
        /// 初始化布局
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        /// <param name="fit">自适应布局</param>
        public Layout( ITextWriter textWriter, bool fit )
            : base( textWriter ) {
            AddClass( "easyui-layout" );
            if ( fit )
                AddDataOption( "fit", true );
        }
    }
}
