using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局区域
    /// </summary>
    public class LayoutRegion : LayoutRegion<ILayoutRegion>, ILayoutRegion {
        /// <summary>
        /// 初始化布局区域
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public LayoutRegion( ITextWriter textWriter )
            : base( textWriter ) {
        }
    }
}
