using System;
using System.Security.Principal;

namespace Util.Security {
    /// <summary>
    /// 安全主体
    /// </summary>
    public class Principal : IPrincipal{
        /// <summary>
        /// 初始化安全主体
        /// </summary>
        public Principal() 
            : this( new UnauthenticatedIdentity() ){
        }

        /// <summary>
        /// 初始化安全主体
        /// </summary>
        public Principal( IIdentity identity ) {
            Identity = identity;
        }

        /// <summary>
        /// 身份标识
        /// </summary>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// 验证用户是否在指定角色中
        /// </summary>
        /// <param name="role">角色</param>
        public bool IsInRole( string role ) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 未认证安全主体
        /// </summary>
        public static UnauthenticatedPrincipal Unauthenticated() {
            return new UnauthenticatedPrincipal();
        }
    }
}
