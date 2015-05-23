using System.Collections.Generic;
using System.Linq;

namespace Util.Webs.EasyUi.Commons {
    /// <summary>
    /// 验证器
    /// </summary>
    internal class Validator {
        /// <summary>
        /// 初始化验证器
        /// </summary>
        public Validator() {
            _validTypes = new List<string>();
            _builder = new JsonAttributeBuilder();
        }

        /// <summary>
        /// 验证类型
        /// </summary>
        private readonly List<string> _validTypes;
        /// <summary>
        /// Json属性生成器
        /// </summary>
        private readonly JsonAttributeBuilder _builder;

        /// <summary>
        /// 添加验证条件
        /// </summary>
        private void AddValidation( string validation, params object[] args ) {
            if ( validation.IsEmpty() )
                return;
            _validTypes.Add( string.Format( validation, args ) );
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        public void Required( bool isRequired = true ) {
            _builder.Add( "required", isRequired.ToString().ToLower() );
        }

        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="message">验证失败消息</param>
        public void Required( string message ) {
            Required();
            _builder.Add( "missingMessage",message,"'");
        }

        /// <summary>
        /// 设置Email验证
        /// </summary>
        public void Email() {
            AddValidation( "email" );
        }

        /// <summary>
        /// 设置手机号验证
        /// </summary>
        public void MobilePhone() {
            AddValidation( "mobilePhone" );
        }

        /// <summary>
        /// 设置Url验证
        /// </summary>
        public void Url() {
            AddValidation( "url" );
        }

        /// <summary>
        /// 设置长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public void Length( int minLength, int maxLength ) {
            AddValidation( "length[{0},{1}]", minLength, maxLength );
        }

        /// <summary>
        /// 设置最小长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        public void MinLength( int minLength ) {
            AddValidation( "minLength[{0}]", minLength );
        }

        /// <summary>
        /// 设置最大长度验证
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public void MaxLength( int maxLength ) {
            AddValidation( "maxLength[{0}]", maxLength );
        }

        /// <summary>
        /// 设置远程验证
        /// </summary>
        /// <param name="url">远程url</param>
        /// <param name="parameterName">参数名</param>
        public void Remote( string url, string parameterName ) {
            AddValidation( "remote[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, url, parameterName );
        }

        /// <summary>
        /// 设置相等验证
        /// </summary>
        /// <param name="targetId">目标元素Id</param>
        /// <param name="message">消息</param>
        public void EqualTo( string targetId, string message = "" ) {
            AddValidation( "equalTo[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, targetId, message );
        }

        /// <summary>
        /// 设置最大值验证
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="message">消息</param>
        public void Max( double maxValue, string message = "" ) {
            AddValidation( "max[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, maxValue, message );
        }

        /// <summary>
        /// 设置最小值验证
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="message">消息</param>
        public void Min( double minValue, string message = "" ) {
            AddValidation( "min[{0}{1}{0},{0}{2}{0}]", HtmlEscape.Quote, minValue, message );
        }

        /// <summary>
        /// 设置数值范围验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="message">消息</param>
        public void Range( double min, double max, string message = "" ) {
            AddValidation( "range[{0}{1}{0},{0}{2}{0},{0}{3}{0}]", HtmlEscape.Quote, min, max, message );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public string GetResult() {
            AddValidType();
            return _builder.GetResult();
        }

        /// <summary>
        /// 添加验证类型
        /// </summary>
        private void AddValidType() {
            var validType = GetValidType();
            if ( validType.IsEmpty() )
                return;
            _builder.Add( "validType", validType );
        }

        /// <summary>
        /// 获取验证类型
        /// </summary>
        private string GetValidType() {
            if ( _validTypes.Count == 0 )
                return string.Empty;
            if ( _validTypes.Count == 1 )
                return string.Format( "'{0}'", _validTypes[0] );
            return string.Format( "[{0}]", _validTypes.Splice( "'" ) );
        }
    }
}
