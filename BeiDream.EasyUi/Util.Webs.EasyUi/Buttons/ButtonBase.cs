using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 按钮
    /// </summary>
    public abstract class ButtonBase<T> : ComponentBase<T>, IButtonBase<T> where T : IButtonBase<T> {
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        protected ButtonBase( string text ) {
            Text = text;
            AddClass( "easyui-linkbutton" );
            _href = "javascript:void(0)";
        }

        /// <summary>
        /// 按钮文本
        /// </summary>
        protected string Text { get; set; }
        /// <summary>
        /// url
        /// </summary>
        protected string _href;

        /// <summary>
        /// 禁用按钮
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        public T Disable( bool disabled = true ) {
            return AddDataOption( "disabled", disabled );
        }

        /// <summary>
        /// 启用平滑效果
        /// </summary>
        /// <param name="isPlain">true为启用平滑效果</param>
        public T Plain( bool isPlain = true ) {
            return AddDataOption( "plain", isPlain );
        }

        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public T Icon( string iconClass ) {
            return AddDataOption( "iconCls", iconClass, true );
        }

        /// <summary>
        /// 设置图标对齐方式
        /// </summary>
        /// <param name="align">图标对齐方式</param>
        public T IconAlign( Align align ) {
            return AddDataOption( "iconAlign", align.Description(), true );
        }

        /// <summary>
        /// 设置为小按钮
        /// </summary>
        public T Small() {
            return AddDataOption( "size", "small", true );
        }

        /// <summary>
        /// 设置为大按钮
        /// </summary>
        public T Large() {
            return AddDataOption( "size", "large", true );
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">事件处理函数或Js代码</param>
        public T Click( string handler ) {
            return AddAttribute( "onClick", handler );
        }

        /// <summary>
        /// 设置url
        /// </summary>
        /// <param name="href">url</param>
        public T Href( string href ) {
            _href = href;
            return This();
        }

        /// <summary>
        /// 设置工具提示
        /// </summary>
        /// <param name="content">提示内容</param>
        public T ToolTip( string content ) {
            return AddClass( "easyui-tooltip" ).AddAttribute( "title", content );
        }

        /// <summary>
        /// 设置工具提示
        /// </summary>
        /// <param name="content">提示内容</param>
        /// <param name="align">对齐方式</param>
        public T ToolTip( string content, Align align ) {
            return ToolTip( content ).AddDataOption( "position", align.Description(), true );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            var result = new StringBuilder();
            result.AppendFormat( "<a href=\"{0}\" {1}>", _href, GetOptions() );
            result.AppendFormat( "{0}</a>", Text );
            return result.ToString();
        }
    }
}
