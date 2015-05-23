using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Util.Webs.EasyUi.Commons;

namespace Util.Webs.EasyUi.Forms.TextBoxs {
    /// <summary>
    /// 实体文本框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntityTextBox<TEntity, TProperty> : TextBox<ITextBox>, ITextBox {
        /// <summary>
        /// 初始化实体文本框
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        public EntityTextBox( Expression<Func<TEntity, TProperty>> propertyExpression )
            : this( propertyExpression,null ) {
        }

        /// <summary>
        /// 初始化实体文本框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public EntityTextBox( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper ) {
            if ( helper != null )
                _value = helper.Value( expression );
            _expression = expression;
            Init();
            ExpressionResolver<ITextBox, TEntity, TProperty>.Resolve( this, expression );
        }

        /// <summary>
        /// 值
        /// </summary>
        private readonly object _value;
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init() {
            InitName();
            InitValue();
        }

        /// <summary>
        /// 初始化name属性
        /// </summary>
        private void InitName() {
            Name( Lambda.GetName( _expression ) );
        }

        /// <summary>
        /// 初始值value属性
        /// </summary>
        private void InitValue() {
            Value( _value.ToStr() );
        }
    }
}
