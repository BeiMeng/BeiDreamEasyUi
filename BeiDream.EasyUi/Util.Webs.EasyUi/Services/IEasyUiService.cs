using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public interface IEasyUiService<TEntity> {
        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="fit">是否自适应</param>
        ILayout Layout( bool fit = false );
        /// <summary>
        /// 布局区域
        /// </summary>
        ILayoutRegion LayoutRegion();
        /// <summary>
        /// 面板
        /// </summary>
        IPanel Panel();
        /// <summary>
        /// 选项卡
        /// </summary>
        ITabs Tabs();
        /// <summary>
        /// 选项卡面板
        /// </summary>
        /// <param name="title">标题</param>
        ITabPanel TabPanel( string title );
        /// <summary>
        /// 表单
        /// </summary>
        /// <param name="id">Id</param>
        IForm Form( string id );
        /// <summary>
        /// 按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        IButton Button( string text );
        /// <summary>
        /// 弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        IDialogButton DialogButton( string text, string url = "" );
        /// <summary>
        /// 文本框
        /// </summary>
        ITextBox TextBox();
        /// <summary>
        /// 组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        ICombox Combox( string name );
        /// <summary>
        /// 组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        ICombox Combox( string name, string id,string url,string value );
        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="id">Id</param>
        IMenu Menu( string id );
        /// <summary>
        /// 菜单项
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="text">文本</param>
        /// <param name="iconClass">图标class</param>
        IMenuItem MenuItem( string id = "", string text = "", string iconClass = "" );
        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="id">Id</param>
        IDataGrid Grid( string id = "" );
        /// <summary>
        /// 子表格
        /// </summary>
        /// <param name="property">集合属性，范例：如果对象包含A元素集合List1，A元素包含集合List2，展示List2使用"List2"，不要写成"List1.List2"</param>
        ISubDataGrid SubGrid( string property );
        /// <summary>
        /// 子表格
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">集合属性,范例：t => t.A，如果存在多级导航属性，可以使用FirstOrDefault，范例： t => t.A.FirstOrDefault().B</param>
        /// <param name="funcCreateColumns">创建列集合方法，范例：c=>new[]{ Html.EasyUi().SubGridColumn( () => c.A,100 ),Html.EasyUi().SubGridColumn( () => c.B,100 )}</param>
        ISubDataGrid SubGrid<TProperty>( Expression<Func<TEntity, IEnumerable<TProperty>>> expression, Func<TProperty, ISubGridColumn[]> funcCreateColumns );
        /// <summary>
        /// 表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">文本</param>
        /// <param name="width">宽度</param>
        IDataGridColumn GridColumn( string field = "", string text = "", int? width = null );
        /// <summary>
        /// 文本框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        ITextBox TextBox<TProperty>( Expression<Func<TEntity, TProperty>> expression );
        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="defaultText">默认值</param>
        ICombox Combox<TProperty>( Expression<Func<TEntity, TProperty>> expression,string defaultText = null );
        /// <summary>
        /// 组合框
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="id">控件Id</param>
        /// <param name="url">远程Url</param>
        ICombox Combox<TProperty>( Expression<Func<TEntity, TProperty>> expression, string id, string url );
        /// <summary>
        /// 表格列
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="width">宽度</param>
        /// <param name="isEdit">是否允许编辑</param>
        IDataGridColumn GridColumn<TProperty>( Expression<Func<TEntity, TProperty>> expression, int? width = null, bool isEdit = false );
        /// <summary>
        /// 树型表格
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="treeField">树显示字段名,默认"Text"</param>
        /// <param name="idField">Id字段名,默认"Id"</param>
        ITreeGrid TreeGrid( string id = "", string treeField = "Text", string idField = "Id" );
        /// <summary>
        /// 树
        /// </summary>
        /// <param name="url">设置远程url</param>
        /// <param name="id">Id</param>
        ITree Tree( string url, string id = "" );
        /// <summary>
        /// 树组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        IComboTree ComboTree( string name, string id, string url, string value );
        /// <summary>
        /// 子表格列
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="text">标题文本</param>
        /// <param name="width">宽度</param>
        ISubGridColumn SubGridColumn( string field, string text, int? width = 100 );
        /// <summary>
        /// 子表格列
        /// </summary>
        /// <param name="expression">属性表达式，范例：() => c.A</param>
        /// <param name="width">宽度</param>
        ISubGridColumn SubGridColumn<TProperty>( Expression<Func<TProperty>> expression, int? width = 100 );
        /// <summary>
        /// 分页
        /// </summary>
        IPagination Pagination();
    }
}
