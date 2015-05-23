using System.Collections.Generic;
using System.Text;

namespace Util.Webs {
    /// <summary>
    /// 数组生成器
    /// </summary>
    public class ArrayBuilder {
        /// <summary>
        /// 初始化数组生成器
        /// </summary>
        public ArrayBuilder() {
            _list = new List<string>();
        }

        /// <summary>
        /// 数组
        /// </summary>
        private readonly List<string> _list;

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="item">项</param>
        public void Add( string item ) {
            if ( item.IsEmpty() )
                return;
            _list.Add( item );
        }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 输出结果
        /// </summary>
        public string GetResult() {
            if ( _list.Count == 0 )
                return string.Empty;
            if ( Method.IsEmpty() )
                return GetArray();
            return GetMethod();
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        private string GetMethod() {
            return string.Format( "{0}({1})", Method, GetArray() );
        }

        /// <summary>
        /// 获取数组
        /// </summary>
        private string GetArray() {
            var result = new StringBuilder();
            result.Append( "[" );
            result.Append( _list.Splice() );
            result.Append( "]" );
            return result.ToString();
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
