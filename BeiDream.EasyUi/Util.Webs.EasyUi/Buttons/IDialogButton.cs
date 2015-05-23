namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 弹出窗口按钮
    /// </summary>
    public interface IDialogButton : IButtonBase<IDialogButton> {
        /// <summary>
        /// 设置弹出窗口网址
        /// </summary>
        /// <param name="url">弹出窗口网址</param>
        IDialogButton Url( string url );
        /// <summary>
        /// 设置弹出窗口标题
        /// </summary>
        /// <param name="title">弹出窗口标题</param>
        IDialogButton Title( string title );
        /// <summary>
        /// 设置弹出窗口底部按钮
        /// </summary>
        /// <param name="buttonDivId">弹出窗口底部按钮区域div的id</param>
        IDialogButton Buttons( string buttonDivId );
        /// <summary>
        /// 设置弹出窗口图标class
        /// </summary>
        /// <param name="iconClass">弹出窗口图标class</param>
        IDialogButton DialogIcon( string iconClass );
        /// <summary>
        /// 设置弹出窗口尺寸
        /// </summary>
        /// <param name="width">弹出窗口宽度</param>
        /// <param name="height">弹出窗口高度</param>
        IDialogButton DialogSize( int width,int height );
        /// <summary>
        /// 允许弹出窗口最大化
        /// </summary>
        /// <param name="allow">true为允许最大化</param>
        IDialogButton Maximizable( bool allow = true );
        /// <summary>
        /// 设置弹出窗口关闭回调函数
        /// </summary>
        /// <param name="callback">弹出窗口关闭回调函数,范例：func</param>
        IDialogButton OnClose( string callback );
        /// <summary>
        /// 设置弹出窗口初始化事件
        /// </summary>
        /// <param name="callback">初始化回调函数,接收option参数，返回false跳出执行</param>
        IDialogButton OnInit( string callback );
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        IDialogButton CloseDialog();
        /// <summary>
        /// 显示编辑窗口
        /// </summary>
        IDialogButton ShowEditDialog();
        /// <summary>
        /// 显示详细窗口
        /// </summary>
        IDialogButton ShowDetailDialog();
        /// <summary>
        /// 显示编辑窗口 - 树
        /// </summary>
        /// <param name="treeId">树控件Id</param>
        IDialogButton ShowEditDialogByTree( string treeId = "tree" );
    }
}
