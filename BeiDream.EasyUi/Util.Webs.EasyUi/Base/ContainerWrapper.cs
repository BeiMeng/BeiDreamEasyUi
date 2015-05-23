using System;

namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 容器包装器，用来支持using
    /// </summary>
    internal class ContainerWrapper<T> : IDisposable where T : IContainer<T> {
        /// <summary>
        /// 初始化容器包装器
        /// </summary>
        /// <param name="container">容器</param>
        public ContainerWrapper( ContainerBase<T> container ) {
            _container = container;
        }

        /// <summary>
        /// 容器
        /// </summary>
        private readonly ContainerBase<T> _container;

        /// <summary>
        /// 使用using输出结束标签
        /// </summary>
        public void Dispose() {
            _container.End();
        }
    }
}
