namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树选项
    /// </summary>
    public class TreeOption {
        /// <summary>
        /// 初始化树选项
        /// </summary>
        public TreeOption() {
            _builder = new JsonAttributeBuilder();
        }

        /// <summary>
        /// 属性生成器
        /// </summary>
        private readonly JsonAttributeBuilder _builder;

        /// <summary>
        /// 设置远程Url
        /// </summary>
        /// <param name="url">远程数据加载Url</param>
        public void Url( string url ) {
            _builder.Add( "url", url, "'" );
        }

        /// <summary>
        /// 启用折叠动画效果
        /// </summary>
        public void Animate() {
            _builder.Add( "animate", true );
        }

        /// <summary>
        /// 显示复选框
        /// </summary>
        /// <param name="onlyLeafCheck">仅显示叶节点复选框</param>
        /// <param name="cascadeCheck">级联选择复选框</param>
        public void Checkbox( bool? onlyLeafCheck = null, bool? cascadeCheck = null ) {
            _builder.Add( "checkbox", true );
            _builder.Add( "onlyLeafCheck", onlyLeafCheck );
            _builder.Add( "cascadeCheck", cascadeCheck );
        }

        /// <summary>
        /// 启用拖拽
        /// </summary>
        public void EnableDrag() {
            _builder.Add( "dnd", true );
        }

        /// <summary>
        /// 设置发送到远程Url的参数
        /// </summary>
        /// <param name="param">请求参数</param>
        public void Params( object param ) {
            _builder.Add( "queryParams", Json.ToJson( param, true ) );
        }

        /// <summary>
        /// 设置选择前事件处理函数
        /// </summary>
        /// <param name="handler">选择前事件处理函数</param>
        public void OnBeforeSelect( string handler ) {
            _builder.Add( "onBeforeSelect",handler );
        }

        /// <summary>
        /// 设置选择事件处理函数
        /// </summary>
        /// <param name="handler">选择事件处理函数</param>
        public void OnSelect( string handler ) {
            _builder.Add( "onSelect", handler );
        }

        /// <summary>
        /// 仅允许选择叶节点
        /// </summary>
        public void SelectLeafOnly() {
            OnBeforeSelect( "$.easyui.fnSelectTreeLeafOnly" );
        }

        /// <summary>
        /// 设置右键菜单事件处理函数
        /// </summary>
        /// <param name="handler">右键菜单事件处理函数</param>
        public void OnContextMenu( string handler ) {
            _builder.Add( "onContextMenu", handler );
        }

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="treeId">树Id</param>
        /// <param name="menuId">菜单Id</param>
        public void Menu( string treeId = "", string menuId = "" ) {
            var builder = new ParamBuilder();
            builder.Add( treeId, "''", true );
            builder.Add( menuId, true );
            OnContextMenu( string.Format( "$.easyui.showTreeMenu_onContextMenu({0})", builder.GetResult() ) );
        }

        /// <summary>
        /// 设置单击事件处理函数
        /// </summary>
        /// <param name="handler">单击事件处理函数</param>
        public void OnClick( string handler ) {
            _builder.Add( "onClick", handler );
        }

        /// <summary>
        /// 单击刷新面板
        /// </summary>
        /// <param name="panelId">面板编号</param>
        /// <param name="url">服务端Url</param>
        /// <param name="paramName">参数</param>
        /// <param name="fnCreateUrl">用于创建Url的回调函数</param>
        public void RefreshPanel( string panelId, string url, string paramName, string fnCreateUrl = "" ) {
            var builder = new ParamBuilder();
            builder.Add( panelId, "''", true );
            builder.Add( url, "''", true );
            builder.Add( paramName, "''", true );
            builder.Add( fnCreateUrl, "null" );
            OnClick( string.Format( "$.easyui.clickTreeNodeRefreshPanel_onClick({0})", builder.GetResult() ) );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            return _builder.GetResult();
        }
    }
}
