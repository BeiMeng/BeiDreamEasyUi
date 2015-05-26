using System;
using System.Collections.Generic;
using BeiDream.PetaPoco.Models;

namespace BeiDream.Services.Systems.Commom {
    /// <summary>
    /// 图标管理器
    /// </summary>
    public interface IIconManager {
        /// <summary>
        /// 上传图标
        /// </summary>
        /// <param name="uploadIconPath">上传图标的路径</param>
        /// <param name="cssPath">图标Css的路径</param>
        Icons Upload(string uploadIconPath, string cssPath );
        /// <summary>
        /// 删除图标
        /// </summary>
        /// <param name="ids">图标编号集合</param>
        /// <param name="cssPath">图标Css的路径</param>
        List<Icons> Delete( List<Guid> ids, string cssPath );
    }
}
