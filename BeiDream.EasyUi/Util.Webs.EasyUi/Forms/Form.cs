using Util.Webs.EasyUi.Base;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Forms {
    /// <summary>
    /// 表单
    /// </summary>
    public class Form : ContainerBase<IForm>,IForm {
        /// <summary>
        /// 初始化面板
        /// </summary>
        /// <param name="textWriter">文本写入器</param>
        public Form( ITextWriter textWriter )
            : base( textWriter ) {
        }

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public IForm Id( string id ) {
            return UpdateAttribute( "id", id );
        }

        /// <summary>
        /// 设置服务端Url
        /// </summary>
        /// <param name="url">服务端Url</param>
        public IForm Action( string url ) {
            return AddAttribute( "action", url );
        }

        /// <summary>
        /// 保存前弹出确认消息
        /// </summary>
        /// <param name="message">确认消息</param>
        public IForm Confirm( string message ) {
            return AddAttribute( "confirm", message );
        }

        /// <summary>
        /// Post提交方式
        /// </summary>
        public IForm Post() {
            return AddAttribute( "method", "post" );
        }

        /// <summary>
        /// 获取起始标签
        /// </summary>
        protected override string GetBeginResult() {
            return string.Format( "<form {0}>", GetOptions() );
        }

        /// <summary>
        /// 获取结束标签
        /// </summary>
        protected override string GetEndResult() {
            return "</form>";
        }
    }
}
