using System.ComponentModel;

namespace Util {
    /// <summary>
    /// 操作符
    /// </summary>
    public enum Operator {
        /// <summary>
        /// 等于
        /// </summary>
        [Description( "等于" )]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description( "不等于" )]
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        [Description( "大于" )]
        Greater,
        /// <summary>
        /// 小于
        /// </summary>
        [Description( "小于" )]
        Less,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description( "大于等于" )]
        GreaterEqual,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description( "小于等于" )]
        LessEqual,
        /// <summary>
        /// 头尾匹配
        /// </summary>
        [Description( "头尾匹配" )]
        Contains,
        /// <summary>
        /// 头匹配
        /// </summary>
        [Description( "头匹配" )]
        Starts,
        /// <summary>
        /// 尾匹配
        /// </summary>
        [Description( "尾匹配" )]
        Ends
    }
}
