namespace Util {
    /// <summary>
    /// 文件及流操作 - 文件路径操作
    /// </summary>
    public partial class File {
        /// <summary>
        /// 连接基路径和子路径,比如把 c: 与 test.doc 连接成 c:\test.doc
        /// </summary>
        /// <param name="basePath">基路径,范例：c:</param>
        /// <param name="subPath">子路径,可以是文件名, 范例：test.doc</param>
        public static string JoinPath( string basePath, string subPath ) {
            basePath = basePath.TrimEnd( '/' ).TrimEnd( '\\' );
            subPath = subPath.TrimStart( '/' ).TrimStart( '\\' );
            string path = basePath + "\\" + subPath;
            return path.Replace( "/", "\\" ).ToLower();
        }
    }
}
