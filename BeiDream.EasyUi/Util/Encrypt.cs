using System;
using System.Security.Cryptography;
using System.Text;

namespace Util {
    /// <summary>
    /// 加密操作
    /// </summary>
    public class Encrypt {

        #region Md5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5By16( string text ) {
            return Md5By16( text, Encoding.UTF8 );
        }

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By16( string text,Encoding encoding ) {
            return Md5( text,encoding, 4, 8 );
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        private static string Md5( string text, Encoding encoding,int? startIndex,int? length ) {
            if ( string.IsNullOrWhiteSpace( text ) )
                return string.Empty;
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try {
                if( startIndex == null )
                    result = BitConverter.ToString( md5.ComputeHash( encoding.GetBytes( text ) ) );
                else
                    result = BitConverter.ToString( md5.ComputeHash( encoding.GetBytes( text ) ), startIndex.SafeValue(), length.SafeValue() );
            }
            finally {
                md5.Clear();
            }
            return result.Replace( "-", "" );
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        public static string Md5By32( string text ) {
            return Md5By32( text, Encoding.UTF8 );
        }

        /// <summary>
        /// Md5加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By32( string text, Encoding encoding ) {
            return Md5( text, encoding, null, null );
        }

        #endregion
    }
}
