using Util.Webs.EasyUi.Buttons;
using Util.Webs.EasyUi.Forms.ComboTrees;
using Util.Webs.EasyUi.Forms.Comboxs;
using Util.Webs.EasyUi.Forms.TextBoxs;
using Util.Webs.EasyUi.Grids;
using Util.Webs.EasyUi.Menus;
using Util.Webs.EasyUi.Paginations;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi {
    /// <summary>
    /// EasyUi工厂
    /// </summary>
    public class EasyUiFactory {
        /// <summary>
        /// 创建按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        public static IButton CreateButton( string text ) {
            return new Button( text );
        }

        /// <summary>
        /// 创建弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        public static IDialogButton CreateDialogButton( string text,string url = "" ) {
            return new DialogButton( text, url );
        }

        /// <summary>
        /// 创建文本框
        /// </summary>
        public static ITextBox CreateTextBox() {
            return new TextBox();
        }

        /// <summary>
        /// 创建组合框
        /// </summary>
        public static ICombox CreateCombox() {
            return new Combox();
        }

        /// <summary>
        /// 创建组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        public static ICombox CreateCombox( string name, string id, string url, string value ) {
            return new Combox().Name( name ).Id( id ).Url( url, value ); ;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="id">Id</param>
        public static IMenu CreateMenu( string id ) {
            return new Menu( id );
        }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        public static IMenuItem CreateMenuItem() {
            return new MenuItem();
        }

        /// <summary>
        /// 创建表格
        /// </summary>
        public static IDataGrid CreateDataGrid() {
            return new DataGrid();
        }

        /// <summary>
        /// 创建表格
        /// </summary>
        public static ISubDataGrid CreateSubGrid() {
            return new SubDataGrid();
        }

        /// <summary>
        /// 创建表格列
        /// </summary>
        public static IDataGridColumn CreateDataGridColumn() {
            return new DataGridColumn();
        }

        /// <summary>
        /// 创建子表格列
        /// </summary>
        public static ISubGridColumn CreateSubGridColumn() {
            return new SubGridColumn();
        }

        /// <summary>
        /// 创建树型表格
        /// </summary>
        public static ITreeGrid CreateTreeGrid() {
            return new TreeGrid();
        }

        /// <summary>
        /// 创建树
        /// </summary>
        public static ITree CreateTree() {
            return new Tree();
        }

        /// <summary>
        /// 创建树组合框
        /// </summary>
        /// <param name="name">name，用于服务端接收值</param>
        /// <param name="id">Id</param>
        /// <param name="url">远程Url</param>
        /// <param name="value">选中的值</param>
        public static IComboTree CreateComboTree( string name, string id, string url, string value ) {
            return new ComboTree().Name( name ).Id( id ).Url( url, value );
        }

        /// <summary>
        /// 创建分页
        /// </summary>
        public static IPagination CreatePagination() {
            return new Pagination();
        }
    }
}
