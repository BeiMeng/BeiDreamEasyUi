using System;
using System.Collections.Generic;
using BeiDream.Common;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.Systems.IServices
{
    public interface IMenuRepository : IRepository<BeiDreamMenu, Guid>
    {
        /// <summary>
        /// 获取导航菜单,也就是第一级（Level=1）菜单
        /// </summary>
        /// <returns></returns>
        List<MenuViewModel> GetNavigationMenu();
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过查找物理路径方式)
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        List<ITreeNode> GetNavigationMenuTreeByPath(string parentId);
        /// <summary>
        /// 根据导航菜单ID获取其下面的菜单树(通过递归查询子节点方式)
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        List<ITreeNode> GetNavigationMenuTreeByChildren(string parentId);

        /// <summary>
        /// 根据父ID找到其下的导航菜单子节点(只是子节点)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<ITreeNode> GetNavigationMenuChildrenNodes(string parentId);
        /// <summary>
        /// 根据父ID找到其下的菜单管理子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<MenuViewModel> GetMenuManageChildrenNodes(string parentId);
        /// <summary>
        /// 获取所有的菜单管理树节点
        /// </summary>
        /// <returns></returns>
        List<MenuViewModel> GetAllTreeNodes();

        List<MenuViewModel> Save(List<MenuViewModel> addList, List<MenuViewModel> updateList,
            List<MenuViewModel> deleteList);

        MenuViewModel CreatNew(string parentId);
    }
}
