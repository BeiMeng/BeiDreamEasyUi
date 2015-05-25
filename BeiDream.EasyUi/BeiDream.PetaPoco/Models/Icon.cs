using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;

namespace BeiDream.PetaPoco.Models
{
    [TableName("Icon")]
    [PrimaryKey("IconId", autoIncrement = false)]
    [ExplicitColumns]
    public partial class Icon : EntityBase<Guid>
    {
        /// <summary>
        /// 图标名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 图标类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// Css代码
        /// </summary>
        public string Css { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
    }
}
