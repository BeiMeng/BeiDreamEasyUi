using System.Web;

namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 组件
    /// </summary>
    public interface IComponent<out T> : IOption<T>, IHtmlString where T : IComponent<T> {
        /// <summary>
        /// 获取标识
        /// </summary>
        string GetId();
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        T Id( string id );
        /// <summary>
        /// 在控件后添加html
        /// </summary>
        /// <param name="html">Html</param>
        T AddAfter( string html );
    }
}
