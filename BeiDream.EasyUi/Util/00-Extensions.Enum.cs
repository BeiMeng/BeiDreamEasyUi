namespace Util {
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description( this System.Enum instance ) {
            return Enum.GetDescription( instance.GetType(), instance );
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value( this System.Enum instance ) {
            return Enum.GetValue( instance.GetType(), instance );
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        public static T Value<T>( this System.Enum instance ) {
            return Conv.To<T>( Value( instance ) );
        }
    }
}
