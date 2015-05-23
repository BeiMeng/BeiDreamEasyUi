using Util.Files;

namespace Util.Images {
    /// <summary>
    /// 图片信息
    /// </summary>
    public class ImageInfo : FileInfo {
        /// <summary>
        /// 初始化图片信息
        /// </summary>
        private ImageInfo( string filePath, byte[] fileBytes, long? fileSize, int width, int height, string fileName )
            : base( filePath, fileBytes, fileSize, fileName ) {
            Size = new Size( width, height );
        }

        /// <summary>
        /// 初始化图片信息
        /// </summary>
        /// <param name="filePath">文件相对路径</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="fileName">文件名</param>
        public static ImageInfo Create( string filePath, long? fileSize, int width, int height, string fileName = "" ) {
            return new ImageInfo( filePath, null, fileSize, width, height, fileName );
        }

        /// <summary>
        /// 初始化图片信息
        /// </summary>
        /// <param name="filePath">文件相对路径</param>
        /// <param name="fileBytes">文件字节流</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="fileName">文件名</param>
        public static ImageInfo Create( string filePath, byte[] fileBytes, int width, int height, string fileName = "" ) {
            return new ImageInfo( filePath, fileBytes, fileBytes.Length, width, height, fileName );
        }

        /// <summary>
        /// 尺寸
        /// </summary>
        public Size Size { get; private set; }
    }
}
