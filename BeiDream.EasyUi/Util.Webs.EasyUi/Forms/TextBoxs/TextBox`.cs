using System.Text;
using Util.Webs.EasyUi.Base;
using Util.Webs.EasyUi.Commons;

namespace Util.Webs.EasyUi.Forms.TextBoxs {
    /// <summary>
    /// 文本框
    /// </summary>
    /// <typeparam name="T">文本框类型</typeparam>
    public abstract class TextBox<T> : ComponentBase<T>, ITextBox<T> where T : ITextBox<T> {
        /// <summary>
        /// 初始化文本框
        /// </summary>
        protected TextBox() {
            AddClass( "easyui-textbox" );
            _validator = new Validator();
            _onchangeBuilder = new ArrayBuilder();
        }

        /// <summary>
        /// 验证器
        /// </summary>
        private readonly Validator _validator;
        /// <summary>
        /// onchange数组生成器
        /// </summary>
        private readonly ArrayBuilder _onchangeBuilder;

        /// <summary>
        /// 设置name属性
        /// </summary>
        /// <param name="name">名称</param>
        public T Name( string name ) {
            return UpdateAttribute( "name", name );
        }

        /// <summary>
        /// 设置提示消息，该消息显示在文本框中
        /// </summary>
        /// <param name="text">提示消息文本</param>
        public T Prompt( string text ) {
            return AddDataOption( "prompt", text,true );
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        public virtual T Value( string value ) {
            return AddDataOption( "value", value, true );
        }

        /// <summary>
        /// 设置为密码框
        /// </summary>
        public T Password() {
            return AddDataOption( "type", "password",true );
        }

        /// <summary>
        /// 设置为多行文本框
        /// </summary>
        /// <param name="width">文本框宽度</param>
        /// <param name="height">文本框高度</param>
        public T MultiLine( int width, int height ) {
            return AddDataOption( "multiline", true ).Width( width ).Height( height );
        }

        /// <summary>
        /// 禁用文本框
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        public T Disable( bool disabled = true ) {
            return AddDataOption( "disabled", disabled );
        }

        /// <summary>
        /// 设置文本框为只读
        /// </summary>
        /// <param name="readOnly">true为只读</param>
        public T ReadOnly( bool readOnly = true ) {
            return AddDataOption( "readonly", readOnly );
        }

        /// <summary>
        /// 设置文本框为可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        public T Editable( bool editable = true ) {
            return AddDataOption( "editable", editable );
        }

        /// <summary>
        /// 设置文本框图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        /// <param name="width">图标宽度，默认18</param>
        /// <param name="align">图标对齐方式，默认右对齐</param>
        public T Icon( string iconClass, int width = 18, AlignLeftRigth align = AlignLeftRigth.Right ) {
            return AddDataOption( "iconCls", iconClass,true )
                .AddDataOption( "iconWidth", width.ToString() )
                .AddDataOption( "iconAlign", align.Description(),true );
        }

        /// <summary>
        /// 设置文本框按钮
        /// </summary>
        /// <param name="iconClass">文本框按钮图标class</param>
        /// <param name="clickCallback">单击回调函数</param>
        /// <param name="text">按钮文本</param>
        /// <param name="align">按钮对齐方式,默认右对齐</param>
        public T Button( string iconClass, string clickCallback, string text = "", AlignLeftRigth align = AlignLeftRigth.Right ) {
            return AddDataOption( "buttonIcon", iconClass,true )
                .AddDataOption( "onClickButton", clickCallback )
                .AddDataOption( "buttonText", text,true )
                .AddDataOption( "buttonAlign", align.Description(),true );
        }

        /// <summary>
        /// 设置文本框文本改变事件处理
        /// </summary>
        /// <param name="callback">文本改变回调函数，只设置函数名，范例：func</param>
        public T OnChange( string callback ) {
            _onchangeBuilder.Add( callback );
            return This();
        }

        /// <summary>
        /// 设置延迟验证的时间
        /// </summary>
        /// <param name="time">延迟验证的时间，单位：毫秒</param>
        public T Delay( int time ) {
            return AddDataOption( "delay", time.ToString() );
        }

        /// <summary>
        /// 设置提示位置
        /// </summary>
        /// <param name="align">提示位置</param>
        public T TipPosition( AlignLeftRigth align ) {
            return AddDataOption( "tipPosition", align.Description() ,true );
        }

        /// <summary>
        /// 设置关闭验证
        /// </summary>
        public T NoValidate() {
            return AddDataOption( "novalidate", true );
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        public T Required( bool isRequired = true ) {
            _validator.Required( isRequired );
            return This();
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        public T Required( string message ) {
            _validator.Required( message );
            return This();
        }

        /// <summary>
        /// 设置Email验证
        /// </summary>
        public T Email() {
            _validator.Email();
            return This();
        }

        /// <summary>
        /// 设置手机号验证
        /// </summary>
        public T MobilePhone() {
            _validator.MobilePhone();
            return This();
        }

        /// <summary>
        /// 设置Url验证
        /// </summary>
        public T ValidateUrl() {
            _validator.Url();
            return This();
        }

        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public T Length( int minLength, int maxLength ) {
            _validator.Length( minLength,maxLength );
            return This();
        }

        /// <summary>
        /// 设置最小长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        public T MinLength( int minLength ) {
            _validator.MinLength( minLength );
            return This();
        }

        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public T MaxLength( int maxLength ) {
            _validator.MaxLength( maxLength );
            return This();
        }

        /// <summary>
        /// 设置远程验证
        /// </summary>
        /// <param name="url">远程url</param>
        /// <param name="parameterName">参数名</param>
        public T Remote( string url, string parameterName ) {
            _validator.Remote( url,parameterName );
            return This();
        }

        /// <summary>
        /// 设置相等验证
        /// </summary>
        /// <param name="targetId">目标元素Id</param>
        /// <param name="message">消息</param>
        public T EqualTo( string targetId, string message = "" ) {
            _validator.EqualTo( targetId,message );
            return This();
        }

        /// <summary>
        /// 设置最大值验证
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="message">消息</param>
        public T Max( double maxValue, string message = "" ) {
            _validator.Max( maxValue,message );
            return This();
        }

        /// <summary>
        /// 设置最小值验证
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="message">消息</param>
        public T Min( double minValue, string message = "" ) {
            _validator.Min( minValue,message );
            return This();
        }

        /// <summary>
        /// 设置数值范围验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="message">消息</param>
        public T Range( double min, double max, string message = "" ) {
            _validator.Range( min,max,message );
            return This();
        }

        /// <summary>
        /// 显示日期框
        /// </summary>
        public T Date() {
            return UpdateClass( "easyui-datebox" ).Editable( false );
        }

        /// <summary>
        /// 显示日期时间框
        /// </summary>
        public T DateTime() {
            return UpdateClass( "easyui-datetimebox" ).Editable( false );
        }

        /// <summary>
        /// 显示时间框
        /// </summary>
        public T Time() {
            return UpdateClass( "easyui-timespinner" );
        }

        /// <summary>
        /// 设置为数值文本框
        /// </summary>
        /// <param name="precision">精度，即小数位数</param>
        public virtual T Number( int precision ) {
            if ( precision < 0 )
                precision = 0;
            return UpdateClass( "easyui-numberbox" ).AddDataOption( "precision", precision.ToString() );
        }

        /// <summary>
        /// 设置为整数文本框
        /// </summary>
        public virtual T Int() {
            return Number( 0 );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            AddValidations();
            AddEvents();
            var result = new StringBuilder();
            result.AppendFormat( "<input {0}/>", GetOptions() );
            return result.ToString();
        }

        /// <summary>
        /// 添加验证
        /// </summary>
        protected void AddValidations() {
            AddDataOption( _validator.GetResult() );
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        protected void AddEvents() {
            _onchangeBuilder.Method = "$.easyui.textbox_onChange";
            AddDataOption( "onChange", _onchangeBuilder.GetResult() );
        }
    }
}
