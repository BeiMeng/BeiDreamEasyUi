using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Commons {
    /// <summary>
    /// 验证
    /// </summary>
    public interface IValidation<out T> : IComponent<T> where T : IComponent<T> {
        /// <summary>
        /// 设置必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        T Required( string message );
        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        T MaxLength( int maxLength );
        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        T Length( int minLength, int maxLength );
        /// <summary>
        /// 设置为日期控件，不带时间
        /// </summary>
        T Date();
        /// <summary>
        /// 设置为数值文本控件，只能输入数值
        /// </summary>
        /// <param name="precision">精度，即小数位数</param>
        T Number( int precision );
        /// <summary>
        /// 设置为整数文本控件，只能输入整数
        /// </summary>
        T Int();
        /// <summary>
        /// 设置Email验证
        /// </summary>
        T Email();
        /// <summary>
        /// 设置手机号验证
        /// </summary>
        T MobilePhone();
        /// <summary>
        /// 设置Url验证
        /// </summary>
        T ValidateUrl();
    }
}
