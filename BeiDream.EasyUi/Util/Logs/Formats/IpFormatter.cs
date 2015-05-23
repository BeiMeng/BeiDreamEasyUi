
namespace Util.Logs.Formats {
    /// <summary>
    /// Ip格式器
    /// </summary>
    internal class IpFormatter : FormatterBase{
        /// <summary>
        /// 初始化Ip格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public IpFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( "Ip", Message.Ip );
            Add( "主机", Message.Host );
            Add( "线程号", Message.ThreadId );
            return Result.ToString();
        }
    }
}
