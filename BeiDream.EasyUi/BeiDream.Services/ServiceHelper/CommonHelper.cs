using System;
using System.Collections.Generic;
using System.Linq;
using BeiDream.Common;

namespace BeiDream.Services.ServiceHelper
{
    public class CommonHelper
    {
        /// <summary>
        /// 过滤无效数据
        /// </summary>
        public static void FilterList<TEntity>(List<TEntity> list, IEnumerable<TEntity> deleteList) where TEntity:IDto
        {
            list.Select(t => t.Id).ToList().ForEach(id =>
            {
                if (deleteList.Any(d => d.Id == id))
                    list.Remove(list.Find(t => t.Id == id));
            });
        }
        /// <summary>
        /// 版本号,乐观离线锁通过为每行数据添加一个版本号来识别当前数据的版本，在获取数据时将版本号保存下来，
        /// 更新数据时将版本号作为Where中的过滤条件，如果该记录被更新，则版本号会发生变化，所以导致更新数据时影响行数为0，
        /// 通过引发一个并发更新异常让你了解数据已经被别人更新。
        /// </summary>
        //验证版本号
        public static bool ValidateVersion<TEntity>(TEntity newEntity, TEntity oldEntity) where TEntity : EntityBase<Guid>
        {
            if (newEntity.Version == null)
                return false;
            for (int i = 0; i < oldEntity.Version.Length; i++)
                if (newEntity.Version[i] != oldEntity.Version[i])
                    return false;
            return true;
        }
        /// <summary>
        /// 获取所有前台增删改的数据id
        /// </summary>
        /// <param name="addList"></param>
        /// <param name="updateList"></param>
        /// <param name="deleteList"></param>
        /// <returns></returns>
        public static List<string> GetIds<TEntity>(List<TEntity> addList, List<TEntity> updateList, List<TEntity> deleteList) where TEntity : EntityBase<Guid>
        {
            List<string> strList = addList.Select(addModel => addModel.Id.ToString()).ToList();
            strList.AddRange(updateList.Select(updateModel => updateModel.Id.ToString()));
            strList.AddRange(deleteList.Select(deleteModel => deleteModel.Id.ToString()));
            return strList;
        }
    }
}
