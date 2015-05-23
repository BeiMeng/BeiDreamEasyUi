using System;

namespace Util.Webs.TextWriters {
    /// <summary>
    /// 文本写入器
    /// </summary>
    public class TextWriter : ITextWriter {
        /// <summary>
        /// 初始化文本写入器
        /// </summary>
        /// <param name="writer">文本写入器</param>
        public TextWriter(System.IO.TextWriter writer) {
            _writer = writer;
        }

        /// <summary>
        /// 文本写入器
        /// </summary>
        private readonly System.IO.TextWriter _writer;

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="text">文本</param>
        public void Write( string text ) {
            _writer.Write( text );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            throw new NotImplementedException();
        }
    }
}
