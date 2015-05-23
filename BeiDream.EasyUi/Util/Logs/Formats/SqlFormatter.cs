
namespace Util.Logs.Formats {
    /// <summary>
    /// Sql格式器
    /// </summary>
    internal class SqlFormatter : FormatterBase {
        /// <summary>
        /// 初始化Sql格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public SqlFormatter( LogMessage message )
            : base( message ) {
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            if ( string.IsNullOrWhiteSpace( Message.Sql ) )
                return string.Empty;
            Result.AddLine( "Sql语句:" );
            Result.Add( Message.Sql );
            return Result.ToString();
        }
    }
}
