using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 表格
    /// </summary>
    /// <typeparam name="T">表格类型</typeparam>
    public interface IDataGrid<out T> : IComponent<T> where T : IDataGrid<T> {
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="isShow">是否显示行号</param>
        T RowNumber( bool isShow = true );
        /// <summary>
        /// 设置自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        T Fit( bool isFit = true );
        /// <summary>
        /// 设置列为自适应布局
        /// </summary>
        /// <param name="isFit">是否自适应</param>
        T FitColumns( bool isFit = true );
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="isPagination">是否分页</param>
        /// <param name="pageSize">每页显示行数</param>
        T Pagination( bool isPagination = true,int pageSize = 20 );
        /// <summary>
        /// 设置排序
        /// </summary>
        /// <param name="sortName">排序名</param>
        /// <param name="isDesc">是否倒排序</param>
        T Sort( string sortName,bool isDesc = true );
        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        /// <param name="isCheck">是否选中</param>
        T CheckOnSelect( bool isCheck = true );
        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        T SelectOnCheck( bool isSelect = true );
        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        /// <param name="isSingle">是否只能选中一行</param>
        T SingleSelect( bool isSingle = true );
        /// <summary>
        /// 设置是否显示条纹
        /// </summary>
        /// <param name="isShow">是否显示条纹</param>
        T Strip( bool isShow = true );
        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="toolbarId">工具栏Id</param>
        T Toolbar( string toolbarId );
        /// <summary>
        /// 设置加载数据的Url
        /// </summary>
        /// <param name="url">Url</param>
        T Url( string url );
        /// <summary>
        /// 设置双击行事件处理函数
        /// </summary>
        /// <param name="handler">双击行事件处理函数</param>
        T OnDblClickRow( string handler );
        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        T OnContextMenu( string handler );
        /// <summary>
        /// 设置加载成功事件处理函数
        /// </summary>
        /// <param name="handler">加载成功事件处理函数</param>
        T OnLoadSuccess( string handler );
        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        T Columns( params IDataGridColumn[] columns );
        /// <summary>
        /// 设置子表格
        /// </summary>
        /// <param name="subGrid">子表格</param>
        T SubGrid( ISubDataGrid subGrid );
        /// <summary>
        /// 设置行展开详细内容
        /// </summary>
        /// <param name="url">远程Url</param>
        /// <param name="isShowBorder">是否显示面板边框</param>
        /// <param name="fnCreateUrl">创建Url的js函数</param>
        /// <param name="paramName">发送参数</param>
        /// <param name="btnDivId">按钮div Id</param>
        T Detail( string url,bool isShowBorder = false,string fnCreateUrl ="",string paramName = "id",string btnDivId = "" );
        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="gridId">表格Id</param>
        /// <param name="menuId">菜单Id</param>
        T Menu( string gridId = "", string menuId = "" );
        /// <summary>
        /// 双击表格行，显示编辑表单窗口
        /// </summary>
        /// <param name="btnId">编辑按钮Id</param>
        T ShowEditDialogByDblClick( string btnId = "" );
    }
}
