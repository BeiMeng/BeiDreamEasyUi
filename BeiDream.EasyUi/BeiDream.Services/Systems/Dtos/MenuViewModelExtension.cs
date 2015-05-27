using System;
using BeiDream.PetaPoco.Models;
using Util;

namespace BeiDream.Services.Systems.Dtos
{
    public static class MenuViewModelExtension
    {
        public static MenuViewModel ToDto(this BeiDreamMenu entity)
        {
            MenuViewModel menuViewModel = new MenuViewModel
            {
                Id=entity.Id.ToString(),
                ParentId=entity.ParentId.ToString(),
                Code=entity.Code,
                Text=entity.Text,
                Path=entity.Path,
                Level=entity.Level,
                SortId=entity.SortId,
                Attributes = entity.Url == null ? null : new { url = entity.Url },
                Url=entity.Url,
                IconClass=entity.IconClass,
                iconCls=entity.IconClass,
                PinYin=entity.PinYin,
                Enabled=entity.Enabled,
                UpdateTime=entity.UpdateTime,
                Version=entity.Version
            };
            return menuViewModel;
        }
        public static BeiDreamMenu ToEntity(this MenuViewModel dto, BeiDreamMenu beiDreamMenuModel)
        {
            beiDreamMenuModel.Id = dto.Id.ToGuid();
            beiDreamMenuModel.ParentId = dto.ParentId.ToGuid();
            beiDreamMenuModel.Code = dto.Code;
            beiDreamMenuModel.Text = dto.Text;
            beiDreamMenuModel.Path = dto.Path;
            beiDreamMenuModel.Level = Conv.ToInt(dto.Level);
            beiDreamMenuModel.SortId = Conv.ToInt(dto.SortId);
            beiDreamMenuModel.Url = dto.Url;
            beiDreamMenuModel.IconClass = dto.IconClass;
            beiDreamMenuModel.PinYin = dto.PinYin;
            beiDreamMenuModel.Enabled = dto.Enabled;
            beiDreamMenuModel.Version = dto.Version;
            return beiDreamMenuModel;
        }
    }
}
