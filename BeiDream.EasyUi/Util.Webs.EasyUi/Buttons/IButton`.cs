namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 按钮
    /// </summary>
    /// <typeparam name="T">按钮类型</typeparam>
    public interface IButton<out T> : IButtonBase<T> where T : IButton<T> {
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        T Submit( string beforeHandler = "",string successHandler = "", string formId = "" );
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="url">删除对应的后台url</param>
        /// <param name="handler">成功回调函数</param>
        /// <param name="gridId">表格Id</param>
        T Delete( string url = "", string handler = "", string gridId = "" );
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        T Query( string formId = "", string gridId = "" );
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        T Refresh( string formId = "", string gridId = "" );
        /// <summary>
        /// 添加表格行
        /// </summary>
        /// <param name="row">新行初始数据</param>
        /// <param name="gridId">表格Id</param>
        /// <param name="beforeHandler">添加前操作</param>
        T AddByGrid( object row = null, string gridId = "", string beforeHandler = "" );
        /// <summary>
        /// 编辑表格行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T EditByGrid( string gridId = "" );
        /// <summary>
        /// 取消表格行编辑状态
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T CancelByGrid( string gridId = "" );
        /// <summary>
        /// 删除表格行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T DeleteByGrid( string gridId = "" );
        /// <summary>
        /// 保存表格所有操作
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        T SaveByGrid( string url = "", string handler = "", string gridId = "" );
        /// <summary>
        /// 提交表格checkbox选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        T SubmitIdsByGrid( string url = "", string handler = "", string gridId = "" );
        /// <summary>
        /// 添加树型表格根节点
        /// </summary>
        /// <param name="row">新行初始数据</param>
        /// <param name="gridId">表格Id</param>
        /// <param name="beforeHandler">添加前操作</param>
        T AddRootByTreeGrid( object row = null, string gridId = "", string beforeHandler = "" );
        /// <summary>
        /// 添加树型表格下级节点
        /// </summary>
        /// <param name="row">新行初始数据</param>
        /// <param name="gridId">表格Id</param>
        T AddToChildByTreeGrid( object row = null, string gridId = "" );
        /// <summary>
        /// 编辑树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T EditByTreeGrid( string gridId = "" );
        /// <summary>
        /// 删除树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T DeleteByTreeGrid( string gridId = "" );
        /// <summary>
        /// 取消树型表格节点编辑状态
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T CancelByTreeGrid( string gridId = "" );
        /// <summary>
        /// 保存树型表格所有操作
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        T SaveByTreeGrid( string url = "", string handler = "", string gridId = "" );
        /// <summary>
        /// 提交树型表格checkbox选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        T SubmitIdsByTreeGrid( string url = "", string handler = "", string gridId = "" );
        /// <summary>
        /// 查询树型表格
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        T QueryByTreeGrid( string formId = "", string gridId = "" );
        /// <summary>
        /// 刷新树型表格
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        T RefreshByTreeGrid( string formId = "",string gridId = "" );
        /// <summary>
        /// 上移树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T MoveUpByTreeGrid( string gridId = "" );
        /// <summary>
        /// 下移树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T MoveDownByTreeGrid( string gridId = "" );
        /// <summary>
        /// 展开表格添加新行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T AddByDetail( string gridId = "" );
        /// <summary>
        /// 展开表格更新行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        T EditByDetail( string gridId = "" );
        /// <summary>
        /// 提交表格行展开的更新表单
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        T SubmitByDetail( string beforeHandler = "", string successHandler = "", string formId = "" );
        /// <summary>
        /// 提交树选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="treeId">树Id</param>
        /// <param name="message">确认消息</param>
        T SubmitIdsByTree( string url = "", string handler = "", string treeId = "", string message = "" );
        /// <summary>
        /// 删除选中的树节点
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="treeId">树Id</param>
        T DeleteByTree( string url = "", string handler = "", string treeId = "" );
        /// <summary>
        /// 提交表单，成功刷新树
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        /// <param name="treeId">树Id</param>
        T SubmitByTree( string beforeHandler = "", string successHandler = "", string formId = "",string treeId = "" );
    }
}
