using System;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Commons {
    /// <summary>
    /// 延迟设置值
    /// </summary>
    public class LazyValue {
        /// <summary>
        /// 初始化
        /// </summary>
        private LazyValue() {
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="control">控件</param>
        /// <param name="value">值</param>
        /// <param name="handler">回调函数</param>
        public static T SetValue<T>( IComponent<T> control, string value,string handler ) where T : IComponent<T> {
            if ( control.GetId().IsEmpty() )
                throw new ArgumentException( "设置LazyValue前必须设置Id" );
            return control.AddAttribute( "lazyValue", value ).AddDataOption( "onLoadSuccess", string.Format( "{0}('{1}')",handler, control.GetId() ) );
        }
    }
}
