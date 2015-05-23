using System.Collections.Generic;

namespace Util {
    /// <summary>
    /// 工具扩展
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 转换为用分隔符拼接的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Splice<T>( this IEnumerable<T> list, string quotes = "", string separator = "," ) {
            return Str.Splice( list, quotes, separator );
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="length">返回长度</param>
        /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        public static string Truncate( this string text, int length, int endCharCount = 0, string endChar = "." ) {
            return Str.Truncate( text, length, endCharCount, endChar );
        }
    }
}
