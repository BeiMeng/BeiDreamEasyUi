
namespace Util.Logs.Formats {
    /// <summary>
    /// 用户格式器
    /// </summary>
    internal class UserFormatter : FormatterBase{
        /// <summary>
        /// 初始化用户格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public UserFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( "操作人编号", Message.UserId );
            Add( "操作人姓名", Message.Operator );
            Add( "角色", Message.Role );
            return Result.ToString();
        }
    }
}
