using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Util.Validations {
    /// <summary>
    /// 验证操作
    /// </summary>
    public class Validation : IValidation {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="target">验证目标</param>
        public ValidationResultCollection Validate( object target ) {
            target.CheckNull( "target" );
            var result = new ValidationResultCollection();
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext( target, null, null );
            var isValid = Validator.TryValidateObject( target, context, validationResults, true );
            if ( !isValid )
                result.AddResults( validationResults );
            return result;
        }
    }
}
