using Util.Webs.EasyUi.Base;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局区域
    /// </summary>
    public class LayoutRegion<T> : ContainerBase<T> where T : ILayoutRegion<T> {
        /// <summary>
        /// 初始化布局区域
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public LayoutRegion( ITextWriter textWriter  ) : base( textWriter ) {
        }

        /// <summary>
        /// 设置为顶部区域
        /// </summary>
        public T Top() {
            return AddDataOption( "region", "'north'" );
        }

        /// <summary>
        /// 设置为底部区域
        /// </summary>
        public T Bottom() {
            return AddDataOption( "region", "'south'" );
        }

        /// <summary>
        /// 设置为左侧区域
        /// </summary>
        public T Left() {
            return AddDataOption( "region", "'west'" );
        }

        /// <summary>
        /// 设置为右侧区域
        /// </summary>
        public T Right() {
            return AddDataOption( "region", "'east'" );
        }

        /// <summary>
        /// 设置为中间内容区域
        /// </summary>
        public T Center() {
            return AddDataOption( "region", "'center'" );
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        public T Title( string title ) {
            return AddDataOption( "title", title, true );
        }

        /// <summary>
        /// 显示边框
        /// </summary>
        /// <param name="isShow">是否显示边框，默认为显示</param>
        public T Border( bool isShow = true ) {
            return AddDataOption( "border", isShow );
        }

        /// <summary>
        /// 显示分隔条
        /// </summary>
        public T Split() {
            return AddDataOption( "split", true );
        }

        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public T Icon( string iconClass ) {
            return AddDataOption( "iconCls", iconClass, true );
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        public T Href( string url ) {
            return AddDataOption( "href", url, true );
        }

        /// <summary>
        /// 允许折叠
        /// </summary>
        /// <param name="collapsible">是否允许折叠</param>
        public T Collapsible( bool collapsible = true ) {
            return AddDataOption( "collapsible", collapsible );
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        public override T Width( int? width, bool isPercent = false ) {
            return base.Width( width, isPercent ).AddDataOption( "minWidth", width.ToStr() );
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public override T Height( int height ) {
            return base.Height( height ).AddDataOption( "minHeight", height.ToString() );
        }

        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="minWidth">最小宽度</param>
        public T MinWidth( int minWidth ) {
            return Width( minWidth );
        }

        /// <summary>
        /// 设置最小高度
        /// </summary>
        /// <param name="minHeight">最小高度</param>
        public T MinHeight( int minHeight ) {
            return Height( minHeight );
        }

        /// <summary>
        /// 设置最大宽度
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        public T MaxWidth( int maxWidth ) {
            return AddDataOption( "maxWidth", maxWidth.ToString() );
        }

        /// <summary>
        /// 设置最大高度
        /// </summary>
        /// <param name="maxHeight">最大高度</param>
        public T MaxHeight( int maxHeight ) {
            return AddDataOption( "maxHeight", maxHeight.ToString() );
        }
    }
}
