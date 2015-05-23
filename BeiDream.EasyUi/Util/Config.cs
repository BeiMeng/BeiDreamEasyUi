using System.Configuration;
using System.Text;

namespace Util {
    /// <summary>
    /// 配置
    /// </summary>
    public static class Config {

        #region DefaultEncoding(默认编码)

        /// <summary>
        /// 默认编码,值为utf-8
        /// </summary>
        public static Encoding DefaultEncoding {
            get { return Encoding.GetEncoding( "utf-8" ); }
        }

        #endregion

        #region GetAppSettings(获取appSettings)

        /// <summary>
        /// 获取appSettings
        /// </summary>
        /// <param name="key">键名</param>
        public static string GetAppSettings( string key ) {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion

        #region GetConnectionString(获取连接字符串)

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="key">键名</param>        
        public static string GetConnectionString( string key ) {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        #endregion

        #region GetProviderName(获取数据提供程序名称)

        /// <summary>
        /// 获取数据提供程序名称
        /// </summary>
        /// <param name="key">键名</param>
        public static string GetProviderName( string key ) {
            return ConfigurationManager.ConnectionStrings[key].ProviderName;
        }

        #endregion

        #region GetLogContextKey(获取日志上下文键名)

        /// <summary>
        /// 获取日志上下文键名
        /// </summary>
        public static string GetLogContextKey() {
            return "Util.Logs.ContextLogger";
        }

        #endregion
    }
}
