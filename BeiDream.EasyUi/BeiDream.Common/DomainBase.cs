using System.Collections.Generic;
using Util.Validations;

namespace BeiDream.Common {
    /// <summary>
    /// 领域层顶级基类
    /// </summary>
    public abstract class DomainBase {

        #region 构造方法

        /// <summary>
        /// 初始化领域层顶级基类
        /// </summary>
        protected DomainBase() {
            _rules = new List<IValidationRule>();
            _handler = new ValidationHandler();
        }

        #endregion

        #region 字段

        /// <summary>
        /// 验证规则集合
        /// </summary>
        private readonly List<IValidationRule> _rules;
        /// <summary>
        /// 验证处理器
        /// </summary>
        private IValidationHandler _handler;

        #endregion

        #region SetValidationHandler(设置验证处理器)

        /// <summary>
        /// 设置验证处理器
        /// </summary>
        /// <param name="handler">验证处理器</param>
        public void SetValidationHandler( IValidationHandler handler ) {
            if ( handler == null )
                return;
            _handler = handler;
        }

        #endregion

        #region AddValidationRule(添加验证规则)

        /// <summary>
        /// 添加验证规则
        /// </summary>
        /// <param name="rule">验证规则</param>
        public void AddValidationRule( IValidationRule rule ) {
            if ( rule == null )
                return;
            _rules.Add( rule );
        }

        #endregion

        #region Validate(验证)

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate() {
            var result = GetValidationResult();
            HandleValidationResult( result );
        }

        /// <summary>
        /// 获取验证结果
        /// </summary>
        private ValidationResultCollection GetValidationResult() {
            var result = ValidationFactory.Create().Validate( this );
            Validate( result );
            foreach ( var rule in _rules )
                result.Add( rule.Validate() );
            return result;
        }

        /// <summary>
        /// 验证并添加到验证结果集合
        /// </summary>
        /// <param name="results">验证结果集合</param>
        protected virtual void Validate( ValidationResultCollection results ) {
        }

        /// <summary>
        /// 处理验证结果
        /// </summary>
        private void HandleValidationResult( ValidationResultCollection results ) {
            if ( results.IsValid )
                return;
            _handler.Handle( results );
        }

        #endregion
    }
}
