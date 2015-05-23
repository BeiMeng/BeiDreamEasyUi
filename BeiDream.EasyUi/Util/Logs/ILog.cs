using System;

namespace Util.Logs {
    /// <summary>
    /// 日志操作
    /// </summary>
    public interface ILog {
        /// <summary>
        /// 跟踪号
        /// </summary>
        string TraceId { get; set; }

        /// <summary>
        /// 业务编号
        /// </summary>
        string BusinessId { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        string Application { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        string Tenant { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        string Class { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        string Method { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        Str Params { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        Str Caption { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        Str Content { get; set; }
        /// <summary>
        /// Sql语句
        /// </summary>
        Str Sql { get; set; }
        /// <summary>
        /// Sql参数
        /// </summary>
        Str SqlParams { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        string ErrorCode { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// 调试
        /// </summary>
        void Debug();
        /// <summary>
        /// 信息
        /// </summary>
        void Info();
        /// <summary>
        /// 警告
        /// </summary>
        void Warn();
        /// <summary>
        /// 错误
        /// </summary>
        void Error();
        /// <summary>
        /// 致命错误
        /// </summary>
        void Fatal();
        /// <summary>
        /// 启动计时器
        /// </summary>
        void Start();
    }
}
