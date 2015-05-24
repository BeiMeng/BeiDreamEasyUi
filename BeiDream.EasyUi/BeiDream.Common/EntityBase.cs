using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common.PetaPoco;

namespace BeiDream.Common
{
    public class EntityBase<TKey>
    {
        [Column]
        public TKey Id { get; set; }
    }
}
