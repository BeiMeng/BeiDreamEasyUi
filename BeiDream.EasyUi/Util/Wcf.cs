using System.ServiceModel;

namespace Util {
    /// <summary>
    /// Wcf操作
    /// </summary>
    public static class Wcf {
        /// <summary>
        /// 创建客户端代理对象
        /// </summary>
        /// <typeparam name="T">操作契约类型,范例: IService</typeparam>
        /// <param name="endpointConfigName">配置文件中客户端终结点的名称</param>
        public static T CreateProxy<T>( string endpointConfigName ) where T : class {
            return CreateProxy<T>( endpointConfigName, string.Empty, string.Empty );
        }

        /// <summary>
        /// 创建客户端代理对象
        /// </summary>
        /// <typeparam name="T">操作契约类型,范例: IService</typeparam>
        /// <param name="endpointConfigName">配置文件中客户端终结点的名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public static T CreateProxy<T>( string endpointConfigName, string userName, string password ) where T : class {
            var factory = new ChannelFactory<T>( endpointConfigName );
            if ( factory.Credentials == null || string.IsNullOrWhiteSpace( userName ) || string.IsNullOrWhiteSpace( password ) )
                return factory.CreateChannel();
            factory.Credentials.UserName.UserName = userName;
            factory.Credentials.UserName.Password = password;
            return factory.CreateChannel();
        }

        /// <summary>
        /// 关闭客户端代理对象
        /// </summary>
        /// <param name="proxy">客户端代理对象</param>
        public static void CloseProxy( object proxy ) {
            var communicationObject = proxy as ICommunicationObject;
            if ( communicationObject == null )
                return;
            try {
                if ( communicationObject.State == CommunicationState.Faulted )
                    return;
                communicationObject.Close();
            }
            catch {
                communicationObject.Abort();
            }
        }
    }
}
