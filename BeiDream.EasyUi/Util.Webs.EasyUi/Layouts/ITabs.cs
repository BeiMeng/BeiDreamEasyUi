using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 选项卡
    /// </summary>
    public interface ITabs : IContainer<ITabs> {
        /// <summary>
        /// 启用平滑效果
        /// </summary>
        /// <param name="plain">是否启用平滑效果</param>
        ITabs Plain( bool plain = true );
        /// <summary>
        /// 设置自适应
        /// </summary>
        ITabs Fit();
        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="isShow">是否显示边框</param>
        ITabs Border( bool isShow = true );
        /// <summary>
        /// 设置选项卡面板位置
        /// </summary>
        /// <param name="align">对齐方式</param>
        ITabs TabPosition( Align align );
        /// <summary>
        /// 设置选项卡标题宽度
        /// </summary>
        /// <param name="width">标题宽度</param>
        ITabs HeaderWidth( int width );
        /// <summary>
        /// 设置选项卡面板宽度
        /// </summary>
        /// <param name="width">选项卡面板宽度</param>
        ITabs TabWidth( int width );
        /// <summary>
        /// 设置选项卡面板高度
        /// </summary>
        /// <param name="width">选项卡面板高度</param>
        ITabs TabHeight( int width );
    }
}
