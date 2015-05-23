using System;
using System.Security.Principal;

namespace Util.Security {
    /// <summary>
    /// 身份标识
    /// </summary>
    public class Identity : IIdentity {
        /// <summary>
        /// 初始化身份标识
        /// </summary>
        public Identity()
            : this( false, "" ) {
        }

        /// <summary>
        /// 初始化身份标识
        /// </summary>
        /// <param name="isAuthenticated">是否认证</param>
        /// <param name="userId">用户标识</param>
        /// <param name="roleIds">角色编号列表</param>
        /// <param name="applicationId">应用程序编号</param>
        /// <param name="tenantId">租户编号</param>
        public Identity( bool isAuthenticated, string userId, string[] roleIds = null, string applicationId = "", string tenantId = "") {
            IsAuthenticated = isAuthenticated;
            Name = userId;
            RoleIds = roleIds;
            ApplicationId = applicationId;
            TenantId = tenantId;
        }

        /// <summary>
        /// 认证类型
        /// </summary>
        public string AuthenticationType { get { return "Custom"; } }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuthenticated { get; private set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId {
            get { return Name; }
        }

        /// <summary>
        /// 角色编号列表
        /// </summary>
        public string[] RoleIds { get; private set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 应用程序编号
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// 应用程序编码
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// 皮肤
        /// </summary>
        public string Skin { get; set; }

        /// <summary>
        /// 菜单样式
        /// </summary>
        public string MenuStyle { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        public bool Validate() {
            if ( !IsAuthenticated )
                return false;
            if ( UserId.ToGuid() == Guid.Empty )
                return false;
            return true;
        }

        /// <summary>
        /// 未认证的身份标识
        /// </summary>
        public static UnauthenticatedIdentity Unauthenticated() {
            return new UnauthenticatedIdentity();
        }
    }
}
