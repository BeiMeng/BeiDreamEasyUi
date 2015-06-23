using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.EasyUi.Areas.Common;
using BeiDream.PetaPoco;
using BeiDream.Services.CalibrationManagement.Dots;
using BeiDream.Services.CalibrationManagement.IServices;
using Util.Webs;
using Util.Webs.EasyUi.Forms;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Areas.CalibrationManagement.Controllers
{
    [FormExceptionHandler]
    public class ParameterManageController : Controller
    {
        public ParameterManageController(IParameterRepository parameterRepository)
        {
            ParameterRepository = parameterRepository;
        }

        protected IParameterRepository ParameterRepository { get; private set; }
        private const LoadMode ParameterLoadMode = LoadMode.Sync;
        //
        // GET: /CalibrationManagement/ParameterManage/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateNewRow(string parentId)
        {
            ParameterViewModel newDto = ParameterRepository.CreatNew(parentId);
            return Content(Util.Json.ToJson(newDto));
        }
        public ActionResult Query(QueryModel query)
        {
            switch (ParameterLoadMode)
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
        /// 异步，一次只加载一级节点
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult AsyncQueryLoad(QueryModel query)
        {
            List<ParameterViewModel> treeNodes;
            if (query.Id == null)
            {
                treeNodes = ParameterRepository.GetAllTopLevel().Select(parameter=>parameter.ToDto()).ToList();
                CommonHelper.SetState(treeNodes, ParameterLoadMode);
                PagedList<ParameterViewModel> result = new PagedList<ParameterViewModel>(treeNodes, query.Page, query.Rows);
                return new TreeGridResult(result, true, result.TotalItemCount).GetResult();
            }
            else
            {
                treeNodes = ParameterRepository.GetChidrenLevel(new Guid(query.Id)).Select(parameter => parameter.ToDto()).ToList();
                CommonHelper.SetState(treeNodes, ParameterLoadMode);
                return new TreeGridResult(treeNodes, true, -1).GetResult();
            }
        }
        /// <summary>
        /// 同步一次性加载
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private ActionResult SyncQueryLoad(QueryModel query)
        {
            List<ParameterViewModel> parameterTreeNodes = ParameterRepository.FindByQuery(Sql.Builder.OrderBy("SortId ASC")).Select(parameter => parameter.ToDto()).ToList();
            //CommonHelper.SetState(parameterTreeNodes, ParameterLoadMode);//返回treegrid数据时会进行处理
            PagedList<ParameterViewModel> result =new PagedList<ParameterViewModel>(parameterTreeNodes, query.Page, query.Rows);
            return new TreeGridResult(result, ParameterLoadMode != LoadMode.Sync, result.TotalItemCount).GetResult();
        }
        public ActionResult Save(string addList, string updateList, string deleteList)
        {
            var listAdd = Util.Json.ToObject<List<ParameterViewModel>>(addList);
            var listUpdate = Util.Json.ToObject<List<ParameterViewModel>>(updateList);
            var listDelete = Util.Json.ToObject<List<ParameterViewModel>>(deleteList);
            var data = ParameterRepository.Save(listAdd, listUpdate, listDelete);
            //CommonHelper.SetState(data, ParameterLoadMode);
            return new EasyUiResult(StateCode.Ok, "操作成功", data).GetResult();
        }
    }
}
