namespace Util.Logs.Formats {
    /// <summary>
    /// Url格式器
    /// </summary>
    internal class UrlFormatter : FormatterBase{
        /// <summary>
        /// 初始化Url格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public UrlFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            AddUrl();
            return Result.ToString();
        }

        /// <summary>
        /// 添加Url
        /// </summary>
        private void AddUrl() {
            if ( string.IsNullOrWhiteSpace( Message.Url ) )
                return;
            Result.Add( "Url: {0}", Message.Url );
        }
    }
}
