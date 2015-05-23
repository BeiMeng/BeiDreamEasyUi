namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 选项
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public interface IOption<out T> where T : IOption<T> {
        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        T Width( int? width, bool isPercent = false );
        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        T Height( int height );
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        T AddAttribute( string name, string value );
        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        T AddStyle( string name, string value );
        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        T AddClass( string @class );
        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        /// <param name="isAddQuote">是否为值添加引号</param>
        T AddDataOption( string name, string value, bool isAddQuote = false );
        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        T AddDataOption( string name, bool value );
        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        T AddDataOption( string name, bool? value );
        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="value">外边距值</param>
        T Margin( int value );
        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        T Margin( int topBottom,int leftRight );
        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        T Margin( int top, int right,int bottom,int left );
        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="value">内边距值</param>
        T Padding( int value );
        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        T Padding( int topBottom, int leftRight );
        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        T Padding( int top, int right, int bottom, int left );
    }
}
