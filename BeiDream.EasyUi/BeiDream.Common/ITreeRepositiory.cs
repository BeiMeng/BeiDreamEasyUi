using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Common
{
    /// <summary>
    /// 通用树形结构表操作
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TParentId">父ID</typeparam>
    public interface ITreeRepositiory<TEntity, in TParentId> where TEntity : class 
    {
        /// <summary>
        /// 获取所有顶级数据，即Level=1或ParentId=null的数据
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllTopLevel();
        /// <summary>
        /// 根据父ID，仅获取他的子数据，子数据下的不获取
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        List<TEntity> GetChidrenLevel(TParentId parentId);

        /// <summary>
        /// 获取父节点下的子节点最大的排序号的实体
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        TEntity GetMaxSortIdByParentId(TParentId parentId);

        /// <summary>
        /// 获取父节点下的所有子孙节点
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="primaryKeyName">主键名称</param>
        /// <returns></returns>
        List<TEntity> GetAllChildrenParameterByPath(TParentId parentId, string primaryKeyName);
    }
}
