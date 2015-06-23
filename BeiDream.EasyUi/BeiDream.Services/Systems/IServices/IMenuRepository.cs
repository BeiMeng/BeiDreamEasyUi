using System;
using System.Collections.Generic;
using BeiDream.Common;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.Systems.IServices
{
    public interface IMenuRepository : IPetaPocoRepositiory<BeiDreamMenu, Guid>, ITreePetaPocoRepositiory<BeiDreamMenu,Guid?>
    {
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

        List<MenuViewModel> Save(List<MenuViewModel> addList, List<MenuViewModel> updateList,
            List<MenuViewModel> deleteList);

        MenuViewModel CreatNew(string parentId);
    }
}
