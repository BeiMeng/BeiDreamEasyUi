using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;

namespace BeiDream.PetaPoco.Models
{
    [TableName("Icons")]
    [PrimaryKey("Id", autoIncrement = false)]
    [ExplicitColumns]
    public partial class Icons : EntityBase<Guid>
    {
        /// <summary>
        /// 图标名称
        /// </summary>
        [Column]
        public string Name { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        [Column]
        public string Path { get; set; }
        /// <summary>
        /// 图标类名
        /// </summary>
        [Column]
        public string ClassName { get; set; }
        /// <summary>
        /// Css代码
        /// </summary>
        [Column]
        public string Css { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        [Column]
        public int Size { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [Column]
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [Column]
        public int Height { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [ResultColumn]
        public byte[] Version { get; set; }
    }
}
