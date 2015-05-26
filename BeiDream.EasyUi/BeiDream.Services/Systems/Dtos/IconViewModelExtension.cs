using System;
using BeiDream.PetaPoco.Models;
using Util;
using Util.Files;

namespace BeiDream.Services.Systems.Dtos {
    /// <summary>
    /// 图标数据传输对象扩展
    /// </summary>
    public static class IconDtoExtension {
        /// <summary>
        /// 转换为图标实体
        /// </summary>
        /// <param name="dto">图标数据传输对象</param>
        public static Icons ToEntity(this IconViewModel dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 转换为图标数据传输对象
        /// </summary>
        /// <param name="entity">图标实体</param>
        public static IconViewModel ToDto(this Icons entity)
        {
            return new IconViewModel
            {
                Id = entity.Id.ToString(),
                Icon = entity.Path,
                Name = entity.Name,
                Path = entity.Path,
                ClassName = entity.ClassName,
                Size = GetSize(entity.Size),
                Width = entity.Width,
                Height = entity.Height,
                Css = entity.Css,
                CreateTime = entity.CreateTime,
                Version = entity.Version,
            };
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        public static string GetSize(int size)
        {
            return new FileSize(size).ToString();
        }
    }
}
