using System;
using System.Net;
using System.Web;

namespace Util {
    /// <summary>
    /// 网络操作
    /// </summary>
    public class Net {

        #region Ip(获取Ip)

        /// <summary>
        /// 获取Ip
        /// </summary>
        public static string Ip {
            get {
                try {
                    var ip = string.Empty;
                    if ( HttpContext.Current != null )
                        ip = HttpContext.Current.Request.UserHostAddress;
                    if ( !ip.IsEmpty() && !ip.Contains( ":" ) )
                        return ip;
                    return GetLanIp();
                }
                catch {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp() {
            var addressList = Dns.GetHostEntry( Dns.GetHostName() ).AddressList;
            foreach( var address in addressList ) {
                if ( address.ToString().Contains( "%" ) )
                    continue;
                return address.ToString();
            }
            return string.Empty;
        }

        #endregion

        #region Host(获取主机名)

        /// <summary>
        /// 获取主机名
        /// </summary>
        public static string Host {
            get {
                return Dns.GetHostName();
            }
        }

        #endregion
    }
}
