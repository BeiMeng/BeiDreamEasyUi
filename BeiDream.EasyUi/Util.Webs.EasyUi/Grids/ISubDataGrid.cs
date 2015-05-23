using Util.Webs.EasyUi.Configs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 子表格
    /// </summary>
    public interface ISubDataGrid : IDataGrid<ISubDataGrid> {
        /// <summary>
        /// 获取配置项
        /// </summary>
        SubGridOption GetOption();
        /// <summary>
        /// 设置列集合
        /// </summary>
        /// <param name="columns">列集合</param>
        ISubDataGrid Columns( params ISubGridColumn[] columns );
        /// <summary>
        /// 设置导航属性，用于加载子表格
        /// </summary>
        /// <param name="navigateProperty">导航属性</param>
        ISubDataGrid Property( string navigateProperty );
    }
}
