using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Util {
    /// <summary>
    /// Web操作
    /// </summary>
    public static class Web {

        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名,即域名，
        /// 范例：用户输入网址http://www.a.com/b.htm?a=1&amp;b=2，
        /// 返回值为: www.a.com
        /// </summary>
        public static string Host {
            get {
                return HttpContext.Current.Request.Url.Host;
            }
        }
        #endregion

        #region ResolveUrl(解析相对Url)

        /// <summary>
        /// 解析相对Url
        /// </summary>
        /// <param name="relativeUrl">相对Url</param>
        public static string ResolveUrl(string relativeUrl) {
            if (string.IsNullOrWhiteSpace(relativeUrl))
                return string.Empty;
            relativeUrl = relativeUrl.Replace("\\", "/");
            if (relativeUrl.StartsWith("/"))
                return relativeUrl;
            if (relativeUrl.Contains("://"))
                return relativeUrl;
            return VirtualPathUtility.ToAbsolute(relativeUrl);
        }

        #endregion

        #region HtmlEncode(对html字符串进行编码)

        /// <summary>
        /// 对html字符串进行编码
        /// </summary>
        /// <param name="html">html字符串</param>
        public static string HtmlEncode( string html ) {
            return HttpUtility.HtmlEncode( html );
        }

        #endregion

        #region UrlEncode(对Url进行编码)

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode( string url, bool isUpper = false ) {
            return UrlEncode( url, Encoding.UTF8, isUpper );
        }

        /// <summary>
        /// 对Url进行编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode( string url, Encoding encoding, bool isUpper = false ) {
            var result = HttpUtility.UrlEncode( url, encoding );
            if ( !isUpper )
                return result;
            return GetUpperEncode( result );
        }

        /// <summary>
        /// 获取大写编码字符串
        /// </summary>
        private static string GetUpperEncode( string encode ) {
            var result = new StringBuilder();
            int index = 0;
            for ( int i = 0; i < encode.Length; i++ ) {
                string character = encode[i].ToString();
                if ( character == "%" )
                    index = i;
                if ( i - index == 1 || i - index == 2 )
                    character = character.ToUpper();
                result.Append( character );
            }
            return result.ToString();
        }

        #endregion

        #region UrlDecode(对Url进行解码)

        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url</param>
        public static string UrlDecode( string url ) {
            return HttpUtility.UrlDecode( url );
        }

        /// <summary>
        /// 对Url进行解码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码,对于javascript的encodeURIComponent函数编码参数,应使用utf-8字符编码来解码</param>
        public static string UrlDecode( string url, Encoding encoding ) {
            return HttpUtility.UrlDecode( url, encoding );
        }

        #endregion

        #region SetSession(创建Session)

        /// <summary>
        /// 创建Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void SetSession<T>( string key, T value ) {
            if ( key.IsEmpty() )
                return;
            HttpContext.Current.Session[key] = value;
        }

        /// <summary>
        /// 创建Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void SetSession( string key, string value ) {
            SetSession<string>( key, value );
        }

        #endregion

        #region GetSession(读取Session)

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>        
        public static T GetSession<T>( string key ) {
            if ( key.IsEmpty() )
                return default( T );
            return Conv.To<T>( HttpContext.Current.Session[key] );
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public static string GetSession( string key ) {
            if ( key.IsEmpty() )
                return string.Empty;
            return HttpContext.Current.Session[key] as string;
        }

        #endregion

        #region RemoveSession(删除指定Session)

        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void RemoveSession( string key ) {
            if ( key.IsEmpty() )
                return;
            HttpContext.Current.Session.Contents.Remove( key );
        }

        #endregion

        #region DownloadFile(下载文件)

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static void DownloadFile( string filePath, string fileName ) {
            DownloadFile( filePath, fileName, Encoding.UTF8 );
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void DownloadFile( string filePath, string fileName, Encoding encoding ) {
            var bytes = Util.File.ReadToBytes( filePath );
            Download( bytes, fileName, encoding );
        }

        #endregion

        #region DownloadUrl(从Http地址下载)

        /// <summary>
        /// 从Http地址下载
        /// </summary>
        /// <param name="url">Http地址，范例：http://www.test.com/a.rar </param>
        /// <param name="fileName">文件名，包括扩展名</param>
        public static void DownloadUrl( string url, string fileName ) {
            DownloadUrl( url, fileName, Encoding.UTF8 );
        }

        /// <summary>
        /// 从Http地址下载
        /// </summary>
        /// <param name="url">Http地址，范例：http://www.test.com/a.rar </param>
        /// <param name="fileName">文件名，包括扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void DownloadUrl( string url, string fileName, Encoding encoding ) {
            var client = new WebClient();
            var bytes = client.DownloadData( url );
            Download( bytes, fileName, encoding );
        }

        #endregion

        #region Download(下载)

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static void Download( string text, string fileName ) {
            Download( text, fileName, Encoding.UTF8 );
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download( string text, string fileName, Encoding encoding ) {
            var bytes = Util.File.StringToBytes( text, encoding );
            Download( bytes, fileName, encoding );
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static void Download( Stream stream, string fileName ) {
            Download( stream, fileName, Encoding.UTF8 );
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download( Stream stream, string fileName, Encoding encoding ) {
            Download( Util.File.StreamToBytes( stream ), fileName, encoding );
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download( byte[] bytes, string fileName ) {
            Download( bytes, fileName, Encoding.UTF8 );
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download( byte[] bytes, string fileName, Encoding encoding ) {
            if ( bytes == null || bytes.Length == 0 )
                return;
            HttpContext.Current.Response.ContentType = "application/cotet-stream";
            HttpContext.Current.Response.AddHeader( "Content-Disposition", "attachment; filename=" + UrlEncode( fileName.Replace( " ", "" ) ) );
            HttpContext.Current.Response.BinaryWrite( bytes );
            HttpContext.Current.Response.ContentEncoding = encoding;
            HttpContext.Current.Response.End();
        }

        #endregion

        #region GetFileControls(获取客户端文件控件集合)

        /// <summary>
        /// 获取有效客户端文件控件集合,文件控件必须上传了内容，为空将被忽略,
        /// 注意:Form标记必须加入属性 enctype="multipart/form-data",服务器端才能获取客户端file控件.
        /// </summary>
        public static List<HttpPostedFile> GetFileControls() {
            var result = new List<HttpPostedFile>();
            var files = HttpContext.Current.Request.Files;
            if ( files.Count == 0 )
                return result;
            for ( int i = 0; i < files.Count; i++ ) {
                var file = files[i];
                if ( file.ContentLength == 0 )
                    continue;
                result.Add( files[i] );
            }
            return result;
        }

        #endregion

        #region GetFileControl(获取第一个有效客户端文件控件)

        /// <summary>
        /// 获取第一个有效客户端文件控件,文件控件必须上传了内容，为空将被忽略,
        /// 注意:Form标记必须加入属性 enctype="multipart/form-data",服务器端才能获取客户端file控件.
        /// </summary>
        public static HttpPostedFile GetFileControl() {
            var files = GetFileControls();
            if ( files == null || files.Count == 0 )
                return null;
            return files[0];
        }

        #endregion
    }
}
