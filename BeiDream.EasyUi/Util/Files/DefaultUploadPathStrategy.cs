namespace Util.Files {
    /// <summary>
    /// 默认上传路径策略
    /// </summary>
    public class DefaultUploadPathStrategy : IUploadPathStrategy {
        /// <summary>
        /// 获取上传路径,形式：\文件分类目录\文件名-HHmmss.扩展名
        /// </summary>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基目录</param>
        public string GetPath( string fileName, string fileCategory = "", string baseCategory = "" ) {
            return FileInfo.Join( fileCategory, FileInfo.GetSafeName( fileName ) );
        }
    }
}
