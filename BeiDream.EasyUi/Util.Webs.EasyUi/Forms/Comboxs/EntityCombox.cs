using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Util.Webs.EasyUi.Commons;

namespace Util.Webs.EasyUi.Forms.Comboxs {
    /// <summary>
    /// 实体组合框
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntityCombox<TEntity, TProperty> : Combox<ICombox>, ICombox {
        /// <summary>
        /// 初始化实体组合框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        public EntityCombox( Expression<Func<TEntity, TProperty>> expression )
            : this( expression,null ) {
        }

        /// <summary>
        /// 初始化实体组合框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        public EntityCombox( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper ) 
            : this( expression,helper,"","" ){
        }

        /// <summary>
        /// 初始化实体组合框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="id">控件Id</param>
        /// <param name="url">远程Url</param>
        public EntityCombox( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper,string id , string url ) {
            if ( helper != null )
                _value = helper.Value( expression );
            if ( !id.IsEmpty() )
                Id( id );
            Load( url );
            _expression = expression;
            _memberInfo = Lambda.GetMember( _expression );
            Init();
            ExpressionResolver<ICombox, TEntity, TProperty>.Resolve( this, expression );
        }

        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;
        /// <summary>
        /// 值
        /// </summary>
        private readonly object _value;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init() {
            InitName();
            InitValue();
            InitType();
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
            if ( IsLoad ) {
                LazyValue( _value.ToStr() );
                return;
            }
            Value( _value.ToStr() );
        }

        /// <summary>
        /// 根据类型初始化控件
        /// </summary>
        private void InitType() {
            if ( Reflection.IsBool( _memberInfo ) ) {
                Bool();
                return;
            }
            if ( Reflection.IsEnum( _memberInfo ) ) {
                InitEnum();
            }
        }

        /// <summary>
        /// 初始化枚举
        /// </summary>
        private void InitEnum() {
            Enum<TProperty>();
            if ( _value.ToStr().IsEmpty() )
                return;
            Value( Util.Enum.GetValue<TProperty>( _value ).ToStr() );
        }
    }
}
