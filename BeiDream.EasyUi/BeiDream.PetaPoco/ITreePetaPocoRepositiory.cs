using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;

namespace BeiDream.PetaPoco
{
    /// <summary>
    /// PetaPoco树形的特殊仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParentId"></typeparam>
    public interface ITreePetaPocoRepositiory<TEntity, in TParentId> : ITreeRepositiory<TEntity,TParentId> where TEntity : class
    {
    }
}
