namespace BeiDream.Common.Page
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pager : StateDescription, IPager {
        /// <summary>
        /// 初始化分页
        /// </summary>
        public Pager()
            : this( 1 ) {
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数,默认20</param> 
        /// <param name="order">排序条件</param>
        public Pager( int page, int pageSize, string order )
            : this( page, pageSize, 0, order ) {
        }

        /// <summary>
        /// 初始化分页
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数,默认20</param> 
        /// <param name="totalCount">总行数</param>
        /// <param name="order">排序条件</param>
        public Pager( int page, int pageSize = 15, int totalCount = 0, string order = "" ) {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Order = order;
        }

        private int _pageIndex;
        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int Page {
            get {
                if ( _pageIndex <= 0 )
                    _pageIndex = 1;
                return _pageIndex;
            }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount {
            get {
                if ( TotalCount == 0 )
                    return 0;
                if ( ( TotalCount % PageSize ) == 0 )
                    return TotalCount / PageSize;
                return ( TotalCount / PageSize ) + 1;
            }
        }

        /// <summary>
        /// 跳过的行数
        /// </summary>
        public int SkipCount {
            get {
                if ( Page > PageCount )
                    Page = PageCount;
                return PageSize * ( Page - 1 );
            }
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 起始行数
        /// </summary>
        public int StartNumber {
            get { return ( Page - 1 ) * PageSize + 1; }
        }
        /// <summary>
        /// 结束行数
        /// </summary>
        public int EndNumber {
            get { return Page * PageSize; }
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected override void AddDescriptions() {
            base.AddDescriptions();
            AddDescription( "Page", Page );
            AddDescription( "PageSize", PageSize );
            AddDescription( "Order", Order );
        }
    }
}
