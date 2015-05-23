using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Webs.EasyUi.Base;
using Util.Webs.EasyUi.Commons;
using Util.Webs.EasyUi.Configs;
using Util.Webs.EasyUi.Forms.Comboxs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 表格列
    /// </summary>
    public class DataGridColumn : ComponentBase<IDataGridColumn>, IDataGridColumn {
        /// <summary>
        /// 初始化表格列
        /// </summary>
        /// <param name="isEdit">是否允许编辑</param>
        public DataGridColumn( bool isEdit = false ) {
            _validator = new Validator();
            _builder = new JsonAttributeBuilder();
            _isEdit = isEdit;
        }

        /// <summary>
        /// 文本
        /// </summary>
        private string _text;
        /// <summary>
        /// 是否冻结
        /// </summary>
        private bool _isFrozen;
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        private bool _isEdit;
        /// <summary>
        /// 编辑控件类型
        /// </summary>
        private string _editorType;
        /// <summary>
        /// 验证器
        /// </summary>
        private readonly Validator _validator;
        /// <summary>
        /// Json属性生成器
        /// </summary>
        private readonly JsonAttributeBuilder _builder;

        /// <summary>
        /// 设置字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public IDataGridColumn Field( string fieldName ) {
            return AddDataOption( "field", fieldName, true );
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        public IDataGridColumn Text( string text ) {
            _text = text;
            return This();
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="headerAlign">标题对齐方式</param>
        /// <param name="align">内容对齐方式</param>
        public IDataGridColumn Align( AlignLeftRigthCenter headerAlign = AlignLeftRigthCenter.Center, AlignLeftRigthCenter align = AlignLeftRigthCenter.Left ) {
            return AddDataOption( "halign", headerAlign.Description(), true ).AddDataOption( "align", align.Description(), true );
        }

        /// <summary>
        /// 是否允许排序
        /// </summary>
        /// <param name="isSort">是否允许排序</param>
        public IDataGridColumn Sort( bool isSort = true ) {
            return AddDataOption( "sortable", isSort );
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        /// <param name="isShow">是否显示复选框</param>
        public IDataGridColumn CheckBox( bool isShow = true ) {
            if ( _isEdit ) {
                _editorType = "checkbox";
                _builder.Add( "on", "1" );
                _builder.Add( "off", "0" );
            }
            else
                AddDataOption( "checkbox", isShow );
            return This();
        }

        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        public IDataGridColumn Combox( string url, string valueField = "value", string textField = "text", string groupField = "group" ) {
            if ( !_isEdit )
                return This();
            _builder.Add( "url", url, "'" );
            AddComboxParams( valueField, textField, groupField );
            _builder.Add( "onBeforeLoad", string.Format( "$.easyui.loadGridColumnCombox_onBeforeLoad('{0}')",url ) );
            return Format( string.Format( "$.easyui.formatComboxFromUrl('{0}','{1}','{2}')", url, valueField, textField ) );
        }

        /// <summary>
        /// 添加下拉列表参数
        /// </summary>
        private void AddComboxParams( string valueField = "value", string textField = "text", string groupField = "group" ) {
            _editorType = "combobox";
            _builder.Add( "valueField", valueField, "'" );
            _builder.Add( "textField", textField, "'" );
            _builder.Add( "groupField", groupField, "'" );
        }

        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="items">项集合</param>
        public IDataGridColumn Combox( IEnumerable<ComboxItem> items ) {
            if ( !_isEdit )
                return This();
            _builder.Add( "data", Json.ToJson( items,true ) );
            AddComboxParams();
            return Format( string.Format( "$.easyui.formatCombox({0},'value','text')", Json.ToJson( items, true ) ) );
        }

        /// <summary>
        /// 绑定枚举下拉列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public IDataGridColumn Combox<TEnum>() {
            return Combox( Enum.GetItems<TEnum>().Select( t => new ComboxItem( t.Text,t.Value ) ) );
        }

        /// <summary>
        /// 显示下拉树
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"id"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        public IDataGridColumn ComboTree( string url, string valueField = "id", string textField = "text" ) {
            if ( !_isEdit )
                return This();
            _editorType = "combotree";
            _builder.Add( "url", url, "'" );
            _builder.Add( "onBeforeLoad", string.Format( "$.easyui.loadGridColumnComboTree_onBeforeLoad('{0}')", url ) );
            return Format( string.Format( "$.easyui.formatComboxFromUrl('{0}','{1}','{2}')", url, valueField, textField ) );
        }

        /// <summary>
        /// 设置面板高度，即下拉列表的高度
        /// </summary>
        /// <param name="height">面板高度，值为"auto"为自适应，也可以指定高度，范例"100"</param>
        public IDataGridColumn PanelHeight( string height = "auto" ) {
            if ( !_isEdit )
                return This();
            _builder.Add( "panelHeight", height, "'" );
            return This();
        }

        /// <summary>
        /// 设置是否可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        public IDataGridColumn Editable( bool editable = true ) {
            if ( !_isEdit )
                return This();
            _builder.Add( "editable", editable.ToString().ToLower() );
            return This();
        }

        /// <summary>
        /// 设置格式化
        /// </summary>
        /// <param name="fn">格式化函数</param>
        public IDataGridColumn Format( string fn ) {
            return AddDataOption( "formatter", fn );
        }

        /// <summary>
        /// 格式化布尔值
        /// </summary>
        public IDataGridColumn FormatBool() {
            return Format( "$.easyui.formatBool" );
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        public IDataGridColumn FormatDate() {
            return Format( "$.easyui.formatDate" );
        }

        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="isClass">值是否为class</param>
        public IDataGridColumn FormatImage( int width = 16, int height = 16, bool isClass = false ) {
            return Format( string.Format( "$.easyui.formatImage({0},{1},{2})", width, height, isClass.ToString().ToLower() ) );
        }

        /// <summary>
        /// 冻结列
        /// </summary>
        public IDataGridColumn Frozen() {
            _isFrozen = true;
            return This();
        }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public IDataGridColumn Edit() {
            _isEdit = true;
            return This();
        }

        /// <summary>
        /// 验证必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        public IDataGridColumn Required( bool isRequired = true ) {
            _validator.Required( isRequired );
            return This();
        }

        /// <summary>
        /// 验证必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        public IDataGridColumn Required( string message ) {
            _validator.Required( message );
            return This();
        }

        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public IDataGridColumn Length( int minLength, int maxLength ) {
            _validator.Length( minLength, maxLength );
            return This();
        }

        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public IDataGridColumn MaxLength( int maxLength ) {
            _validator.MaxLength( maxLength );
            return This();
        }

        /// <summary>
        /// 显示日期框
        /// </summary>
        public IDataGridColumn Date() {
            _editorType = "datebox";
            _builder.Add( "editable", "false" );
            return This();
        }

        /// <summary>
        /// 设置为数值文本框
        /// </summary>
        /// <param name="precision">精度，即小数位数</param>
        public IDataGridColumn Number( int precision ) {
            _editorType = "numberbox";
            if ( precision < 0 )
                precision = 0;
            _builder.Add( "precision", precision.ToString() );
            return This();
        }

        /// <summary>
        /// 查找带回
        /// </summary>
        /// <param name="option">查找带回配置选项</param>
        public IDataGridColumn Lookup( LookupOption option ) {
            _editorType = "lookup";
            _builder.Add( Json.ToJsonWithoutBrackets( option,true ) );
            return This();
        }

        /// <summary>
        /// 设置为整数文本框
        /// </summary>
        public IDataGridColumn Int() {
            return Number( 0 );
        }

        /// <summary>
        /// 设置Email验证
        /// </summary>
        public IDataGridColumn Email() {
            _validator.Email();
            return This();
        }

        /// <summary>
        /// 设置手机号验证
        /// </summary>
        public IDataGridColumn MobilePhone() {
            _validator.MobilePhone();
            return This();
        }

        /// <summary>
        /// 设置Url验证
        /// </summary>
        public IDataGridColumn ValidateUrl() {
            _validator.Url();
            return This();
        }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFrozen {
            get { return _isFrozen; }
        }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool IsEdit {
            get { return _isEdit; }
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            var result = new StringBuilder();
            InitEditor();
            return GetResult( result );
        }

        /// <summary>
        /// 初始化编辑器
        /// </summary>
        private void InitEditor() {
            if ( !IsEdit )
                return;
            AddDataOption( "editor", GetEditor() );
        }

        /// <summary>
        /// 获取编辑器
        /// </summary>
        private string GetEditor() {
            var result = new StringBuilder();
            var options = GetEditorOptions();
            result.Append( "{" );
            result.AppendFormat( "type:'{0}'", GetEditorType( options ) );
            GetEditorOptions( result, options );
            result.Append( "}" );
            return result.ToString();
        }

        /// <summary>
        /// 获取编辑器选项
        /// </summary>
        private string GetEditorOptions() {
            _builder.Add( _validator.GetResult() );
            return _builder.GetResult();
        }

        /// <summary>
        /// 获取编辑器类型
        /// </summary>
        private string GetEditorType( string options ) {
            if ( !_editorType.IsEmpty() )
                return _editorType;
            if ( !options.IsEmpty() )
                return "validatebox";
            return "text";
        }

        /// <summary>
        /// 获取验证选项
        /// </summary>
        private void GetEditorOptions( StringBuilder result, string options ) {
            if ( options.IsEmpty() )
                return;
            result.Append( ",options:{" );
            result.Append( options );
            result.Append( "}" );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        private string GetResult( StringBuilder result ) {
            result.AppendFormat( "<th {0}>", GetOptions() );
            result.Append( _text );
            result.Append( "</th>" );
            return result.ToString();
        }
    }
}
