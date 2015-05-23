using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Util {
    /// <summary>
    /// 线程操作
    /// </summary>
    public class Thread {

        #region ThreadId(线程编号)

        /// <summary>
        /// 线程编号
        /// </summary>
        public static string ThreadId {
            get {
                return System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            }
        }

        #endregion

        #region CurrentPrincipal(获取当前安全主体)

        /// <summary>
        /// 获取当前安全主体
        /// </summary>
        public static IPrincipal CurrentPrincipal {
            get { return System.Threading.Thread.CurrentPrincipal; }
            set { System.Threading.Thread.CurrentPrincipal = value; }
        }

        #endregion

        #region Sleep(线程等待)

        /// <summary>
        /// 线程等待
        /// </summary>
        /// <param name="time">等待时间，单位：毫秒</param>
        public static void Sleep( int time ) {
            System.Threading.Thread.Sleep( time );
        }

        #endregion

        #region 获取线程池中最大线程数

        /// <summary>
        /// 获取线程池中最大线程数
        /// </summary>
        public static int MaxThreadNumberInThreadPool {
            get {
                int maxNumber;
                int ioNumber;
                ThreadPool.GetMaxThreads( out maxNumber, out ioNumber );
                return maxNumber;
            }
        }

        #endregion

        #region StartTask(启动异步任务)

        /// <summary>
        /// 启动异步任务
        /// </summary>
        /// <param name="handler">任务，范例：() => { 代码 }</param>
        public static void StartTask( Action handler ) {
            Task.Factory.StartNew( handler );
        }

        #endregion
    }
}
