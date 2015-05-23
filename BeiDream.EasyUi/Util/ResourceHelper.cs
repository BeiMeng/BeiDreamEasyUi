using System;
using System.Reflection;
using System.Resources;

namespace Util {
    /// <summary>
    /// 资源操作
    /// </summary>
    public class ResourceHelper {
        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名,应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        public static string GetString( string resourceName, string key ) {
            return GetString( resourceName, key, Assembly.GetCallingAssembly() );
        }

        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名,应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        /// <param name="assemblyName">程序集名称</param>
        public static string GetString( string resourceName, string key, string assemblyName ) {
            return GetString( resourceName, key, Reflection.GetAssembly( assemblyName ) );
        }

        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        /// <param name="resourceName">资源名,应使用完全限定名称，范例：Test.Unit.Resources.TestResource</param>
        /// <param name="key">键名</param>
        /// <param name="assembly">程序集</param>
        public static string GetString( string resourceName, string key, Assembly assembly ) {
            ValidateGetString( resourceName, key );
            string result = GetResourceStringFromAssmbly( resourceName, key, assembly );
            if( !string.IsNullOrWhiteSpace( result ) )
                return result;
            assembly = Assembly.GetExecutingAssembly();
            return GetResourceStringFromAssmbly( resourceName, key, assembly );
        }

        /// <summary>
        /// 验证获取资源文件中的字符串
        /// </summary>
        private static void ValidateGetString( string resourceName, string key ) {
            if ( string.IsNullOrWhiteSpace( resourceName ) )
                throw new ArgumentNullException( "resourceName" );
            if ( string.IsNullOrWhiteSpace( key ) )
                throw new ArgumentNullException( "key" );
        }

        /// <summary>
        /// 从资源中获取字符串
        /// </summary>
        private static string GetResourceStringFromAssmbly( string resourceName, string key, Assembly assembly ) {
            string result = GetStringByManager( resourceName, key, assembly );
            if ( !string.IsNullOrWhiteSpace( result ) )
                return result;
            return GetStringByManager( GetResourceFullName( resourceName, assembly ), key, assembly );
        }

        /// <summary>
        /// 获取资源文件中的字符串
        /// </summary>
        private static string GetStringByManager( string resourceName, string key, Assembly assembly ) {
            try {
                var manager = new ResourceManager( resourceName, assembly );
                return manager.GetString( key );
            }
            catch ( MissingManifestResourceException ) {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取资源名的全名
        /// </summary>
        private static string GetResourceFullName( string resourceName, Assembly assembly ) {
            string[] resources = assembly.GetManifestResourceNames();
            const string extension = ".resources";
            foreach( var resource in resources ) {
                if ( resource.EndsWith( string.Format( "{0}{1}", resourceName, extension ) ) )
                    return resource.Replace( extension, "" );
            }
            return string.Empty;
        }
    }
}
