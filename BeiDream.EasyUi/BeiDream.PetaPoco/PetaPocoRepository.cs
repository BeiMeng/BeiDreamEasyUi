using System;
using System.Collections.Generic;
using BeiDream.Common;
using BeiDream.Common.Page;

namespace BeiDream.PetaPoco {
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class PetaPocoRepository<TEntity, TKey> : IPetaPocoRepositiory<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 初始化仓储
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        protected PetaPocoRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = (PetaPocoUnitOfWork)unitOfWork;
        }

        /// <summary>
        /// Ef工作单元
        /// </summary>
        protected PetaPocoUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Add(TEntity entity)
        {
            UnitOfWork.Insert( entity );
            UnitOfWork.CommitByStart();
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        public void Add( IEnumerable<TEntity> entities ) {
            if ( entities == null )
                return;
            foreach (var item in entities)
            {
                Add(item);
            }
            UnitOfWork.CommitByStart();
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Update( TEntity entity ) {
            UnitOfWork.Update(entity);
            UnitOfWork.CommitByStart();
        }
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        public void Remove( TKey id ) {
            UnitOfWork.Delete<TEntity>(id);
            UnitOfWork.CommitByStart();
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Remove( TEntity entity ) {
            UnitOfWork.Delete(entity);
            UnitOfWork.CommitByStart();
        }

        /// <summary>
        /// 获取工作单元
        /// </summary>
        public IUnitOfWork GetUnitOfWork() {
            return UnitOfWork;
        }


        public void Remove(IEnumerable<TKey> ids)
        {
            if (ids == null)
                return;
            foreach (var id in ids)
            {
                Remove(id);
            }
            UnitOfWork.CommitByStart();
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return;
            foreach (var entity in entities)
            {
                Remove(entity);
            }
            UnitOfWork.CommitByStart();
        }

        public List<TEntity> FindAll()
        {
            return UnitOfWork.Fetch<TEntity>("");
        }

        public List<TEntity> Find(IEnumerable<TKey> ids,string primaryKeyName)
        {
            Sql sql = new Sql();
            foreach (var id in ids)
            {
                sql.WhereOR(primaryKeyName+"=@0", id);
            }
            List<TEntity> entities = UnitOfWork.Fetch<TEntity>(sql);
            return entities;
        }


        public virtual PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageDate = UnitOfWork.Page<dynamic>(pageIndex, pageSize, sql, args);
            return new PagedList<dynamic>(pageDate.Items, pageIndex, pageSize, (int)pageDate.TotalItems);
        }

        public virtual PagedList<TEntity> PagedList(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = UnitOfWork.Page<TEntity>(pageIndex, pageSize, sql, args);
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, (int)pageData.TotalItems);
        }

        public virtual PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, string sql, params object[] args)
        {
            var pageData = UnitOfWork.Page<TDto>(pageIndex, pageSize, sql, args);
            return new PagedList<TDto>(pageData.Items, pageIndex, pageSize, (int)pageData.TotalItems);
        }

        public List<TEntity> FindByQuery(Sql sql)
        {
            return UnitOfWork.Fetch<TEntity>(sql);
        }


        public PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, Sql sql)
        {
            var pageDate = UnitOfWork.Page<dynamic>(pageIndex, pageSize, sql);
            return new PagedList<dynamic>(pageDate.Items, pageIndex, pageSize, (int)pageDate.TotalItems);
        }

        public PagedList<TEntity> PagedList(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = UnitOfWork.Page<TEntity>(pageIndex, pageSize, sql);
            return new PagedList<TEntity>(pageData.Items, pageIndex, pageSize, (int)pageData.TotalItems);
        }

        public PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = UnitOfWork.Page<TDto>(pageIndex, pageSize, sql);
            return new PagedList<TDto>(pageData.Items, pageIndex, pageSize, (int)pageData.TotalItems);
        }
    }
}
