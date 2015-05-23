using System.Collections.Generic;
using Util.Webs.EasyUi.Commons;
using Util.Webs.EasyUi.Configs;
using Util.Webs.EasyUi.Forms.Comboxs;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 表格列
    /// </summary>
    public interface IDataGridColumn<out T> : IValidation<T> where T : IDataGridColumn<T> {
        /// <summary>
        /// 设置字段名
        /// </summary>
        /// <param name="fieldName">字段名</param>
        T Field( string fieldName );
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text">文本</param>
        T Text( string text );
        /// <summary>
        /// 设置对齐方式
        /// </summary>
        /// <param name="headerAlign">标题对齐方式</param>
        /// <param name="align">内容对齐方式</param>
        T Align( AlignLeftRigthCenter headerAlign = AlignLeftRigthCenter.Center, AlignLeftRigthCenter align = AlignLeftRigthCenter.Left );
        /// <summary>
        /// 是否允许排序
        /// </summary>
        /// <param name="isSort">是否允许排序</param>
        T Sort( bool isSort = true );
        /// <summary>
        /// 显示复选框
        /// </summary>
        /// <param name="isShow">是否显示复选框</param>
        T CheckBox( bool isShow = true );
        /// <summary>
        /// 设置格式化
        /// </summary>
        /// <param name="fn">格式化函数</param>
        T Format( string fn );
        /// <summary>
        /// 格式化布尔值
        /// </summary>
        T FormatBool();
        /// <summary>
        /// 格式化日期
        /// </summary>
        T FormatDate();
        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="isClass">值是否为class</param>
        T FormatImage( int width = 16, int height = 16,bool isClass = false );
        /// <summary>
        /// 冻结列
        /// </summary>
        T Frozen();
        /// <summary>
        /// 允许编辑
        /// </summary>
        T Edit();
        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"value"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        /// <param name="groupField">组字段名，默认为"group"</param>
        T Combox( string url, string valueField = "value", string textField = "text", string groupField = "group" );
        /// <summary>
        /// 显示下拉列表
        /// </summary>
        /// <param name="items">项集合</param>
        T Combox( IEnumerable<ComboxItem> items );
        /// <summary>
        /// 绑定枚举下拉列表
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        T Combox<TEnum>();
        /// <summary>
        /// 显示下拉树
        /// </summary>
        /// <param name="url">远程Url，返回Json数据</param>
        /// <param name="valueField">值字段名，默认为"id"</param>
        /// <param name="textField">文本字段名，默认为"text"</param>
        T ComboTree( string url, string valueField = "id", string textField = "text" );
        /// <summary>
        /// 验证必填项
        /// </summary>
        /// <param name="isRequired">true为必填项</param>
        T Required( bool isRequired = true );
        /// <summary>
        /// 是否冻结
        /// </summary>
        bool IsFrozen { get; }
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        bool IsEdit { get; }
        /// <summary>
        /// 设置面板高度，即下拉列表的高度
        /// </summary>
        /// <param name="height">面板高度，值为"auto"为自适应，也可以指定高度，范例"100"</param>
        T PanelHeight( string height = "auto" );
        /// <summary>
        /// 设置是否可编辑
        /// </summary>
        /// <param name="editable">true为可编辑</param>
        T Editable( bool editable = true );
        /// <summary>
        /// 查找带回
        /// </summary>
        /// <param name="option">查找带回配置选项</param>
        T Lookup( LookupOption option );
    }
}
