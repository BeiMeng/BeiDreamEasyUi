using System.Collections.Generic;
using System.Text;

namespace Util.Files {
    /// <summary>
    /// 文件管理器
    /// </summary>
    public class FileManager : IFileManager {
        /// <summary>
        /// 结果
        /// </summary>
        private StringBuilder _result;

        /// <summary>
        /// 文件绝对路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 添加内容到文件末尾
        /// </summary>
        /// <param name="content">内容</param>
        public void Append( string content ) {
            Init();
            _result.Append( content );
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init() {
            if ( _result != null )
                return;
            _result = new StringBuilder();
            lock( FilePath ) {
                _result.Append( File.Read( FilePath ) );
            }
        }

        /// <summary>
        /// 移除内容
        /// </summary>
        /// <param name="content">内容</param>
        public void Remove( string content ) {
            Init();
            _result.Replace( content, "" );
        }

        /// <summary>
        /// 移除内容
        /// </summary>
        /// <param name="list">内容列表</param>
        public void Remove( IEnumerable<string> list ) {
            foreach( var content in list )
                Remove( content );
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save() {
            lock( FilePath ) {
                File.Write( FilePath, _result.ToString() );    
            }
        }

        /// <summary>
        /// 删除文件列表
        /// </summary>
        /// <param name="paths">文件路径列表</param>
        public void DeleteFiles( IEnumerable<string> paths ) {
            File.Delete( paths );
        }
    }
}
