using System.Web.Mvc;

namespace Util.Webs.EasyUi.Results {
    /// <summary>
    /// 输出结果
    /// </summary>
    public abstract class ResultBase {
        /// <summary>
        /// 获取输出结果
        /// </summary>
        public ActionResult GetResult() {
            return new ContentResult { Content = ToString() };
        }
    }
}
