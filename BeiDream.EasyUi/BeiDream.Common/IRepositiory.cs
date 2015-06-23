using System.Collections.Generic;
using BeiDream.Common.Page;

namespace BeiDream.Common {
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IRepositiory<TEntity, in TKey> where TEntity : class {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add( TEntity entity );
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体</param>
        void Add( IEnumerable<TEntity> entities );
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update( TEntity entity );
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Remove( TKey id );
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove( TEntity entity );
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        void Remove( IEnumerable<TKey> ids );
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Remove( IEnumerable<TEntity> entities );
        /// <summary>
        /// 查找实体集合
        /// </summary>
        List<TEntity> FindAll();

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">实体标识列表</param>
        /// <param name="primaryKeyName">主键名称</param>
        List<TEntity> Find(IEnumerable<TKey> ids, string primaryKeyName);

        /// <summary>
        /// 动态查询，返回dynamic类型的列表
        /// 请使用标准SQL语句进行查询(SELECT ... FROM ...)
        /// </summary> 
        /// <returns></returns>
        PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, string sql, params object[] args);
        PagedList<TEntity> PagedList(int pageIndex, int pageSize, string sql, params object[] args);

        PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, string sql, params object[] args);
        /// <summary>
        /// 获取工作单元
        /// </summary>
        IUnitOfWork GetUnitOfWork();
    }
}
