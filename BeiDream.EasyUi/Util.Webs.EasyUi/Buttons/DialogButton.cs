using System.Text;

namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 弹出窗口按钮
    /// </summary>
    public class DialogButton : ButtonBase<IDialogButton>, IDialogButton {
        /// <summary>
        /// 初始化弹出窗口按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="url">弹出窗口网址</param>
        public DialogButton( string text,string url = "" )
            : base( text ) {
            Builder = new JsonAttributeBuilder();
            Title( text ).Buttons( "dialogButtons" ).Url( url );
        }

        /// <summary>
        /// 是否关闭窗口
        /// </summary>
        private bool _isClose;

        /// <summary>
        /// 属性生成器
        /// </summary>
        protected JsonAttributeBuilder Builder { get; set; }

        /// <summary>
        /// 设置弹出窗口网址
        /// </summary>
        /// <param name="url">弹出窗口网址</param>
        public IDialogButton Url( string url ) {
            Builder.Add( "url", url,"'" );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口标题
        /// </summary>
        /// <param name="title">弹出窗口标题</param>
        public IDialogButton Title( string title ) {
            Builder.Add( "title", title,"'" );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口底部按钮
        /// </summary>
        /// <param name="buttonDivId">弹出窗口底部按钮区域div的id</param>
        public IDialogButton Buttons( string buttonDivId ) {
            Builder.Add( "buttons", buttonDivId,"'" );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口图标class
        /// </summary>
        /// <param name="iconClass">弹出窗口图标class</param>
        public IDialogButton DialogIcon( string iconClass ) {
            Builder.Add( "icon", iconClass,"'" );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口尺寸
        /// </summary>
        /// <param name="width">弹出窗口宽度</param>
        /// <param name="height">弹出窗口高度</param>
        public IDialogButton DialogSize( int width, int height ) {
            Builder.Add( "width", width.ToString() );
            Builder.Add( "height", height.ToString() );
            return This();
        }

        /// <summary>
        /// 允许弹出窗口最大化
        /// </summary>
        /// <param name="allow">true为允许最大化</param>
        public IDialogButton Maximizable( bool allow = true ) {
            Builder.Add( "maximizable", allow.ToString().ToLower() );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口关闭回调函数
        /// </summary>
        /// <param name="callback">弹出窗口关闭回调函数,范例：func</param>
        public IDialogButton OnClose( string callback ) {
            Builder.Add( "closeCallback", callback );
            return This();
        }

        /// <summary>
        /// 设置弹出窗口初始化事件
        /// </summary>
        /// <param name="callback">初始化回调函数,接收option参数，返回false跳出执行</param>
        public IDialogButton OnInit( string callback ) {
            Builder.Add( "onInit", callback );
            return This();
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        public IDialogButton CloseDialog() {
            _isClose = true;
            return This();
        }

        /// <summary>
        /// 显示编辑窗口
        /// </summary>
        public IDialogButton ShowEditDialog() {
            return OnInit( "$.easyui.initEditDialog" );
        }

        /// <summary>
        /// 显示详细窗口
        /// </summary>
        public IDialogButton ShowDetailDialog() {
            return OnInit( "$.easyui.initDetailDialog" );
        }

        /// <summary>
        /// 显示编辑窗口 - 树
        /// </summary>
        /// <param name="treeId">树控件Id</param>
        public IDialogButton ShowEditDialogByTree( string treeId = "tree" ) {
            return OnInit( string.Format( "$.easyui.initEditDialogByTree('{0}')",treeId ) );
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected override string GetResult() {
            var handler = new StringBuilder();
            CreateClickHandler( handler );
            Click( handler.ToString() );
            return base.GetResult();
        }

        /// <summary>
        /// 创建单击事件处理
        /// </summary>
        private void CreateClickHandler( StringBuilder handler ) {
            if ( _isClose )
                CreateCloseDialog( handler );
            else
                CreateOpenDialog( handler );
        }

        /// <summary>
        /// 打开弹出窗口
        /// </summary>
        private void CreateOpenDialog( StringBuilder handler ) {
            handler.Append( "$.easyui.dialog({" );
            handler.Append( Builder.GetResult() );
            handler.Append( "})" );
        }

        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        private void CreateCloseDialog( StringBuilder handler ) {
            handler.AppendFormat( "$.easyui.closeDialog()" );
        }
    }
}
