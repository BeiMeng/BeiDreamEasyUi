using System.Collections;

namespace Util.Webs.EasyUi.Results {
    /// <summary>
    /// DataGrid输出结果
    /// </summary>
    public class DataGridResult : ResultBase {
        /// <summary>
        /// 初始化DataGrid输出结果
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="totalCount">总行数</param>
        public DataGridResult( IEnumerable data, int totalCount ) {
            _data = data;
            _totalCount = totalCount;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        private readonly IEnumerable _data;
        /// <summary>
        /// 总行数
        /// </summary>
        private readonly int _totalCount;

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            var result = "{\"total\":\"0\",\"rows\":[]}";
            if ( _totalCount > 0 )
                result = Json.ToJson( new { total = _totalCount, rows = _data } );
            return result;
        }
    }
}
