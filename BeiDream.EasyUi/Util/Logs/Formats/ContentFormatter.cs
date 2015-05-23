
namespace Util.Logs.Formats {
    /// <summary>
    /// 内容格式器
    /// </summary>
    internal class ContentFormatter : FormatterBase{
        /// <summary>
        /// 初始化内容格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public ContentFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            if ( string.IsNullOrWhiteSpace( Message.Content ) )
                return string.Empty;
            Result.AddLine( "内容:" );
            Result.Add( Message.Content );
            return Result.ToString();
        }
    }
}
