using System;

namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 容器
    /// </summary>
    /// <typeparam name="T">容器类型</typeparam>
    public interface IContainer<out T> : IOption<T> where T : IContainer<T> {
        /// <summary>
        /// 输出容器起始标签
        /// </summary>
        IDisposable Begin();
    }
}
