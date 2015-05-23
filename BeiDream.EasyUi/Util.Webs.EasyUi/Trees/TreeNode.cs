using System.Collections.Generic;
using System.Runtime.Serialization;
using Json.Net;

namespace Util.Webs.EasyUi.Trees {
    /// <summary>
    /// 树节点
    /// </summary>
    [DataContract]
    public class TreeNode : ITreeNode {
        /// <summary>
        /// 标识
        /// </summary>
        [Json( PropertyName = "id" )]
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 父标识
        /// </summary>
        [Json( NullValueHandling = NullValueHandling.Ignore )]
        public string ParentId { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Json( NullValueHandling = NullValueHandling.Ignore )]
        public string Path { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        [Json( PropertyName = "text", NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public string Text { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Json( PropertyName = "iconCls", NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public string IconClass { get; set; }
        /// <summary>
        /// 级数
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [Json( PropertyName = "checked", NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public bool? Checked { get; set; }
        /// <summary>
        /// 自定义扩展属性
        /// </summary>
        [Json( PropertyName = "attributes", NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public object Attributes { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Json( NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public string state { get; set; }
        /// <summary>
        /// 子节点集合
        /// </summary>
        [Json( NullValueHandling = NullValueHandling.Ignore )]
        [DataMember]
        public List<ITreeNode> children { get; set; }
    }
}
