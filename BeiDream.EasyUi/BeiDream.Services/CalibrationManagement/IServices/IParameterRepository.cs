using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using BeiDream.PetaPoco;
using BeiDream.PetaPoco.CalibrationManagementModel;
using BeiDream.PetaPoco.Models;
using BeiDream.Services.CalibrationManagement.Dots;

namespace BeiDream.Services.CalibrationManagement.IServices
{
    public interface IParameterRepository : IPetaPocoRepositiory<Parameter, Guid>, ITreePetaPocoRepositiory<Parameter,Guid?>
    {
        ParameterViewModel CreatNew(string parentId);
        List<ParameterViewModel> Save(List<ParameterViewModel> addList, List<ParameterViewModel> updateList,
    List<ParameterViewModel> deleteList);
    }
}
