using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.PetaPoco;

namespace BeiDream.Services.Systems
{
    public class SysPetaPocoUnitOfWork : PetaPocoUnitOfWork
    {
        public SysPetaPocoUnitOfWork()
            : base("Sys")
        {
        }
    }
}
