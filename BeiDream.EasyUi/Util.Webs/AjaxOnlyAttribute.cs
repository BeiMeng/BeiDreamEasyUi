using System;
using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// 仅允许Ajax操作
    /// </summary>
    [AttributeUsage( AttributeTargets.Method, Inherited = true, AllowMultiple = false )]
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute {
        /// <summary>
        /// 初始化仅允许Ajax操作
        /// </summary>
        /// <param name="ignore">跳过Ajax检测</param>
        public AjaxOnlyAttribute( bool ignore = false ) {
            Ignore = ignore;
        }

        /// <summary>
        /// 跳过Ajax检测
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// 验证请求有效性
        /// </summary>
        /// <param name="controllerContext">控制器上下文</param>
        /// <param name="methodInfo">方法</param>
        public override bool IsValidForRequest( ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo ) {
            if ( Ignore )
                return true;
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
