using System.Web;

namespace Util.Security.Webs {
    /// <summary>
    /// HttpContext适配器
    /// </summary>
    public class HttpContextAdapter : IHttpContextAdapter {
        /// <summary>
        /// 获取身份标识
        /// </summary>
        public Identity GetIdentity() {
            if ( HttpContext.Current == null )
                return Identity.Unauthenticated();
            if ( HttpContext.Current.User == null )
                return Identity.Unauthenticated();
            var identity = HttpContext.Current.User.Identity as Identity;
            if ( identity == null )
                return Identity.Unauthenticated();
            return identity;
        }
    }
}
