using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BeiDream.Common;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.Services.CalibrationManagement.Dots
{
    public class ParameterViewModel :DomainBase, ITreeNode, IDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称不能为空")]
        [StringLength(200, ErrorMessage = "参数名称输入过长，不能超过100位")]
        [Display(Name = "参数名称")]
        [DataMember]
        public string Text { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        [Required(ErrorMessage = "排序号不能为空")]
        [Display(Name = "排序号")]
        [DataMember]
        public int SortId { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [Display(Name = "描述信息")]
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "创建时间不能为空")]    //可以不需要，这个字段是后台字段赋值，前台只是显示也不可编辑
        [Display(Name = "创建时间")]
        [DataMember]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        [Required(ErrorMessage = "启用不能为空")]
        [Display(Name = "启用")]
        [DataMember]
        public bool Enabled { get; set; }
        /// <summary>
        /// 是否展开子节点
        /// </summary>
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
