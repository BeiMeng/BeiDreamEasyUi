using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.EasyUi.Areas.Common;
using BeiDream.EasyUi.Controllers;
using BeiDream.PetaPoco;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util;
using Util.Webs;
using Util.Webs.EasyUi.Forms;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Areas.Systems.Controllers
{
    [FormExceptionHandler]
    public class MenuManageController : Controller
    {
        public MenuManageController(IMenuRepository menuRepository)
        {
            MenuRepository = menuRepository;
        }

        protected IMenuRepository MenuRepository { get; private set; }
        private const LoadMode MenuLoadMode = LoadMode.Sync;
        //
        // GET: /Systems/MenuManage/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateNewRow(string parentId)
        {
            MenuViewModel newDto = MenuRepository.CreatNew(parentId);
            return Content(Util.Json.ToJson(newDto));
        }

        public ActionResult Query(QueryModel query)
        {
            switch (MenuLoadMode)
            {
                case LoadMode.Async:
                    return AsyncQueryLoad(query);
                case LoadMode.Sync:
                    return SyncQueryLoad(query);
                default:
                    return AsyncQueryLoad(query);
            }
        }
        /// <summary>
        /// 同步一次性加载
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult SyncQueryLoad(QueryModel query)
        {
            List<MenuViewModel> treeNodes = MenuRepository.FindByQuery(Sql.Builder.OrderBy("SortId ASC")).Select(menu => menu.ToDto()).ToList();
            CommonHelper.SetState(treeNodes,MenuLoadMode);
            PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(treeNodes, query.Page, query.Rows);
            return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
        }
        /// <summary>
        /// 异步，一次只加载一级节点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult AsyncQueryLoad(QueryModel query)
        {
            List<MenuViewModel> treeNodes;
            if (query.Id == null)
            {
                treeNodes = MenuRepository.GetAllTopLevel().Select(menu=>menu.ToDto()).ToList();
                CommonHelper.SetState(treeNodes, MenuLoadMode);
                PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(treeNodes, query.Page, query.Rows);
                return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
            }
            else
            {
                treeNodes = MenuRepository.GetChidrenLevel(new Guid(query.Id)).Select(menu => menu.ToDto()).ToList();
                CommonHelper.SetState(treeNodes, MenuLoadMode);
                return new TreeGridResult(treeNodes, true, -1).GetResult();
            }
        }

        public ActionResult Save(string addList, string updateList, string deleteList)
        {
            var listAdd = Util.Json.ToObject<List<MenuViewModel>>(addList);
            var listUpdate = Util.Json.ToObject<List<MenuViewModel>>(updateList);
            var listDelete = Util.Json.ToObject<List<MenuViewModel>>(deleteList);
            var data = MenuRepository.Save(listAdd, listUpdate, listDelete);
            CommonHelper.SetState(data, MenuLoadMode);
            return new EasyUiResult(StateCode.Ok, "操作成功", data).GetResult();
        }
    }
}
