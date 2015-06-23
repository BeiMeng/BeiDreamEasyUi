using System.Collections.Generic;
using System.Linq;
using BeiDream.Common;
using BeiDream.PetaPoco;

namespace BeiDream.Services.ServiceHelper
{
    /// <summary>
    /// 修正增改数据的Path和level
    /// </summary>
    public class TreeServiceHelper<TEntity, TKey, TParentId> where TEntity : TreeEntityBase<TKey, TParentId>
    {
        #region 字段

        private readonly string _primaryKeyName;
        /// <summary>
        /// 需要更新路径的实体列表
        /// </summary>
        private readonly List<TEntity> _pathChangeList;

        /// <summary>
        /// 已更新路径Id列表
        /// </summary>
        private readonly List<TKey> _updatedPathIds;

        #endregion
        /// <summary>
        /// 新增列表
        /// </summary>
        protected List<TEntity> AddList;
        /// <summary>
        /// 修改列表
        /// </summary>
        protected List<TEntity> UpdateList;

        protected PetaPocoUnitOfWork UnitOfWork;

        public TreeServiceHelper(List<TEntity> addList, List<TEntity> updateList, PetaPocoUnitOfWork unitOfWork,string primaryKeyName)
        {
            this.AddList = addList;
            this.UpdateList = updateList;
            this.UnitOfWork = unitOfWork;
            this._primaryKeyName = primaryKeyName;
            _pathChangeList = new List<TEntity>();
            _updatedPathIds = new List<TKey>();
            var parentChangeList = GetParentChanges();
            GetPathChangeList(parentChangeList);
            InitPath();
        }
        /// <summary>
        /// 获取父节点被修改的集合
        /// </summary>
        private List<TEntity> GetParentChanges()
        {
            var result = new List<TEntity>();
            UpdateList.ForEach(t =>
            {
                var entity = UnitOfWork.SingleOrDefault<TEntity>(t.Id);
                if (entity == null)
                    return;
                if (t.ParentId.Equals(entity.ParentId))
                    return;
                result.Add(t);
            });
            FilterByPath(result);
            return result;
        }

        /// <summary>
        /// 根据路径过滤，仅保留最顶级节点
        /// </summary>
        protected void FilterByPath(List<TEntity> entities)
        {
            entities.Select(t => t.Path)
                .ToList()
                .ForEach(path => entities.RemoveAll(t => t.Path.StartsWith(path) && t.Path != path));
        }

        /// <summary>
        /// 初始化需要更新路径的实体列表
        /// </summary>
        private void GetPathChangeList(List<TEntity> parentChangeList)
        {
            AddPathChangeList(parentChangeList);
            AddPathChangeList(AddList);
            foreach (var parent in parentChangeList)
            {
                AddPathChangeList(UpdateList.Where(t => t.Path.StartsWith(parent.Path)).ToList());
                AddPathChangeList(GetAllChilds(parent));
            }
        }
        /// <summary>
        /// 获取全部下级实体
        /// </summary>
        /// <param name="parent">父实体</param>
        protected virtual List<TEntity> GetAllChilds(TEntity parent)
        {
            Sql sql = new Sql();
            sql.Where("Path like @0", parent.Path + "%");
            sql.Where(this._primaryKeyName+" <>@0", parent.Id);
            sql.OrderBy("SortId ASC");   //默认ASC升序，降序为DESC
            List<TEntity> list = UnitOfWork.Fetch<TEntity>(sql);
            return list.Where(t => !t.Id.Equals(parent.Id)).ToList();
        }
        /// <summary>
        /// 添加需要更新路径的实体列表
        /// </summary>
        private void AddPathChangeList(List<TEntity> list)
        {
            list.ForEach(entity =>
            {
                if (_pathChangeList.Any(t => t.Id.Equals(entity.Id)))
                    return;
                _pathChangeList.Add(entity);
            });
        }
        /// <summary>
        /// 初始化路径
        /// </summary>
        private void InitPath()
        {
            _pathChangeList.ForEach(InitPath);
        }

        /// <summary>
        /// 初始化路径
        /// </summary>
        private void InitPath(TEntity entity)
        {
            if (entity == null)
                return;
            if (_updatedPathIds.Contains(entity.Id))
                return;
            UpdateParentPath(entity);
            InitEntityPath(entity);
        }

        //更新父节点路径
        private void UpdateParentPath(TEntity entity)
        {
            if (Equals(entity.ParentId, null))
                return;
            var parent = _pathChangeList.Find(t => t.Id.Equals(entity.ParentId));
            InitPath(parent);
        }

        /// <summary>
        /// 初始化实体路径
        /// </summary>
        private void InitEntityPath(TEntity entity)
        {
            var aa = GetParent(entity);
            EntityInitPath(GetParent(entity), entity);
            _updatedPathIds.Add(entity.Id);
        }
        /// <summary>
        /// 初始化路径
        /// </summary>
        /// <param name="parent">父对象</param>
        public void EntityInitPath(TEntity parent, TEntity me)
        {
            if (Equals(parent, null))
                InitFirstLevel(me);
            else
                InitChild(parent, me);
        }
        /// <summary>
        /// 初始化1级节点
        /// </summary>
        private void InitFirstLevel(TEntity me)
        {
            me.ParentId = default(TParentId);   //一级节点前台会自动生成"0000-0000-0000-00000000"
            me.Level = 1;
            me.Path = string.Format("{0},", me.Id);
        }

        /// <summary>
        /// 初始化下级节点
        /// </summary>
        private void InitChild(TEntity parent, TEntity me)
        {
            me.Level = parent.Level + 1;
            me.Path = string.Format("{0}{1},", parent.Path, me.Id);
        }
        /// <summary>
        /// 获取父节点
        /// </summary>
        private TEntity GetParent(TEntity entity)
        {
            var result = _pathChangeList.Find(t => t.Id.Equals(entity.ParentId));
            //if (result == null)
            //    return UnitOfWork.SingleOrDefault<TEntity>(entity.ParentId);
            //return result;           
            return result ?? UnitOfWork.SingleOrDefault<TEntity>(entity.ParentId);
        }
    }
}
