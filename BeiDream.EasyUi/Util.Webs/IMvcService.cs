using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// Mvc服务
    /// </summary>
    public interface IMvcService {
        /// <summary>
        /// 导入Css
        /// </summary>
        /// <param name="path">Css文件相对路径</param>
        MvcHtmlString ImportCss( string path );
        /// <summary>
        /// 导入Js
        /// </summary>
        /// <param name="path">Js文件相对路径</param>
        MvcHtmlString ImportJs( string path );
    }
}
