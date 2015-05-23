using Util.Webs.EasyUi.Base;

namespace Util.Webs.EasyUi.Paginations {
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPagination : IComponent<IPagination> {
        /// <summary>
        /// 设置总记录数
        /// </summary>
        /// <param name="number">总记录数</param>
        IPagination Total( int number );
        /// <summary>
        /// 设置每页显示行数
        /// </summary>
        /// <param name="number">每页显示行数</param>
        IPagination PageSize( int number = 20 );
        /// <summary>
        /// 设置分页大小集合
        /// </summary>
        /// <param name="numbers">分页大小集合,范例：10,20,30,50</param>
        IPagination PageList( params int[] numbers );
        /// <summary>
        /// 设置当前第几页
        /// </summary>
        /// <param name="number">第几页</param>
        IPagination PageNumber( int number );
        /// <summary>
        /// 设置分页事件处理函数
        /// </summary>
        /// <param name="handler">分页事件处理函数</param>
        IPagination OnSelectPage( string handler );
        /// <summary>
        /// 设置分页Url
        /// </summary>
        /// <param name="url">点击分页按钮时刷新该Url</param>
        /// <param name="fnRefresh">刷新操作</param>
        IPagination Url( string url,string fnRefresh );
    }
}
