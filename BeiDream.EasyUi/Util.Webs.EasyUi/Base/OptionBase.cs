namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 基选项
    /// </summary>
    public abstract class OptionBase<T> : IOption<T> where T : IOption<T> {
        /// <summary>
        /// 初始化基选项
        /// </summary>
        protected OptionBase() {
            _builder = new EasyUiAttributeBuilder();
        }

        /// <summary>
        /// 属性生成器
        /// </summary>
        private readonly EasyUiAttributeBuilder _builder;

        /// <summary>
        /// 获取属性生成器
        /// </summary>
        protected EasyUiAttributeBuilder GetAttributeBuilder() {
            return _builder;
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        public virtual T Width( int? width, bool isPercent = false ) {
            string value = width.ToStr();
            if ( isPercent && width != null )
                value = string.Format( "'{0}%'", value );
            return AddDataOption( "width", value );
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public virtual T Height( int height ) {
            return AddDataOption( "height", height );
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public T AddAttribute( string name, string value ) {
            _builder.Add( name, value );
            return This();
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public T AddAttribute( string name, bool value ) {
            _builder.Add( name, value.ToString().ToLower() );
            return This();
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public T UpdateAttribute( string name, string value ) {
            _builder.Update( name, value );
            return This();
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        public T AddStyle( string name, string value ) {
            _builder.AddStyle( name, value );
            return This();
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        public T AddClass( string @class ) {
            _builder.AddClass( @class );
            return This();
        }

        /// <summary>
        /// 更新class属性
        /// </summary>
        /// <param name="class">class属性</param>
        protected T UpdateClass( string @class ) {
            _builder.UpdateClass( @class );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        /// <param name="isAddQuote">是否为值添加引号</param>
        public T AddDataOption( string name, string value,bool isAddQuote = false ) {
            if ( value.IsEmpty() )
                return This();
            _builder.AddDataOption( name, value, isAddQuote );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, bool value ) {
            _builder.AddDataOption( name, value );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, bool? value ) {
            _builder.AddDataOption( name, value );
            return This();
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, int value ) {
            return AddDataOption( name, value.ToString() );
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public T AddDataOption( string name, int? value ) {
            return AddDataOption( name, value.ToStr() );
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="option">项</param>
        public T AddDataOption( string option ) {
            _builder.AddDataOption( option );
            return This();
        }

        /// <summary>
        /// 返回组件
        /// </summary>
        protected T This() {
            return (T)( (object)this );
        }

        /// <summary>
        /// 获取选项
        /// </summary>
        protected string GetOptions() {
            return _builder.GetResult();
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="value">外边距值</param>
        public T Margin( int value ) {
            return AddStyle( "margin", string.Format( "{0}px", value ) );
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public T Margin( int topBottom, int leftRight ) {
            return AddStyle( "margin", string.Format( "{0}px {1}px", topBottom, leftRight ) );
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public T Margin( int top, int right, int bottom, int left ) {
            return AddStyle( "margin", string.Format( "{0}px {1}px {2}px {3}px", top, right,bottom,left ) );
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="value">内边距值</param>
        public T Padding( int value ) {
            return AddStyle( "padding", string.Format( "{0}px", value ) );
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public T Padding( int topBottom, int leftRight ) {
            return AddStyle( "padding", string.Format( "{0}px {1}px", topBottom, leftRight ) );
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public T Padding( int top, int right, int bottom, int left ) {
            return AddStyle( "padding", string.Format( "{0}px {1}px {2}px {3}px", top, right, bottom, left ) );
        }
    }
}
