(function ($) {
    //提交失败回调函数
    $.easyui.submitError = function (xmlHttpRequest, ajaxOptions, error) {
        $.easyui.warn("Http status: " + xmlHttpRequest.status + " " + xmlHttpRequest.statusText + "\najaxOptions: " + ajaxOptions + "\nerror:" + error + "\n" + xmlHttpRequest.responseText);
    };
    //发送请求
    $.easyui.ajax = function (url, data, callback, dataType, type, async) {
        dataType = dataType || "json";
        type = type || 'POST';
        $.easyui.showLoading();
        $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: dataType,
            cache: false,
            async: async,
            success: function (result) {
                $.easyui.removeLoading();
                if (callback)
                    callback(result);
            },
            error: function (result) {
                $.easyui.removeLoading();
                $.easyui.submitError(result);
            }
        });
    }
    $.easyui.refresh = function (formId, gridId) {
        ///	<summary>
        ///	刷新
        ///	</summary>
        ///	<param name="formId" type="String">
        ///	查询表单Id
        ///	</param>
        ///	<param name="gridId" type="String">
        ///	表格Id
        ///	</param>
        $.easyui.clearForm($.easyui.getQueryForm(formId));
        $.easyui.query(formId, gridId);
    };
    $.easyui.clearForm = function (formId) {
        ///	<summary>
        ///	清空表单
        ///	</summary>
        ///	<param name="formId" type="String">
        ///	表单Id
        ///	</param>
        if (!$.isObject(formId))
            formId = $('#' + formId);
        formId.form("reset");
    };
    $.easyui.query = function (formId, gridId) {
        ///	<summary>
        ///	查询
        ///	</summary>
        ///	<param name="formId" type="String">
        ///	查询表单Id
        ///	</param>
        ///	<param name="gridId" type="String">
        ///	表格Id
        ///	</param>
        $.easyui.getGrid(gridId).datagrid({
            pageNumber: 1,
            queryParams: $.easyui.getQueryForm(formId).serializeJson()
        });
    };
    $.easyui.submit = function (fnBefore, fnSuccess, formId) {
        ///	<summary>
        ///	提交更新表单
        ///	</summary>
        ///	<param name="fnBefore" type="Function">
        ///	提交前操作
        ///	</param>
        ///	<param name="fnSuccess" type="Function">
        ///	成功操作
        ///	</param>
        ///	<param name="formId" type="String">
        ///	表单Id
        ///	</param>
        var form = $.easyui.getForm(formId);
        if (!validate())
            return;
        if (!submitBefore())
            return;
        ajaxSubmit();

        //验证表单
        function validate() {
            return form.form('validate');
        }

        //提交前操作
        function submitBefore() {
            if (!fnBefore)
                return true;
            return fnBefore(form);
        }

        //提交
        function ajaxSubmit() {
            var message = form.attr("confirm");
            if (message)
                $.easyui.confirm(message, ajax);
            else
                ajax();
        }

        //发送请求
        function ajax() {
            $.easyui.ajax(form.attr("action"), form.serializeArray(), ajaxCallback);

            //回调
            function ajaxCallback(result) {
                $.easyui.showFormMessage(result);
                if (result.Code !== $.easyui.state.ok)
                    return;
                if (fnSuccess)
                    fnSuccess(result, getGridId());
                else
                    submitSuccess();
            }

            //成功回调函数
            function submitSuccess() {
                var grid = $.easyui.getById(getGridId());
                if (grid)
                    grid.datagrid('reload');
                $.easyui.closeDialog();
            }
        };

        //获取表格编号
        function getGridId() {
            return form.attr("gridId") || $.easyui.gridId;
        }
    };
    $.easyui.submitByDetail = function (fnBefore, fnSuccess, formId) {
        ///	<summary>
        ///	提交表格行展开的更新表单
        ///	</summary>
        ///	<param name="fnBefore" type="Function">
        ///	提交前操作
        ///	</param>
        ///	<param name="fnSuccess" type="Function">
        ///	成功操作
        ///	</param>
        ///	<param name="formId" type="String">
        ///	表单Id
        ///	</param>
        fnSuccess = fnSuccess || submitSuccess;
        $.easyui.submit(fnBefore, fnSuccess, formId);

        //成功回调函数
        function submitSuccess(result, gridId) {
            $.easyui.getGrid(gridId).datagrid('reload');
        }
    };
    $.easyui.submitByTree = function (fnBefore, fnSuccess, formId, treeId) {
        ///	<summary>
        ///	提交更新表单-树操作，成功刷新树
        ///	</summary>
        ///	<param name="fnBefore" type="Function">
        ///	提交前操作
        ///	</param>
        ///	<param name="fnSuccess" type="Function">
        ///	成功操作
        ///	</param>
        ///	<param name="formId" type="String">
        ///	表单Id
        ///	</param>
        ///	<param name="treeId" type="String">
        ///	树Id
        ///	</param>
        $.easyui.submit(fnBefore, submitSuccess, formId);

        //成功回调函数
        function submitSuccess(result) {
            treeId = treeId || $.easyui.treeId;
            $.easyui.getById(treeId).tree("reload");
            if (fnSuccess)
                fnSuccess(result);
            $.easyui.closeDialog();
        }
    };
    //显示表单消息
    $.easyui.showFormMessage = function (result) {
        if (result.Code === $.easyui.state.ok)
            $.easyui.topShow(result.Message);
        else if (result.Code === $.easyui.state.fail)
            $.easyui.warn(result.Message);
    };
    $.easyui.deleteByUrl = function (url, callback, gridId) {
        ///	<summary>
        ///	删除记录
        ///	</summary>
        ///	<param name="url" type="String">
        ///	删除对应的后台url
        ///	</param>
        ///	<param name="callback" type="Function">
        ///	成功回调函数
        ///	</param>
        ///	<param name="gridId" type="String">
        ///	表格Id
        ///	</param>
        url = url || $.easyui.deleteUrl;
        if (!url) {
            $.easyui.warn("删除Url未设置，请联系管理员");
            return;
        }
        var grid = $.easyui.getGrid(gridId);
        var rows = getRows();
        if ($.isEmptyArray(rows)) {
            $.easyui.warn($.easyui.deleteNotSelectedMessage);
            return;
        }
        $.easyui.confirm($.easyui.deleteConfirmMessage, ajaxDelete);

        //获取待删除的记录
        function getRows() {
            var result = grid.datagrid("getChecked");
            if (!$.isEmptyArray(result))
                return result;
            var row = grid.datagrid('getSelected');
            if (!row)
                return result;
            result.push(row);
            return result;
        }

        //发送删除请求
        function ajaxDelete() {
            var ids = $.easyui.getIds(rows);
            ajax(ids);
        }

        //发送请求
        function ajax(id) {
            url = url || $.easyui.deleteUrl;
            var param = { ids: id, __RequestVerificationToken: $.getAntiForgeryToken() };
            $.easyui.ajax(url, param, function (result) {
                if (callback)
                    callback(result);
                else
                    deleteSuccess(result);
            });
        };

        //删除成功回调函数
        function deleteSuccess(result) {
            grid.datagrid('reload');
            $.easyui.showFormMessage(result);
        }
    };
    //获取id列表字符串，用逗号拼接
    $.easyui.getIds = function (rows) {
        if (!rows)
            return "";
        var ids = "";
        $(rows).each(function (i, row) {
            ids += i == 0 ? row.Id : "," + row.Id;
        });
        return ids;
    };
    $.easyui.initDialog = function (options, msg, id) {
        ///	<summary>
        ///	初始化弹出窗口
        ///	</summary>
        ///	<param name="options" type="Object">
        ///	选项
        ///	</param>
        ///	<param name="msg" type="String">
        ///	消息
        ///	</param>
        ///	<param name="id" type="String">
        ///	业务编号
        ///	</param>
        if (!id) {
            $.easyui.warn(msg);
            return false;
        }
        options.url = $.joinUrl(options.url, "id=" + id);
        return true;
    };
    $.easyui.initDialogByGrid = function (options, msg, gridId) {
        ///	<summary>
        ///	初始化弹出窗口-表格
        ///	</summary>
        ///	<param name="options" type="Object">
        ///	选项
        ///	</param>
        ///	<param name="msg" type="String">
        ///	消息
        ///	</param>
        ///	<param name="gridId" type="String">
        ///	表格Id
        ///	</param>
        var row = $.easyui.getGrid(gridId).datagrid('getSelected');
        return $.easyui.initDialog(options, msg, row && row.Id);
    };
    $.easyui.initDialogByTree = function (options, treeId) {
        ///	<summary>
        ///	初始化弹出窗口-树
        ///	</summary>
        ///	<param name="options" type="Object">
        ///	选项
        ///	</param>
        ///	<param name="treeId" type="String">
        ///	树Id
        ///	</param>
        var node = $.easyui.getTree(treeId).tree('getSelected');
        return $.easyui.initDialog(options, $.easyui.editTreeNotSelectedMessage, node && node.id);
    };
    $.easyui.gridRowsToHidden = function (gridId, form, hiddenName) {
        ///	<summary>
        ///	将表格所有行转成JSON字符串，并保存到表单的隐藏域中
        ///	</summary>
        ///	<param name="gridId" type="String">
        ///	表格Id
        ///	</param>
        ///	<param name="form" type="jQuery对象">
        ///	表单jQuery对象
        ///	</param>
        ///	<param name="hiddenName" type="String">
        ///	隐藏域的name
        ///	</param>
        if (!$.easyui.grid.validate(gridId))
            return false;
        var hidden = form.find(":hidden[name='" + hiddenName + "']");
        hidden.val($.easyui.grid.getRowsJson(gridId));
        return true;
    };
    $.easyui.refreshPanel = function (panelId, url) {
        ///	<summary>
        ///	刷新面板
        ///	</summary>
        ///	<param name="panelId" type="String">
        ///	面板Id
        ///	</param>
        ///	<param name="url" type="String">
        ///	远程Url
        ///	</param>
        var panel = $.easyui.getById(panelId);
        panel.panel("refresh", url);
    };
})(jQuery);

