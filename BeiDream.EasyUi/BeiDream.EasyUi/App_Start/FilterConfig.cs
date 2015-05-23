using System.Web;
using System.Web.Mvc;

namespace BeiDream.EasyUi
{
    /// <summary>
    /// 过滤器注册
    /// </summary>
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}