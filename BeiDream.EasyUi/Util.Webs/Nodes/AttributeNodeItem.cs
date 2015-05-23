namespace Util.Webs.Nodes {
    /// <summary>
    /// 属性节点项目
    /// </summary>
    internal sealed class AttributeNodeItem {
        /// <summary>
        /// 初始化属性节点项目
        /// </summary>
        /// <param name="value">值</param>
        public AttributeNodeItem( string value ) {
            Value = value;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; }
    }
}
