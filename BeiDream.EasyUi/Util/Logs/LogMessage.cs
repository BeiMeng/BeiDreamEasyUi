using Util.Logs.Formats;

namespace Util.Logs {
    /// <summary>
    /// 日志消息
    /// </summary>
    public class LogMessage {
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public string TotalSeconds { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public string Tenant { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 线程号
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作人角色
        /// </summary>
        public string Role { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Sql语句
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// Sql参数
        /// </summary>
        public string SqlParams { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        public override string ToString() {
            return new LogMessageFormatter( this ).Format();
        }
    }
}
