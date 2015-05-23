namespace Util.Webs.Nodes {
    /// <summary>
    /// 属性节点
    /// </summary>
    public interface IAttributeNode {
        /// <summary>
        /// 值分隔符
        /// </summary>
        string ValueSeparator { get; set; }
        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="value">属性值</param>
        void Add( string value );
        /// <summary>
        /// 清空属性值
        /// </summary>
        void Clear();
        /// <summary>
        /// 获取结果
        /// </summary>
        string GetResult();
    }
}
