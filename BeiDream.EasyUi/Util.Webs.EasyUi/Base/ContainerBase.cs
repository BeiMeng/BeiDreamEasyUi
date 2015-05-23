using System;
using Util.Webs.TextWriters;

namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 基容器
    /// </summary>
    /// <typeparam name="T">基容器类型</typeparam>
    public abstract class ContainerBase<T> : OptionBase<T>, IContainer<T> where T : IContainer<T> {
        /// <summary>
        /// 初始化基容
        /// </summary>
        /// <param name="writer">文本写入器</param>
        protected ContainerBase( ITextWriter writer ) {
            writer.CheckNull( "writer" );
            _writer = writer;
        }

        /// <summary>
        /// 文本写入器
        /// </summary>
        private readonly ITextWriter _writer;

        /// <summary>
        /// 容器起始标签
        /// </summary>
        public IDisposable Begin() {
            _writer.Write( GetBeginResult() );
            return new ContainerWrapper<T>( this );
        }

        /// <summary>
        /// 获取容器起始标签
        /// </summary>
        protected virtual string GetBeginResult() {
            return string.Format( "<div {0}>", GetOptions() );
        }

        /// <summary>
        /// 使用using输出结束标签
        /// </summary>
        public void End() {
            _writer.Write( GetEndResult() );
        }

        /// <summary>
        /// 获取容器结束标签
        /// </summary>
        protected virtual string GetEndResult() {
            return "</div>";
        }
    }
}
