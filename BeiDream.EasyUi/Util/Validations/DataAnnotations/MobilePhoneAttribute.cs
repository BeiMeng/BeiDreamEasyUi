using System.Globalization;
using Util;
using Util.Validations.DataAnnotations;

namespace System.ComponentModel.DataAnnotations {
    /// <summary>
    /// 手机号验证
    /// </summary>
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class MobilePhoneAttribute : ValidationAttribute {
        /// <summary>
        /// 格式化错误消息
        /// </summary>
        public override string FormatErrorMessage( string name ) {
            if ( ErrorMessage == null && ErrorMessageResourceName == null )
                ErrorMessage = ValidatorResources.InvalidMobilePhone;
            return String.Format( CultureInfo.CurrentCulture, ErrorMessageString );
        }
        
        /// <summary>
        /// 是否验证通过
        /// </summary>
        protected override ValidationResult IsValid( object value, ValidationContext validationContext ) {
            if ( value.ToStr().IsEmpty() )
                return null;
            const string pattern = "^1[3|4|5|7|8|][0-9]{9}$";
            if ( Regex.IsMatch( value.ToStr(), pattern ) )
                return null;
            return new ValidationResult( FormatErrorMessage(string.Empty) );
        }
    }
}
