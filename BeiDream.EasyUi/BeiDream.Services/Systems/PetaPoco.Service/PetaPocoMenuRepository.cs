using System;
using System.Collections.Generic;
using System.Linq;
using BeiDream.Common;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.ServiceHelper;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util;
using Util.Exceptions;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.Systems.PetaPoco.Service
{
    public class PetaPocoMenuRepository : PetaPocoTreeRepositiory<BeiDreamMenu,Guid,Guid?>, IMenuRepository
    {
        /// 初始化仓储
        /// <param name="unitOfWork">工作单元</param>
        public PetaPocoMenuRepository(ISysPetaPocoUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过递归查询子节点方式)
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        public List<ITreeNode> GetNavigationMenuTreeByChildren(string parentId)
        {
            List<ITreeNode> treeNodes = GetNavigationMenuChildrenNodes(parentId);
            return GetTreeChildren(treeNodes);
        }
         //<summary>
         //根据父ID找到其下的导航菜单子节点(只是子节点)
         //</summary>
         //<param name="parentId"></param>
         //<returns></returns>
        public List<ITreeNode> GetNavigationMenuChildrenNodes(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = this.FindByQuery(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode
                {
                    Id = menu.Id.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = parentId
                };
                if (menu.Url != null)
                    treeNode.Attributes = new { url = menu.Url };
                treeNodes.Add(treeNode);
            }
            return treeNodes;
        }

        private List<ITreeNode> GetTreeChildren(List<ITreeNode> treeList)
        {
            foreach (var treeNode in treeList)
            {
                List<ITreeNode> childrenTreeNodes = GetNavigationMenuChildrenNodes(treeNode.Id);
                treeNode.children = childrenTreeNodes;
                if (treeList.Count > 0)
                {
                    GetTreeChildren(childrenTreeNodes);
                }
            }
            return treeList;
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="addList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public List<MenuViewModel> Save(List<MenuViewModel> addList, List<MenuViewModel> updateList, List<MenuViewModel> deleteList)
        {
            ViewModelHandle(addList, updateList, deleteList);
            List<BeiDreamMenu> addEntityList = AddEntitysHandle(addList);
            List<BeiDreamMenu> updateEntityList = UpdateEntitysHandle(updateList);
            List<BeiDreamMenu> deleteEntityList = DeleteEntitysHandle(deleteList);
            //修正增改数据的Path和level
            new TreeServiceHelper<BeiDreamMenu, Guid, Guid?>(addEntityList, updateEntityList, UnitOfWork, "Id");

            SaveData(addEntityList, updateEntityList, deleteEntityList);

            //返回被编过的视图数据，供前台使用
            List<string> ids = CommonHelper.GetIds(addEntityList, updateEntityList, deleteEntityList);
            List<Guid> gids = ids.Select(id => id.ToGuid()).ToList();
            var parameterList = Find(gids, "Id");
            return parameterList.Select(parameter => parameter.ToDto()).ToList();
        }
        #region 前台视图数据的验证及过滤操作
        /// <summary>
        /// 对前台数据的处理,1.过滤无效数据，2.服务端视图实体数据验证(即用户输入的数据服务器验证)
        /// </summary>
        /// <param name="addList">新增列表</param>
        /// <param name="updateList">修改列表</param>
        /// <param name="deleteList">删除列表</param>
        private void ViewModelHandle(List<MenuViewModel> addList, List<MenuViewModel> updateList, List<MenuViewModel> deleteList)
        {
            //1.过滤无效数据
            CommonHelper.FilterList(addList, deleteList);
            CommonHelper.FilterList(updateList, deleteList);
            //2.服务端视图实体数据验证(即用户输入的数据服务器验证)
            foreach (var addModel in addList)
            {
                addModel.Validate();
            }
            foreach (var updateModel in updateList)
            {
                updateModel.Validate();
            }
        }
        #endregion

        #region 保存实体前的验证操作
        private List<BeiDreamMenu> AddEntitysHandle(List<MenuViewModel> addList)
        {
            var addEntityList = DtosToEntitys(addList);
            foreach (var addEntity in addEntityList)
            {
                addEntity.AddInit();
                ValidateAddCodeRepeatAndTextRepeat(addEntity);
            }
            return addEntityList;
        }
        /// <summary>
        /// 验证编码和名称重复问题
        /// </summary>
        private void ValidateAddCodeRepeatAndTextRepeat(BeiDreamMenu beiDreamMenu)
        {
            Sql sql = new Sql();
            sql.Where("Code=@Code", new { beiDreamMenu.Code });
            List<BeiDreamMenu> menus = this.FindByQuery(sql);
            if (menus == null || menus.Count == 0)
            {
                Sql sqlText = new Sql();
                sqlText.Where("Text=@Text", new { beiDreamMenu.Text });
                List<BeiDreamMenu> menusText = this.FindByQuery(sqlText);
                if (menusText == null || menusText.Count == 0)
                {
                    return;
                }
                else
                    throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "名称"));
            }
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        private List<BeiDreamMenu> UpdateEntitysHandle(List<MenuViewModel> updateList)
        {
            var updateEntityList = DtosToEntitys(updateList);
            foreach (var updateEntity in updateEntityList)
            {
                updateEntity.UpdateInit();
                var oldEntity = UnitOfWork.SingleOrDefault<BeiDreamMenu>(updateEntity.Id);
                if (!CommonHelper.ValidateVersion(updateEntity, oldEntity))
                    throw new ConcurrencyException();

                ValidateUpdateCodeRepeatAndTextRepeat(updateEntity);
            }
            return updateEntityList;
        }
        /// <summary>
        /// 验证编码或菜单名称重复问题
        /// </summary>
        private void ValidateUpdateCodeRepeatAndTextRepeat(BeiDreamMenu beiDreamMenu)
        {
            Sql sqlCode = new Sql();
            sqlCode.Where("Code=@Code and Id<>@Id", new { Code = beiDreamMenu.Code, Id = beiDreamMenu.Id });
            List<BeiDreamMenu> menusCode = this.FindByQuery(sqlCode);
            if (menusCode == null || menusCode.Count == 0)
            {
                Sql sql = new Sql();
                sql.Where("Text=@Text and Id<>@Id", new { Text = beiDreamMenu.Text, Id = beiDreamMenu.Id });
                List<BeiDreamMenu> menus = this.FindByQuery(sql);
                if (menus == null || menus.Count == 0)
                    return;
                else
                    throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "名称"));
            }
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        private List<BeiDreamMenu> DeleteEntitysHandle(List<MenuViewModel> deleteList)
        {
            var deleteEntityList = DtosToEntitys(deleteList);
            return deleteEntityList;
        }

        private List<BeiDreamMenu> DtosToEntitys(List<MenuViewModel> dtoList)
        {
            var entityList = dtoList.Select(ToEntity).Distinct().ToList();
            foreach (var entity in entityList)
            {
                entity.Validate();
            }
            return entityList;
        }
        #endregion

        private void SaveData(List<BeiDreamMenu> addList, List<BeiDreamMenu> updateList,
    List<BeiDreamMenu> deleteList)
        {
            UnitOfWork.Start();
            Add(addList);
            foreach (var updateEntity in updateList)
            {
                Update(updateEntity);
            }
            foreach (var deleteEntity in deleteList)
            {
                Remove(deleteEntity);
                List<BeiDreamMenu> childrenParameters = this.GetAllChildrenParameterByPath(deleteEntity.Id, "Id");
                if (childrenParameters.Count != 0)
                {
                    Remove(childrenParameters);
                }
            }
            UnitOfWork.Commit();
        }
        /// <summary>
        /// 创建新行，供前台调用
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public MenuViewModel CreatNew(string parentId)
        {
            var beiDreamMenu = GetMaxSortIdByParentId(parentId.ToGuidOrNull());
            MenuViewModel newDto = new MenuViewModel
            {
                ParentId = parentId,
                SortId = beiDreamMenu==null?0:beiDreamMenu.SortId+1,
                Id = Guid.NewGuid().ToString(),
                Enabled = true
            };
            return newDto;
        }

        #region 数据传输对象Dto和实体Enitiy相互转化的方法
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        private BeiDreamMenu ToEntity(MenuViewModel dto)
        {
            BeiDreamMenu model = UnitOfWork.SingleOrDefault<BeiDreamMenu>(dto.Id.ToGuid());
            return dto.ToEntity(model ?? new BeiDreamMenu());
        }
        #endregion
    }
}
