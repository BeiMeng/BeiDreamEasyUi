using System.Security.Principal;
using System.Threading;
using System.Web;

namespace Util.Security {
    /// <summary>
    /// 安全上下文
    /// </summary>
    public class SecurityContext {
        /// <summary>
        /// 当前用户安全主体
        /// </summary>
        public static Principal User {
            get {
                IPrincipal currentPrincipal = HttpContext.Current == null ? Thread.CurrentPrincipal : HttpContext.Current.User;
                var result = currentPrincipal as Principal;
                return result ?? Principal.Unauthenticated();
            }
            set {
                if( HttpContext.Current == null )
                    Thread.CurrentPrincipal = value;
                else
                    HttpContext.Current.User = value;
            }
        }

        /// <summary>
        /// 当前用户身份标识
        /// </summary>
        public static Identity Identity {
            get {
                var result = User.Identity as Identity;
                return result ?? Identity.Unauthenticated();
            }
        }
    }
}
