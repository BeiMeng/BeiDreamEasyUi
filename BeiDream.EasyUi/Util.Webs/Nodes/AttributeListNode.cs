using System.Collections.Generic;

namespace Util.Webs.Nodes {
    /// <summary>
    /// 属性列表节点
    /// </summary>
    public class AttributeListNode : IAttributeNode {
        /// <summary>
        /// 初始化属性列表节点
        /// </summary>
        /// <param name="value">属性值</param>
        public AttributeListNode( string value = "" ) {
            _values = new List<string>();
            ValueSeparator = ";";
            Add( value );
        }

        /// <summary>
        /// 值
        /// </summary>
        private readonly List<string> _values; 

        /// <summary>
        /// 值分隔符
        /// </summary>
        public string ValueSeparator { get; set; }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="value">属性值</param>
        public void Add( string value ) {
            if ( value.IsEmpty() )
                return;
            _values.Add( value );
        }

        /// <summary>
        /// 清空属性值
        /// </summary>
        public void Clear() {
            _values.Clear();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            if ( _values.Count == 0 )
                return string.Empty;
            return _values.Splice( "", ValueSeparator );
        }
    }
}
