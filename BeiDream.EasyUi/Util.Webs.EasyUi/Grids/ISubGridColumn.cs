using Util.Webs.EasyUi.Configs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 子表格列
    /// </summary>
    public interface ISubGridColumn : IDataGridColumn<ISubGridColumn>{
        /// <summary>
        /// 获取配置项
        /// </summary>
        DataGridColumnOption GetOption();
    }
}
