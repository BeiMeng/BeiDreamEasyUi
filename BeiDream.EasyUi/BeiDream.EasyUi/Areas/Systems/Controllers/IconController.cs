using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util.Webs;
using Util.Webs.EasyUi.Forms;
using Util.Webs.EasyUi.Results;

namespace BeiDream.EasyUi.Areas.Systems.Controllers
{
    public class IconController : Controller
    {
        //
        // GET: /Systems/Icon/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取批量上传窗口
        /// </summary>
        [AjaxOnly]
        public PartialViewResult BatchAdd()
        {
            return PartialView("Parts/Icon.Form");
        }
        /// <summary>
        /// 上传图标
        /// </summary>
        [HttpPost]
        [FormExceptionHandler]
        public ActionResult Upload()
        {
            //IconService.Upload(categoryId);
            return new EasyUiResult(StateCode.Ok, "操作成功").GetResult();
        }
    }
}
