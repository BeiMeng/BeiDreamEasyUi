using System;

namespace Util.Contexts {
    /// <summary>
    /// Windows上下文
    /// </summary>
    internal class WindowsContext : IContext {
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">对象</param>
        public void Add<T>( string key, T value ) {
            LocalDataStoreSlot slot = System.Threading.Thread.GetNamedDataSlot( key );
            System.Threading.Thread.SetData( slot, value );
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        public T Get<T>( string key ) {
            LocalDataStoreSlot slot = System.Threading.Thread.GetNamedDataSlot( key );
            return (T)System.Threading.Thread.GetData( slot );
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove( string key ) {
            LocalDataStoreSlot slot = System.Threading.Thread.GetNamedDataSlot( key );
            System.Threading.Thread.SetData( slot, null );
        }
    }
}
