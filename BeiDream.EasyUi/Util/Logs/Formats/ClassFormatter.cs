namespace Util.Logs.Formats {
    /// <summary>
    /// 类格式器
    /// </summary>
    internal class ClassFormatter : FormatterBase {
        /// <summary>
        /// 初始化类格式器
        /// </summary>
        /// <param name="message">日志消息</param>
        public ClassFormatter( LogMessage message )
            : base( message ){
        }

        /// <summary>
        /// 格式化
        /// </summary>
        public override string Format() {
            Add( "类名", Message.Class );
            Add( "方法", Message.Method );
            AddParams();
            return Result.ToString();
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        private void AddParams() {
            if ( string.IsNullOrWhiteSpace( Message.Params ) )
                return;
            Result.AddLine( "参数: " );
            Result.Add( Message.Params );
        }
    }
}
