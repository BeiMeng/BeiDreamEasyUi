using System.Collections.Generic;
using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Menus {
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : ComponentBase<IMenu>,IMenu {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="id">Id</param>
        public Menu( string id ) {
            _items = new List<IMenuItem>();
            Id( id ).AddClass( "easyui-menu" );
        }

        /// <summary>
        /// 菜单项
        /// </summary>
        private readonly List<IMenuItem> _items; 

        /// <summary>
        /// 设置zIndex属性
        /// </summary>
        /// <param name="value">值</param>
        public IMenu ZIndex( int value ) {
            return AddDataOption( "zIndex", value.ToString() );
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="left">左部偏移量</param>
        /// <param name="top">顶部偏移量</param>
        public IMenu Position( int left = 0,int top = 0 ) {
            return AddDataOption( "left", left.ToString() ).AddDataOption( "top", top.ToString() );
        }

        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="width">宽度</param>
        public IMenu MinWidth( int width ) {
            return AddDataOption( "minWidth", width.ToString() );
        }

        /// <summary>
        /// 设置显示持续时间
        /// </summary>
        /// <param name="time">显示持续时间，当鼠标离开菜单时，经过该时间自动隐藏，单位：毫秒</param>
        public IMenu Duration( int time ) {
            return AddDataOption( "duration", time.ToString() );
        }

        /// <summary>
        /// 设置当鼠标离开菜单时是否隐藏菜单
        /// </summary>
        /// <param name="isHide">是否隐藏</param>
        public IMenu HideOnUnHover( bool isHide ) {
            return AddDataOption( "hideOnUnhover", isHide );
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public IMenu Click( string handler ) {
            return AddDataOption( "onClick", handler );
        }

        /// <summary>
        /// 设置菜单项
        /// </summary>
        /// <param name="items">菜单项</param>
        public IMenu Items( params IMenuItem[] items ) {
            if ( items == null )
                return This();
            _items.AddRange( items );
            return This();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        protected override string GetResult() {
            var result = new StringBuilder();
            result.AppendFormat( "<div {0}>", GetOptions() );
            foreach( var item in _items )
                result.Append( item.ToHtmlString() );
            result.Append( "</div>" );
            return result.ToString();
        }
    }
}
