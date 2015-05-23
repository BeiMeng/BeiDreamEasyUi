using System.Collections.Generic;
using Util.Webs.EasyUi.Forms.TextBoxs;

namespace Util.Webs.EasyUi.Forms.Comboxs {
    /// <summary>
    /// 组合框
    /// </summary>
    /// <typeparam name="T">组合框控件</typeparam>
    public interface ICombox<out T> : ICombo<T> where T : ICombox<T>{
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        T Add( string text, object value = null );
        /// <summary>
        /// 添加项集合
        /// </summary>
        /// <param name="items">项集合</param>
        T Add( IEnumerable<ComboxItem> items );
        /// <summary>
        /// 添加默认项
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        T AddDefault( string text, object value = null );
        /// <summary>
        /// 绑定bool值
        /// </summary>
        T Bool();
        /// <summary>
        /// 绑定bool值
        /// </summary>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        T Bool( string text, object value = null );
        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        T Enum<TEnum>();
        /// <summary>
        /// 绑定枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="text">默认项的文本</param>
        /// <param name="value">默认项的值</param>
        T Enum<TEnum>( string text, object value = null );
        /// <summary>
        /// 从远程加载数据
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        T Load( string url, string valueField = "value", string textField = "text", string groupField = "group" );
        /// <summary>
        /// 设置联动子控件
        /// </summary>
        /// <param name="childId">联动子控件</param>
        /// <param name="requestParam">请求参数</param>
        /// <param name="url">子控件加载url</param>
        T Child( string childId,string requestParam = "id",string url = "" );
        /// <summary>
        /// 延迟设置值，当数据加载完成时设置
        /// </summary>
        /// <param name="value">值</param>
        T LazyValue( string value );
        /// <summary>
        /// 添加隐藏控件，用于保存文本
        /// </summary>
        /// <param name="hiddenName">hidden控件name</param>
        /// <param name="text">显示文本</param>
        T Hidden( string hiddenName,string text );
        /// <summary>
        /// 从远程加载数据，该方法将Load和LazyValue方法合并，提供一个更易用的操作
        /// </summary>
        /// <param name="url">远程Url，服务端请使用ToComboxResult方法返回数据</param>
        /// <param name="value">值</param>
        T Url( string url, string value );
    }
}
