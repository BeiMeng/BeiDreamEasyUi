using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using BeiDream.Common.Page;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;

namespace BeiDream.Services.Systems.IServices
{
    public interface IIconRepositiory : IRepositiory<Icons, Guid>
    {
        void UpLoadAndAddIcon(string uploadIconPath, string cssPath);
        void DeleteIconsAndDeleteCss(List<Guid> ids,string cssPath);

        List<IconViewModel> GetAll();

        List<IconViewModel> GetAllByQuery(Sql sql);

        PagedList<IconViewModel> PagedLists(int pageIndex, int pageSize,Sql sql);
    }
}
