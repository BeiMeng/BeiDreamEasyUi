using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Util.Webs.EasyUi.Buttons;
using Util.Webs.EasyUi.Forms;
using Util.Webs.EasyUi.Forms.ComboTrees;
using Util.Webs.EasyUi.Forms.Comboxs;
using Util.Webs.EasyUi.Forms.TextBoxs;
using Util.Webs.EasyUi.Grids;
using Util.Webs.EasyUi.Layouts;
using Util.Webs.EasyUi.Menus;
using Util.Webs.EasyUi.Paginations;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi.Services {
    /// <summary>
    /// EasyUi服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class EasyUiService<TEntity> : IEasyUiService<TEntity> {
        /// <summary>
        /// 初始化EasyUi服务
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        public EasyUiService( HtmlHelper<TEntity> helper ) {
            _helper = helper;
        }

        /// <summary>
        /// HtmlHelper
        /// </summary>
        private readonly HtmlHelper<TEntity> _helper;

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="fit">是否自适应</param>
        public ILayout Layout( bool fit = false ) {
            return EasyUiFactory<TEntity>.CreateLayout( _helper, fit );
        }

        /// <summary>
        /// 布局区域
        /// </summary>
        public ILayoutRegion LayoutRegion() {
            return EasyUiFactory<TEntity>.CreateLayoutRegion( _helper ).Border( false ); 
        }

        /// <summary>
        /// 面板
        /// </summary>
        public IPanel Panel() {
            return EasyUiFactory<TEntity>.CreatePanel( _helper ); 
        }

        /// <summary>
        /// 选项卡
        /// </summary>
        public ITabs Tabs() {
            return EasyUiFactory<TEntity>.CreateTabs( _helper ); 
        }

        /// <summary>
        /// 选项卡面板
        /// </summary>
        /// <param name="title">标题</param>
        public ITabPanel TabPanel( string title ) {
            return EasyUiFactory<TEntity>.CreateTabPanel( _helper ).Title( title );
        }

        /// <summary>
        /// 表单
        /// </summary>
        /// <param name="id">Id</param>
        public IForm Form( string id ) {
            return EasyUiFactory<TEntity>.CreateForm( _helper ).Id( id ); 
        }

        /// <summary>
        /// 按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public IButton Button( string text ) {
            return EasyUiFactory.CreateButton( text );
        }

        /// <summary>
        /// 弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        public IDialogButton DialogButton( string text, string url = "" ) {
            return EasyUiFactory.CreateDialogButton( text, url );
        }

        /// <summary>
        /// 文本框
        /// </summary>
        public ITextBox TextBox() {
            return EasyUiFactory.CreateTextBox();
        }

        /// <summary>
        /// 组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        public ICombox Combox( string name ) {
            return EasyUiFactory.CreateCombox().Name( name );
        }

        /// <summary>
        /// 组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        public ICombox Combox( string name, string id, string url, string value ) {
            return EasyUiFactory.CreateCombox( name, id, url, value );
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="id">Id</param>
        public IMenu Menu( string id ) {
            return EasyUiFactory.CreateMenu( id );
        }

        /// <summary>
        /// 菜单项
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="text">文本</param>
        /// <param name="iconClass">图标class</param>
        public IMenuItem MenuItem( string id = "", string text = "", string iconClass = "" ) {
            return EasyUiFactory.CreateMenuItem().Id( id ).Text( text ).Icon( iconClass );
        }

        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="id">Id</param>
        public IDataGrid Grid( string id = "" ) {
            return EasyUiFactory.CreateDataGrid().Id( id ).RowNumber()
                .Pagination().CheckOnSelect( false ).SelectOnCheck( false ).SingleSelect().Strip();
        }

        /// <summary>
        /// 子表格
        /// </summary>
        /// <param name="property">集合属性，范例：如果对象包含A元素集合List1，A元素包含集合List2，展示List2使用"List2"，不要写成"List1.List2"</param>
        public ISubDataGrid SubGrid( string property ) {
            return EasyUiFactory.CreateSubGrid().RowNumber().FitColumns().Strip().Property( property ).CheckOnSelect( false ).SelectOnCheck( false ).SingleSelect();
        }

        /// <summary>
        /// 子表格
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">集合属性,范例：t => t.Users</param>
        /// <param name="funcCreateColumns">创建列集合方法</param>
        public ISubDataGrid SubGrid<TProperty>( Expression<Func<TEntity, IEnumerable<TProperty>>> expression, Func<TProperty, ISubGridColumn[]> funcCreateColumns ) {
            return EasyUiFactory<TEntity>.CreateSubGrid( expression, funcCreateColumns ).RowNumber().FitColumns().Strip().CheckOnSelect( false ).SelectOnCheck( false ).SingleSelect();
        }

        /// <summary>
        /// 表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">文本</param>
        /// <param name="width">宽度</param>
        public IDataGridColumn GridColumn( string field = "", string text = "", int? width = null ) {
            return EasyUiFactory.CreateDataGridColumn().Field( field ).Text( text ).Width( width ).Align();
        }

        /// <summary>
        /// 文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        public ITextBox TextBox<TProperty>( Expression<Func<TEntity, TProperty>> expression ) {
            return EasyUiFactory<TEntity>.CreateTextBox( expression, _helper );
        }

        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="defaultText">默认值</param>
        public ICombox Combox<TProperty>( Expression<Func<TEntity, TProperty>> expression, string defaultText = null ) {
            var combox = EasyUiFactory<TEntity>.CreateCombox( expression, _helper );
            if ( defaultText != null )
                combox.AddDefault( defaultText );
            return combox;
        }

        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="id">控件Id</param>
        /// <param name="url">远程Url</param>
        public ICombox Combox<TProperty>( Expression<Func<TEntity, TProperty>> expression, string id, string url ) {
            return EasyUiFactory<TEntity>.CreateCombox( expression, _helper, id, url );
        }

        /// <summary>
        /// 创建表格列
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="width">宽度</param>
        /// <param name="isEdit">是否允许编辑</param>
        public IDataGridColumn GridColumn<TProperty>( Expression<Func<TEntity, TProperty>> expression, int? width = null, bool isEdit = false ) {
            return EasyUiFactory<TEntity>.CreateDataGridColumn( expression, _helper, isEdit ).Width( width ).Align();
        }

        /// <summary>
        /// 树型表格
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="treeField">树显示字段名</param>
        /// <param name="idField">Id字段名</param>
        public ITreeGrid TreeGrid( string id = "", string treeField = "Text", string idField = "Id" ) {
            return EasyUiFactory.CreateTreeGrid().Id( id ).IdField( idField ).TreeField( treeField ).RowNumber()
                .Pagination().CheckOnSelect( false ).SelectOnCheck( false ).SingleSelect().Strip().Sort( "SortId", false );
        }

        /// <summary>
        /// 树
        /// </summary>
        /// <param name="url">设置远程url</param>
        /// <param name="id">Id</param>
        public ITree Tree( string url, string id = "" ) {
            return EasyUiFactory.CreateTree().Url( url ).Id( id );
        }

        /// <summary>
        /// 树组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        public IComboTree ComboTree( string name, string id, string url, string value ) {
            return EasyUiFactory.CreateComboTree( name, id, url, value );
        }

        /// <summary>
        /// 子表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">标题文本</param>
        /// <param name="width">宽度</param>
        public ISubGridColumn SubGridColumn( string field, string text, int? width = 100 ) {
            return EasyUiFactory.CreateSubGridColumn().Field( field ).Text( text ).Width( width ).Align();
        }

        /// <summary>
        /// 子表格列
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="width">宽度</param>
        public ISubGridColumn SubGridColumn<TProperty>( Expression<Func<TProperty>> expression, int? width = 100 ) {
            return EasyUiFactory<TProperty>.CreateSubGridColumn( expression ).Width( width ).Align();
        }

        /// <summary>
        /// 创建分页
        /// </summary>
        public IPagination Pagination() {
            return EasyUiFactory.CreatePagination();
        }
    }
}
