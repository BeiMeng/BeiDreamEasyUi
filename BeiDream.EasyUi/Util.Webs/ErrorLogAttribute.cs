using System;
using System.Web.Mvc;
using Util.Logs.Log4;

namespace Util.Webs {
    /// <summary>
    /// 记录异常日志
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true )]
    public class ErrorLogAttribute : HandleErrorAttribute{
        /// <summary>
        /// 处理异常
        /// </summary>
        public override void OnException( ExceptionContext context ) {
            base.OnException( context );
            WriteLog( context );
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        private void WriteLog( ExceptionContext context ) {
            if ( context == null )
                return;
            var log = Log.GetContextLog( context.Controller );
            log.Caption.Add( "Mvc全局异常捕获" );
            log.Exception = context.Exception;
            Warning.WriteLog( log,context.Exception );
        }
    }
}
