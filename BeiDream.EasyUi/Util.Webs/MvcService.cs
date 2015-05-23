using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// Mvc服务
    /// </summary>
    public class MvcService : MvcBase,IMvcService {
        /// <summary>
        /// 导入Css
        /// </summary>
        /// <param name="path">Css文件相对路径</param>
        public MvcHtmlString ImportCss( string path ) {
            return MvcResult( "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />",Web.ResolveUrl( path ) );
        }

        /// <summary>
        /// 导入Js
        /// </summary>
        /// <param name="path">Js文件相对路径</param>
        public MvcHtmlString ImportJs( string path ) {
            return MvcResult( "<script type=\"text/javascript\" src=\"{0}\"></script>", Web.ResolveUrl( path ) );
        }
    }
}
