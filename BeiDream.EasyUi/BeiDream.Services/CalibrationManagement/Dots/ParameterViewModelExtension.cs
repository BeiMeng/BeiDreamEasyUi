using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.PetaPoco.CalibrationManagementModel;
using BeiDream.Services.Systems.Dtos;
using Util;

namespace BeiDream.Services.CalibrationManagement.Dots
{
    public static class ParameterViewModelExtension
    {
        public static ParameterViewModel ToDto(this Parameter entity)
        {
            ParameterViewModel menuViewModel = new ParameterViewModel
            {
                Id = entity.Id.ToString(),
                ParentId = entity.ParentId.ToString(),
                Text = entity.Name,
                Path = entity.Path,
                Level = entity.Level,
                SortId = entity.SortId,
                Enabled = entity.Enabled,
                Description=entity.Description,
                CreateTime = entity.CreateTime,
                Version = entity.Version
            };
            return menuViewModel;
        }
        public static Parameter ToEntity(this ParameterViewModel dto, Parameter parameterModel)
        {
            parameterModel.Id = dto.Id.ToGuid();
            parameterModel.ParentId = dto.ParentId.ToGuid();
            parameterModel.Name = dto.Text;
            parameterModel.Path = dto.Path;
            parameterModel.Level = dto.Level;
            parameterModel.SortId = dto.SortId;
            parameterModel.Enabled = dto.Enabled;
            parameterModel.Description = dto.Description;
            parameterModel.CreateTime = dto.CreateTime;
            parameterModel.Version = dto.Version;
            parameterModel.Init();
            return parameterModel;
        }
    }
}
