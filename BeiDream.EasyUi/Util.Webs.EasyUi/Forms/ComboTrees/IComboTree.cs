using Util.Webs.EasyUi.Forms.TextBoxs;
using Util.Webs.EasyUi.Trees;

namespace Util.Webs.EasyUi.Forms.ComboTrees {
    /// <summary>
    /// 树型组合框
    /// </summary>
    public interface IComboTree : ICombo<IComboTree>,ITree<IComboTree> {
        /// <summary>
        /// 设置远程Url
        /// </summary>
        /// <param name="url">远程数据加载Url</param>
        /// <param name="value">值</param>
        IComboTree Url( string url, string value );
        /// <summary>
        /// 延迟设置值，当数据加载完成时设置
        /// </summary>
        /// <param name="value">值</param>
        IComboTree LazyValue( string value );
    }
}
