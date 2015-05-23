using System.IO;

namespace Util {
    /// <summary>
    /// 图片操作
    /// </summary>
    public class Image {
        /// <summary>
        /// 图片文件的绝对路径
        /// </summary>
        /// <param name="filePath">图片文件的绝对路径</param>
        public static System.Drawing.Image FromFile( string filePath ) {
            return System.Drawing.Image.FromFile( filePath );
        }

        /// <summary>
        /// 图片文件的绝对路径
        /// </summary>
        /// <param name="stream">流</param>
        public static System.Drawing.Image FromStream( Stream stream ) {
            return System.Drawing.Image.FromStream( stream );
        }

        /// <summary>
        /// 图片文件的绝对路径
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static System.Drawing.Image FromStream( byte[] buffer ) {
            using( var stream = new MemoryStream(buffer) ) {
                return FromStream( stream );
            }
        }
    }
}
