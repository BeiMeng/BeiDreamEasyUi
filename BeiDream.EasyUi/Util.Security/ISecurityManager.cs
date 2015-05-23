using System;

namespace Util.Security {
    /// <summary>
    /// 安全管理器
    /// </summary>
    public interface ISecurityManager {
        /// <summary>
        /// 检测该用户是否属于当前应用程序
        /// </summary>
        /// <param name="userId">用户编号</param>
        bool IsInApplication( Guid userId );
        /// <summary>
        /// 检测该用户是否属于当前租户
        /// </summary>
        /// <param name="userId">用户编号</param>
        bool IsInTenant( Guid userId );
        /// <summary>
        /// 获取资源的权限列表
        /// </summary>
        /// <param name="resourceUri">资源标识</param>
        ResourcePermissions GetPermissionsByResource( string resourceUri );
    }
}
