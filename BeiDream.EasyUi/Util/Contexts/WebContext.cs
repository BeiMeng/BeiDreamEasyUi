using System.Web;

namespace Util.Contexts {
    /// <summary>
    /// Web上下文
    /// </summary>
    internal class WebContext : IContext {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">对象</param>
        public void Add<T>( string key, T value ) {
            if ( HttpContext.Current == null )
                return;
            HttpContext.Current.Items[key] = value;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        public T Get<T>( string key ) {
            if ( HttpContext.Current == null )
                return default(T);
            return (T)HttpContext.Current.Items[key];
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove( string key ) {
            if ( HttpContext.Current == null )
                return;
            HttpContext.Current.Items.Remove( key );
        }
    }
}
