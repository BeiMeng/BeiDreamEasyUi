using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Util.Images;

namespace Util.Files {
    /// <summary>
    /// 文件上传操作
    /// </summary>
    public class FileUpload : IFileUpload {

        #region 构造方法

        /// <summary>
        /// 初始化文件上传操作
        /// </summary>
        /// <param name="uploadPathStrategy">上传路径策略</param>
        public FileUpload( IUploadPathStrategy uploadPathStrategy ) {
            UploadPathStrategy = uploadPathStrategy;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 上传路径策略
        /// </summary>
        public IUploadPathStrategy UploadPathStrategy { get; set; }

        #endregion

        #region GetFile(获取上传文件)

        /// <summary>
        /// 获取上传文件
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public FileInfo GetFile( string fileCategory = "", string baseCategory = "" ) {
            return ToFileInfo( Web.GetFileControl(), fileCategory, baseCategory );
        }

        /// <summary>
        /// 转换为文件信息
        /// </summary>
        private FileInfo ToFileInfo( HttpPostedFile file, string fileCategory, string baseCategory ) {
            var path = GetFilePath( file, fileCategory, baseCategory );
            return FileInfo.Create( path, File.StreamToBytes( file.InputStream ), GetFileName( file ) );
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        private string GetFilePath( HttpPostedFile file, string fileCategory, string baseCategory ) {
            UploadPathStrategy.CheckNull( "UploadPathStrategy" );
            return UploadPathStrategy.GetPath( GetFileName( file ), fileCategory, baseCategory );
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        private string GetFileName( HttpPostedFile file ) {
            return Path.GetFileName( file.FileName );
        }

        #endregion

        #region GetImage(获取上传图片)

        /// <summary>
        /// 获取上传图片
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public ImageInfo GetImage( string fileCategory = "", string baseCategory = "" ) {
            return ToImageInfo( Web.GetFileControl(), fileCategory, baseCategory );
        }

        /// <summary>
        /// 转换为图片信息
        /// </summary>
        private ImageInfo ToImageInfo( HttpPostedFile file, string fileCategory, string baseCategory ) {
            var path = GetFilePath( file, fileCategory, baseCategory );
            var size = GetSize( file.InputStream );
            return ImageInfo.Create( path, File.StreamToBytes( file.InputStream ), size.Width, size.Height, GetFileName( file ) );
        }

        /// <summary>
        /// 获取图片尺寸
        /// </summary>
        private Size GetSize( Stream stream ) {
            System.Drawing.Image image = Image.FromStream( stream );
            if ( image == null )
                return new Size( 0, 0 );
            return new Size( image.Width, image.Height );
        }

        #endregion

        #region GetFiles(获取上传文件集合)

        /// <summary>
        /// 获取上传文件集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public List<FileInfo> GetFiles( string fileCategory = "", string baseCategory = "" ) {
            var files = Web.GetFileControls();
            return files.Select( file => ToFileInfo( file, fileCategory, baseCategory ) ).ToList();
        }

        #endregion

        #region GetImages(获取上传图片集合)

        /// <summary>
        /// 获取上传图片集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public List<ImageInfo> GetImages( string fileCategory = "", string baseCategory = "" ) {
            var files = Web.GetFileControls();
            return files.Select( file => ToImageInfo( file, fileCategory, baseCategory ) ).ToList();
        }

        #endregion

        #region UploadFile(上传文件)

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public FileInfo UploadFile( string fileCategory = "", string baseCategory = "" ) {
            var file = GetFile( fileCategory, baseCategory );
            File.Save( file );
            return file;
        }

        #endregion

        #region UploadImage(上传图片)

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public ImageInfo UploadImage( string fileCategory = "", string baseCategory = "" ) {
            var file = GetImage( fileCategory, baseCategory );
            File.Save( file );
            return file;
        }

        #endregion

        #region UploadFiles(上传文件集合)

        /// <summary>
        /// 上传文件集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public List<FileInfo> UploadFiles( string fileCategory = "", string baseCategory = "" ) {
            var file = GetFiles( fileCategory, baseCategory );
            File.Save( file );
            return file;
        }

        #endregion

        #region UploadImages(上传图片集合)

        /// <summary>
        /// 上传图片集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        public List<ImageInfo> UploadImages( string fileCategory = "", string baseCategory = "" ) {
            var file = GetImages( fileCategory, baseCategory );
            File.Save( file );
            return file;
        }

        #endregion
    }
}
