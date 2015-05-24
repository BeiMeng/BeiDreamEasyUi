using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.Systems.Dtos
{
    public class MenuViewModel:ITreeNode
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "编码不能为空")]
        [StringLength(10, ErrorMessage = "编码输入过长，不能超过10位")]
        [Display(Name = "编码")]
        [DataMember]
        public string Code { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "菜单名称不能为空")]
        [StringLength(200, ErrorMessage = "菜单名称输入过长，不能超过50位")]
        [Display(Name = "菜单名称")]
        [DataMember]
        public string Text { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Required(ErrorMessage = "图标不能为空")]
        [Display(Name = "图标")]
        [DataMember]
        public string IconClass { get; set; }
        public string iconCls { get; set; }
        public int? Level { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [Display(Name = "排序号")]
        [DataMember]
        public int? SortId { get; set; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Display(Name = "拼音简码")]
        [DataMember]
        public string PinYin { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Required(ErrorMessage = "更新时间不能为空")]
        [Display(Name = "更新时间")]
        [DataMember]
        public DateTime? UpdateTime { get; set; }

        public bool? Checked { get; set; }

        public object Attributes { get; set; }

        /// <summary>
        /// 控制器路径
        /// </summary>
        [Display(Name = "控制器路径")]
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [Required(ErrorMessage = "启用不能为空")]
        [Display(Name = "启用")]
        [DataMember]
        public bool Enabled { get; set; }

        public string state { get; set; }

        public List<ITreeNode> children { get; set; }

        /// <summary>
        /// 版本号,乐观离线锁通过为每行数据添加一个版本号来识别当前数据的版本，在获取数据时将版本号保存下来，
        /// 更新数据时将版本号作为Where中的过滤条件，如果该记录被更新，则版本号会发生变化，所以导致更新数据时影响行数为0，
        /// 通过引发一个并发更新异常让你了解数据已经被别人更新。
        /// </summary>
        public Byte[] Version { get; set; }
    }
}
