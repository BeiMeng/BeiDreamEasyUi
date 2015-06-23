using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Commom;
using BeiDream.Services.Systems.Dtos;
using BeiDream.Services.Systems.IServices;
using Util;

namespace BeiDream.Services.Systems.PetaPoco.Service
{
    public class PetaPocoIconRepository : PetaPocoRepository<Icons, Guid>, IIconRepositiory
    {
        /// 初始化仓储
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="iconManager"></param>
        public PetaPocoIconRepository(ISysPetaPocoUnitOfWork unitOfWork, IIconManager iconManager)
            : base(unitOfWork)
        {
            this.IconManager = iconManager;
        }
        /// <summary>
        /// 图标管理器
        /// </summary>
        protected IIconManager IconManager { get; set; }

        /// <summary>
        /// 上传图标并新增图标信息到图标表
        /// </summary>
        /// <param name="uploadIconPath">上传图标的路径</param>
        /// <param name="cssPath">图标Css的路径</param>
        public void UpLoadAndAddIcon(string uploadIconPath, string cssPath)
        {
            Icons entity = IconManager.Upload(uploadIconPath,cssPath);
            entity.AddInit();
            Add(entity);
        }
        public List<IconViewModel> GetAll()
        {
            List<Icons> icons = FindAll();
            return icons.Select(ToDto).ToList();
        }
        #region 数据传输对象Dto和实体Enitiy相互转化的方法
        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="dto">数据传输对象</param>
        private Icons ToEntity(IconViewModel dto)
        {
            return dto.ToEntity();
        }
        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">数据传输对象</param>
        private IconViewModel ToDto(Icons entity)
        {
            return entity.ToDto();
        }
        #endregion


        public List<IconViewModel> GetAllByQuery(Sql sql)
        {
            List<Icons> icons = UnitOfWork.Fetch<Icons>(sql);
            return icons.Select(ToDto).ToList();
        }

        public PagedList<IconViewModel> PagedLists(int pageIndex, int pageSize,Sql sql)
        {
            var pageData = UnitOfWork.Page<Icons>(pageIndex, pageSize, sql);
            return new PagedList<IconViewModel>(pageData.Items.Select(ToDto).ToList(), pageIndex, pageSize, (int)pageData.TotalItems);
        }

        public void DeleteIconsAndDeleteCss(List<Guid> ids, string cssPath)
        {
            if (ids != null && ids.Count > 0)
            {
                List<Icons> icons = Find(ids,"Id");
                IconManager.Delete(icons, cssPath);
                base.Remove(ids);
            }
            else
            {
                throw new Warning("请选择要删除的图标！");
            }
        }
    }
}
