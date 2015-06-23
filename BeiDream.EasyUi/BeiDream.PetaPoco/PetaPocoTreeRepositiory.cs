using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;

namespace BeiDream.PetaPoco
{
    /// <summary>
    /// 树形结构表仓储,实现通用的树形结构表的操作方法
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    /// <typeparam name="TPartenId"></typeparam>
    public abstract class PetaPocoTreeRepositiory<TEntity, TKey, TPartenId> : PetaPocoRepository<TEntity, TKey>, ITreePetaPocoRepositiory<TEntity,TPartenId> where TEntity : class 
    {

        protected PetaPocoTreeRepositiory(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        /// <summary>
        /// 获取所有顶级数据，即Level=1或ParentId=null的数据
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllTopLevel()
        {
            Sql sql = new Sql();
            sql.Where("Level=@0", 1);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            return UnitOfWork.Fetch<TEntity>(sql);
        }

        /// <summary>
        /// 根据父ID，仅获取他的子数据，子数据下的不获取
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<TEntity> GetChidrenLevel(TPartenId parentId)
        {
            Sql sql = new Sql();
            sql.Where("ParentId=@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            return this.FindByQuery(sql);
        }

        /// <summary>
        /// 获取父节点下的子节点最大的排序号的实体
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        public TEntity GetMaxSortIdByParentId(TPartenId parentId)
        {
            Sql sql = new Sql();
            sql.Where(parentId==null ? "ParentId is null" : "ParentId=@0", parentId);
            sql.OrderBy("SortId DESC");   //默认ASC升序，降序为DESC
            TEntity entity = UnitOfWork.FirstOrDefault<TEntity>(sql);
            return entity;
        }


        public List<TEntity> GetAllChildrenParameterByPath(TPartenId parentId, string primaryKeyName)
        {
            Sql sql = new Sql();
            sql.Where("Path like @0", parentId + "%");
            sql.Where(primaryKeyName+" <>@0", parentId);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            return this.FindByQuery(sql);
        }
    }
}
