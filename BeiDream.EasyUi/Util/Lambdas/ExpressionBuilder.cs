using System;
using System.Linq.Expressions;

namespace Util.Lambdas {
    /// <summary>
    /// 表达式生成器
    /// </summary>
    public class ExpressionBuilder<TEntity> {
        /// <summary>
        /// 初始化表达式生成器
        /// </summary>
        public ExpressionBuilder() {
            Parameter = Expression.Parameter( typeof(TEntity), "t" );
        }

        /// <summary>
        /// 参数
        /// </summary>
        private ParameterExpression Parameter { get; set; }

        /// <summary>
        /// 获取参数
        /// </summary>
        public ParameterExpression GetParameter() {
            return Parameter;
        }

        /// <summary>
        /// 创建表达式
        /// </summary>
        /// <param name="property">属性表达式</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public Expression Create<T>( Expression<Func<TEntity,T>> property, Operator @operator, object value ) {
            return Parameter.Property( Lambda.GetMember( property ) ).Operation( @operator,value );
        }

        /// <summary>
        /// 转换为Lambda表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        public Expression<Func<TEntity, bool>> ToLambda( Expression expression ) {
            if( expression == null )
                return null;
            return expression.ToLambda<Func<TEntity, bool>>( Parameter );
        }
    }
}
