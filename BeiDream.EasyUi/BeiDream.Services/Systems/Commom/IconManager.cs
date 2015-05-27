using System;
using System.Collections.Generic;
using System.Linq;
using BeiDream.PetaPoco.Models;
using Util;
using Util.Files;
using Util.Images;

namespace BeiDream.Services.Systems.Commom {
    /// <summary>
    /// 图标管理器
    /// </summary>
    public class IconManager :IIconManager
    {
        /// <summary>
        /// 初始化图标管理器
        /// </summary>
        /// <param name="fileUpload">文件上传操作</param>
        /// <param name="fileManager">文件管理器</param>
        public IconManager( IFileUpload fileUpload,IFileManager fileManager ) {
            FileUpload = fileUpload;
            FileUpload.UploadPathStrategy = new DefaultUploadPathStrategy();
            FileManager = fileManager;
        }

        /// <summary>
        /// 文件上传操作
        /// </summary>
        public IFileUpload FileUpload { get; set; }

        /// <summary>
        /// 文件管理器
        /// </summary>
        public IFileManager FileManager { get; set; }

        /// <summary>
        /// 上传图标
        /// </summary>
        /// <param name="uploadIconPath">上传图标的路径</param>
        /// <param name="cssPath">图标Css的路径</param>
        public Icons Upload(string uploadIconPath, string cssPath ) {
            ValidateUpload(uploadIconPath, cssPath );
            var image = UploadImage( uploadIconPath );
            var icon = ToIcon(image );
            AppendToFile( cssPath, icon );
            return icon;
        }

        /// <summary>
        /// 验证上传
        /// </summary>
        private void ValidateUpload(string uploadIconPath, string cssPath ) {
            if ( uploadIconPath.IsEmpty() )
                throw new ArgumentException("请设置图标上传路径");
            if ( cssPath.IsEmpty() )
                throw new ArgumentException("请设置图标Css路径");
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        private ImageInfo UploadImage( string uploadIconPath ) {
            return FileUpload.UploadImage( uploadIconPath );
        }

        /// <summary>
        /// 转换为图标
        /// </summary>
        private Icons ToIcon(ImageInfo image)
        {
            var result = CreateIcon(image);
            return result;
        }

        /// <summary>
        /// 图片信息转换为图标
        /// </summary>
        public Icons CreateIcon(ImageInfo image)
        {
            var result = new Icons
            {
                Path = image.FilePath,
                Name = image.FileName,
                Width = image.Size.Width,
                Height = image.Size.Height,
                Size = image.Length.GetSize()
            };
            result.GenerateCss(image.FilePath);
            return result;
        }
        /// <summary>
        /// 添加到CSS文件末尾
        /// </summary>
        private void AppendToFile(string cssPath, Icons icon)
        {
            FileManager.FilePath = Sys.GetPhysicalPath( cssPath );
            FileManager.Append( icon.Css );
            FileManager.Save();
        }

        public void Delete(List<Icons> icons, string cssPath)
        {
            DeleteFiles(icons);
            RemoveCss(icons, cssPath);
        }
        /// <summary>
        /// 删除文件集合
        /// </summary>
        private void DeleteFiles(IEnumerable<Icons> icons)
        {
            FileManager.DeleteFiles(icons.Select(t => Sys.GetPhysicalPath(t.Path)));
        }

        /// <summary>
        /// 从Css文件中移除Css代码
        /// </summary>
        private void RemoveCss(IEnumerable<Icons> icons, string cssPath)
        {
            FileManager.FilePath = Sys.GetPhysicalPath( cssPath );
            FileManager.Remove( icons.Select( t => t.Css ) );
            FileManager.Save();
        }
    }
}
