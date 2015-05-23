namespace Util.Security {
    /// <summary>
    /// 未认证的身份标识
    /// </summary>
    public class UnauthenticatedIdentity : Identity{
        /// <summary>
        /// 初始化未认证的身份标识
        /// </summary>
        public UnauthenticatedIdentity()
            : base( false, string.Empty ) {
        }
    }
}
