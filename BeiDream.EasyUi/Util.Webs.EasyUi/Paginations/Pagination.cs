using System.Text;
using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Paginations {
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination : ComponentBase<IPagination>, IPagination {
        /// <summary>
        /// 初始化分页
        /// </summary>
        public Pagination() {
            AddClass( "easyui-pagination" );
        }

        /// <summary>
        /// 设置总记录数
        /// </summary>
        /// <param name="number">总记录数</param>
        public IPagination Total( int number ) {
            return AddDataOption( "total", number );
        }

        /// <summary>
        /// 设置每页显示行数
        /// </summary>
        /// <param name="number">每页显示行数</param>
        public IPagination PageSize( int number = 20 ) {
            return AddDataOption( "pageSize", number );
        }

        /// <summary>
        /// 设置分页大小集合
        /// </summary>
        /// <param name="numbers">分页大小集合,范例：10,20,30,50</param>
        public IPagination PageList( params int[] numbers ) {
            return AddDataOption( "pageList", Json.ToJson( numbers ) );
        }

        /// <summary>
        /// 设置当前第几页
        /// </summary>
        /// <param name="number">第几页</param>
        public IPagination PageNumber( int number ) {
            return AddDataOption( "pageNumber", number );
        }

        /// <summary>
        /// 设置分页事件处理函数
        /// </summary>
        /// <param name="handler">分页事件处理函数</param>
        public IPagination OnSelectPage( string handler ) {
            return AddDataOption( "onSelectPage", handler );
        }

        /// <summary>
        /// 设置分页Url
        /// </summary>
        /// <param name="url">点击分页按钮时刷新该Url</param>
        /// <param name="fnRefresh">刷新操作</param>
        public IPagination Url( string url, string fnRefresh ) {
            return OnSelectPage( string.Format( "$.easyui.clickPageButton_onSelectPage('{0}',{1})", url, fnRefresh ) );
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        protected override string GetResult() {
            var result = new StringBuilder();
            result.AppendFormat( "<div {0}>", GetOptions() );
            result.Append( "</div>" );
            return result.ToString();
        }
    }
}
