using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using BeiDream.Common.Page;

namespace BeiDream.PetaPoco
{
    /// <summary>
    /// PetaPoco的特殊仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IPetaPocoRepositiory<TEntity, in TKey> : IRepositiory<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 查找实体集合
        /// </summary>
        List<TEntity> FindByQuery(Sql sql);
        PagedList<dynamic> DynamicPagedList(int pageIndex, int pageSize, Sql sql);
        PagedList<TEntity> PagedList(int pageIndex, int pageSize, Sql sql);

        PagedList<TDto> PagedList<TDto>(int pageIndex, int pageSize, Sql sql);
    }
}
