namespace Util.Compress {
    /// <summary>
    /// 压缩
    /// </summary>
    public interface ICompress {
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="password">密码</param>
        ICompress Password( string password );
        /// <summary>
        /// 添加源目录,用于压缩或解压缩
        /// </summary>
        /// <param name="fromDirectory">源目录绝对路径</param>
        ICompress AddDirectory( params string[] fromDirectory );
        /// <summary>
        /// 添加源文件,用于压缩或解压缩
        /// </summary>
        /// <param name="fromPath">源文件绝对路径</param>
        ICompress AddFile( params string[] fromPath );
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="toDirectory">压缩到该目录</param>
        /// <param name="toFileName">压缩文件名，不带扩展名，自动添加.zip扩展名</param>
        void Compress( string toDirectory,string toFileName );
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="toDirectory">解压到该目录</param>
        void Decompress( string toDirectory );
    }
}
