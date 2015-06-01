using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Applications.Services.Configs;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.EasyUi.Areas.Systems.QueryModels;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util;
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


        #region 图标管理
        //
        // GET: /Systems/Icon/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">Id集合字符串，多个Id用逗号分隔</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [FormExceptionHandler]
        [AjaxOnly]
        public ActionResult Delete(string ids)
        {
            IconRepository.DeleteIconsAndDeleteCss(ids.ToGuidList(), ConfigInfo.CssPath);
            return new EasyUiResult(StateCode.Ok, "删除成功").GetResult();
        }
        /// <summary>
        /// 查看详细
        /// </summary>
        /// <param name="id">实体编号</param>
        public PartialViewResult Detail(string id)
        {
            IconViewModel model = IconRepository.Find(id.ToGuidList())[0].ToDto();
            return PartialView("Parts/Icon.Detail", model);
        }
        public ActionResult Query(IconQueryModel query)
        {
            Sql sql=new Sql();
            if (query.Size != null)
                sql.Where("Width=@0", query.Size.Split('*')[0]).Where("Height=@0", query.Size.Split('*')[1]);
            if (query.BeginCreateTime != null)
                sql.Where("CreateTime>@0", query.BeginCreateTime);
            if (query.EndCreateTime != null)
                sql.Where("CreateTime<@0", query.EndCreateTime);
            PagedList<IconViewModel> result =IconRepository.PagedLists(query.Page, query.Rows, sql);//new PagedList<IconViewModel>(icons, query.Page, query.Rows);
            return new DataGridResult(result, result.TotalItemCount).GetResult();
        } 
        #endregion

        #region 图标上传
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
            IconRepository.UpLoadAndAddIcon(ConfigInfo.UploadIconPath, ConfigInfo.CssPath);
            return new EasyUiResult(StateCode.Ok, "操作成功").GetResult();
        } 
        #endregion

        #region 图标选择弹出层
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
        public PartialViewResult GetSizeTabs()
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
        public PartialViewResult GetIconTab(int width, int height, QueryModel query)
        {
            Sql sql = new Sql();
            sql.Where("Width=@0", width).Where("Height=@0", height);
            PagedList<IconViewModel> result = IconRepository.PagedLists(query.Page, query.Rows,sql);
            IconListViewModel iconListViewModel = new IconListViewModel(width, height, result);
            return PartialView("IconsControl/Tab", iconListViewModel);
        } 
        #endregion
    }
}
