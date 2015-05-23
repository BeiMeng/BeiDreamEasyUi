
namespace Util.Logs.Formats {
    /// <summary>
    /// 标题格式器
    /// </summary>
    internal class CaptionFormatter : FormatterBase{
        /// <summary>
        /// 初始化标题格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public CaptionFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( "标题", Message.Caption );
            return Result.ToString();
        }
    }
}
