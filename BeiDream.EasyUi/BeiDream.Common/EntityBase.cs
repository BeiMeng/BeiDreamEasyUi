using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common.PetaPoco;

namespace BeiDream.Common
{
    public class EntityBase<TKey> : DomainBase
    {
        public virtual TKey Id { get; set; }

        public virtual DateTime? CreateTime { get; set; }

        public virtual byte[] Version { get; set; }

    }
}
