namespace Util.Webs.TextWriters {
    /// <summary>
    /// 文本写入器
    /// </summary>
    public interface ITextWriter {
        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="text">文本</param>
        void Write( string text );
        /// <summary>
        /// 获取结果
        /// </summary>
        string GetResult();
    }
}
