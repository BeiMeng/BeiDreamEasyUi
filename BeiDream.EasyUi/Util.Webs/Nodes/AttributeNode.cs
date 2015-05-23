using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Webs.Nodes {
    /// <summary>
    /// 属性节点
    /// </summary>
    public class AttributeNode : IAttributeNode {
        /// <summary>
        /// 初始化属性节点
        /// </summary>
        /// <param name="name">属性名</param>
        public AttributeNode( string name ) {
            Name = name;
            _items = new List<AttributeNodeItem>();
            ValueSeparator = ";";
            AttributeSeparator = "=";
            ValueQuotes = "\"";
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// 值分隔符
        /// </summary>
        public string ValueSeparator { get; set; }

        /// <summary>
        /// 属性分隔符
        /// </summary>
        public string AttributeSeparator { get; set; }

        /// <summary>
        /// 值两边的引号字符串
        /// </summary>
        public string ValueQuotes { get; set; }

        /// <summary>
        /// 属性节点项目集合
        /// </summary>
        private readonly List<AttributeNodeItem> _items;

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="value">属性值</param>
        public void Add( string value ) {
            _items.Add( new AttributeNodeItem( value ) );
        }

        /// <summary>
        /// 清空属性值
        /// </summary>
        public void Clear() {
            _items.Clear();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            if ( _items.Count == 0 )
                return string.Empty;
            var result = new StringBuilder();
            result.AppendFormat( "{0}{1}", Name, AttributeSeparator );
            result.AppendFormat( "{0}{1}{0}",ValueQuotes, GetValue() );
            return result.ToString();
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        private string GetValue() {
            return _items.Select( t => t.Value ).Splice( "", ValueSeparator );
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
