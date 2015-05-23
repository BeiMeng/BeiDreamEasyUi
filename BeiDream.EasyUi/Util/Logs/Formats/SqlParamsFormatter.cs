
namespace Util.Logs.Formats {
    /// <summary>
    /// Sql参数格式器
    /// </summary>
    internal class SqlParamsFormatter : FormatterBase {
        /// <summary>
        /// 初始化Sql参数格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public SqlParamsFormatter( LogMessage message )
            : base( message ) {
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            if ( string.IsNullOrWhiteSpace( Message.SqlParams ) )
                return string.Empty;
            Result.AddLine( "Sql参数:" );
            Result.Add( Message.SqlParams );
            return Result.ToString();
        }
    }
}
