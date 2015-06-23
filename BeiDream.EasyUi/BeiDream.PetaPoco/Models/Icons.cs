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
        [Column]
        public string CreatePerson { get; set; }
        [Column]
        public override DateTime? CreateTime { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [ResultColumn]
        public override byte[] Version { get; set; }

        public void Init()
        {
            if (CreateTime == null)
                CreateTime = DateTime.Now;
            CreatePerson = "BeiDream";
        }

        public void AddInit()
        {
            Init();
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 生成Css
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void GenerateCss(string filePath)
        {
            ClassName = CreateClassName(filePath);
            Css = CreateCss(filePath);
        }

        /// <summary>
        /// 创建类名
        /// </summary>
        private string CreateClassName(string filePath)
        {
            return string.Format("icon-{0}", System.IO.Path.GetFileNameWithoutExtension(filePath));
        }
        /// <summary>
        /// 创建Css
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static string CreateCss(string filePath)
        {
            var result = new StringBuilder();
            result.AppendFormat(".icon-{0}", System.IO.Path.GetFileNameWithoutExtension(filePath));
            result.Append("{");
            result.AppendFormat("background:url(images/{0}) no-repeat center center;", System.IO.Path.GetFileName(filePath));
            result.Append("}");
            return result.ToString();
        }
    }
}
