using System;

namespace BeiDream.Common {
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork: IDisposable {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 提交更新
        /// </summary>
        void Commit();
    }
}
