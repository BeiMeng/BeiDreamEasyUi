using System.Collections.Generic;
using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// 基控制器
    /// </summary>
    [ErrorLog]
    [TraceLog]
    public abstract class ControllerBase : Controller {
        /// <summary>
        /// 转换为Json字符串
        /// </summary>
        /// <param name="data">对象</param>
        public string ToJson( object data ) {
            return Util.Json.ToJson( data );
        }

        /// <summary>
        /// 转换为Json字符串
        /// </summary>
        /// <param name="data">对象</param>
        public string ToJson( IEnumerable<object> data ) {
            return Util.Json.ToJson( data );
        }

        /// <summary>
        /// 转换为Json结果
        /// </summary>
        /// <param name="data">对象</param>
        public ActionResult ToJsonResult( object data ) {
            return Content( ToJson( data ) );
        }

        /// <summary>
        /// 转换为Json结果
        /// </summary>
        /// <param name="data">对象</param>
        public ActionResult ToJsonResult( IEnumerable<object> data ) {
            return Content( ToJson( data ) );
        }
    }
}
