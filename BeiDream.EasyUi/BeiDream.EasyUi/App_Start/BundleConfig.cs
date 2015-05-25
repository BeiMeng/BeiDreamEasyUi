using System.Web.Optimization;

namespace BeiDream.EasyUi
{
    /// <summary>
    /// 资源打包配置
    /// </summary>
    public class BundleConfig {
        /// <summary>
        /// 注册资源打包
        /// </summary>
        public static void RegisterBundles( BundleCollection bundles ) {
            //启用打包压缩
            //BundleTable.EnableOptimizations = true;
            //css样式
            bundles.Add(new StyleBundle("~/Css/css").Include(
                "~/Css/icon.css",
                "~/Css/common.css"));
            //Easyui扩展
            bundles.Add(new ScriptBundle("~/Scripts/EasyUi/js").Include(
                "~/Scripts/EasyUi/easyui-lang-zh_CN.js",
                "~/scripts/EasyUi/jquery.easyui.edatagrid.js",
                "~/scripts/EasyUi/jquery.easyui.treegrid.dnd.js",
                "~/scripts/EasyUi/jquery.easyui.etreegrid.js",
                "~/scripts/EasyUi/jquery.easyui.datagrid.detailview.js",
                "~/scripts/EasyUi/jquery.easyui.datagrid.lookup.js"));
            //util js
            bundles.Add(new ScriptBundle("~/Scripts/Utils/js").Include(
                "~/Scripts/Utils/util.js",
                "~/Scripts/Utils/jquery.util.js",
                "~/Scripts/Utils/jquery.util.webuploader.js",
                "~/Scripts/Utils/jquery.util.easyui.extension.js",
                "~/Scripts/Utils/jquery.util.easyui.js",
                "~/Scripts/Utils/jquery.util.easyui.config.js",
                "~/Scripts/Utils/jquery.util.easyui.form.js",
                "~/Scripts/Utils/jquery.util.easyui.grid.js",
                "~/Scripts/Utils/jquery.util.easyui.tree.js",
                "~/Scripts/Utils/jquery.util.easyui.fn.js"));
        }
    }
}