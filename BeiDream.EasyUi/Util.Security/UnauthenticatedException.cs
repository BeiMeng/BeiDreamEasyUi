using System;

namespace Util.Security {
    /// <summary>
    /// 未授权访问异常
    /// </summary>
    public class UnauthenticatedException : Exception {
        /// <summary>
        /// 初始化未授权访问异常
        /// </summary>
        public UnauthenticatedException( Exception ex )
            : this("访问未经授权",ex){
        }

        /// <summary>
        /// 初始化未授权访问异常
        /// </summary>
        public UnauthenticatedException( string message, Exception ex )
            : base( message, ex ) {
        }
    }
}
