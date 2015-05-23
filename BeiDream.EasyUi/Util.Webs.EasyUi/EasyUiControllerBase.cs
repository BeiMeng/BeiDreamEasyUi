using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Util.Webs.EasyUi.Forms.Comboxs;
using Util.Webs.EasyUi.Results;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi {
    /// <summary>
    /// EasyUi基控制器
    /// </summary>
    public abstract class EasyUiControllerBase : ControllerBase {
        /// <summary>
        /// 转换为DataGrid输出结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">实体列表</param>
        /// <param name="totalCount">总行数</param>
        protected ActionResult ToDataGridResult<T>( IList<T> data, int totalCount = 0 ) {
            return new DataGridResult( data, GetTotalCount( data, totalCount ) ).GetResult();
        }

        /// <summary>
        /// 获取总行数
        /// </summary>
        private int GetTotalCount<T>( IList<T> data, int totalCount ) {
            if ( totalCount == 0 )
                return data.Count;
            return totalCount;
        }

        /// <summary>
        /// 转换为TreeGrid输出结果
        /// </summary>
        /// <param name="data">实体列表</param>
        /// <param name="isAyncLoad">是否异步加载</param>
        /// <param name="totalCount">总行数</param> 
        protected ActionResult ToTreeGridResult( IEnumerable<ITreeNode> data, bool isAyncLoad = false, int totalCount = -1 ) {
            return new TreeGridResult( data, isAyncLoad, totalCount ).GetResult();
        }

        /// <summary>
        /// 转换为Tree输出结果
        /// </summary>
        /// <param name="data">实体列表</param>
        /// <param name="isAyncLoad">是否异步加载</param>
        protected ActionResult ToTreeResult( IEnumerable<ITreeNode> data, bool isAyncLoad = false ) {
            return new TreeResult( data, isAyncLoad ).GetResult();
        }

        /// <summary>
        /// 转换为Tree输出结果
        /// </summary>
        /// <param name="data">实体列表</param>
        /// <param name="rootName">根节点名称</param>
        protected ActionResult ToTreeResult( IEnumerable<ITreeNode> data, string rootName ) {
            var nodes = data.ToList();
            foreach ( var node in nodes ) {
                if ( node.ParentId.IsEmpty() )
                    node.ParentId = Guid.Empty.ToString();
            }
            nodes.Add( new TreeNode { Id = Guid.Empty.ToString(), Text = rootName } );
            return ToTreeResult( nodes );
        }

        /// <summary>
        /// 转换为Combox输出结果
        /// </summary>
        /// <param name="items">组合框项集合</param>
        protected ActionResult ToComboxResult( IEnumerable<ComboxItem> items ) {
            return new ContentResult { Content = Combox.ToJson( items ) };
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        protected virtual ActionResult Ok( string message = "操作成功", IEnumerable<object> data = null ) {
            return new EasyUiResult( StateCode.Ok, message, data ).GetResult();
        }

        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        protected ActionResult Fail( string message ) {
            return new EasyUiResult( StateCode.Fail, message ).GetResult();
        }

        /// <summary>
        /// 远程验证成功
        /// </summary>
        protected ActionResult RemoteOk() {
            return new ContentResult { Content = "true" };
        }

        /// <summary>
        /// 远程验证失败
        /// </summary>
        /// <param name="message">消息</param>
        protected ActionResult RemoteFail( string message ) {
            return new ContentResult { Content = message };
        }

        /// <summary>
        /// 获取分页的页索引
        /// </summary>
        protected int GetPageIndex() {
            var page = Request["page"].ToInt();
            return page > 0 ? page : 1;
        }

        /// <summary>
        /// 获取分页大小
        /// </summary>
        protected int GetPageSize() {
            var pageSize = Request["rows"].ToInt();
            return pageSize > 0 ? pageSize : 20;
        }

        /// <summary>
        /// 获取排序
        /// </summary>
        protected string GetOrder() {
            return string.Format( "{0} {1}", Request["sort"].ToStr(), Request["order"].ToStr() );
        }
    }
}
