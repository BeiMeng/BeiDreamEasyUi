using System.Collections.Generic;
using System.Linq;
using Json.Net;

namespace Util.Webs.EasyUi.Configs {
    /// <summary>
    /// 表格配置项
    /// </summary>
    public class DataGridOption {
        /// <summary>
        /// 列自适应
        /// </summary>
        [Json( PropertyName = "fitColumns", NullValueHandling = NullValueHandling.Ignore )]
        public bool? FitColumns { get; set; }
        /// <summary>
        /// 外键列
        /// </summary>
        [Json( PropertyName = "foreignField", NullValueHandling = NullValueHandling.Ignore )]
        public string ForeignField { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        [Json( PropertyName = "property", NullValueHandling = NullValueHandling.Ignore )]
        public string Property { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        [Json( PropertyName = "columns", NullValueHandling = NullValueHandling.Ignore )]
        protected List<List<DataGridColumnOption>> Columns { get; private set; }
        /// <summary>
        /// 显示行号
        /// </summary>
        [Json( PropertyName = "rownumbers", NullValueHandling = NullValueHandling.Ignore )]
        public bool? RowNumbers { get; set; }
        /// <summary>
        /// 设置自适应布局
        /// </summary>
        [Json( PropertyName = "fit", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Fit { get; set; }
        /// <summary>
        /// 显示分页
        /// </summary>
        [Json( PropertyName = "pagination", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Pagination { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        [Json( PropertyName = "pageSize", NullValueHandling = NullValueHandling.Ignore )]
        public int? PageSize { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Json( PropertyName = "sortName", NullValueHandling = NullValueHandling.Ignore )]
        public string SortName { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        [Json( PropertyName = "sortOrder", NullValueHandling = NullValueHandling.Ignore )]
        public string SortOrder { get; set; }
        /// <summary>
        /// 选择表格行时是否同时选中复选框
        /// </summary>
        [Json( PropertyName = "checkOnSelect", NullValueHandling = NullValueHandling.Ignore )]
        public bool? CheckOnSelect { get; set; }
        /// <summary>
        /// 选中复选框时是否同时选中表格行
        /// </summary>
        [Json( PropertyName = "selectOnCheck", NullValueHandling = NullValueHandling.Ignore )]
        public bool? SelectOnCheck { get; set; }
        /// <summary>
        /// 是否只能选中一行
        /// </summary>
        [Json( PropertyName = "singleSelect", NullValueHandling = NullValueHandling.Ignore )]
        public bool? SingleSelect { get; set; }
        /// <summary>
        /// 是否显示条纹
        /// </summary>
        [Json( PropertyName = "striped", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Striped { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        [Json( PropertyName = "url", NullValueHandling = NullValueHandling.Ignore )]
        public string Url { get; set; }
        /// <summary>
        /// 双击行事件处理函数
        /// </summary>
        [Json( false, PropertyName = "onDblClickRow", NullValueHandling = NullValueHandling.Ignore )]
        public string OnDblClickRow { get; set; }
        /// <summary>
        /// 右键单击行事件处理函数
        /// </summary>
        [Json( false, PropertyName = "onRowContextMenu", NullValueHandling = NullValueHandling.Ignore )]
        public string OnRowContextMenu { get; set; }
        /// <summary>
        /// 加载成功事件处理函数
        /// </summary>
        [Json( false, PropertyName = "onLoadSuccess", NullValueHandling = NullValueHandling.Ignore )]
        public string OnLoadSuccess { get; set; }

        /// <summary>
        /// 添加列配置项
        /// </summary>
        /// <param name="option">列配置项</param>
        public void AddColumn( DataGridColumnOption option ) {
            if ( option == null )
                return;
            if ( Columns == null )
                Columns = new List<List<DataGridColumnOption>>();
            var options = Columns.FirstOrDefault();
            if ( options == null ) {
                options = new List<DataGridColumnOption>();
                Columns.Add( options );
            }
            options.Add( option );
        }

        /// <summary>
        /// 输出Json结果
        /// </summary>
        public override string ToString() {
            return Json.ToJson( this, true );
        }
    }
}
