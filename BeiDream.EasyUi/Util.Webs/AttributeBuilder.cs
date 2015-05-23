using System;
using System.Collections.Generic;
using Util.Webs.Nodes;

namespace Util.Webs {
    /// <summary>
    /// 属性生成器
    /// </summary>
    public class AttributeBuilder {
        /// <summary>
        /// 初始化属性生成器
        /// </summary>
        /// <param name="attributeSeparator">属性分隔符,范例a="1"中的等号</param>
        /// <param name="nodeSeparator">属性节点分隔符，范例a="1",b="2"中的逗号</param>
        public AttributeBuilder( string attributeSeparator = "=", string nodeSeparator = " " ) {
            _nodes = new Dictionary<string, IAttributeNode>();
            _attributeSeparator = attributeSeparator;
            _nodeSeparator = nodeSeparator;
        }

        /// <summary>
        /// 属性节点集合
        /// </summary>
        private readonly Dictionary<string, IAttributeNode> _nodes;
        /// <summary>
        /// 属性分隔符
        /// </summary>
        private readonly string _attributeSeparator;
        /// <summary>
        /// 属性节点分隔符
        /// </summary>
        private readonly string _nodeSeparator;

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="value">属性值</param>
        public void Add( string value ) {
            if ( value.IsEmpty() )
                return;
            _nodes.Add( Guid.NewGuid().ToString(),new AttributeListNode(value) );
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        /// <param name="separator">属性值分隔符，默认为分号</param>
        /// <param name="quotes">属性值两边的引号,默认为双引号</param>
        public void Add( string name, string value, string separator = ";",string quotes = "\"" ) {
            if ( name.IsEmpty() )
                return;
            if ( _nodes.ContainsKey( name ) )
                MergeNode( name, value, separator );
            else
                AddNode( name, value, separator, quotes );
        }

        /// <summary>
        /// 合并节点
        /// </summary>
        private void MergeNode( string name, string value, string separator ) {
            var node = _nodes[name];
            node.ValueSeparator = separator;
            node.Add( value );
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        private void AddNode( string name, string value, string separator, string quotes ) {
            var node = new AttributeNode( name ) { ValueSeparator = separator, AttributeSeparator = _attributeSeparator, ValueQuotes = quotes };
            node.Add( value );
            _nodes.Add( name, node );
        }

        /// <summary>
        /// 更新属性,不存在则添加
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="separator">属性值分隔符，默认为分号</param>
        /// <param name="quotes">属性值两边的引号,默认为双引号</param>
        public void Update( string name, string value, string separator = ";", string quotes = "\"" ) {
            if ( name.IsEmpty() )
                return;
            if ( _nodes.ContainsKey( name ) )
                UpdateNode( name, value );
            else
                AddNode( name, value, separator, quotes );
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        private void UpdateNode( string name, string value ) {
            var node = _nodes[name];
            node.Clear();
            node.Add( value );
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="name">属性名</param>
        public void Remove( string name ) {
            if ( !_nodes.ContainsKey( name ) )
                return;
            _nodes.Remove( name );
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性值</param>
        public void AddClass( string @class ) {
            Add( "class", @class, " " );
        }

        /// <summary>
        /// 更新class属性
        /// </summary>
        /// <param name="class">class属性值</param>
        public void UpdateClass( string @class ) {
            Update( "class", @class, " " );
        }

        /// <summary>
        /// 添加style属性
        /// </summary>
        /// <param name="name">style属性名</param>
        /// <param name="value">style属性值</param>
        public void AddStyle( string name, string value ) {
            Add( "style", string.Format( "{0}:{1}", name, value ) );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            var result = new Str();
            foreach ( var node in _nodes )
                result.Add( "{0}{1}", node.Value.GetResult(), _nodeSeparator );
            return result.ToString().TrimEnd( _nodeSeparator.ToCharArray() );
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
