namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 树型表格
    /// </summary>
    public interface ITreeGrid : IDataGrid<ITreeGrid> {
        /// <summary>
        /// 设置Id属性名
        /// </summary>
        /// <param name="field">Id属性名</param>
        ITreeGrid IdField( string field );
        /// <summary>
        /// 设置树属性名
        /// </summary>
        /// <param name="field">属性名</param>
        ITreeGrid TreeField( string field );
        /// <summary>
        /// 启用拖拽
        /// </summary>
        /// <param name="minLevel">允许拖动的最小级数,设置为2，表示第1级无法拖动</param>
        ITreeGrid EnableDrag( int minLevel = 1 );
        /// <summary>
        /// 开启动画效果
        /// </summary>
        ITreeGrid Animate();
    }
}
