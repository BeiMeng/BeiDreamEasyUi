using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BeiDream.Common;

namespace BeiDream.EasyUi.Areas.Systems.QueryModels
{
    public class IconQueryModel : QueryModel
    {
        /// <summary>
        /// 分辨率大小
        /// </summary>
        [Display( Name = "分辨率" )]
        public string Size { get; set; }

        /// <summary>
        /// 起始创建时间
        /// </summary>
        [Display( Name = "起始创建时间" )]
        public System.DateTime? BeginCreateTime { get; set; }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        [Display( Name = "结束创建时间" )]
        public System.DateTime? EndCreateTime { get; set; }
    }
}