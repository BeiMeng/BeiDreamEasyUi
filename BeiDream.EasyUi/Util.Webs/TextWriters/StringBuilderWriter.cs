using System.Text;

namespace Util.Webs.TextWriters {
    /// <summary>
    /// 文本写入器
    /// </summary>
    public class StringBuilderWriter : ITextWriter{
        /// <summary>
        /// 初始化文本写入器
        /// </summary>
        public StringBuilderWriter() {
            _writer = new StringBuilder();
        }

        /// <summary>
        /// 文本写入器
        /// </summary>
        private readonly StringBuilder _writer;

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="text">文本</param>
        public void Write( string text ) {
            _writer.Append( text );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            return _writer.ToString();
        }
    }
}
