using System.Collections.Generic;
using System.Linq;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi.Results {
    /// <summary>
    /// 树结果
    /// </summary>
    public class TreeResult : ResultBase {
        /// <summary>
        /// 初始化树结果
        /// </summary>
        /// <param name="nodes">树节点集合</param>
        /// <param name="isAyncLoad">是否异步加载</param>
        public TreeResult( IEnumerable<ITreeNode> nodes, bool isAyncLoad = false ) {
            _nodes = nodes;
            _isAsyncLoad = isAyncLoad;
        }

        /// <summary>
        /// 树节点集合
        /// </summary>
        private readonly IEnumerable<ITreeNode> _nodes;
        /// <summary>
        /// 是否异步加载
        /// </summary>
        private readonly bool _isAsyncLoad;

        /// <summary>
        /// 输出结果
        /// </summary>
        public override string ToString() {
            return Json.ToJson( GetNodes() );
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        public List<ITreeNode> GetNodes() {
            var result = new List<ITreeNode>();
            if ( _nodes == null )
                return result;
            foreach ( var root in _nodes.Where( IsRoot ) )
                AddNode( result, root );
            return result;
        }

        /// <summary>
        /// 是否根节点
        /// </summary>
        private bool IsRoot( ITreeNode node ) {
            if ( _nodes.Any( t => t.ParentId.IsEmpty() ) )
                return node.ParentId.IsEmpty();
            return node.Level == _nodes.Min( t => t.Level );
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        private void AddNode( List<ITreeNode> result, ITreeNode node ) {
            if ( node == null )
                return;
            if ( IsRoot( node ) )
                result.Add( node );
            if ( IsLeaf( node ) ) {
                SetState( node );
                return;
            }
            node.children = GetChilds( node );
            foreach ( var child in node.children )
                AddNode( result, child );
        }

        /// <summary>
        /// 是否叶节点
        /// </summary>
        private bool IsLeaf( ITreeNode node ) {
            return _nodes.All( t => t.ParentId != node.Id );
        }

        /// <summary>
        /// 设置叶节点状态
        /// </summary>
        private void SetState( ITreeNode leaf ) {
            if ( _isAsyncLoad )
                leaf.state = TreeNodeState.Closed.Description();
        }

        /// <summary>
        /// 获取节点直接下级
        /// </summary>
        private List<ITreeNode> GetChilds( ITreeNode node ) {
            return _nodes.Where( t => t.ParentId == node.Id ).ToList();
        }
    }
}
