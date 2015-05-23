using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树
    /// </summary>
    /// <typeparam name="T">树类型</typeparam>
    public interface ITree<out T> : IComponent<T> where T : ITree<T> {
        /// <summary>
        /// 设置远程Url
        /// </summary>
        /// <param name="url">远程数据加载Url</param>
        T Url( string url );
        /// <summary>
        /// 启用折叠动画效果
        /// </summary>
        T Animate();
        /// <summary>
        /// 显示复选框
        /// </summary>
        /// <param name="onlyLeafCheck">仅显示叶节点复选框</param>
        /// <param name="cascadeCheck">级联选择复选框</param>
        T Checkbox( bool? onlyLeafCheck = null, bool? cascadeCheck = null );
        /// <summary>
        /// 启用拖拽
        /// </summary>
        T EnableDrag();
        /// <summary>
        /// 设置发送到远程Url的参数
        /// </summary>
        /// <param name="param">请求参数</param>
        T Params( object param );
        /// <summary>
        /// 设置选择前事件处理函数
        /// </summary>
        /// <param name="handler">选择前事件处理函数</param>
        T OnBeforeSelect( string handler );
        /// <summary>
        /// 设置选择事件处理函数
        /// </summary>
        /// <param name="handler">选择事件处理函数</param>
        T OnSelect( string handler );
        /// <summary>
        /// 仅允许选择叶节点
        /// </summary>
        T SelectLeafOnly();
        /// <summary>
        /// 设置右键菜单事件处理函数
        /// </summary>
        /// <param name="handler">右键菜单事件处理函数</param>
        T OnContextMenu( string handler );
        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="treeId">树Id</param>
        /// <param name="menuId">菜单Id</param>
        T Menu( string treeId = "", string menuId = "" );
        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        T Click( string handler );
        /// <summary>
        /// 单击刷新面板
        /// </summary>
        /// <param name="panelId">面板编号</param>
        /// <param name="url">服务端Url</param>
        /// <param name="paramName">参数</param>
        /// <param name="fnCreateUrl">用于创建Url的回调函数</param>
        T RefreshPanel( string panelId, string url, string paramName = "id", string fnCreateUrl = "" );
    }
}
