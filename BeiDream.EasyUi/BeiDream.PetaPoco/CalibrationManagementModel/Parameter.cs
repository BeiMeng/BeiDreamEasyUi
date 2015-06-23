using System;
using System.ComponentModel.DataAnnotations;
using BeiDream.Common;

namespace BeiDream.PetaPoco.CalibrationManagementModel
{
    /// <summary>
    /// 组织机构
    /// </summary>
    [TableName("T_Parameter")]


    [PrimaryKey("P_ID", autoIncrement = false)]

    [ExplicitColumns]
    public class Parameter : TreeEntityBase<Guid, Guid?>
    {
        [Column("P_ID")]
        public override Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称不能为空")]
        [StringLength(100, ErrorMessage = "参数名称输入过长，不能超过100位")]
        [Column]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "描述信息输入过长，不能超过200位")]
        [Column]
        public string Description { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Column]
        public bool Enabled { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column]
        public bool? IsDeleted { get; set; }
        [Required(ErrorMessage = "创建时间不能为空")]
        [Column]
        public override DateTime? CreateTime { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [ResultColumn]
        public override byte[] Version { get; set; }
        /// <summary>
        /// 扩展一
        /// </summary>
        [Column]
        public string Expansion1 { get; set; }
        /// <summary>
        /// 扩展二
        /// </summary>
        [Column]
        public string Expansion2 { get; set; }
        /// <summary>
        /// 扩展三
        /// </summary>
        [Column]
        public string Expansion3 { get; set; }
        /// <summary>
        /// 实体本身的初始化操作
        /// </summary>
        public void Init()
        {
            if (CreateTime == null)
            {
                CreateTime = DateTime.Now;
            }
            if (IsDeleted == null)
            {
                IsDeleted = false;
            }
        }
    }
}
