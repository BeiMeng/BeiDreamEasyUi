namespace Util.Logs.Formats {
    /// <summary>
    /// 标题格式器
    /// </summary>
    internal class TitleFormatter : FormatterBase {
        /// <summary>
        /// 初始化标题格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public TitleFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            AddLevel();
            AddTraceId();
            AddTime();
            AddTotalSeconds();
            return Result.ToString();
        }

        /// <summary>
        /// 添加日志级别
        /// </summary>
        private void AddLevel() {
            Result.Add( "{0} >> ", Message.Level );
        }

        /// <summary>
        /// 添加跟踪号
        /// </summary>
        private void AddTraceId() {
            Result.Add( "跟踪号: {0}   ", Message.TraceId );
        }

        /// <summary>
        /// 添加操作时间
        /// </summary>
        private void AddTime() {
            Result.Add( "操作时间: {0}   ", Message.Time );
        }

        /// <summary>
        /// 添加已执行时间
        /// </summary>
        private void AddTotalSeconds() {
            if ( string.IsNullOrWhiteSpace( Message.TotalSeconds ) )
                return;
            Result.Add( "已执行: {0} 秒", Message.TotalSeconds );
        }
    }
}
