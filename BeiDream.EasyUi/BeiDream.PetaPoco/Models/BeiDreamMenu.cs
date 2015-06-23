using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using Util;

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
        public string UpdatePerson { get; set; }
        [Column]
        public DateTime? UpdateTime { get; set; }
        [Column]
        public override DateTime? CreateTime { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [ResultColumn]
        public override byte[] Version { get; set; }
        /// <summary>
        /// 实体本身的初始化操作
        /// </summary>
        public void Init()
        {
            PinYin = Str.PinYin(Text);
        }
        /// <summary>
        /// 新增初始化操作
        /// </summary>
        public void AddInit()
        {
            this.Init();
            if (CreateTime == null)
                CreateTime = DateTime.Now;
            CreatePerson = "BeiDream";
        }
        /// <summary>
        /// 更新初始化操作
        /// </summary>
        public void UpdateInit()
        {
            this.Init();
            if (UpdateTime == null)
                UpdateTime = DateTime.Now;
            UpdatePerson = "BeiDream";
        }
    }
}
