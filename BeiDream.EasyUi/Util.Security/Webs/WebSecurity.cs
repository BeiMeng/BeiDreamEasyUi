using System.Web.Security;

namespace Util.Security.Webs {
    /// <summary>
    /// Web安全操作
    /// </summary>
    public class WebSecurity {
        /// <summary>
        /// 安全认证
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isPersistent">是否</param>
        public static void Authenticate( string userId, bool isPersistent = false ) {
            FormsAuthentication.SetAuthCookie( userId, isPersistent );
        }
    }
}
