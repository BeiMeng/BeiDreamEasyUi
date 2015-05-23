using System.Text;

namespace Util {
    /// <summary>
    /// 字符串操作
    /// </summary>
    public sealed partial class Str {
        /// <summary>
        /// 初始化字符串操作
        /// </summary>
        public Str() {
            Builder = new StringBuilder();
        }

        /// <summary>
        /// 字符串生成器
        /// </summary>
        private StringBuilder Builder { get; set; }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="value">值</param>
        public void Add<T>( T value ) {
            Builder.Append( value );
        }

        /// <summary>
        /// 添加内容
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void Add( string value, params object[] args ) {
            if ( args == null )
                args = new object[] { string.Empty };
            if ( args.Length == 0 )
                Builder.Append( value );
            else
                Builder.AppendFormat( value, args );
        }

        /// <summary>
        /// 添加换行
        /// </summary>
        public void AddLine() {
            Builder.AppendLine();
        }

        /// <summary>
        /// 添加内容并换行
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="value">值</param>
        public void AddLine<T>( T value ) {
            Add( value );
            Builder.AppendLine();
        }

        /// <summary>
        /// 添加内容并换行
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="args">参数</param>
        public void AddLine( string value, params object[] args ) {
            Add( value, args );
            Builder.AppendLine();
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="end">末尾字符串</param>
        public void RemoveEnd( string end ) {
            string result = Builder.ToString();
            if ( !result.EndsWith( end ) )
                return;
            int index = result.LastIndexOf( end, System.StringComparison.Ordinal );
            Builder = Builder.Remove( index, end.Length );
        }

        /// <summary>
        /// 清空字符串
        /// </summary>
        public void Clear() {
            Builder = Builder.Clear();
        }

        /// <summary>
        /// 字符串长度
        /// </summary>
        public int Length {
            get {
                return Builder.Length;
            }
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString() {
            return Builder.ToString();
        }
    }
}
