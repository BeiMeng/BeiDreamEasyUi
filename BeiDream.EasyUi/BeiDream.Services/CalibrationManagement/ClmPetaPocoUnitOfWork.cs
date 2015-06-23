using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.PetaPoco;

namespace BeiDream.Services.CalibrationManagement
{
    public class ClmPetaPocoUnitOfWork : PetaPocoUnitOfWork,IClmPetaPocoUnitOfWork
    {
        public ClmPetaPocoUnitOfWork()
            : base("CalibrationManagement")
        {
        }
    }
}
