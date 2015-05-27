using System;
using BeiDream.Common.Page;

namespace BeiDream.Services.Systems.Dtos {
    /// <summary>
    /// 图标集合视图实体
    /// </summary>
    public class IconListViewModel {
        /// <summary>
        /// 初始化图标集合视图实体
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="icons">图标分页集合</param>
        public IconListViewModel(int? width, int? height, PagedList<IconViewModel> icons)
        {
            Width = width;
            Height = height;
            Icons = icons;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// 图标分页集合
        /// </summary>
        public PagedList<IconViewModel> Icons { get; set; }
    }
}