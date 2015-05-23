using System.Collections.Generic;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Menus {
    /// <summary>
    /// 菜单
    /// </summary>
    public interface IMenu : IComponent<IMenu> {
        /// <summary>
        /// 设置zIndex属性
        /// </summary>
        /// <param name="value">值</param>
        IMenu ZIndex( int value );
        /// <summary>
        /// 设置位置
        /// </summary>
        /// <param name="left">左部偏移量</param>
        /// <param name="top">顶部偏移量</param>
        IMenu Position( int left = 0,int top = 0 );
        /// <summary>
        /// 设置最小宽度
        /// </summary>
        /// <param name="width">宽度</param>
        IMenu MinWidth( int width );
        /// <summary>
        /// 设置显示持续时间
        /// </summary>
        /// <param name="time">显示持续时间，当鼠标离开菜单时，经过该时间自动隐藏，单位：毫秒</param>
        IMenu Duration( int time );
        /// <summary>
        /// 设置当鼠标离开菜单时是否隐藏菜单
        /// </summary>
        /// <param name="isHide">是否隐藏</param>
        IMenu HideOnUnHover( bool isHide );
        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        IMenu Click( string handler );
        /// <summary>
        /// 设置菜单项
        /// </summary>
        /// <param name="items">菜单项</param>
        IMenu Items( params IMenuItem[] items );
    }
}
