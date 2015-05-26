using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.EasyUi.Controllers;
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
        private const LoadMode loadMode = LoadMode.Sync;
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
            switch (loadMode)
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
            List<MenuViewModel> treeNodes = MenuRepository.GetAllTreeNodes();
            SetState(treeNodes);
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
                treeNodes = MenuRepository.GetNavigationMenu();
                SetState(treeNodes);
                PagedList<MenuViewModel> result = new PagedList<MenuViewModel>(treeNodes, query.Page, query.Rows);
                return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
            }
            else
            {
                treeNodes = MenuRepository.GetMenuManageChildrenNodes(query.Id);
                SetState(treeNodes);
                return new TreeGridResult(treeNodes, true, -1).GetResult();
            }
        }

        private void SetState(List<MenuViewModel> treeNodes)
        {
            foreach (var treeNode in treeNodes)
            {
                switch (loadMode)
                {
                    case LoadMode.Async:
                        treeNode.state = "closed";
                        break;
                    case LoadMode.Sync:
                        treeNode.state = "open";
                        break;
                    default:
                       treeNode.state = "closed";
                       break;
                }
            }
        }

        public ActionResult Save(string addList, string updateList, string deleteList)
        {
            var listAdd = Util.Json.ToObject<List<MenuViewModel>>(addList);
            var listUpdate = Util.Json.ToObject<List<MenuViewModel>>(updateList);
            var listDelete = Util.Json.ToObject<List<MenuViewModel>>(deleteList);
            var data = MenuRepository.Save(listAdd, listUpdate, listDelete);
            SetState(data);
            return new EasyUiResult(StateCode.Ok, "操作成功", data).GetResult();
        }
    }
}
