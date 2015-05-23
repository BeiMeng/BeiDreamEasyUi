using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Webs.EasyUi.Base;
using Util.Webs.EasyUi.Configs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="T">表格类型</typeparam>
    public abstract class DataGrid<T> : ComponentBase<T>, IDataGrid<T> where T : IDataGrid<T> {
        /// <summary>
        /// 初始化表格
        /// </summary>
        protected DataGrid() {
            AddClass( "easyui-datagrid" );
            _columns = new List<IDataGridColumn>();
        }

        /// <summary>
        /// 列集合
        /// </summary>
        private readonly List<IDataGridColumn> _columns;

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="isShow">是否显示行号</param>
        public T RowNumber( bool isShow = true ) {
            return AddDataOption( "rownumbers", isShow );
        }

        /// <summary>
        /// 设置自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public T Fit( bool isFit = true ) {
            return AddDataOption( "fit", isFit );
        }

        /// <summary>
        /// 设置列为自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public T FitColumns( bool isFit = true ) {
            return AddDataOption( "fitColumns", isFit );
        }

        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="isPagination">是否分页</param>
        /// <param name="pageSize">每页显示行数</param>
        public T Pagination( bool isPagination = true,int pageSize = 20 ) {
            return AddDataOption( "pagination", isPagination ).AddDataOption( "pageSize", pageSize.ToString() );
        }

        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="sortName">排序名</param>
        /// <param name="isDesc">是否倒排序</param>
        public T Sort( string sortName, bool isDesc = true ) {
            AddDataOption( "sortName", sortName,true );
            if ( isDesc )
                AddDataOption( "sortOrder", "desc",true );
            return This();
        }

        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        /// <param name="isCheck">是否选中</param>
        public T CheckOnSelect( bool isCheck = true ) {
            return AddDataOption( "checkOnSelect", isCheck );
        }

        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        public T SelectOnCheck( bool isSelect = true ) {
            return AddDataOption( "selectOnCheck", isSelect );
        }

        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        /// <param name="isSingle">是否只能选中一行</param>
        public T SingleSelect( bool isSingle = true ) {
            return AddDataOption( "singleSelect", isSingle );
        }

        /// <summary>
        /// 设置是否显示条纹
        /// </summary>
        /// <param name="isShow">是否显示条纹</param>
        public T Strip( bool isShow = true ) {
            return AddDataOption( "striped", isShow );
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="toolbarId">工具栏Id</param>
        public T Toolbar( string toolbarId ) {
            return AddDataOption( "toolbar", "#" + toolbarId,true );
        }

        /// <summary>
        /// 设置加载数据的Url
        /// </summary>
        /// <param name="url">Url</param>
        public T Url( string url ) {
            return AddDataOption( "url", url,true );
        }

        /// <summary>
        /// 设置双击行事件处理函数
        /// </summary>
        /// <param name="handler">双击行事件处理函数</param>
        public T OnDblClickRow( string handler ) {
            return AddDataOption( "onDblClickRow", handler );
        }

        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        public virtual T OnContextMenu( string handler ) {
            return AddDataOption( "onRowContextMenu", handler );
        }

        /// <summary>
        /// 设置加载成功事件处理函数
        /// </summary>
        /// <param name="handler">加载成功事件处理函数</param>
        public T OnLoadSuccess( string handler ) {
            return AddDataOption( "onLoadSuccess", handler );
        }

        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        public T Columns( params IDataGridColumn[] columns ) {
            if ( columns == null )
                return This();
            _columns.AddRange( columns );
            return This();
        }

        /// <summary>
        /// 设置子表格
        /// </summary>
        /// <param name="subGrid">子表格</param>
        public T SubGrid( ISubDataGrid subGrid ) {
            var option = new SubGridOption();
            option.SetSubGrid( subGrid.GetOption() );
            return OnLoadSuccess( string.Format( "$.easyui.loadSubGrid_onLoadSuccess({0})", option ) );
        }

        /// <summary>
        /// 设置行展开详细内容
        /// </summary>
        /// <param name="url">远程Url</param>
        /// <param name="isShowBorder">是否显示面板边框</param>
        /// <param name="fnCreateUrl">创建Url的js函数</param>
        /// <param name="paramName">发送参数</param>
        /// <param name="btnDivId">按钮div Id</param>
        public T Detail( string url, bool isShowBorder = false, string fnCreateUrl = "", string paramName = "id", string btnDivId = "" ) {
                return AddDataOption( "view", "detailview" )
                    .AddDataOption( "detailFormatter", "$.easyui.gridDetail_detailFormatter()" )
                    .AddDataOption( "onExpandRow", string.Format( "$.easyui.gridDetail_onExpandRow({0})", GetDetailParams( url, isShowBorder,fnCreateUrl,paramName, btnDivId ) ) );
        }

        /// <summary>
        /// 获取行展开详细内容参数
        /// </summary>
        private string GetDetailParams( string url, bool isShowBorder, string fnCreateUrl, string paramName, string btnDivId ) {
            var builder = new ParamBuilder();
            builder.Add( url,true );
            builder.Add( isShowBorder.ToStr().ToLower() );
            builder.Add( fnCreateUrl,"null" );
            builder.Add( paramName,true );
            builder.Add( btnDivId,true );
            return builder.GetResult();
        }

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="gridId">表格Id</param>
        /// <param name="menuId">菜单Id</param>
        public T Menu( string gridId = "", string menuId = "" ) {
            var builder = new ParamBuilder();
            builder.Add( gridId,"''",true );
            builder.Add( menuId,true );
            return OnContextMenu( string.Format( "{0}({1})",GetMenuFunction(), builder.GetResult() ) );
        }

        /// <summary>
        /// 获取右键菜单函数名
        /// </summary>
        protected virtual string GetMenuFunction() {
            return "$.easyui.showGridMenu_onRowContextMenu";
        }

        /// <summary>
        /// 双击表格行，显示编辑表单窗口
        /// </summary>
        /// <param name="btnId">编辑按钮Id</param>
        public T ShowEditDialogByDblClick( string btnId = "" ) {
            return OnDblClickRow( string.Format( "$.easyui.showEditDialog_onDblClickRow('{0}')", btnId ) );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            InitEdit();
            var result = new StringBuilder();
            result.AppendFormat( "<table {0}>", GetOptions() );
            AddFrozenColumns( result );
            AddColumns( result );
            result.Append( "</table>" );
            return result.ToString();
        }

        /// <summary>
        /// 初始化编辑状态
        /// </summary>
        private void InitEdit() {
            if ( !AllowEdit() )
                return;
            UpdateClass( GetEditClass() );
        }

        /// <summary>
        /// 获取编辑class
        /// </summary>
        protected virtual string GetEditClass() {
            return "easyui-edatagrid";
        }

        /// <summary>
        /// 允许编辑
        /// </summary>
        private bool AllowEdit() {
            return _columns.Any( t => t.IsEdit );
        }

        /// <summary>
        /// 添加冻结列
        /// </summary>
        private void AddFrozenColumns( StringBuilder result ) {
            if ( !_columns.Any( t => t.IsFrozen ) )
                return;
            result.Append( "<thead data-options=\"frozen:true\"><tr>" );
            foreach ( var column in _columns.Where( t => t.IsFrozen ) )
                result.Append( column.ToHtmlString() );
            result.Append( "</tr></thead>" );
        }

        /// <summary>
        /// 添加非冻结列
        /// </summary>
        private void AddColumns( StringBuilder result ) {
            result.Append( "<thead><tr>" );
            foreach ( var column in _columns.Where( t => !t.IsFrozen ) )
                result.Append( column.ToHtmlString() );
            result.Append( "</tr></thead>" );
        }
    }
}
