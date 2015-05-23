
namespace Util.Logs.Formats {
    /// <summary>
    /// 业务格式器
    /// </summary>
    internal class BusinessFormatter : FormatterBase{
        /// <summary>
        /// 初始化业务格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public BusinessFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( "业务编号", Message.BusinessId );
            Add( "应用程序", Message.Application );
            Add( "租户", Message.Tenant );
            Add( "分类", Message.Category );
            return Result.ToString();
        }
    }
}
