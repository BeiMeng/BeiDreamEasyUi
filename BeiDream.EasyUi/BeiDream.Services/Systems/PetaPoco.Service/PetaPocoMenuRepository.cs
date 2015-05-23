using System;
using System.Collections.Generic;
using System.Linq;
using BeiDream.Common;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Dtos.Systems;
using BeiDream.Services.IServices;
using Util;
using Util.Exceptions;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.PetaPoco.Service
{
    public class PetaPocoMenuRepository : PetaPocoRepository<BeiDreamMenu, Guid>, IMenuRepository
    {
        /// 初始化仓储
        /// <param name="unitOfWork">工作单元</param>
        public PetaPocoMenuRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public List<MenuViewModel> GetNavigationMenu()
        {
            
            Sql sql = new Sql();
            sql.Where("Level=@0", 1);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }

        public List<MenuViewModel> GetAllChildrenMenuByPath(string parentId)
        {
            List<MenuViewModel> treeNodes = new List<MenuViewModel>();
            Sql sql = new Sql();
            sql.Where("Path like @0", parentId + "%");
            sql.Where("MenuId <>@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            foreach (var menu in menus)
            {
                MenuViewModel treeNode = new MenuViewModel
                {
                    Id = menu.MenuId.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = parentId
                };
                if (menu.Url != null)
                    treeNode.Attributes = new { url = menu.Url };
                treeNodes.Add(menu.ToDto());
            }
            return treeNodes;
        }
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过查找物理路径方式)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ITreeNode> GetNavigationMenuTreeByPath(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("Path like @0", parentId + "%");
            sql.Where("MenuId <>@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode
                {
                    Id = menu.MenuId.ToString(),
                    Text = menu.Text,
                    IconClass = menu.IconClass,
                    ParentId = menu.ParentId.ToString()
                };
                if (menu.Url != null)
                    treeNode.Attributes = new { url = menu.Url };
                treeNodes.Add(treeNode);
            }
            return treeNodes;
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
        /// <summary>
        /// 根据父ID找到其下的导航菜单子节点(只是子节点)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<ITreeNode> GetNavigationMenuChildrenNodes(string parentId)
        {
            List<ITreeNode> treeNodes = new List<ITreeNode>();
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            foreach (var menu in menus)
            {
                TreeNode treeNode = new TreeNode
                {
                    Id = menu.MenuId.ToString(),
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
        /// 根据父ID找到其下的菜单管理子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<MenuViewModel> GetMenuManageChildrenNodes(string parentId)
        {
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }
        /// <summary>
        /// 获取所有的菜单管理树节点
        /// </summary>
        /// <returns></returns>
        public List<MenuViewModel> GetAllTreeNodes()
        {
            Sql sql = new Sql();
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            return menus.Select(menu => menu.ToDto()).ToList();
        }


        #region 新增的一系列操作
        private void AddBefore(MenuViewModel dto)
        {
            BeiDreamMenu beiDreamMenu = dto.ToEntity();
            ValidateAddCodeRepeatAndTextRepeat(beiDreamMenu);
        }
        /// <summary>
        /// 验证编码重复问题
        /// </summary>
        private void ValidateAddCodeRepeatAndTextRepeat(BeiDreamMenu beiDreamMenu)
        {
            Sql sql = new Sql();
            sql.Where("Code=@Code  or Text=@Text", new { beiDreamMenu.Code, beiDreamMenu.Text });
            List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            if (menus == null || menus.Count == 0)
                return;
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        /// <summary>
        /// 新增初始化
        /// </summary>
        /// <param name="addModel"></param>
        private void AddInit(BeiDreamMenu addModel)
        {
            //FixPathAndLevel(addModel);
            addModel.CreatePerson = "BeiDrem";
            addModel.CreateTime = DateTime.Now;
            addModel.PinYin = Str.PinYin(addModel.Text);
        }
        public void Add(BeiDreamMenu beiDreamMenu)
        {
            AddInit(beiDreamMenu);
            UnitOfWork.Insert(beiDreamMenu);
        }
        #endregion

        public void Delete(BeiDreamMenu beiDreamMenu)
        {
            UnitOfWork.Delete(beiDreamMenu);
        }
        #region 更新的一系列操作
        private void UpdateBefore(MenuViewModel dto)
        {
            BeiDreamMenu beiDreamMenu = dto.ToEntity();
            ValidateUpdateCodeRepeatAndTextRepeat(beiDreamMenu);
            if (!ValidateVersion(beiDreamMenu))
                throw new ConcurrencyException();
        }
        /// <summary>
        /// 验证编码或菜单名称重复问题
        /// </summary>
        private void ValidateUpdateCodeRepeatAndTextRepeat(BeiDreamMenu beiDreamMenu)
        {
            Sql sqlCode = new Sql();
            sqlCode.Where("Code=@Code and MenuId<>@MenuId", new { Code = beiDreamMenu.Code, MenuId = beiDreamMenu.MenuId });
            List<BeiDreamMenu> menusCode = UnitOfWork.Fetch<BeiDreamMenu>(sqlCode);
            if (menusCode == null || menusCode.Count == 0)
            {
                Sql sql = new Sql();
                sql.Where("Text=@Text and MenuId<>@MenuId", new { Text = beiDreamMenu.Text, MenuId = beiDreamMenu.MenuId });
                List<BeiDreamMenu> menus = UnitOfWork.Fetch<BeiDreamMenu>(sql);
                if (menus == null || menus.Count == 0)
                    return;
                else
                    throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "名称"));
            }
            else
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "编码"));
        }
        /// <summary>
        /// 更新初始化
        /// </summary>
        /// <param name="updateModel"></param>
        private void UpdateInit(BeiDreamMenu updateModel)
        {
            //FixPathAndLevel(updateModel);
            updateModel.UpdatePerson = "BeiDrem";
            updateModel.UpdateTime = DateTime.Now;
            updateModel.PinYin = Str.PinYin(updateModel.Text);
        }
        public override void Update(BeiDreamMenu beiDreamMenu)
        {
            UpdateInit(beiDreamMenu);
            UnitOfWork.Update(beiDreamMenu);
        }
        /// <summary>
        /// 版本号,乐观离线锁通过为每行数据添加一个版本号来识别当前数据的版本，在获取数据时将版本号保存下来，
        /// 更新数据时将版本号作为Where中的过滤条件，如果该记录被更新，则版本号会发生变化，所以导致更新数据时影响行数为0，
        /// 通过引发一个并发更新异常让你了解数据已经被别人更新。
        /// </summary>
        //验证版本号
        private bool ValidateVersion(BeiDreamMenu newBeiDreamMenu)
        {
            BeiDreamMenu oldBeiDreamMenu =
                UnitOfWork.SingleOrDefault<BeiDreamMenu>(newBeiDreamMenu.MenuId);
            if (newBeiDreamMenu.Version == null)
                return false;
            for (int i = 0; i < oldBeiDreamMenu.Version.Length; i++)
                if (newBeiDreamMenu.Version[i] != oldBeiDreamMenu.Version[i])
                    return false;
            return true;
        }
        #endregion
        /// <summary>
        /// 获取所有前台增删改的数据id
        /// </summary>
        /// <param name="addList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        private List<string> GetIds(List<BeiDreamMenu> addList, List<BeiDreamMenu> updateList, List<BeiDreamMenu> deleteList)
        {
            List<string> strList = addList.Select(addModel => addModel.MenuId.ToString()).ToList();
            strList.AddRange(updateList.Select(updateModel => updateModel.MenuId.ToString()));
            strList.AddRange(deleteList.Select(deleteModel => deleteModel.MenuId.ToString()));
            return strList;
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
            SaveBefore(addList, updateList, deleteList);
            var _addList = addList.Select(ToEntity).Distinct().ToList();
            var _updateList = updateList.Select(ToEntity).Distinct().ToList();
            var _deleteList = deleteList.Select(ToEntity).Distinct().ToList();
            List<string> ids = GetIds(_addList, _updateList, _deleteList);
            //修正增改数据的Path和level
            //new TreeServiceHelper(_addList, _updateList);
            #region 保存操作数据到数据库的事,物
            UnitOfWork.Start();
            try
            {
                foreach (var addModel in _addList)
                {
                    AddInit(addModel);
                    UnitOfWork.Insert(addModel);
                }
                foreach (var updateModel in _updateList)
                {
                    UpdateInit(updateModel);

                    UnitOfWork.Update(updateModel);
                }
                foreach (var deleteModel in _deleteList)
                {
                    UnitOfWork.Delete<BeiDreamMenu>(deleteModel.MenuId);
                    List<MenuViewModel> childrenMenus = this.GetAllChildrenMenuByPath(deleteModel.MenuId.ToString());
                    if (childrenMenus == null || childrenMenus.Count == 0)
                        continue;
                    else
                    {
                        foreach (var childrenMenu in childrenMenus)
                        {
                            UnitOfWork.Delete<BeiDreamMenu>((object)childrenMenu.Id);
                        }
                    }
                }
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.AbortTransaction();
                throw new Warning(ex);
            }
            #endregion
            Sql sql = new Sql();
            foreach (var id in ids)
            {
                sql.WhereOR("MenuId=@0", id);
            }
            List<BeiDreamMenu> returnList = UnitOfWork.Fetch<BeiDreamMenu>(sql);
            return returnList.Select(ToDto).ToList();
        }
        /// <summary>
        /// 保存前操作
        /// </summary>
        /// <param name="addList">新增列表</param>
        /// <param name="updateList">修改列表</param>
        /// <param name="deleteList">删除列表</param>
        private void SaveBefore(List<MenuViewModel> addList, List<MenuViewModel> updateList, List<MenuViewModel> deleteList)
        {
            FilterList(addList, deleteList);
            FilterList(updateList, deleteList);
            addList.ForEach(AddBefore);
            updateList.ForEach(UpdateBefore);
        }
        /// <summary>
        /// 过滤无效数据
        /// </summary>
        private void FilterList(List<MenuViewModel> list, IEnumerable<MenuViewModel> deleteList)
        {
            list.Select(t => t.Id).ToList().ForEach(id =>
            {
                if (deleteList.Any(d => d.Id == id))
                    list.Remove(list.Find(t => t.Id == id));
            });
        }

        /// <summary>
        /// 创建新行，供前台调用
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public MenuViewModel CreatNew(string parentId)
        {
            MenuViewModel newDto = new MenuViewModel
            {
                ParentId = parentId,
                SortId = GetSortId(parentId),
                Id = Guid.NewGuid().ToString(),
                Enabled = true
            };
            return newDto;
        }
        /// <summary>
        /// 获取新增行的此层级的排序id
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private int GetSortId(string parentId)
        {
            Sql sql = new Sql();
            sql.Where(string.IsNullOrEmpty(parentId) ? "ParentId is null" : "ParentId=@0", parentId);
            sql.OrderBy("SortId DESC");   //默认ASC升序，降序为DESC
            BeiDreamMenu beiDreamMenu = UnitOfWork.FirstOrDefault<BeiDreamMenu>(sql);
            if (beiDreamMenu == null)
                return 0;
            return beiDreamMenu.SortId + 1;
        }

        #region 数据传输对象Dto和实体Enitiy相互转化的方法
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        private BeiDreamMenu ToEntity(MenuViewModel dto)
        {
            return dto.ToEntity();
        }
        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">数据传输对象</param>
        private MenuViewModel ToDto(BeiDreamMenu entity)
        {
            return entity.ToDto();
        }
        #endregion
    }
}
