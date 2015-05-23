using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Webs.EasyUi.Forms.TextBoxs;

namespace Util.Webs.EasyUi.Forms.Comboxs {
    /// <summary>
    /// 组合框
    /// </summary>
    public class Combox<T> : Combo<T>,ICombox<T> where T : ICombox<T> {
        /// <summary>
        /// 初始化组合框
        /// </summary>
        public Combox() {
            _items = new List<ComboxItem>();
            UpdateClass( "easyui-combobox" ).Editable( false ).PanelHeight();
        }

        /// <summary>
        /// 组合框选项集合
        /// </summary>
        private readonly List<ComboxItem> _items;
        /// <summary>
        /// 选中的值
        /// </summary>
        private object _selectedValue;
        /// <summary>
        /// 是否远程加载
        /// </summary>
        protected bool IsLoad;

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public T Add( string text, object value = null ) {
            _items.Add( new ComboxItem( text,value ) );
            return This();
        }

        /// <summary>
        /// 添加项集合
        /// </summary>
        /// <param name="items">项集合</param>
        public T Add( IEnumerable<ComboxItem> items ) {
            if ( items == null )
                return This();
            foreach( var item in items )
                _items.Add( item );
            return This();
        }

        /// <summary>
        /// 添加默认项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public T AddDefault( string text, object value = null ) {
            _items.Insert( 0,new ComboxItem( text,value ) );
            return This();
        }

        /// <summary>
        /// 选中项
        /// </summary>
        /// <param name="value">值</param>
        public override T Value( string value ) {
            _selectedValue = value;
            return This();
        }

        /// <summary>
        /// 绑定bool值
        /// </summary>
        public T Bool() {
            return Add( "否", "false" ).Add( "是", "true" ).Width( 50 );
        }

        /// <summary>
        /// 绑定bool值
        /// </summary>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        public T Bool( string text, object value = null ) {
            Add( text, value );
            return Bool();
        }

        /// <summary>
        /// 设置为数值文本框
        /// </summary>
        /// <param name="precision">精度，即小数位数</param>
        public override T Number( int precision ) {
            return This();
        }

        /// <summary>
        /// 设置为整数文本框
        /// </summary>
        public override T Int() {
            return This();
        }

        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public T Enum<TEnum>() {
            return Add( Util.Enum.GetItems<TEnum>().Select( t => new ComboxItem( t.Text, t.Value ) ) );
        }

        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        public T Enum<TEnum>( string text, object value = null ) {
            Add( text, value );
            return Enum<TEnum>();
        }

        /// <summary>
        /// 从远程加载数据
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        public T Load( string url, string valueField = "value", string textField = "text",string groupField = "group" ) {
            if ( string.IsNullOrWhiteSpace( url ) )
                return This();
            IsLoad = true;
            return AddDataOption( "url", url,true )
                .AddDataOption( "valueField", valueField,true )
                .AddDataOption( "textField", textField,true )
                .AddDataOption( "groupField", groupField,true );
        }

        /// <summary>
        /// 设置联动子控件
        /// </summary>
        /// <param name="childId">联动子控件</param>
        /// <param name="requestParam">请求参数</param>
        /// <param name="url">子控件加载url</param>
        public T Child( string childId, string requestParam = "id", string url = "" ) {
            var comboxParams = GetChildParams( childId, requestParam, url );
            return OnChange( string.Format( "$.easyui.setChildCombox_onChange({0})", comboxParams ) );
        }

        /// <summary>
        /// 获取联动子控件参数
        /// </summary>
        private string GetChildParams( string childId, string requestParam, string url ) {
            var builder = new ParamBuilder();
            builder.Add( childId,"''", true );
            builder.Add( requestParam, true );
            builder.Add( url, true );
            return builder.GetResult();
        }

        /// <summary>
        /// 延迟设置值，当数据加载完成时设置
        /// </summary>
        /// <param name="value">值</param>
        public T LazyValue( string value ) {
            return Commons.LazyValue.SetValue( this, value, "$.easyui.setComboxLazyValue_onLoadSuccess" );
        }

        /// <summary>
        /// 添加隐藏控件，用于保存文本
        /// </summary>
        /// <param name="hiddenName">hidden控件name</param>
        /// <param name="text">显示文本</param>
        public T Hidden( string hiddenName, string text ) {
            return OnChange( string.Format( "$.easyui.setComboxHiddenText_onChange('{0}')", hiddenName ) )
                .AddAfter( string.Format( "<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", hiddenName, text ) );
        }

        /// <summary>
        /// 从远程加载数据，该方法将Load和LazyValue方法合并，提供一个更易用的操作
        /// </summary>
        /// <param name="url">远程Url，服务端请使用ToComboxResult方法返回数据</param>
        /// <param name="value">值</param>
        public T Url( string url, string value ) {
            return Load( url ).LazyValue( value );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            AddValidations();
            AddEvents();
            var result = new StringBuilder();
            if ( IsLoad )
                RenderInput( result );
            else 
                RenderSelect( result );
            return result.ToString();
        }

        /// <summary>
        /// 渲染为input
        /// </summary>
        private void RenderInput( StringBuilder result ) {
            result.AppendFormat( "<input {0}/>",GetOptions() );
        }

        /// <summary>
        /// 渲染为select
        /// </summary>
        private void RenderSelect( StringBuilder result ) {
            result.AppendFormat( "<select {0}>", GetOptions() );
            GetOptions( result );
            result.Append( "</select>" );
        }

        /// <summary>
        /// 获取项集合
        /// </summary>
        private void GetOptions( StringBuilder result ) {
            foreach ( var item in _items )
                GetOption( result, item );
        }

        /// <summary>
        /// 获取项
        /// </summary>
        private void GetOption( StringBuilder result, ComboxItem item ) {
            if ( IsSelect( item ) )
                result.AppendFormat( "<option{0} selected=\"selected\">{1}</option>", GetValueHtml(item.Value), item.Text );
            else
                result.AppendFormat( "<option{0}>{1}</option>", GetValueHtml( item.Value ), item.Text );
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        private bool IsSelect( ComboxItem item ) {
            if ( _selectedValue == null )
                return false;
            if ( item.Value == null )
                return false;
            if ( _selectedValue.ToString().Trim().ToLower() == item.Value.ToString().Trim().ToLower() )
                return true;
            return false;
        }

        /// <summary>
        /// 获取值Html属性
        /// </summary>
        private string GetValueHtml( object value ) {
            if ( value == null )
                return string.Empty;
            return string.Format( " value=\"{0}\"", value );
        }

        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <param name="items">组合框项集合</param>
        public static string ToJson( IEnumerable<ComboxItem> items ) {
            return Json.ToJson( items );
        }
    }
}
