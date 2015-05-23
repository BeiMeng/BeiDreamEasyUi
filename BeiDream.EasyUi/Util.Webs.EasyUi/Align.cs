using System.ComponentModel;

namespace Util.Webs.EasyUi {
    /// <summary>
    /// 对齐方式
    /// </summary>
    public enum Align {
        /// <summary>
        /// 左侧
        /// </summary>
        [Description( "left" )]
        Left,
        /// <summary>
        /// 右侧
        /// </summary>
        [Description( "right" )]
        Right,
        /// <summary>
        /// 顶部
        /// </summary>
        [Description( "top" )]
        Top,
        /// <summary>
        /// 底部
        /// </summary>
        [Description( "bottom" )]
        Bottom
    }

    /// <summary>
    /// 左右对齐方式
    /// </summary>
    public enum AlignLeftRigth {
        /// <summary>
        /// 左侧
        /// </summary>
        [Description( "left" )]
        Left,
        /// <summary>
        /// 右侧
        /// </summary>
        [Description( "right" )]
        Right
    }

    /// <summary>
    /// 左右居中对齐方式
    /// </summary>
    public enum AlignLeftRigthCenter {
        /// <summary>
        /// 左侧
        /// </summary>
        [Description( "left" )]
        Left,
        /// <summary>
        /// 右侧
        /// </summary>
        [Description( "right" )]
        Right,
        /// <summary>
        /// 居中
        /// </summary>
        [Description( "center" )]
        Center
    }
}
