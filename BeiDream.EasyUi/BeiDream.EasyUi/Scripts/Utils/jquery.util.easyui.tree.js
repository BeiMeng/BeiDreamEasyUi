(function ($) {
    //树操作
    $.easyui.tree = function () {
        //提交树节点
        function submit(url, callback, treeId,msgWarning,msgConfirm) {
            var tree = $.easyui.getTree(treeId);
            var nodes = getNodes();
            if (!validate()) {
                $.easyui.warn(msgWarning);
                return;
            }
            $.easyui.confirm(msgConfirm, ajax);

            //获取选中节点
            function getNodes() {
                var result = [];
                var checkedNodes = tree.tree("getChecked", "checked");
                if (checkedNodes && checkedNodes.length > 0)
                    return checkedNodes;
                var selectedNode = tree.tree("getSelected");
                if (selectedNode)
                    result.push(selectedNode);
                return result;
            }

            //验证非法操作
            function validate() {
                if (!nodes || nodes.length === 0)
                    return false;
                return true;
            }

            //发送请求
            function ajax() {
                var param = { ids: getIds(), __RequestVerificationToken: $.getAntiForgeryToken() };
                $.easyui.ajax(url, param, ajaxCallback);

                //获取勾选Id列表
                function getIds() {
                    var result = "";
                    $(nodes).each(function (i, node) {
                        result += i == 0 ? node.id : "," + node.id;
                    });
                    return result;
                };

                //回调
                function ajaxCallback(result) {
                    $.easyui.showFormMessage(result);
                    if (result.Code !== $.easyui.state.ok)
                        return;
                    tree.tree("reload");
                    if (callback)
                        callback(result);
                }
            }
        }

        return {
            submitIds: function (url, callback, treeId,msgConfirm) {
                ///	<summary>
                ///	提交选中的Id列表
                ///	</summary>
                ///	<param name="url" type="String">
                ///	处理Url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="treeId" type="String">
                ///	树Id
                ///	</param>
                ///	<param name="msgConfirm" type="String">
                ///	确认消息
                ///	</param>
                url = url || $.easyui.submitIdsUrlByTree;
                submit(url, callback, treeId, $.easyui.treeNotCheckedMessage, msgConfirm);
            },
            deleteByUrl: function (url, callback, treeId) {
                ///	<summary>
                ///	通过Url删除树节点
                ///	</summary>
                ///	<param name="url" type="String">
                ///	处理Url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="treeId" type="String">
                ///	树Id
                ///	</param>
                url = url || $.easyui.deleteUrlByTree;
                $.easyui.tree.submitIds(url, callback, treeId, $.easyui.deleteConfirmMessage);
            }
        };
    }();
})(jQuery);