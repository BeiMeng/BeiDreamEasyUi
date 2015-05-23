using System;
using System.Security.Principal;
using System.Web;

namespace Util.Security.Webs {
    /// <summary>
    /// 基授权模块
    /// </summary>
    public abstract class AuthorizeModuleBase : IHttpModule {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init( HttpApplication context ) {
            context.AuthenticateRequest += SetPrincipal;
        }

        /// <summary>
        /// 设置安全主体
        /// </summary>
        private void SetPrincipal( object sender, EventArgs e ) {
            var context = sender as HttpApplication;
            if ( context == null )
                return;
            if ( context.User == null || context.User.Identity.IsAuthenticated == false ) {
                HttpContext.Current.User = GetPrincipal( Guid.Empty );
                return;
            }
            HttpContext.Current.User = GetPrincipal( context.User.Identity.Name.ToGuid() );
        }

        /// <summary>
        /// 获取安全主体
        /// </summary>
        private IPrincipal GetPrincipal( Guid userId ) {
            try {
                return new Principal( GetIdentity( userId ) );
            }
            catch( Exception ex ) {
                throw new UnauthenticatedException(ex);
            }
        }

        /// <summary>
        /// 获取用户标识
        /// </summary>
        /// <param name="userId">用户编号</param>
        protected abstract Identity GetIdentity( Guid userId );

        /// <summary>
        /// 获取安全服务
        /// </summary>
        protected abstract ISecurityManager GetSecurityService();

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() {
        }
    }
}
