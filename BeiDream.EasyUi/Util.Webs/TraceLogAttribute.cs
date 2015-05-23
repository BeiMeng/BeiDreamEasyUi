using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Util.Logs;

namespace Util.Webs {
    /// <summary>
    /// 记录跟踪日志
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false )]
    public class TraceLogAttribute : ActionFilterAttribute {
        /// <summary>
        /// 是否忽略,为true不记录日志
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        private ILog Log { get; set; }

        /// <summary>
        /// 执行前
        /// </summary>
        public override void OnActionExecuting( ActionExecutingContext context ) {
            base.OnActionExecuting( context );
            if ( context.IsChildAction )
                return;
            if ( Ignore )
                return;
            WriteLog( context );
        }

        /// <summary>
        /// 执行前日志
        /// </summary>
        private void WriteLog( ActionExecutingContext context ) {
            Log = Logs.Log4.Log.GetContextLog( context.Controller );
            Log.Method = context.ActionDescriptor.ActionName;
            AddParams( context.ActionParameters );
            Log.Debug();
            Log.Start();
        }

        /// <summary>
        /// 添加参数列表
        /// </summary>
        private void AddParams( IEnumerable<KeyValuePair<string, object>> paramList ) {
            foreach ( var parameter in paramList ) {
                if ( IsSecret( parameter.Key ) )
                    continue;
                AddParams( parameter );
            }
        }

        /// <summary>
        /// 是否机密
        /// </summary>
        private bool IsSecret( string name ) {
            return name.ToLower().Contains( "password" );
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        private void AddParams( KeyValuePair<string, object> parameter ) {
            Log.Params.Add( "{0}:{1},", parameter.Key, parameter.Value );
        }

        /// <summary>
        /// 执行后
        /// </summary>
        public override void OnActionExecuted( ActionExecutedContext context ) {
            base.OnActionExecuted( context );
            if ( context.IsChildAction )
                return;
            if ( Ignore )
                return;
            WriteLog( context );
        }

        /// <summary>
        /// 执行后日志
        /// </summary>
        private void WriteLog( ActionExecutedContext context ) {
            Log.Method = context.ActionDescriptor.ActionName;
            Log.Debug();
        }
    }
}
