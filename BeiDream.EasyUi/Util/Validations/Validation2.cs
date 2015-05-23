using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Util.Validations {
    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validation2 : IValidation {
        /// <summary>
        /// 初始化验证操作
        /// </summary>
        public Validation2() {
            _result = new ValidationResultCollection();
        }

        /// <summary>
        /// 验证目标
        /// </summary>
        private object _target;
        /// <summary>
        /// 结果
        /// </summary>
        private readonly ValidationResultCollection _result;

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">验证目标</param>
        public ValidationResultCollection Validate( object target ) {
            target.CheckNull( "target" );
            _target = target;
            Type type = target.GetType();
            var properties = type.GetProperties();
            foreach( var property in properties )
                ValidateProperty( property );
            return _result;
        }

        /// <summary>
        /// 验证属性
        /// </summary>
        private void ValidateProperty( PropertyInfo property ) {
            var attributes = property.GetCustomAttributes( typeof( ValidationAttribute ), true );
            foreach( var attribute in attributes ) {
                var validationAttribute = attribute as ValidationAttribute;
                if ( validationAttribute == null )
                    continue;
                ValidateAttribute( property, validationAttribute );
            }
        }

        /// <summary>
        /// 验证特性
        /// </summary>
        private void ValidateAttribute( PropertyInfo property, ValidationAttribute attribute ) {
            bool isValid = attribute.IsValid( property.GetValue( _target ) );
            if( isValid )
                return;
            _result.Add( new ValidationResult( GetErrorMessage( attribute ) ) ); 
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        private string GetErrorMessage( ValidationAttribute attribute ) {
            if( !string.IsNullOrEmpty( attribute.ErrorMessage ) )
                return attribute.ErrorMessage;
            return ResourceHelper.GetString( attribute.ErrorMessageResourceType.FullName, attribute.ErrorMessageResourceName,attribute.ErrorMessageResourceType.Assembly );
        }
    }
}
