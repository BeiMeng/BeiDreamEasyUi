using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeiDream.Services.Systems.Dtos
{
    public class IconViewModel
    {
        /// <summary>
        /// 标识
        /// </summary>
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        [DataMember]
        public string Icon { get; set; }

        /// <summary>
        /// 图标名称
        /// </summary>
        [Required(ErrorMessage = "图标名称不能为空")]
        [StringLength(100, ErrorMessage = "图标名称输入过长，不能超过100位")]
        [Display(Name = "图标名称")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 图标路径
        /// </summary>
        [StringLength(200, ErrorMessage = "图标路径输入过长，不能超过200位")]
        [Display(Name = "图标路径")]
        [DataMember]
        public string Path { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [Required(ErrorMessage = "类名不能为空")]
        [StringLength(100, ErrorMessage = "类名输入过长，不能超过100位")]
        [Display(Name = "类名")]
        [DataMember]
        public string ClassName { get; set; }

        /// <summary>
        /// 图标大小
        /// </summary>
        [Required(ErrorMessage = "图标大小不能为空")]
        [Display(Name = "图标大小")]
        [DataMember]
        public string Size { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [Required(ErrorMessage = "宽度不能为空")]
        [Display(Name = "宽度")]
        [DataMember]
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [Required(ErrorMessage = "高度不能为空")]
        [Display(Name = "高度")]
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// Css代码
        /// </summary>
        [Required(ErrorMessage = "Css代码不能为空")]
        [StringLength(500, ErrorMessage = "Css代码输入过长，不能超过500位")]
        [Display(Name = "Css代码")]
        [DataMember]
        public string Css { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间不能为空")]
        [Display(Name = "创建时间")]
        [DataMember]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Display(Name = "版本号")]
        [DataMember]
        public Byte[] Version { get; set; }
    }
}
