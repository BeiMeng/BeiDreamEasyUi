using System.Collections.Generic;
using Util.Webs.EasyUi.Configs;
using Util.Webs.EasyUi.Forms.Comboxs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 子表格列
    /// </summary>
    public class SubGridColumn : ISubGridColumn {
        /// <summary>
        /// 初始化子表格列
        /// </summary>
        public SubGridColumn() {
            _option = new DataGridColumnOption();
        }

        /// <summary>
        /// 表格列配置项
        /// </summary>
        private readonly DataGridColumnOption _option;

        /// <summary>
        /// 获取列配置项
        /// </summary>
        public DataGridColumnOption GetOption() {
            return _option;
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public string ToHtmlString() {
            return ToString();
        }

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return _option.ToString();
        }

        /// <summary>
        /// 设置必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        public ISubGridColumn Required( string message ) {
            return this;
        }

        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public ISubGridColumn MaxLength( int maxLength ) {
            return this;
        }

        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public ISubGridColumn Length( int minLength, int maxLength ) {
            return this;
        }

        /// <summary>
        /// 设置为日期控件，不带时间
        /// </summary>
        public ISubGridColumn Date() {
            return this;
        }

        /// <summary>
        /// 设置为数值文本控件，只能输入数值
        /// </summary>
        /// <param name="precision">精度，即小数位数</param>
        public ISubGridColumn Number( int precision ) {
            return this;
        }

        /// <summary>
        /// 设置为整数文本控件，只能输入整数
        /// </summary>
        public ISubGridColumn Int() {
            return this;
        }

        /// <summary>
        /// 设置字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public ISubGridColumn Field( string fieldName ) {
            _option.Field = fieldName;
            return this;
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        public ISubGridColumn Text( string text ) {
            _option.Title = text;
            return this;
        }

        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="headerAlign">标题对齐方式</param>
        /// <param name="align">内容对齐方式</param>
        public ISubGridColumn Align( AlignLeftRigthCenter headerAlign = AlignLeftRigthCenter.Center, AlignLeftRigthCenter align = AlignLeftRigthCenter.Left ) {
            _option.HeaderAlign = headerAlign.Description();
            _option.Align = align.Description();
            return this;
        }

        /// <summary>
        /// 是否允许排序
        /// </summary>
        /// <param name="isSort">是否允许排序</param>
        public ISubGridColumn Sort( bool isSort = true ) {
            return this;
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        /// <param name="isShow">是否显示复选框</param>
        public ISubGridColumn CheckBox( bool isShow = true ) {
            _option.CheckBox = isShow;
            return this;
        }

        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        public ISubGridColumn Combox( string url, string valueField = "value", string textField = "text", string groupField = "group" ) {
            return this;
        }

        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="items">项集合</param>
        public ISubGridColumn Combox( IEnumerable<ComboxItem> items ) {
            return this;
        }

        /// <summary>
        /// 绑定枚举下拉列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public ISubGridColumn Combox<TEnum>() {
            return this;
        }

        /// <summary>
        /// 显示下拉树
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"id"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        public ISubGridColumn ComboTree( string url, string valueField = "id", string textField = "text" ) {
            return this;
        }

        /// <summary>
        /// 设置面板高度，即下拉列表的高度
        /// </summary>
        /// <param name="height">面板高度，值为"auto"为自适应，也可以指定高度，范例"100"</param>
        public ISubGridColumn PanelHeight( string height = "auto" ) {
            return this;
        }

        /// <summary>
        /// 设置是否可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        public ISubGridColumn Editable( bool editable = true ) {
            return this;
        }

        /// <summary>
        /// 设置格式化
        /// </summary>
        /// <param name="fn">格式化函数</param>
        public ISubGridColumn Format( string fn ) {
            _option.Formatter = fn;
            return this;
        }

        /// <summary>
        /// 格式化布尔值
        /// </summary>
        public ISubGridColumn FormatBool() {
            return Format( "$.easyui.formatBool" );
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        public ISubGridColumn FormatDate() {
            return Format( "$.easyui.formatDate" );
        }

        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="isClass">值是否为class</param>
        public ISubGridColumn FormatImage( int width = 16, int height = 16, bool isClass = false ) {
            return this;
        }

        /// <summary>
        /// 获取标识
        /// </summary>
        public string GetId() {
            return string.Empty;
        }

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public ISubGridColumn Id( string id ) {
            return this;
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        public ISubGridColumn Width( int? width, bool isPercent = false ) {
            _option.Width = width.ToStr();
            if ( isPercent )
                _option.Width = width.SafeValue() + "%";
            return this;
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public ISubGridColumn Height( int height ) {
            _option.Height = height;
            return this;
        }

        /// <summary>
        /// 冻结列
        /// </summary>
        public ISubGridColumn Frozen() {
            return this;
        }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public ISubGridColumn Edit() {
            return this;
        }

        /// <summary>
        /// 验证必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        public ISubGridColumn Required( bool isRequired = true ) {
            return this;
        }

        /// <summary>
        /// 设置Email验证
        /// </summary>
        public ISubGridColumn Email() {
            return this;
        }

        /// <summary>
        /// 设置手机号验证
        /// </summary>
        public ISubGridColumn MobilePhone() {
            return this;
        }

        /// <summary>
        /// 设置Url验证
        /// </summary>
        public ISubGridColumn ValidateUrl() {
            return this;
        }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFrozen { get; private set; }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool IsEdit { get; private set; }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public ISubGridColumn AddAttribute( string name, string value ) {
            return this;
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        public ISubGridColumn AddStyle( string name, string value ) {
            return this;
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        public ISubGridColumn AddClass( string @class ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        /// <param name="isAddQuote">是否为值添加引号</param>
        public ISubGridColumn AddDataOption( string name, string value, bool isAddQuote = false ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public ISubGridColumn AddDataOption( string name, bool value ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public ISubGridColumn AddDataOption( string name, bool? value ) {
            return this;
        }

        /// <summary>
        /// 在控件后添加html
        /// </summary>
        /// <param name="html">Html</param>
        public ISubGridColumn AddAfter( string html ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="value">外边距值</param>
        public ISubGridColumn Margin( int value ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public ISubGridColumn Margin( int topBottom, int leftRight ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public ISubGridColumn Margin( int top, int right, int bottom, int left ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="value">内边距值</param>
        public ISubGridColumn Padding( int value ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public ISubGridColumn Padding( int topBottom, int leftRight ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public ISubGridColumn Padding( int top, int right, int bottom, int left ) {
            return this;
        }

        /// <summary>
        /// 查找带回
        /// </summary>
        /// <param name="option">查找带回配置选项</param>
        public ISubGridColumn Lookup( LookupOption option ) {
            return this;
        }
    }
}
