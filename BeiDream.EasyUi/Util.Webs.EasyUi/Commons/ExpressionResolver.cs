using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Commons {
    /// <summary>
    /// 表达式解析器
    /// </summary>
    /// <typeparam name="TControl">控件类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class ExpressionResolver<TControl, TEntity, TProperty> where TControl : IComponent<TControl> {
        /// <summary>
        /// 初始化表达式解析器
        /// </summary>
        /// <typeparam name="TControl">控件类型</typeparam>
        /// <param name="control">控件</param>
        /// <param name="expression">属性表达式</param>
        private ExpressionResolver( IValidation<TControl> control, Expression<Func<TEntity, TProperty>> expression ) {
            _control = control;
            _expression = expression;
            _memberInfo = Lambda.GetMember( _expression );
        }

        /// <summary>
        /// 控件
        /// </summary>
        private readonly IValidation<TControl> _control;
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="expression">属性表达式</param>
        public static void Resolve( IValidation<TControl> control, Expression<Func<TEntity, TProperty>> expression ) {
            new ExpressionResolver<TControl, TEntity, TProperty>( control, expression ).Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            InitType();
            var attributes = Lambda.GetAttributes<TEntity, TProperty, ValidationAttribute>( _expression );
            foreach ( var attribute in attributes )
                InitValidation( attribute );
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        private void InitType() {
            if ( Reflection.IsDate( _memberInfo ) ) {
                _control.Date();
                return;
            }
            if ( Reflection.IsInt( _memberInfo ) ) {
                _control.Int();
                return;
            }
            if ( Reflection.IsNumber( _memberInfo ) ) {
                _control.Number( 2 );
                return;
            }
        }

        /// <summary>
        /// 初始化验证
        /// </summary>
        private void InitValidation( ValidationAttribute validationAttribute ) {
            if ( InitRequired( validationAttribute ) )
                return;
            if ( InitStringLength( validationAttribute ) )
                return;
            if ( InitEmail( validationAttribute ) )
                return;
            if ( InitMobilePhone( validationAttribute ) )
                return;
            if ( InitUrl( validationAttribute ) )
                return;
        }

        /// <summary>
        /// 初始化必填项验证
        /// </summary>
        private bool InitRequired( ValidationAttribute validationAttribute ) {
            var attribute = validationAttribute as RequiredAttribute;
            if ( attribute == null )
                return false;
            _control.Required( attribute.GetErrorMessage() );
            return true;
        }

        /// <summary>
        /// 初始化字符串长度验证
        /// </summary>
        private bool InitStringLength( ValidationAttribute validationAttribute ) {
            var attribute = validationAttribute as StringLengthAttribute;
            if ( attribute == null )
                return false;
            if ( attribute.MinimumLength <= 0 ) {
                _control.MaxLength( attribute.MaximumLength );
                return true;
            }
            _control.Length( attribute.MinimumLength, attribute.MaximumLength );
            return true;
        }

        /// <summary>
        /// 初始化电子邮件验证
        /// </summary>
        private bool InitEmail( ValidationAttribute validationAttribute ) {
            var attribute = validationAttribute as EmailAddressAttribute;
            if ( attribute == null )
                return false;
            _control.Email();
            return true;
        }

        /// <summary>
        /// 初始化手机号验证
        /// </summary>
        private bool InitMobilePhone( ValidationAttribute validationAttribute ) {
            var attribute = validationAttribute as MobilePhoneAttribute;
            if ( attribute == null )
                return false;
            _control.MobilePhone();
            return true;
        }

        /// <summary>
        /// 初始化Url验证
        /// </summary>
        private bool InitUrl( ValidationAttribute validationAttribute ) {
            var attribute = validationAttribute as UrlAttribute;
            if ( attribute == null )
                return false;
            _control.ValidateUrl();
            return true;
        }
    }
}
