using System;
using System.Web.Mvc;
using Util.Webs.EasyUi.Results;

namespace Util.Webs.EasyUi.Forms {
    /// <summary>
    /// EasyUi表单异常处理器
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true )]
    public class FormExceptionHandlerAttribute : HandleErrorAttribute {
        /// <summary>
        /// 处理异常
        /// </summary>
        public override void OnException( ExceptionContext context ) {
            base.OnException( context );
            context.ExceptionHandled = true;
            string errorMsg = Warning.GetPrompt( context.Exception );
            context.Result = new EasyUiResult( StateCode.Fail, errorMsg ).GetResult();
        }
    }
}
