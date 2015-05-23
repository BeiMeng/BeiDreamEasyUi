namespace Util.Security.Webs {
    /// <summary>
    /// Web权限管理器
    /// </summary>
    public class WebPermissionManager : PermissionManagerBase {
        /// <summary>
        /// 初始化Web权限管理器
        /// </summary>
        /// <param name="httpContext">Http上下文</param>
        /// <param name="securityManager">安全管理器</param>
        /// <param name="ignore">是否忽视权限</param>
        public WebPermissionManager( IHttpContextAdapter httpContext, ISecurityManager securityManager, bool ignore )
            : base( securityManager, ignore ) {
            _httpContext = httpContext;
        }

        /// <summary>
        /// Http上下文
        /// </summary>
        private readonly IHttpContextAdapter _httpContext;

        /// <summary>
        /// 创建用户身份标识
        /// </summary>
        protected override Identity GetIdentity() {
            _httpContext.CheckNull( "_httpContext" );
            return _httpContext.GetIdentity();
        }
    }
}
