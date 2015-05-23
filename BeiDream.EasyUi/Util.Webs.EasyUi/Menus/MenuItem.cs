using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Menus {
    /// <summary>
    /// 菜单项
    /// </summary>
    public class MenuItem : ComponentBase<IMenuItem>,IMenuItem {
        /// <summary>
        /// 文本
        /// </summary>
        private string _text;
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        public IMenuItem Text( string text ) {
            _text = text;
            return This();
        }

        /// <summary>
        /// 设置图标class
        /// </summary>
        /// <param name="iconClass">图标class</param>
        public IMenuItem Icon( string iconClass ) {
            return AddDataOption( "iconCls", iconClass,true );
        }

        /// <summary>
        /// 设置Url
        /// </summary>
        /// <param name="url">Url</param>
        public IMenuItem Href( string url ) {
            return AddDataOption( "href", url,true );
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public IMenuItem Disable() {
            return AddDataOption( "disabled", true );
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public IMenuItem Click( string handler ) {
            return AddAttribute( "onclick", handler );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            var result = new StringBuilder();
            result.AppendFormat( "<div {0}>{1}</div>", GetOptions(),_text );
            return result.ToString();
        }
    }
}
