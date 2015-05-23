using System.Web;

namespace Util.Security.Webs {
    /// <summary>
    /// HttpContextBase适配器
    /// </summary>
    public class HttpContextBaseAdapter : IHttpContextAdapter{
        /// <summary>
        /// 初始化HttpContextBase适配器
        /// </summary>
        /// <param name="httpContext">Http上下文</param>
        public HttpContextBaseAdapter( HttpContextBase httpContext ) {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Http上下文
        /// </summary>
        private readonly HttpContextBase _httpContext;

        /// <summary>
        /// 获取身份标识
        /// </summary>
        public Identity GetIdentity() {
            if ( _httpContext == null )
                return Identity.Unauthenticated();
            if ( _httpContext.User == null )
                return Identity.Unauthenticated();
            var identity = _httpContext.User.Identity as Identity;
            if ( identity == null )
                return Identity.Unauthenticated();
            return identity;
        }
    }
}
