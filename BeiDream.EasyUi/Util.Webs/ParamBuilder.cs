using System.Collections.Generic;

namespace Util.Webs {
    /// <summary>
    /// 参数生成器
    /// </summary>
    public class ParamBuilder {
        /// <summary>
        /// 初始化参数生成器
        /// </summary>
        /// <param name="quotes">引号，默认为单引号</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        public ParamBuilder( string quotes = "'",string separator = "," ) {
            _result = new List<string>();
            _quotes = quotes;
            _separator = separator;
        }

        /// <summary>
        /// 结果
        /// </summary>
        private readonly List<string> _result;
        /// <summary>
        /// 引号
        /// </summary>
        private readonly string _quotes;
        /// <summary>
        /// 分隔符
        /// </summary>
        private readonly string _separator;

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isAddQuotes">是否添加引号</param>
        public void Add( string value, bool isAddQuotes = false ) {
            Add( value, string.Empty, isAddQuotes );
        }

        /// <summary>
        /// 添加值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="defaultValue">默认值，当值为空时使用</param>
        /// <param name="isAddQuotes">是否添加引号</param>
        public void Add( string value, string defaultValue, bool isAddQuotes = false ) {
            value = GetQuotesValue( value, isAddQuotes );
            if ( value.IsEmpty() )
                value = defaultValue;
            _result.Add( value );
        }

        /// <summary>
        /// 获取添加引号后的值
        /// </summary>
        private string GetQuotesValue( string value, bool isAddQuotes ) {
            if ( value.IsEmpty() )
                return string.Empty;
            if ( !isAddQuotes )
                return value;
            return string.Format( "{0}{1}{0}", _quotes, value );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            return _result.Splice( "",_separator );
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return GetResult();
        }
    }
}
