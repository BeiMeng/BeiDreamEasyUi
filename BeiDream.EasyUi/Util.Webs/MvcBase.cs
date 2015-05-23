using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// Mvc基操作
    /// </summary>
    public abstract class MvcBase {
        /// <summary>
        /// 返回Mvc Html结果
        /// </summary>
        /// <param name="result">结果</param>
        /// <param name="args">参数</param>
        protected MvcHtmlString MvcResult( string result, params object[] args ) {
            return new MvcHtmlString( string.Format( result, args ) );
        }
    }
}
