using System;
using System.Collections;
using System.Text;
using Util.Exceptions;
using Util.Logs;

namespace Util {
    /// <summary>
    /// 应用程序异常
    /// </summary>
    public class Warning : Exception {

        #region 构造方法

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        public Warning( string message )
            : this( message, "" ) {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public Warning( string message, string code )
            : this( message, code, LogLevel.Warning ) {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="level">日志级别</param>
        public Warning( string message, string code, LogLevel level )
            : this( message, code, level, null ) {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="exception">异常</param>
        public Warning( Exception exception )
            : this( "", "", LogLevel.Warning, exception ) {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="exception">异常</param>
        public Warning( string message, string code, Exception exception )
            : this( message, code, LogLevel.Warning, exception ) {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="level">日志级别</param>
        /// <param name="exception">异常</param>
        public Warning( string message, string code, LogLevel level, Exception exception )
            : base( message ?? "", exception ) {
            Code = code;
            Level = level;
            _message = GetMessage();
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        private string GetMessage() {
            var result = new StringBuilder();
            AppendSelfMessage( result );
            AppendInnerMessage( result, InnerException );
            return result.ToString().TrimEnd( Environment.NewLine.ToCharArray() );
        }

        /// <summary>
        /// 添加本身消息
        /// </summary>
        private void AppendSelfMessage( StringBuilder result ) {
            if ( string.IsNullOrWhiteSpace( base.Message ) )
                return;
            result.AppendLine( base.Message );
        }

        /// <summary>
        /// 添加内部异常消息
        /// </summary>
        private void AppendInnerMessage( StringBuilder result, Exception exception ) {
            if ( exception == null )
                return;
            if ( exception is Warning ) {
                result.AppendLine( exception.Message );
                return;
            }
            result.AppendLine( exception.Message );
            result.Append( GetData( exception ) );
            AppendInnerMessage( result, exception.InnerException );
        }

        /// <summary>
        /// 获取添加的额外数据
        /// </summary>
        private string GetData( Exception ex ) {
            var result = new StringBuilder();
            foreach ( DictionaryEntry data in ex.Data )
                result.AppendFormat( "{0}:{1}{2}", data.Key, data.Value, Environment.NewLine );
            return result.ToString();
        }

        #endregion

        #region Message(错误消息)

        /// <summary>
        /// 错误消息
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// 错误消息
        /// </summary>
        public override string Message {
            get {
                if ( Data.Count == 0 )
                    return _message;
                return _message + Environment.NewLine + GetData( this );
            }
        }

        #endregion

        #region TraceId(跟踪号)

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }

        #endregion

        #region Code(错误码)

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }

        #endregion

        #region Level(日志级别)

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel Level { get; set; }

        #endregion

        #region StackTrace(堆栈跟踪)

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public override string StackTrace {
            get {
                if ( !string.IsNullOrWhiteSpace( base.StackTrace ) )
                    return base.StackTrace;
                if ( base.InnerException == null )
                    return string.Empty;
                return base.InnerException.StackTrace;
            }
        }

        #endregion

        #region WriteLog(写日志)

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志组件</param>
        public void WriteLog( ILog log ) {
            switch ( Level ) {
                case LogLevel.Debug:
                    log.Debug();
                    break;
                case LogLevel.Information:
                    log.Info();
                    break;
                case LogLevel.Warning:
                    log.Warn();
                    break;
                case LogLevel.Error:
                    log.Error();
                    break;
                case LogLevel.Fatal:
                    log.Fatal();
                    break;
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志组件</param>
        /// <param name="ex">异常</param>
        public static void WriteLog( ILog log, Exception ex ) {
            var exception = ex as Warning;
            if ( exception == null ) {
                log.Error();
                return;
            }
            log.TraceId = exception.TraceId;
            log.ErrorCode = exception.Code;
            exception.WriteLog( log );
        }

        #endregion

        #region GetPrompt(获取友情提示)

        /// <summary>
        /// 获取友情提示
        /// </summary>
        public string GetPrompt() {
            if ( Level == LogLevel.Debug )
                return R.SystemError;
            if ( Level == LogLevel.Error )
                return R.SystemError;
            return Message;
        }

        /// <summary>
        /// 获取友情提示
        /// </summary>
        /// <param name="exception">异常</param>
        public static string GetPrompt( Exception exception ) {
            if ( exception is ConcurrencyException )
                return R.ConcurrencyExceptionMessage;
            if ( IsRefrenceError( exception ) )
                return R.DataBaseRefrenceError;
            var warning = exception as Warning;
            if ( warning == null )
                return R.SystemError;
            return warning.GetPrompt();
        }

        /// <summary>
        /// 是否数据库外键约束错误
        /// </summary>
        private static bool IsRefrenceError( Exception exception ) {
            var ex = new Warning( exception );
            return ex.Message.Contains( "DELETE 语句与 REFERENCE 约束" );
        }

        #endregion
    }
}
