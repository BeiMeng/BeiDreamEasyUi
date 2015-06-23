using System;
using BeiDream.Common;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.CalibrationManagementModel;
using BeiDream.Services.CalibrationManagement.Dots;
using BeiDream.Services.CalibrationManagement.IServices;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeiDream.Services.ServiceHelper;
using Util;
using Util.Exceptions;

namespace BeiDream.Services.CalibrationManagement.PetaPoco.Service
{
    public class PetaPocoParameterRepository : PetaPocoTreeRepositiory<Parameter, Guid,Guid?>, IParameterRepository
    {
        public PetaPocoParameterRepository(IClmPetaPocoUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        #region 新增行数据服务
        public ParameterViewModel CreatNew(string parentId)
        {
            var parameter = GetMaxSortIdByParentId(parentId.ToGuidOrNull());
            ParameterViewModel newDto = new ParameterViewModel
            {
                ParentId = parentId,
                SortId = parameter == null ? 0 : parameter.SortId + 1,
                Id = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                Enabled = true
            };
            return newDto;
        } 
        #endregion

        #region 保存增删改数据的一系列操作
        public List<ParameterViewModel> Save(List<ParameterViewModel> addList, List<ParameterViewModel> updateList, List<ParameterViewModel> deleteList)
        {
            ViewModelHandle(addList, updateList, deleteList);
            List<Parameter> addEntityList = AddEntitysHandle(addList);
            List<Parameter> updateEntityList = UpdateEntitysHandle(updateList);
            List<Parameter> deleteEntityList = DeleteEntitysHandle(deleteList);
            //修正增改数据的Path和level
            new TreeServiceHelper<Parameter, Guid, Guid?>(addEntityList, updateEntityList, UnitOfWork, "P_ID");

            SaveData(addEntityList, updateEntityList, deleteEntityList);

            //返回被编过的视图数据，供前台使用
            List<string> ids = CommonHelper.GetIds(addEntityList, updateEntityList, deleteEntityList);
            List<Guid> gids = ids.Select(id => id.ToGuid()).ToList();
            var parameterList = Find(gids, "P_ID");
            return parameterList.Select(parameter => parameter.ToDto()).ToList();
        }

        #region 前台视图数据的验证及过滤操作
        /// <summary>
        /// 对前台数据的处理,1.过滤无效数据，2.服务端视图实体数据验证(即用户输入的数据服务器验证)
        /// </summary>
        /// <param name="addList">新增列表</param>
        /// <param name="updateList">修改列表</param>
        /// <param name="deleteList">删除列表</param>
        private void ViewModelHandle(List<ParameterViewModel> addList, List<ParameterViewModel> updateList, List<ParameterViewModel> deleteList)
        {
            //1.过滤无效数据
            CommonHelper.FilterList(addList, deleteList);
            CommonHelper.FilterList(updateList, deleteList);
            //2.服务端视图实体数据验证(即用户输入的数据服务器验证)
            foreach (var addModel in addList)
            {
                addModel.Validate();
            }
            foreach (var updateModel in updateList)
            {
                updateModel.Validate();
            }
        }
        #endregion

        #region 保存实体前的验证操作
        private List<Parameter> AddEntitysHandle(List<ParameterViewModel> addList)
        {
            var addEntityList = DtosToEntitys(addList);
            foreach (var addEntity in addEntityList)
            {
                AddValidateTextRepeat(addEntity);
            }
            return addEntityList;
        }
        /// <summary>
        /// 验证新增数据的名称是否重复
        /// </summary>
        /// <param name="parameter"></param>
        private void AddValidateTextRepeat(Parameter parameter)
        {
            Sql sqlText = new Sql();
            sqlText.Where("Name=@Name", new { parameter.Name });
            List<Parameter> parameters = FindByQuery(sqlText);
            if (parameters.Count != 0)
            {
                throw new Warning(string.Format("参数 '{0}' 已存在，请修改", "名称"));
            }

        }
        private List<Parameter> UpdateEntitysHandle(List<ParameterViewModel> updateList)
        {
            var updateEntityList = DtosToEntitys(updateList);
            foreach (var updateEntity in updateEntityList)
            {
                var oldEntity = UnitOfWork.SingleOrDefault<Parameter>(updateEntity.Id);
                if (!CommonHelper.ValidateVersion(updateEntity, oldEntity))
                    throw new ConcurrencyException();

                ValidateUpdateTextRepeat(updateEntity);
            }
            return updateEntityList;
        }
        /// <summary>
        /// 验证更新数据的名称是否重复
        /// </summary>
        private void ValidateUpdateTextRepeat(Parameter parameter)
        {
            Sql sql = new Sql();
            sql.Where("Name=@Name and P_ID<>@P_ID", new { Name = parameter.Name, P_ID = parameter.Id });
            List<Parameter> parameters = this.FindByQuery(sql);
            if (parameters.Count != 0)
            {
                throw new Warning(string.Format("菜单 '{0}' 已存在，请修改", "名称"));
            }
        }
        private List<Parameter> DeleteEntitysHandle(List<ParameterViewModel> deleteList)
        {
            var deleteEntityList = DtosToEntitys(deleteList);
            return deleteEntityList;
        }

        private List<Parameter> DtosToEntitys(List<ParameterViewModel> dtoList)
        {
            var entityList = dtoList.Select(ToEntity).Distinct().ToList();
            foreach (var entity in entityList)
            {
                entity.Validate();
            }
            return entityList;
        }
        #endregion
        private void SaveData(List<Parameter> addList, List<Parameter> updateList,
            List<Parameter> deleteList)
        {
            UnitOfWork.Start();
            Add(addList);
            foreach (var updateEntity in updateList)
            {
                Update(updateEntity);
            }
            foreach (var deleteEntity in deleteList)
            {
                Remove(deleteEntity);
                List<Parameter> childrenParameters = this.GetAllChildrenParameterByPath(deleteEntity.Id, "P_ID");
                if (childrenParameters.Count != 0)
                {
                    Remove(childrenParameters);
                }
            }
            UnitOfWork.Commit();
        }
        
        #endregion
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        private Parameter ToEntity(ParameterViewModel dto)
        {
            Parameter model = UnitOfWork.SingleOrDefault<Parameter>(dto.Id.ToGuid());
            return dto.ToEntity(model ?? new Parameter());
        }
    }
}
