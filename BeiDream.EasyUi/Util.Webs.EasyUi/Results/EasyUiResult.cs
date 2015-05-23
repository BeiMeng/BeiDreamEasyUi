using System.Collections.Generic;

namespace Util.Webs.EasyUi.Results {
    /// <summary>
    /// EasyUi提交表单返回结果
    /// </summary>
    public class EasyUiResult : ResultBase {
        /// <summary>
        /// 初始化EasyUi提交表单返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public EasyUiResult( StateCode code, string message, IEnumerable<object> data = null ) {
            _code = code;
            _message = message;
            _data = data;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        private readonly StateCode _code;
        /// <summary>
        /// 消息
        /// </summary>
        private readonly string _message;
        /// <summary>
        /// 数据
        /// </summary>
        private readonly IEnumerable<object> _data;

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return Json.ToJson( new { Code = _code.Value(), Message = _message, Data = _data } );
        }
    }
}
