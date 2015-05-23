using System;
using System.Collections.Generic;
using System.IO;
using FileInfo = Util.Files.FileInfo;

namespace Util {
    /// <summary>
    /// 文件及流操作 - 文件信息
    /// </summary>
    public partial class File {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="files">文件信息集合</param>
        public static void Save( IEnumerable<FileInfo> files ) {
            foreach( var file in files )
                Save( file );
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件信息</param>
        public static void Save( FileInfo file ) {
            ValidateSave( file );
            CreateDirectory( file );
            SaveFile( file );
        }

        /// <summary>
        /// 验证保存文件
        /// </summary>
        private static void ValidateSave( FileInfo file ) {
            file.CheckNull( "file" );
            if ( file.FileBytes == null || file.FileBytes.Length == 0 )
                throw new Warning( string.Format( R.InvalidFile,file.FileName ) );
            if( file.GetPhysicalPath().IsEmpty() )
                throw new ArgumentException("上传路径不正确");
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        private static void CreateDirectory( FileInfo file ) {
            var path = Path.GetDirectoryName( file.GetPhysicalPath() );
            if ( path == null )
                return;
            if ( Directory.Exists( path ) )
                return;
            Directory.CreateDirectory( path );
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        private static void SaveFile( FileInfo file ) {
            Write( file.GetPhysicalPath(), file.FileBytes );
        }
    }
}
