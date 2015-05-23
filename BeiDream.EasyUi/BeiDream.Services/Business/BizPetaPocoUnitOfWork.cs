using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.PetaPoco;

namespace BeiDream.Services.Business
{
    class BizPetaPocoUnitOfWork : PetaPocoUnitOfWork
    {
        public BizPetaPocoUnitOfWork() 
            : base("Biz")
        {
        }
    }
}
