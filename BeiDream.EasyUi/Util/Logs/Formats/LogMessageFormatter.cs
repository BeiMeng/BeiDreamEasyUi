namespace Util.Logs.Formats {
    /// <summary>
    /// 日志消息格式器
    /// </summary>
    internal class LogMessageFormatter : FormatterBase{
        /// <summary>
        /// 初始化日志消息格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public LogMessageFormatter( LogMessage message )
            : base( message ) {
            Line = 1;
        }

        /// <summary>
        /// 行号
        /// </summary>
        private int Line { get; set; }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( new TitleFormatter( Message ) );
            Add( new UrlFormatter( Message ) );
            Add( new BusinessFormatter( Message ) );
            Add( new ClassFormatter( Message ) );
            Add( new IpFormatter( Message ) );
            Add( new UserFormatter( Message ) );
            Add( new CaptionFormatter( Message ) );
            Add( new ContentFormatter( Message ) );
            Add( new SqlFormatter( Message ) );
            Add( new SqlParamsFormatter( Message ) );
            Add( new ErrorFormatter( Message ) );
            Add( new StackTraceFormatter( Message ) );
            Finish();
            return Result.ToString();
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        private void Add( FormatterBase formatter ) {
            string result = formatter.Format();
            if ( string.IsNullOrWhiteSpace( result ) )
                return;
            Result.AddLine( "{0}. {1}", Line++, result );
        }

        /// <summary>
        /// 结束
        /// </summary>
        private void Finish() {
            for( int i = 0; i < 125; i++ )
                Result.Add( "-" );
            Result.AddLine();
        }
    }
}
