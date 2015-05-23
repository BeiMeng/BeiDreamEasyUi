﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Util.Compress {
    /// <summary>
    /// GZip压缩
    /// </summary>
    public class GZip {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="text">文本</param>
        public static string Compress( string text ) {
            if ( text.IsEmpty() )
                return string.Empty;
            byte[] buffer = Encoding.UTF8.GetBytes( text );
            return Convert.ToBase64String( Compress( buffer ) );
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="text">文本</param>
        public static string Decompress( string text ) {
            if ( text.IsEmpty() )
                return string.Empty;
            byte[] buffer = Convert.FromBase64String( text );
            using( var ms = new MemoryStream(buffer) ) {
                using ( var zip = new GZipStream( ms, CompressionMode.Decompress ) ) {
                    using ( var reader = new StreamReader( zip ) ) {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] Compress( byte[] buffer ) {
            if ( buffer == null )
                return null;
            using ( var ms = new MemoryStream() ) {
                using ( var zip = new GZipStream( ms, CompressionMode.Compress, true ) ) {
                    zip.Write( buffer, 0, buffer.Length );
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="buffer">字节流</param>
        public static byte[] Decompress( byte[] buffer ) {
            if ( buffer == null )
                return null;
            return Decompress( new MemoryStream( buffer ) );
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] Compress( Stream stream ) {
            if ( stream == null || stream.Length == 0 )
                return null;
            return Compress( Util.File.StreamToBytes( stream ) );
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] Decompress( Stream stream ) {
            if ( stream == null || stream.Length == 0 )
                return null;
            using ( var zip = new GZipStream( stream, CompressionMode.Decompress ) ) {
                using ( var reader = new StreamReader(zip) ) {
                    return Encoding.UTF8.GetBytes( reader.ReadToEnd() );
                }
            }
        }
    }
}
