using System.ComponentModel;

namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树节点状态
    /// </summary>
    public enum TreeNodeState {
        /// <summary>
        /// 展开
        /// </summary>
        [Description( "open" )]
        Open,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description( "closed" )]
        Closed
    }
}
