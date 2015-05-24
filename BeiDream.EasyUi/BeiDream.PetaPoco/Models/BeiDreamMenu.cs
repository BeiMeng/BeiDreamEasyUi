using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;

namespace BeiDream.PetaPoco.Models
{
    [TableName("BeiDreamMenu")]
    [PrimaryKey("Id", autoIncrement = false)]
    [ExplicitColumns]
    public partial class BeiDreamMenu : TreeEntityBase<Guid, Guid?>
    {
        [Column]
        public string Code { get; set; }
        [Column]
        public string Text { get; set; }
        [Column]
        public int SortId { get; set; }
        [Column]
        public string Url { get; set; }
        [Column]
        public string IconClass { get; set; }
        [Column]
        public string PinYin { get; set; }
        [Column]
        public bool Enabled { get; set; }
        [Column]
        public string CreatePerson { get; set; }
        [Column]
        public DateTime? CreateTime { get; set; }
        [Column]
        public string UpdatePerson { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }
        [ResultColumn]
        public byte[] Version { get; set; }
    }
}
