using Util.Webs.EasyUi.Commons;

namespace Util.Webs.EasyUi.Forms.TextBoxs {
    /// <summary>
    /// 文本框
    /// </summary>
    /// <typeparam name="T">文本框类型</typeparam>
    public interface ITextBox<out T> : IValidation<T> where T : ITextBox<T> {
        /// <summary>
        /// 设置name属性
        /// </summary>
        /// <param name="name">名称</param>
        T Name( string name );
        /// <summary>
        /// 设置提示消息，该消息显示在文本框中
        /// </summary>
        /// <param name="text">提示消息文本</param>
        T Prompt( string text );
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        T Value( string value );
        /// <summary>
        /// 设置为密码框
        /// </summary>
        T Password();
        /// <summary>
        /// 设置为多行文本框
        /// </summary>
        /// <param name="width">文本框宽度</param>
        /// <param name="height">文本框高度</param>
        T MultiLine( int width, int height );
        /// <summary>
        /// 禁用文本框
        /// </summary>
        /// <param name="disabled">true为禁用</param>
        T Disable( bool disabled = true );
        /// <summary>
        /// 设置文本框为只读
        /// </summary>
        /// <param name="readOnly">true为只读</param>
        T ReadOnly( bool readOnly = true );
        /// <summary>
        /// 设置文本框为可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        T Editable( bool editable = true );
        /// <summary>
        /// 设置文本框图标
        /// </summary>
        /// <param name="iconClass">图标class</param>
        /// <param name="width">图标宽度，默认18</param>
        /// <param name="align">图标对齐方式，默认右对齐</param>
        T Icon( string iconClass, int width = 18, AlignLeftRigth align = AlignLeftRigth.Right );
        /// <summary>
        /// 设置文本框按钮
        /// </summary>
        /// <param name="iconClass">文本框按钮图标class</param>
        /// <param name="clickCallback">单击回调函数，只设置函数名，范例：func</param>
        /// <param name="text">按钮文本</param>
        /// <param name="align">按钮对齐方式,默认右对齐</param>
        T Button( string iconClass, string clickCallback, string text = "", AlignLeftRigth align = AlignLeftRigth.Right );
        /// <summary>
        /// 设置文本框文本改变事件处理
        /// </summary>
        /// <param name="callback">文本改变回调函数，只设置函数名，范例：func</param>
        T OnChange( string callback );
        /// <summary>
        /// 设置延迟验证的时间，从最后输入完成开始计时
        /// </summary>
        /// <param name="time">延迟验证的时间，单位：毫秒</param>
        T Delay( int time );
        /// <summary>
        /// 设置提示位置
        /// </summary>
        /// <param name="align">提示位置</param>
        T TipPosition( AlignLeftRigth align );
        /// <summary>
        /// 设置关闭验证
        /// </summary>
        T NoValidate();
        /// <summary>
        /// 设置文本框为必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        T Required( bool isRequired = true );
        /// <summary>
        /// 设置最小长度验证
        /// </summary>
        /// <param name="minLength">最小长度</param>
        T MinLength( int minLength );
        /// <summary>
        /// 设置远程验证
        /// </summary>
        /// <param name="url">远程url</param>
        /// <param name="parameterName">参数名</param>
        T Remote( string url, string parameterName );
        /// <summary>
        /// 设置相等验证
        /// </summary>
        /// <param name="targetId">目标元素Id</param>
        /// <param name="message">消息</param>
        T EqualTo( string targetId, string message = "" );
        /// <summary>
        /// 设置最大值验证
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="message">消息</param>
        T Max( double maxValue, string message = "" );
        /// <summary>
        /// 设置最小值验证
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="message">消息</param>
        T Min( double minValue, string message = "" );
        /// <summary>
        /// 设置数值范围验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="message">消息</param>
        T Range( double min, double max, string message = "" );
        /// <summary>
        /// 显示日期时间框
        /// </summary>
        T DateTime();
        /// <summary>
        /// 显示时间框
        /// </summary>
        T Time();
    }
}
