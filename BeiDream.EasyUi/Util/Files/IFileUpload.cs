using System.Collections.Generic;
using Util.Images;

namespace Util.Files {
    /// <summary>
    /// 文件上传操作
    /// </summary>
    public interface IFileUpload : IDependency {
        /// <summary>
        /// 上传路径策略
        /// </summary>
        IUploadPathStrategy UploadPathStrategy { get; set; }
        /// <summary>
        /// 获取上传文件
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        FileInfo GetFile( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 获取上传图片
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        ImageInfo GetImage( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 获取上传文件集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        List<FileInfo> GetFiles( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 获取上传图片集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        List<ImageInfo> GetImages( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        FileInfo UploadFile( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        ImageInfo UploadImage( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 上传文件集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        List<FileInfo> UploadFiles( string fileCategory = "", string baseCategory = "" );
        /// <summary>
        /// 上传图片集合
        /// </summary>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基分类目录</param>
        List<ImageInfo> UploadImages( string fileCategory = "", string baseCategory = "" );
    }
}
