using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.Systems.Dtos;

namespace BeiDream.Services.Systems.IServices
{
    public interface IIconRepositiory : IRepository<Icons, Guid>
    {
        void UpLoadAndAddIcon(string uploadIconPath, string cssPath);
        void DeleteIconsAndDeleteCss(List<Guid> ids,string cssPath);

        List<IconViewModel> GetAll();

        List<IconViewModel> GetAllByQuery(int width,int height);
    }
}
