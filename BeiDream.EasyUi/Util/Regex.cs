using System.Text.RegularExpressions;

namespace Util {
    /// <summary>
    /// 正则表达式操作
    /// </summary>
    public class Regex {
        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch( string input, string pattern ) {
            return IsMatch( input, pattern, RegexOptions.IgnoreCase );
        }

        /// <summary>
        /// 验证输入与模式是否匹配
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件,比如是否忽略大小写</param>
        public static bool IsMatch( string input, string pattern, RegexOptions options ) {
            return System.Text.RegularExpressions.Regex.IsMatch( input, pattern, options );
        }

        /// <summary>
        /// 获取匹配的值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="replaceText">结果模式字符串,范例："$1"用来获取第一个( )内的值</param>
        /// <param name="count">替换次数</param>
        public static string Replace( string input, string pattern, string replaceText, int count = 0 ) {
            var reg = new System.Text.RegularExpressions.Regex( pattern );
            if ( count == 0 )
                return reg.Replace( input, replaceText );
            return reg.Replace( input, replaceText, count );
        }
    }
}
