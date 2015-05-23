using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树
    /// </summary>
    public class Tree : ComponentBase<ITree>, ITree {
        /// <summary>
        /// 初始化树
        /// </summary>
        public Tree() {
            AddClass( "easyui-tree" );
            _option = new TreeOption();
        }

        /// <summary>
        /// 树选项
        /// </summary>
        private readonly TreeOption _option;

        /// <summary>
        /// 设置远程Url
        /// </summary>
        /// <param name="url">远程数据加载Url</param>
        public ITree Url( string url ) {
            _option.Url( url );
            return This();
        }

        /// <summary>
        /// 启用折叠动画效果
        /// </summary>
        public ITree Animate() {
            _option.Animate();
            return This();
        }

        /// <summary>
        /// 显示复选框
        /// </summary>
        /// <param name="onlyLeafCheck">仅显示叶节点复选框</param>
        /// <param name="cascadeCheck">级联选择复选框</param>
        public ITree Checkbox( bool? onlyLeafCheck = null, bool? cascadeCheck = null ) {
            _option.Checkbox( onlyLeafCheck, cascadeCheck );
            return This();
        }

        /// <summary>
        /// 启用拖拽
        /// </summary>
        public ITree EnableDrag() {
            _option.EnableDrag();
            return This();
        }

        /// <summary>
        /// 设置发送到远程Url的参数
        /// </summary>
        /// <param name="param">请求参数</param>
        public ITree Params( object param ) {
            _option.Params( param );
            return This();
        }

        /// <summary>
        /// 设置选择前事件处理函数
        /// </summary>
        /// <param name="handler">选择前事件处理函数</param>
        public ITree OnBeforeSelect( string handler ) {
            _option.OnBeforeSelect( handler );
            return This();
        }

        /// <summary>
        /// 设置选择事件处理函数
        /// </summary>
        /// <param name="handler">选择事件处理函数</param>
        public ITree OnSelect( string handler ) {
            _option.OnSelect( handler );
            return This();
        }

        /// <summary>
        /// 仅允许选择叶节点
        /// </summary>
        public ITree SelectLeafOnly() {
            _option.SelectLeafOnly();
            return This();
        }

        /// <summary>
        /// 设置右键菜单事件处理函数
        /// </summary>
        /// <param name="handler">右键菜单事件处理函数</param>
        public ITree OnContextMenu( string handler ) {
            _option.OnContextMenu( handler );
            return This();
        }

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="treeId">树Id</param>
        /// <param name="menuId">菜单Id</param>
        public ITree Menu( string treeId = "", string menuId = "" ) {
            _option.Menu( treeId, menuId );
            return This();
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public ITree Click( string handler ) {
            _option.OnClick( handler );
            return This();
        }

        /// <summary>
        /// 单击刷新面板
        /// </summary>
        /// <param name="panelId">面板编号</param>
        /// <param name="url">服务端Url</param>
        /// <param name="paramName">参数</param>
        /// <param name="fnCreateUrl">用于创建Url的回调函数</param>
        public ITree RefreshPanel( string panelId, string url, string paramName = "id", string fnCreateUrl = "" ) {
            _option.RefreshPanel( panelId, url, paramName, fnCreateUrl );
            return This();
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        protected override string GetResult() {
            AddDataOption( _option.GetResult() );
            var result = new StringBuilder();
            result.AppendFormat( "<ul {0}></ul>", GetOptions() );
            return result.ToString();
        }
    }
}
