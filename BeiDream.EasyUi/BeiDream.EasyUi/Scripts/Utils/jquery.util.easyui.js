(function ($) {
    //index页面对应的jQuery对象
    var $parent = parent.$;
    $.easyui = (function () {
        //弹出窗口标识
        var dialogsKey = "dialogs";
        //全局数据编号
        var dataKey = "#div_index_data";

        //获取弹出窗口数据集合
        function getDialogs() {
            return $.easyui.getArray(dialogsKey);
        }

        //获取当前弹出窗口数据
        function getCurrentDialog() {
            return $.easyui.getItem(dialogsKey);
        }

        //获取当前弹出窗口Id
        function getCurrentDialogId() {
            return getCurrentDialog().id;
        }

        //添加弹出窗口数据,包括弹出窗口Id和jQuery对象
        function addDialog(id) {
            $.easyui.addItem(dialogsKey, { id: id, $this: $ });
        }

        //移除当前弹出窗口数据
        function removeCurrentDialog() {
            var dialogs = getDialogs();
            dialogs.pop();
        }

        //获取消息管理器
        function getMessager() {
            return $parent.messager;
        }

        return {
            //添加全局数据，存储到index页面
            addData: function (key, data) {
                $parent(dataKey).data(key, data);
            },
            //添加全局数据，将项添加到数组中
            addItem: function (key, item) {
                var list = $.easyui.getArray(key);
                list.push(item);
                $.easyui.addData(key, list);
            },
            //获取数据
            getData: function (key) {
                return $parent(dataKey).data(key);
            },
            //获取数组
            getArray: function (key) {
                var data = $.easyui.getData(key);
                data = data || [];
                if ($.isEmptyArray(data))
                    return [];
                return data;
            },
            //获取项
            getItem: function (key) {
                var list = $.easyui.getArray(key);
                if (list.length === 0)
                    return {};
                return list[list.length - 1];
            },
            //获取当前$
            getCurrent$: function () {
                var dialog = getCurrentDialog();
                if (!dialog)
                    return $;
                if (!dialog.$this)
                    return $;
                return dialog.$this;
            },
            //通过Id获取jQuery对象
            getById: function (id) {
                var current$ = $.easyui.getCurrent$();
                return current$("#" + id);
            },
            //显示全屏加载进度条
            showLoading: function () {
                var $body = $("body");
                $("<div id=\"div_loading\" class=\"datagrid-mask\"></div>").css({ zIndex: "99998", display: "block" }).appendTo($body);
                $("<div id=\"div_loading_msg\" class=\"datagrid-mask-msg\"></div>").html("处理中，请稍候...").appendTo($body).css({ zIndex: "99999", display: "block", left: "50%", fontSize: "12px" });
            },
            //移除全屏加载进度条
            removeLoading: function () {
                $("#div_loading").remove();
                $("#div_loading_msg").remove();
            },
            //格式化日期
            formatDate: function (value) {
                if (!value)
                    return "";
                try {
                    if (value.replace)
                        value = value.replace("T", " ");
                    var date = new Date(value);
                    if (isNaN(date))
                        return $.formatIsoDate(value);
                    return date.format("yyyy-MM-dd HH:mm");
                } catch (e) {
                    return $.formatIsoDate(value);
                }
            },
            //格式化布尔值
            formatBool: function (value) {
                if (value === true || value === 'true' || value === 1 || value === '1')
                    return "是";
                return "否";
            },
            //格式化图片
            formatImage: function (width, height, isClass) {
                width = width || 16;
                height = height || 16;
                return function (value, row) {
                    if (!value)
                        return "";
                    return "<image " + getProperty() + "='" + value + "' style='width:" + getWidth() + "px;height:" + getHeight() + "px' />";

                    //获取路径
                    function getProperty() {
                        return isClass ? "class" : "src";
                    }

                    //获取宽度
                    function getWidth() {
                        return getSize(row.Width, width);
                    }

                    //获取高度
                    function getHeight() {
                        return getSize(row.Height, height);
                    }

                    //获取大小
                    function getSize(rowSize, size) {
                        if (!rowSize)
                            return size;
                        if (rowSize < 100)
                            return rowSize;
                        return size;
                    }
                };
            },
            //格式化Combox控件(支持ComboTree) - 修正combox控件显示值的问题
            formatCombox: function (data, valueField, textField) {
                return function (value) {
                    if (!data)
                        return "";
                    var result = value;
                    for (var i = 0; i < data.length; i++) {
                        result = getText(data[i]);
                        if (result)
                            break;
                    }
                    return result;

                    //获取对应文本
                    function getText(node) {
                        result = getResult();
                        if (result)
                            return result;
                        if (!node.children || node.children.length === 0)
                            return result;
                        for (var j = 0; j < node.children.length; j++) {
                            result = getText(node.children[j]);
                            if (result)
                                break;
                        }
                        return result;

                        //获取结果
                        function getResult() {
                            return node[valueField] == value ? node[textField] : "";
                        }
                    }
                };
            },
            //格式化Combox控件(支持ComboTree) - 基于Url加载
            formatComboxFromUrl: function (url, valueField, textField) {
                var data = null;
                getData();
                return $.easyui.formatCombox(data, valueField, textField);

                //获取数据
                function getData() {
                    if ($.isObject(url))
                        return;
                    $.easyui.ajax(url, "", function (result) {
                        data = result;
                    }, "", "GET", false);
                }
            },
            addIframeToTabs: function (tabsId, title, url, icon, closable) {
                ///	<summary>
                ///	为tabs添加iframe选项卡
                ///	</summary>
                ///	<param name="tabsId" type="String">
                ///	选项卡Id
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题，可以重复
                ///	</param>
                ///	<param name="url" type="String">
                ///	网址,必须唯一
                ///	</param>
                ///	<param name="icon" type="String">
                ///	图标class
                ///	</param>
                ///	<param name="closable" type="Bool">
                ///	是否允许关闭
                ///	</param>
                if (!title && !url)
                    return;
                var tabs = $('#' + tabsId);
                var index;
                var iframe = null;
                if (!exists())
                    createTab();
                selectTab();

                //判断选项卡是否存在,根据url进行判断
                function exists() {
                    var allTabs = tabs.tabs("tabs");
                    for (index = 0; index < allTabs.length; index++) {
                        iframe = allTabs[index].find('iframe');
                        if (iframe.length == 0)
                            continue;
                        if ($.getUrlPath(iframe[0].src) === url)
                            return true;
                    }
                    return false;
                }

                //创建选项卡
                function createTab() {
                    $.easyui.showLoading();
                    addTab();
                    removeLoading();

                    //添加选项卡
                    function addTab() {
                        var content = '<div class="easyui-layout" data-options="fit:true"><iframe scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe><div>';
                        tabs.tabs('add', {
                            title: title,
                            closable: closable,
                            content: content,
                            iconCls: icon,
                            selected: 0
                        });
                    }

                    //关闭进度条
                    function removeLoading() {
                        var tab = tabs.tabs("getTab", index);
                        iframe = tab.find('iframe');
                        $(iframe).bind({
                            load: function () {
                                $.easyui.removeLoading();
                            },
                            error: function () {
                                $.easyui.removeLoading();
                            }
                        });
                    }
                }

                //选中选项卡
                function selectTab() {
                    tabs.tabs('select', index);
                }
            },
            refreshTabs: function (tabsId) {
                ///	<summary>
                ///	刷新选项卡
                ///	</summary>
                ///	<param name="tabsId" type="String">
                ///	选项卡Id
                ///	</param>
                var tabs = $('#' + tabsId);
                var tab = tabs.tabs('getSelected');
                var iframe = tab.find('iframe');
                if (iframe.length == 0)
                    return;
                iframe[0].contentWindow.location.href = iframe[0].contentWindow.location.href;
            },
            dialog: function (options) {
                ///	<summary>
                ///	弹出模态窗，解决在Iframe中无法全屏遮罩,
                /// 注意:仅支持url弹窗
                ///	</summary>
                ///	<param name="options" type="Object">
                ///  1. title:标题
                ///  2. url:网址
                ///  3. buttons:显示在窗口底部的按钮区域div的id
                ///  4. icon:图标class
                ///  5. width:宽度
                ///  6. height:高度
                ///  7. onInit:初始化事件，返回false跳出执行
                ///	</param>
                initOptions();
                if (!options.onInit(options))
                    return;
                var dialog = createDialow();
                show();
                addDialog(options.id);

                //初始化参数
                function initOptions() {
                    options = $.extend({
                        id: $.newGuid(""),
                        title: '',
                        url: '',
                        icon: '',
                        width: 800,
                        height: 360,
                        closed: false,
                        maximizable: true,
                        resizable: true,
                        cache: false,
                        modal: true,
                        buttons: $.easyui.buttonsDivId,
                        onInit: function () {
                            return true;
                        },
                        closeCallback: function () { }
                    }, options || {});
                }

                //创建窗口div
                function createDialow() {
                    return $parent("<div id='" + options.id + "'></div>").appendTo('body');
                }

                //弹出窗口
                function show() {
                    dialog.dialog({
                        title: options.title,
                        href: options.url,
                        width: options.dialogWidth || options.width,
                        height: options.dialogHeight || options.height,
                        closed: options.closed,
                        maximizable: options.maximizable,
                        resizable: options.resizable,
                        cache: options.cache,
                        modal: options.modal,
                        iconCls: options.icon,
                        onLoad: function () {
                            var win = $parent("#" + options.id).window("window");
                            $parent("#" + options.buttons).addClass("dialog-button").appendTo(win);
                        },
                        onClose: function () {
                            if (options.closeCallback)
                                options.closeCallback();
                            $parent("#" + getCurrentDialogId()).dialog('destroy');
                            removeCurrentDialog();
                        }
                    });
                }
            },
            //关闭弹出窗口
            closeDialog: function () {
                var dialogId = getCurrentDialogId();
                if (!dialogId)
                    return;
                $parent('#' + dialogId).dialog('close');
            },
            showMenu: function (menuId, e) {
                ///	<summary>
                ///	显示上下文菜单
                ///	</summary>
                ///	<param name="menuId" type="String">
                ///	上下文菜单Id
                ///	</param>
                ///	<param name="e" type="Event">
                ///	事件
                ///	</param>
                e.preventDefault();
                var menu = menuId;
                if (!$.isObject(menu))
                    menu = $('#' + menuId);
                menu.menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });
                return menu;
            },
            topShow: function (msg, title) {
                ///	<summary>
                ///	在顶部显示消息
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                if (!msg)
                    return;
                getMessager().show({
                    title: title || "信息",
                    msg: msg,
                    showType: 'slide',
                    style: {
                        right: '',
                        top: document.body.scrollTop + document.documentElement.scrollTop,
                        bottom: ''
                    }
                });
            },
            info: function (msg, title) {
                ///	<summary>
                ///	弹出信息框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                if (!msg)
                    return;
                getMessager().alert(title || "信息", msg, 'info');
            },
            warn: function (msg, title) {
                ///	<summary>
                ///	弹出警告框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                if (!msg)
                    return;
                getMessager().alert(title || "错误", msg, 'error');
            },
            confirm: function (msg, callback, title) {
                ///	<summary>
                ///	弹出确认框
                ///	</summary>
                ///	<param name="msg" type="String">
                ///	内容
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	点击ok按钮后的回调函数
                ///	</param>
                ///	<param name="title" type="String">
                ///	标题
                ///	</param>
                if (!msg) {
                    callback();
                    return;
                }
                getMessager().confirm(title || "确认", msg, function (result) {
                    if (result)
                        callback();
                });
            }
        };
    })();
})(jQuery);

