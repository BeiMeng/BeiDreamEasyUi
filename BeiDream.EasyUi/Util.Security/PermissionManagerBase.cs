using System;

namespace Util.Security {
    /// <summary>
    /// 权限管理器
    /// </summary>
    public abstract class PermissionManagerBase : IPermissionManager {
        /// <summary>
        /// 初始化权限管理器
        /// </summary>
        /// <param name="securityManager">安全管理器</param>
        /// <param name="ignore">是否忽视权限</param>
        protected PermissionManagerBase( ISecurityManager securityManager, bool ignore ) {
            _securityManager = securityManager;
            _ignore = ignore;
        }

        /// <summary>
        /// 用户身份标识
        /// </summary>
        private Identity _identity;
        /// <summary>
        /// 安全管理器
        /// </summary>
        private readonly ISecurityManager _securityManager;
        /// <summary>
        /// 是否忽视权限
        /// </summary>
        private readonly bool _ignore;

        /// <summary>
        /// 检查当前用户是否具有该资源的权限
        /// </summary>
        /// <param name="resourceUri">资源标识，一般为网页地址,范例：/a/b/c</param>
        public bool HasPermission( string resourceUri ) {
            if ( resourceUri.IsEmpty() )
                return false;
            _identity = GetIdentity();
            Validate();
            if ( !ValidateBefore() )
                return false;
            if ( !ValidateAuthenticated() )
                return false;
            if ( !ValidateApplication() )
                return false;
            if ( !ValidateTenant() )
                return false;
            if ( IsIgnore() )
                return true;
            if ( !ValidateRoles( resourceUri ) )
                return false;
            if ( !ValidateAfter() )
                return false;
            return true;
        }

        /// <summary>
        /// 获取用户身份标识
        /// </summary>
        protected abstract Identity GetIdentity();

        /// <summary>
        /// 基础验证
        /// </summary>
        private void Validate() {
            _identity.CheckNull( "identity" );
            _securityManager.CheckNull( "securityManager" );
        }

        /// <summary>
        /// 验证前
        /// </summary>
        private bool ValidateBefore() {
            return true;
        }

        /// <summary>
        /// 验证是否已登录
        /// </summary>
        private bool ValidateAuthenticated() {
            return _identity.Validate();
        }

        /// <summary>
        /// 验证用户是否属于当前应用程序
        /// </summary>
        private bool ValidateApplication() {
            return _securityManager.IsInApplication( GetUserId() );
        }

        /// <summary>
        /// 获取用户编号
        /// </summary>
        private Guid GetUserId() {
            return _identity.UserId.ToGuid();
        }

        /// <summary>
        /// 验证用户是否属于当前租户
        /// </summary>
        private bool ValidateTenant() {
            return _securityManager.IsInTenant( GetUserId() );
        }

        /// <summary>
        /// 忽视权限检测
        /// </summary>
        private bool IsIgnore() {
            return _ignore;
        }

        /// <summary>
        /// 验证用户角色是否被授权访问该资源
        /// </summary>
        private bool ValidateRoles(string resourceUri) {
            var permissions =_securityManager.GetPermissionsByResource( resourceUri );
            return permissions.HasPermission( _identity.RoleIds );
        }

        /// <summary>
        /// 验证后
        /// </summary>
        private bool ValidateAfter() {
            return true;
        }
    }
}
