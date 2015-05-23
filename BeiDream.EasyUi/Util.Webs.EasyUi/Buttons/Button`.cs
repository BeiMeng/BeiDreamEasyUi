namespace Util.Webs.EasyUi.Buttons {
    /// <summary>
    /// 按钮
    /// </summary>
    public abstract class Button<T> : ButtonBase<T>, IButton<T> where T : IButton<T> {
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="text">按钮文本</param>
        protected Button( string text )
            : base( text ) {
        }

        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        public T Submit( string beforeHandler = "", string successHandler = "", string formId = "" ) {
            return Click( string.Format( "$.easyui.submit({0})", GetSubmitParams( beforeHandler, successHandler, formId ) ) );
        }

        /// <summary>
        /// 获取表单参数
        /// </summary>
        private string GetSubmitParams( string beforeHandler, string successHandler, string formId, string treeId = "" ) {
            var builder = new ParamBuilder();
            builder.Add( beforeHandler, "null" );
            builder.Add( successHandler, "null" );
            builder.Add( formId, "''", true );
            builder.Add( treeId, "''", true );
            return builder.GetResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="url">删除对应的后台url</param>
        /// <param name="handler">成功回调函数</param>
        /// <param name="gridId">表格Id</param>
        public T Delete( string url = "", string handler = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.deleteByUrl({0})", GetParams( url, handler, gridId ) ) );
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        private string GetParams( string url, string handler, string id, string message = "" ) {
            var builder = new ParamBuilder();
            builder.Add( url, "''", true );
            builder.Add( handler, "null" );
            builder.Add( id, "''", true );
            builder.Add( message, "''", true );
            return builder.GetResult();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        public T Query( string formId = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.query({0})", GetParams( formId, gridId ) ) );
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        private string GetParams( string formId, string gridId ) {
            var builder = new ParamBuilder();
            builder.Add( formId, "''", true );
            builder.Add( gridId, "''", true );
            return builder.GetResult();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        public T Refresh( string formId = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.refresh({0})", GetParams( formId, gridId ) ) );
        }


        /// <summary>
        /// 添加表格行
        /// </summary>
        /// <param name="row">新行初始数据</param>
        /// <param name="gridId">表格Id</param>
        /// <param name="beforeHandler">添加前操作</param>
        public T AddByGrid( object row = null, string gridId = "", string beforeHandler = "" ) {
            return Click( string.Format( "$.easyui.grid.add({0})", GetAddParams( row, gridId, beforeHandler ) ) );
        }

        /// <summary>
        /// 获取添加参数
        /// </summary>
        private string GetAddParams( object row, string gridId, string beforeHandler = "" ) {
            var builder = new ParamBuilder();
            builder.Add( Json.ToJson( row, true ) );
            builder.Add( gridId, "''", true );
            if ( !beforeHandler.IsEmpty() )
                builder.Add( beforeHandler );
            return builder.GetResult();
        }

        /// <summary>
        /// 编辑表格行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T EditByGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.edit({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 获取表格Id
        /// </summary>
        private string GetGridId( string gridId ) {
            if ( gridId.IsEmpty() )
                return string.Empty;
            return string.Format( "'{0}'", gridId );
        }

        /// <summary>
        /// 取消表格行编辑状态
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T CancelByGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.cancel({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 删除表格行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T DeleteByGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.deleteById({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 保存表格所有操作
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        public T SaveByGrid( string url = "", string handler = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.save({0})", GetParams( url, handler, gridId ) ) );
        }

        /// <summary>
        /// 提交checkbox选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        public T SubmitIdsByGrid( string url = "", string handler = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.submitIds({0})", GetParams( url, handler, gridId ) ) );
        }

        /// <summary>
        /// 添加树型表格根节点
        /// </summary>
        /// <param name="row">新行</param>
        /// <param name="gridId">表格Id</param>
        /// <param name="beforeHandler">添加前操作</param>
        public T AddRootByTreeGrid( object row = null, string gridId = "", string beforeHandler = "" ) {
            return Click( string.Format( "$.easyui.treegrid.addRoot({0})", GetAddParams( row, gridId, beforeHandler ) ) );
        }

        /// <summary>
        /// 添加树型表格下级节点
        /// </summary>
        /// <param name="row">新行初始数据</param>
        /// <param name="gridId">表格Id</param>
        public T AddToChildByTreeGrid( object row = null, string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.addToChild({0})", GetAddParams( row, gridId ) ) );
        }

        /// <summary>
        /// 编辑树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T EditByTreeGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.edit({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 删除树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T DeleteByTreeGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.deleteById({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 取消树型表格节点编辑状态
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T CancelByTreeGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.cancel({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 保存树型表格所有操作
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        public T SaveByTreeGrid( string url = "", string handler = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.save({0})", GetParams( url, handler, gridId ) ) );
        }

        /// <summary>
        /// 提交checkbox选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="gridId">表格Id</param>
        public T SubmitIdsByTreeGrid( string url = "", string handler = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.submitIds({0})", GetParams( url, handler, gridId ) ) );
        }

        /// <summary>
        /// 查询树型表格
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        public T QueryByTreeGrid( string formId = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.query({0})", GetParams( formId, gridId ) ) );
        }

        /// <summary>
        /// 刷新树型表格
        /// </summary>
        /// <param name="formId">查询表单Id</param>
        /// <param name="gridId">表格Id</param>
        public T RefreshByTreeGrid( string formId = "", string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.refresh({0})", GetParams( formId, gridId ) ) );
        }

        /// <summary>
        /// 上移树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T MoveUpByTreeGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.moveUp({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 下移树型表格节点
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T MoveDownByTreeGrid( string gridId = "" ) {
            return Click( string.Format( "$.easyui.treegrid.moveDown({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 展开表格添加新行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T AddByDetail( string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.addByDetail({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 展开表格更新行
        /// </summary>
        /// <param name="gridId">表格Id</param>
        public T EditByDetail( string gridId = "" ) {
            return Click( string.Format( "$.easyui.grid.editByDetail({0})", GetGridId( gridId ) ) );
        }

        /// <summary>
        /// 提交表格行展开的更新表单
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        public T SubmitByDetail( string beforeHandler = "", string successHandler = "", string formId = "" ) {
            return Click( string.Format( "$.easyui.submitByDetail({0})", GetSubmitParams( beforeHandler, successHandler, formId ) ) );
        }

        /// <summary>
        /// 提交树选中的Id列表
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="treeId">树Id</param>
        /// <param name="message">确认消息</param>
        public T SubmitIdsByTree( string url = "", string handler = "", string treeId = "", string message = "" ) {
            return Click( string.Format( "$.easyui.tree.submitIds({0})", GetParams( url, handler, treeId, message ) ) );
        }

        /// <summary>
        /// 删除选中的树节点
        /// </summary>
        /// <param name="url">保存Url</param>
        /// <param name="handler">回调函数</param>
        /// <param name="treeId">树Id</param>
        public T DeleteByTree( string url = "", string handler = "", string treeId = "" ) {
            return Click( string.Format( "$.easyui.tree.deleteByUrl({0})", GetParams( url, handler, treeId ) ) );
        }

        /// <summary>
        /// 提交表单，成功刷新树
        /// </summary>
        /// <param name="beforeHandler">提交前操作</param>
        /// <param name="successHandler">提交成功操作</param>
        /// <param name="formId">表单Id</param>
        /// <param name="treeId">树Id</param>
        public T SubmitByTree( string beforeHandler = "", string successHandler = "", string formId = "", string treeId = "" ) {
            return Click( string.Format( "$.easyui.submitByTree({0})", GetSubmitParams( beforeHandler, successHandler, formId, treeId ) ) );
        }
    }
}
