using System.Web.Mvc;
using Util.Security;

namespace Util.Webs {
    /// <summary>
    /// 基视图
    /// </summary>
    /// <typeparam name="TModel">实体</typeparam>
    public abstract class ViewBase<TModel> : WebViewPage<TModel> {
        /// <summary>
        /// 皮肤
        /// </summary>
        public string Skin {
            get { return SecurityContext.Identity.Skin; }
        }
    }
}
