using System.Diagnostics;

namespace Util {
    /// <summary>
    /// 测试操作
    /// </summary>
    public class Test {
        /// <summary>
        /// 初始化
        /// </summary>
        public Test() {
            _watch = new Stopwatch();
        }

        /// <summary>
        /// 测试运行时间
        /// </summary>
        private readonly Stopwatch _watch;

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start() {
            _watch.Start();
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset() {
            _watch.Reset();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        public void Stop() {
            _watch.Stop();
        }

        /// <summary>
        /// 获取运行时间间隔,单位：秒
        /// </summary>
        public double GetElapsed() {
            return _watch.Elapsed.TotalSeconds;
        }

        /// <summary>
        /// 停止并获取运行时间间隔,单位：秒
        /// </summary>
        public double GetElapsedAndStop() {
            Stop();
            return GetElapsed();
        }
    }
}
