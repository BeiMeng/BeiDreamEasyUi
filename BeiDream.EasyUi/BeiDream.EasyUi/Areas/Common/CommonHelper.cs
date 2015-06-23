using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util.Webs.EasyUi.Trees;

namespace BeiDream.EasyUi.Areas.Common
{
    public class CommonHelper
    {
        /// <summary>
        /// 同步或异步加载时的展开与否状态设置
        /// </summary>
        /// <param name="treeNodes"></param>
        /// <param name="loadMode"></param>
        public static void SetState(IEnumerable<ITreeNode> treeNodes, LoadMode loadMode)
        {
            foreach (var treeNode in treeNodes)
            {
                switch (loadMode)
                {
                    case LoadMode.Async:
                        treeNode.state = "closed";
                        break;
                    case LoadMode.Sync:
                        treeNode.state = "open";
                        break;
                    default:
                        treeNode.state = "closed";
                        break;
                }
            }
        }
    }
}