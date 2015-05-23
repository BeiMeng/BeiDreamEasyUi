namespace Util.Webs.EasyUi.Base {
    /// <summary>
    /// 基组件
    /// </summary>
    public abstract class ComponentBase<T> : OptionBase<T>, IComponent<T> where T : IComponent<T> {
        /// <summary>
        /// 控件后Html
        /// </summary>
        private string _afterHtml;
        /// <summary>
        /// 标识
        /// </summary>
        private string _id;

        /// <summary>
        /// 获取标识
        /// </summary>
        public string GetId() {
            return _id;
        }

        /// <summary>
        /// 设置标识
        /// </summary>
        /// <param name="id">标识</param>
        public T Id( string id ) {
            _id = id;
            return UpdateAttribute( "id",id );
        }

        /// <summary>
        /// 在控件后添加html
        /// </summary>
        /// <param name="html">Html</param>
        public T AddAfter( string html ) {
            _afterHtml = html;
            return This();
        }

        /// <summary>
        /// 输出
        /// </summary>
        public virtual string ToHtmlString() {
            return GetResult() + _afterHtml;
        }

        /// <summary>
        /// 获取输出结果
        /// </summary>
        protected abstract string GetResult();
    }
}
