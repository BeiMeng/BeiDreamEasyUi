namespace Util.Webs.EasyUi.Layouts {
    /// <summary>
    /// 面板
    /// </summary>
    public interface IPanel : ILayoutRegion<IPanel> {
        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        IPanel Id( string id );
        /// <summary>
        /// 设置自适应
        /// </summary>
        IPanel Fit();
        /// <summary>
        /// 设置页脚
        /// </summary>
        /// <param name="id">页脚div的id</param>
        IPanel Footer( string id );
    }
}
