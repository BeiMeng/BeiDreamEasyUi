using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Applications.Services.Configs;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util.Webs;
using Util.Webs.EasyUi.Forms;
using Util.Webs.EasyUi.Results;

namespace BeiDream.EasyUi.Areas.Systems.Controllers
{
    public class IconController : Controller
    {
        public IconController(IIconRepositiory iconRepository)
        {
            IconRepository = iconRepository;
        }

        protected IIconRepositiory IconRepository { get; private set; }
        //
        // GET: /Systems/Icon/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult IconPanel()
        {
            return PartialView("Parts/IconPanel");
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
            IconRepository.UpLoadAndAddIcon(ConfigInfo.UploadIconPath,ConfigInfo.CssPath);
            return new EasyUiResult(StateCode.Ok, "操作成功").GetResult();
        }

        public ActionResult Query(QueryModel query)
        {
            List<IconViewModel> icons = IconRepository.GetAll();
            PagedList<IconViewModel> result = new PagedList<IconViewModel>(icons, query.Page, query.Rows);
            return new DataGridResult(result,result.TotalItemCount).GetResult();
        }

        /// <summary>
        /// 选择图标控件
        /// </summary>
        [AjaxOnly]
        public PartialViewResult IconsControl()
        {
            return PartialView();
        }
        /// <summary>
        /// 获取图标尺寸选项卡
        /// </summary>
        [AjaxOnly]
        public PartialViewResult GetSizeTabs(QueryModel query)
        {
            return PartialView("IconsControl/SizeTabs");
        }

        /// <summary>
        /// 获取图标控件选项卡面板
        /// </summary>
        /// <param name="height"></param>
        /// <param name="query">图标查询实体</param>
        /// <param name="width"></param>
        [AjaxOnly]
        public PartialViewResult GetIconTab(int width,int height,QueryModel query)
        {
            List<IconViewModel> icons = IconRepository.GetAllByQuery(width, height);
            PagedList<IconViewModel> result = new PagedList<IconViewModel>(icons, query.Page, query.Rows);
            IconListViewModel iconListViewModel = new IconListViewModel(width, height, result);
            return PartialView("IconsControl/Tab", iconListViewModel);
        }
    }
}
