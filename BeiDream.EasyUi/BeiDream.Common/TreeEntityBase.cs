using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common.PetaPoco;

namespace BeiDream.Common
{
    public class TreeEntityBase<TKey,TParentId>:EntityBase<TKey>
    {
        [Column]
        public TParentId ParentId { get; set; }
        [Column]
        public string Path { get; set; }
        [Column]
        public int Level { get; set; }
        [Column]
        public int SortId { get; set; }
    }
}
