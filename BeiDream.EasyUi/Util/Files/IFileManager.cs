using System.Collections.Generic;

namespace Util.Files {
    /// <summary>
    /// 文件管理器
    /// </summary>
    public interface IFileManager : IDependency{
        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// 添加内容到文件末尾
        /// </summary>
        /// <param name="content">内容</param>
        void Append( string content );
        /// <summary>
        /// 移除内容
        /// </summary>
        /// <param name="content">内容</param>
        void Remove( string content );
        /// <summary>
        /// 移除内容
        /// </summary>
        /// <param name="list">内容列表</param>
        void Remove( IEnumerable<string> list );
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
        /// <summary>
        /// 删除文件列表
        /// </summary>
        /// <param name="paths">文件路径列表</param>
        void DeleteFiles( IEnumerable<string> paths );
    }
}
