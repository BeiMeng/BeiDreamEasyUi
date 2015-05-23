﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace Util.Tests {
    /// <summary>
    /// 单元测试辅助操作
    /// </summary>
    public static class UnitTest {
        /// <summary>
        /// 并发测试
        /// </summary>
        /// <param name="action">各线程执行的方法</param>
        /// <param name="threadNumber">启动线程数，默认为1个</param>
        public static void TestConcurrency( Action action,int threadNumber = 1 ) {
            var test = new Test();
            test.Start();
            Console.WriteLine( "并发模拟测试开始" );
            var threads = new List<System.Threading.Thread>();
            var resetEvent = new ManualResetEvent( false );
            for ( int i = 0; i < threadNumber; i++ ) {
                var thread = new System.Threading.Thread( number => {
                    Console.WriteLine( "线程{0}执行挂起操作,线程号：{1},耗时：{2}秒", number,Thread.ThreadId, test.GetElapsed() );
                    resetEvent.WaitOne();
                    action();
                    Console.WriteLine( "线程{0}执行任务完成,线程号：{1},耗时：{2}秒", number, Thread.ThreadId, test.GetElapsed() );
                } );
                thread.Start( i + 1 );
                threads.Add( thread );
            }
            Console.WriteLine( "暂停50毫秒后唤醒所有线程" );
            Thread.Sleep( 50 );
            resetEvent.Set();
            threads.ForEach( t => t.Join() );
            Console.WriteLine( "执行完成,即将退出，耗时：{0}秒", test.GetElapsed() );
        }
    }
}
