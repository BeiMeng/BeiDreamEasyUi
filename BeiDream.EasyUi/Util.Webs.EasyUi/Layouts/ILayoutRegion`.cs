using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 布局区域
    /// </summary>
    public interface ILayoutRegion<out T> : IContainer<T> where T : ILayoutRegion<T> {
        /// <summary>
        /// 设置为顶部区域
        /// </summary>
        T Top();
        /// <summary>
        /// 设置为底部区域
        /// </summary>
        T Bottom();
        /// <summary>
        /// 设置为左侧区域
        /// </summary>
        T Left();
        /// <summary>
        /// 设置为右侧区域
        /// </summary>
        T Right();
        /// <summary>
        /// 设置为中间内容区域
        /// </summary>
        T Center();
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title">标题</param>
        T Title( string title );
        /// <summary>
        /// 显示边框
        /// </summary>
        /// <param name="isShow">是否显示边框</param>
        T Border( bool isShow = true );
        /// <summary>
        /// 显示分隔条
        /// </summary>
        T Split();
        /// <summary>
        /// 设置图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        T Icon( string iconClass );
        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        T Href( string url );
        /// <summary>
        /// 允许折叠
        /// </summary>
        /// <param name="collapsible">是否允许折叠</param>
        T Collapsible( bool collapsible = true );
        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="minWidth">最小宽度</param>
        T MinWidth( int minWidth );
        /// <summary>
        /// 设置最小高度
        /// </summary>
        /// <param name="minHeight">最小高度</param>
        T MinHeight( int minHeight );
        /// <summary>
        /// 设置最大宽度
        /// </summary>
        /// <param name="maxWidth">最大宽度</param>
        T MaxWidth( int maxWidth );
        /// <summary>
        /// 设置最大高度
        /// </summary>
        /// <param name="maxHeight">最大高度</param>
        T MaxHeight( int maxHeight );
    }
}
