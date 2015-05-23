namespace Util.Exports {
    /// <summary>
    /// 文件导出操作工厂
    /// </summary>
    public interface IExportFactory {
        /// <summary>
        /// 创建文件导出操作
        /// </summary>
        /// <param name="format">导出格式</param>
        IExport Create( ExportFormat format );
    }
}
