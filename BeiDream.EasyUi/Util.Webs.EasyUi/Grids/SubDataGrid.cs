using System;
using Util.Webs.EasyUi.Configs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 子表格
    /// </summary>
    public class SubDataGrid : ISubDataGrid {
        /// <summary>
        /// 初始化子表格
        /// </summary>
        public SubDataGrid() {
            _subGridOption = new SubGridOption();
            _option = new DataGridOption();
        }

        /// <summary>
        /// 子表格配置项
        /// </summary>
        private readonly SubGridOption _subGridOption;
        /// <summary>
        /// 表格配置项
        /// </summary>
        private readonly DataGridOption _option;

        /// <summary>
        /// 获取配置项
        /// </summary>
        public SubGridOption GetOption() {
            _subGridOption.Options = _option;
            return _subGridOption;
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
        /// 显示行号
        /// </summary>
        /// <param name="isShow">是否显示行号</param>
        public ISubDataGrid RowNumber( bool isShow = true ) {
            _option.RowNumbers = isShow;
            return this;
        }

        /// <summary>
        /// 设置自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public ISubDataGrid Fit( bool isFit = true ) {
            _option.Fit = isFit;
            return this;
        }

        /// <summary>
        /// 设置列为自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        public ISubDataGrid FitColumns( bool isFit = true ) {
            _option.FitColumns = isFit;
            return this;
        }

        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="isPagination">是否分页</param>
        /// <param name="pageSize">每页显示行数</param>
        public ISubDataGrid Pagination( bool isPagination = true,int pageSize = 20 ) {
            _option.Pagination = isPagination;
            _option.PageSize = pageSize;
            return this;
        }

        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="sortName">排序名</param>
        /// <param name="isDesc">是否倒排序</param>
        public ISubDataGrid Sort( string sortName, bool isDesc = true ) {
            _option.SortName = sortName;
            _option.SortOrder = isDesc ? "desc" : "";
            return this;
        }

        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        /// <param name="isCheck">是否选中</param>
        public ISubDataGrid CheckOnSelect( bool isCheck = true ) {
            _option.CheckOnSelect = isCheck;
            return this;
        }

        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        public ISubDataGrid SelectOnCheck( bool isSelect = true ) {
            _option.SelectOnCheck = isSelect;
            return this;
        }

        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        /// <param name="isSingle">是否只能选中一行</param>
        public ISubDataGrid SingleSelect( bool isSingle = true ) {
            _option.SingleSelect = isSingle;
            return this;
        }

        /// <summary>
        /// 设置是否显示条纹
        /// </summary>
        /// <param name="isShow">是否显示条纹</param>
        public ISubDataGrid Strip( bool isShow = true ) {
            _option.Striped = isShow;
            return this;
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="toolbarId">工具栏Id</param>
        public ISubDataGrid Toolbar( string toolbarId ) {
            return this;
        }

        /// <summary>
        /// 设置加载数据的Url
        /// </summary>
        /// <param name="url">Url</param>
        public ISubDataGrid Url( string url ) {
            _option.Url = url;
            return this;
        }

        /// <summary>
        /// 设置双击行事件处理函数
        /// </summary>
        /// <param name="handler">双击行事件处理函数</param>
        public ISubDataGrid OnDblClickRow( string handler ) {
            _option.OnDblClickRow = handler;
            return this;
        }

        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        public ISubDataGrid OnContextMenu( string handler ) {
            _option.OnRowContextMenu = handler;
            return this;
        }

        /// <summary>
        /// 设置加载成功事件处理函数
        /// </summary>
        /// <param name="handler">加载成功事件处理函数</param>
        public ISubDataGrid OnLoadSuccess( string handler ) {
            _option.OnLoadSuccess = handler;
            return this;
        }

        /// <summary>
        /// 设置子表格
        /// </summary>
        /// <param name="subGrid">子表格</param>
        public ISubDataGrid SubGrid( ISubDataGrid subGrid ) {
            _subGridOption.SetSubGrid( subGrid.GetOption() );
            return this;
        }

        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        public ISubDataGrid Columns( params IDataGridColumn[] columns ) {
            throw new ArgumentException( "设置子表格列集合请使用SubGridColumn" );
        }

        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        public ISubDataGrid Columns( params ISubGridColumn[] columns ) {
            if ( columns == null )
                return this;
            foreach( var column in columns )
                _option.AddColumn( column.GetOption() );
            return this;
        }

        /// <summary>
        /// 设置导航属性，用于加载子表格
        /// </summary>
        /// <param name="navigateProperty">导航属性</param>
        public ISubDataGrid Property( string navigateProperty ) {
            _option.Property = navigateProperty;
            return this;
        }

        /// <summary>
        /// 设置行展开详细内容
        /// </summary>
        /// <param name="url">远程Url</param>
        /// <param name="isShowBorder">是否显示面板边框</param>
        /// <param name="fnCreateUrl">创建Url的js函数</param>
        /// <param name="paramName">发送参数</param>
        /// <param name="btnDivId">按钮div Id</param>
        public ISubDataGrid Detail( string url, bool isShowBorder = false, string fnCreateUrl = "", string paramName = "id", string btnDivId = "" ) {
            return this;
        }

        /// <summary>
        /// 双击表格行，显示编辑表单窗口
        /// </summary>
        /// <param name="btnId">编辑按钮Id</param>
        public ISubDataGrid ShowEditDialogByDblClick( string btnId = "" ) {
            return this;
        }

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="gridId">表格Id</param>
        /// <param name="menuId">菜单Id</param>
        public ISubDataGrid Menu( string gridId = "", string menuId = "" ) {
            return this;
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="isPercent">是否百分比</param>
        public ISubDataGrid Width( int? width, bool isPercent = false ) {
            return this;
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        public ISubDataGrid Height( int height ) {
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
        public ISubDataGrid Id( string id ) {
            return this;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="name">属性名,范例：class</param>
        /// <param name="value">属性值</param>
        public ISubDataGrid AddAttribute( string name, string value ) {
            return this;
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="name">样式名称</param>
        /// <param name="value">样式值</param>
        public ISubDataGrid AddStyle( string name, string value ) {
            return this;
        }

        /// <summary>
        /// 添加class属性
        /// </summary>
        /// <param name="class">class属性</param>
        public ISubDataGrid AddClass( string @class ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        /// <param name="isAddQuote">是否为值添加引号</param>
        public ISubDataGrid AddDataOption( string name, string value, bool isAddQuote = false ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public ISubDataGrid AddDataOption( string name, bool value ) {
            return this;
        }

        /// <summary>
        /// 添加data-options属性
        /// </summary>
        /// <param name="name">option属性名</param>
        /// <param name="value">option属性值</param>
        public ISubDataGrid AddDataOption( string name, bool? value ) {
            return this;
        }

        /// <summary>
        /// 在控件后添加html
        /// </summary>
        /// <param name="html">Html</param>
        public ISubDataGrid AddAfter( string html ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="value">外边距值</param>
        public ISubDataGrid Margin( int value ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public ISubDataGrid Margin( int topBottom, int leftRight ) {
            return this;
        }

        /// <summary>
        /// 设置外边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public ISubDataGrid Margin( int top, int right, int bottom, int left ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="value">内边距值</param>
        public ISubDataGrid Padding( int value ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="topBottom">上下边距值</param>
        /// <param name="leftRight">左右边距值</param>
        public ISubDataGrid Padding( int topBottom, int leftRight ) {
            return this;
        }

        /// <summary>
        /// 设置内边距
        /// </summary>
        /// <param name="top">上边距值</param>
        /// <param name="right">右边距值</param>
        /// <param name="bottom">下边距值</param>
        /// <param name="left">左边距值</param>
        public ISubDataGrid Padding( int top, int right, int bottom, int left ) {
            return this;
        }
    }
}
