using System.Collections.Generic;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi.Results {
    /// <summary>
    /// 树型表格输出结果
    /// </summary>
    public class TreeGridResult : ResultBase {
        /// <summary>
        /// 初始化树型表格输出结果
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="isAyncLoad">是否异步加载</param>
        /// <param name="totalCount">总行数</param>
        public TreeGridResult( IEnumerable<ITreeNode> data, bool isAyncLoad = false, int totalCount = -1 ) {
            _data = data;
            _isAyncLoad = isAyncLoad;
            _totalCount = totalCount;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        private readonly IEnumerable<ITreeNode> _data;
        /// <summary>
        /// 是否异步加载
        /// </summary>
        private readonly bool _isAyncLoad;
        /// <summary>
        /// 总行数
        /// </summary>
        private readonly int _totalCount;

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            if ( _totalCount == -1 )
                return new TreeResult( _data, _isAyncLoad ).ToString();
            return new DataGridResult( GetNodes(), _totalCount ).ToString();
        }

        /// <summary>
        /// 处理节点
        /// </summary>
        private IEnumerable<ITreeNode> GetNodes() {
            return new TreeResult( _data, _isAyncLoad ).GetNodes();
        }
    }
}
