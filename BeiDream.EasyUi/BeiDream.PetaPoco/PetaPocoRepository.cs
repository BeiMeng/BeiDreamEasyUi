﻿using System;
using System.Collections.Generic;
using BeiDream.Common;

namespace BeiDream.PetaPoco {
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class PetaPocoRepository<TEntity, TKey> : IRepository<TEntity,TKey> where TEntity : class {
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

        ///// <summary>
        ///// 数据库连接
        ///// </summary>
        //protected IDbConnection Connection {
        //    get { return UnitOfWork.Database.Connection; }
        //}

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add( TEntity entity ) {
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
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindAll()
        {
            return UnitOfWork.Fetch<TEntity>("");
        }

        public List<TEntity> Find(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }
    }
}
