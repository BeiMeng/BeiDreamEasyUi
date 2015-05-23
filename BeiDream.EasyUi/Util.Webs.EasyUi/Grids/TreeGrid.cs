namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 树型表格
    /// </summary>
    public class TreeGrid : DataGrid<ITreeGrid>, ITreeGrid {
        /// <summary>
        /// 初始化树型表格
        /// </summary>
        public TreeGrid() {
            UpdateClass( "easyui-treegrid" );
        }

        /// <summary>
        /// 设置Id属性名
        /// </summary>
        /// <param name="field">Id属性名</param>
        public ITreeGrid IdField( string field ) {
            return AddDataOption( "idField", field,true );
        }

        /// <summary>
        /// 设置树属性名
        /// </summary>
        /// <param name="field">属性名</param>
        public ITreeGrid TreeField( string field ) {
            return AddDataOption( "treeField", field,true );
        }

        /// <summary>
        /// 获取编辑class
        /// </summary>
        protected override string GetEditClass() {
            return "easyui-etreegrid";
        }

        /// <summary>
        /// 设置右键单击行事件处理函数
        /// </summary>
        /// <param name="handler">右键单击行事件处理函数</param>
        public override ITreeGrid OnContextMenu( string handler ) {
            return AddDataOption( "onContextMenu", handler );
        }

        /// <summary>
        /// 启用拖拽
        /// </summary>
        /// <param name="minLevel">允许拖动的最小级数,设置为2，表示第1级无法拖动</param>
        public ITreeGrid EnableDrag( int minLevel = 1 ) {
            return AddAttribute( "enableDrag", minLevel.ToString() );
        }

        /// <summary>
        /// 开启动画效果
        /// </summary>
        public ITreeGrid Animate() {
            return AddDataOption( "animate", true );
        }

        /// <summary>
        /// 获取右键菜单函数名
        /// </summary>
        protected override string GetMenuFunction() {
            return "$.easyui.showTreeGridMenu_onContextMenu";
        }
    }
}
