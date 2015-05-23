namespace Util.Webs.EasyUi {
    /// <summary>
    /// EasyUi属性生成器
    /// </summary>
    public class EasyUiAttributeBuilder : AttributeBuilder {
        /// <summary>
        /// 初始化EasyUi属性生成器
        /// </summary>
        public EasyUiAttributeBuilder() {
            _dataOptionBuilder = new AttributeBuilder( ":", "," );
        }

        /// <summary>
        /// data-options属性生成器
        /// </summary>
        private readonly AttributeBuilder _dataOptionBuilder;

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        /// <param name="isAddQuote">是否给值添加引号</param>
        public void AddDataOption( string name, string value, bool isAddQuote = false ) {
            _dataOptionBuilder.Update( name, value, "", GetQuotes( isAddQuote ) );
            Update( "data-options", _dataOptionBuilder.GetResult() );
        }

        /// <summary>
        /// 获取值
        /// </summary>
        private string GetQuotes( bool isAddQuote ) {
            return isAddQuote ? "'" : "";
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public void AddDataOption( string name, bool value ) {
            AddDataOption( name, value.ToString().ToLower() );
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="option">项</param>
        public void AddDataOption( string option ) {
            if ( option.IsEmpty() )
                return;
            Add( "data-options", option, "," );
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public void AddDataOption( string name, bool? value ) {
            if ( value == null )
                return;
            AddDataOption( name, value.SafeValue() );
        }
    }
}
