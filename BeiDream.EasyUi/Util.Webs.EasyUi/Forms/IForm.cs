using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Forms {
    /// <summary>
    /// 表单
    /// </summary>
    public interface IForm : IContainer<IForm> {
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        IForm Id( string id );
        /// <summary>
        /// 设置服务端Url
        /// </summary>
        /// <param name="url">服务端Url</param>
        IForm Action( string url );
        /// <summary>
        /// 保存前弹出确认消息
        /// </summary>
        /// <param name="message">确认消息</param>
        IForm Confirm( string message );
        /// <summary>
        /// Post提交方式
        /// </summary>
        IForm Post();
    }
}
