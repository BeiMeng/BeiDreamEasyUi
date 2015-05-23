using System;
using BeiDream.Common;

namespace BeiDream.PetaPoco {
    /// <summary>
    /// PetaPoco工作单元
    /// </summary>
    public abstract class PetaPocoUnitOfWork : Database, IUnitOfWork
    {
        /// <summary>
        /// 初始化PetaPoco工作单元
        /// </summary>
        /// <param name="connectionName">连接字符串的名称</param>
        protected PetaPocoUnitOfWork(string connectionName) 
            : base( connectionName ){
            TraceId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 启动标识
        /// </summary>
        private bool IsStart { get; set; }

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; private set; }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start() {
            IsStart = true;
            BeginTransaction();
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        public void Commit() {

            try
            {
                CompleteTransaction();
            }
            catch (Exception)
            {
                AbortTransaction();
                throw;
            }
            finally
            {
                IsStart = false;
            }
        }

        /// <summary>
        /// 通过启动标识执行提交，如果已启动，则不提交
        /// </summary>
        internal void CommitByStart() {
            if ( IsStart )
                return;
            Commit();
        }
    }
}
