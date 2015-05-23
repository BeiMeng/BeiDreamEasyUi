using Util.Webs.EasyUi.Base;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 选项卡
    /// </summary>
    public class Tabs : ContainerBase<ITabs>, ITabs {
        /// <summary>
        /// 初始化选项卡
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public Tabs( ITextWriter textWriter ): base( textWriter ) {
            AddClass( "easyui-tabs" );
        }

        /// <summary>
        /// 启用平滑效果
        /// </summary>
        /// <param name="plain">是否启用平滑效果</param>
        public ITabs Plain( bool plain = true ) {
            return AddDataOption( "plain", plain );
        }

        /// <summary>
        /// 设置自适应
        /// </summary>
        public ITabs Fit() {
            return AddDataOption( "fit", true );
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="isShow">是否显示边框</param>
        public ITabs Border( bool isShow = true ) {
            return AddDataOption( "border", isShow );
        }

        /// <summary>
        /// 设置选项卡面板位置
        /// </summary>
        /// <param name="align">对齐方式</param>
        public ITabs TabPosition( Align align ) {
            return AddDataOption( "tabPosition",align.Description(),true );
        }

        /// <summary>
        /// 设置选项卡标题宽度
        /// </summary>
        /// <param name="width">标题宽度</param>
        public ITabs HeaderWidth( int width ) {
            return AddDataOption( "headerWidth", width );
        }

        /// <summary>
        /// 设置选项卡面板宽度
        /// </summary>
        /// <param name="width">选项卡面板宽度</param>
        public ITabs TabWidth( int width ) {
            return AddDataOption( "tabWidth", width );
        }

        /// <summary>
        /// 设置选项卡面板高度
        /// </summary>
        /// <param name="width">选项卡面板高度</param>
        public ITabs TabHeight( int width ) {
            return AddDataOption( "tabHeight", width );
        }
    }
}
