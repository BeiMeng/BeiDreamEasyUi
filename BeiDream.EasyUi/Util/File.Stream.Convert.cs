using System;
using System.IO;
using System.Text;

namespace Util {
    /// <summary>
    /// 文件及流操作 - 流类型转换
    /// </summary>
    public partial class File {
        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string StreamToString( Stream data ) {
            return StreamToString( data, Config.DefaultEncoding );
        }

        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static string StreamToString( Stream data, Encoding encoding ) {
            if( data == null )
                return String.Empty;
            string result;
            using( var reader = new StreamReader( data, encoding ) ) {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        public static Stream StringToStream( string data ) {
            return StringToStream( data, Config.DefaultEncoding );
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static Stream StringToStream( string data, Encoding encoding ) {
            if( String.IsNullOrWhiteSpace( data ) )
                return Stream.Null;
            return new MemoryStream( StringToBytes( data, encoding ) );
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>        
        public static byte[] StringToBytes( string data ) {
            return StringToBytes( data, Config.DefaultEncoding );
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes( string data, Encoding encoding ) {
            if ( string.IsNullOrWhiteSpace( data ) )
                return new byte[] { };
            return encoding.GetBytes( data );
        }

        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>        
        public static string BytesToString( byte[] data ) {
            return BytesToString( data, Config.DefaultEncoding );
        }

        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString( byte[] data, Encoding encoding ) {
            if ( data == null || data.Length == 0 )
                return string.Empty;
            return encoding.GetString( data );
        }

        /// <summary>
        /// 字节数组转换成整数
        /// </summary>
        /// <param name="data">数据</param>
        public static int BytesToInt( byte[] data ) {
            if ( data == null || data.Length < 4 )
                return 0;
            var buffer = new byte[4];
            Buffer.BlockCopy( data, 0, buffer, 0, 4 );
            return BitConverter.ToInt32( buffer, 0 );
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] StreamToBytes( Stream stream ) {
            stream.Seek( 0, SeekOrigin.Begin );
            var buffer = new byte[stream.Length];
            stream.Read( buffer, 0, buffer.Length );
            return buffer;
        }
    }
}
