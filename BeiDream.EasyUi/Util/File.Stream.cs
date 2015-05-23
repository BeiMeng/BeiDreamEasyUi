using System;

namespace Util {
    /// <summary>
    /// 文件及流操作 - 流操作
    /// </summary>
    public partial class File {
        /// <summary>
        /// 为字节流添加长度，长度从第1个字节到第4个字节
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] AddLength( byte[] buffer ) {
            var result = new byte[buffer.Length + 4];
            Buffer.BlockCopy( BitConverter.GetBytes( buffer.Length ), 0, result, 0, 4 );
            Buffer.BlockCopy( buffer, 0, result, 4, buffer.Length );
            return result;
        }
    }
}
