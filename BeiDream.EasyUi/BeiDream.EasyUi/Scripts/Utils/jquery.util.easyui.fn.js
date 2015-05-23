(function ($) {
    $.easyui = $.extend($.easyui, function () {
        //延迟设置值
        function setLazyValue(id, fnSetValue) {
            return function () {
                var $this = $("#" + id);
                var value = $this.attr("lazyValue");
                if (!value)
                    return;
                fnSetValue($this, value);
                $this.attr("lazyValue", "");
            };
        }

        //获取详细div
        function getDetailHtml() {
            return '<div class="gridDetail"></div>';
        }

        //加载表格列中的控件
        function loadGridColumnControl(url, fnLoadData) {
            var data = null;
            getData();
            return function (param) {
                if (!data)
                    return true;
                fnLoadData($(this), data);
                return false;
            };

            //获取数据
            function getData() {
                if ($.isObject(url))
                    return;
                $.easyui.ajax(url, "", function (result) {
                    data = result;
                }, "", "GET");
            }
        }

        return {
            //初始化编辑窗口
            initEditDialog: function (options) {
                return $.easyui.initDialogByGrid(options, $.easyui.editNotSelectedMessage);
            },
            //初始化详细窗口
            initDetailDialog: function (options) {
                return $.easyui.initDialogByGrid(options, $.easyui.lookNotSelectedMessage);
            },
            //初始化编辑窗口-树
            initEditDialogByTree: function (treeId) {
                return function (options) {
                    return $.easyui.initDialogByTree(options, treeId);
                };
            },
            //显示更新窗口
            showEditDialog_onDblClickRow: function (btnId) {
                return function () {
                    $.easyui.getEditButton(btnId).click();
                };
            },
            //显示更新窗口
            showEditDialog: function () {
                $.easyui.getEditButton().click();
            },
            //显示查看窗口
            showDetailDialog: function () {
                $("#" + $.easyui.btnLookId).click();
            },
            //右键单击表格,显示上下文菜单
            showGridMenu_onRowContextMenu: function (gridId, menuId) {
                return function (e, index) {
                    $.easyui.getGrid(gridId).datagrid("selectRow", index);
                    $.easyui.showMenu($.easyui.getGridMenu(menuId), e);
                };
            },
            //右键单击树型表格,显示上下文菜单
            showTreeGridMenu_onContextMenu: function (gridId, menuId) {
                return function (e, row) {
                    $.easyui.getGrid(gridId).treegrid("select", row.Id);
                    $.easyui.showMenu($.easyui.getGridMenu(menuId), e);
                }
            },
            //右键单击树,显示上下文菜单
            showTreeMenu_onContextMenu: function (treeId, menuId) {
                return function (e, node) {
                    $.easyui.getTree(treeId).tree("select", node.target);
                    $.easyui.showMenu($.easyui.getTreeMenu(menuId), e);
                };
            },
            //单击表格菜单项-表单操作
            fnClickGridMenuItem_Form: function (item) {
                switch (item.id) {
                    case "menuItem_Edit":
                        return $.easyui.showEditDialog();
                    case "menuItem_Delete":
                        return $.easyui.deleteByUrl();
                    case "menuItem_Look":
                        return $.easyui.showDetailDialog();
                    case "menuItem_Refresh":
                        return $.easyui.refresh();
                }
                return true;
            },
            //单击表格菜单项-表格编辑操作
            fnClickGridMenuItem_Grid: function (item) {
                switch (item.id) {
                    case "menuItem_Edit":
                        return $.easyui.grid.edit();
                    case "menuItem_Delete":
                        return $.easyui.grid.deleteById();
                    case "menuItem_Refresh":
                        return $.easyui.refresh();
                }
                return true;
            },
            //单击树型表格菜单项
            fnClickTreeGridMenuItem: function (item) {
                switch (item.id) {
                    case "menuItem_AddToChild":
                        return $.easyui.treegrid.addToChild();
                    case "menuItem_Edit":
                        return $.easyui.treegrid.edit();
                    case "menuItem_Delete":
                        return $.easyui.treegrid.deleteById();
                    case "menuItem_MoveUp":
                        return $.easyui.treegrid.moveUp();
                    case "menuItem_MoveDown":
                        return $.easyui.treegrid.moveDown();
                    case "menuItem_AddToBefore":
                        return $.easyui.treegrid.addToBefore();
                    case "menuItem_AddToAfter":
                        return $.easyui.treegrid.addToAfter();
                    case "menuItem_FixSortId":
                        return $.easyui.treegrid.fixSortId();
                    case "menuItem_Refresh":
                        return $.easyui.treegrid.refresh();
                }
                return true;
            },
            //单击树菜单项
            fnClickTreeMenuItem: function (item) {
                switch (item.id) {
                    case "menuItem_Edit":
                        return $("#" + $.easyui.btnEditTreeId).click();
                    case "menuItem_Delete":
                        return $("#" + $.easyui.btnDeleteTreeId).click();
                }
                return true;
            },
            //仅允许选择树叶节点
            fnSelectTreeLeafOnly: function (node) {
                if (node.children && node.children.length > 0)
                    return false;
                return true;
            },
            //文本更改事件-支持设置多个事件处理函数
            textbox_onChange: function (callbacks) {
                return function (newValue, oldValue) {
                    if (!callbacks)
                        return;
                    var control = $(this);
                    $.each(callbacks, function (i, fn) {
                        fn(newValue, oldValue, control);
                    });
                };
            },
            //设置联动子combox控件 - onChange事件
            setChildCombox_onChange: function (childId, requestParam, url) {
                return function (newValue, oldValue, control) {
                    var child = $("#" + childId);
                    child.combobox("setValue", "");
                    if (!newValue) {
                        child.combobox("loadData", []);
                        return;
                    }
                    child.combobox("reload", getUrl());

                    //获取子控件url
                    function getUrl() {
                        url = url || getComboxUrl();
                        return addUrlParam();

                        //获取组合框url
                        function getComboxUrl() {
                            var options = $.data(control.get(0), "combobox").options;
                            return options.url;
                        }

                        //添加url参数
                        function addUrlParam() {
                            return $.joinUrl( $.removeUrlParams(url) , requestParam + "=" + newValue );
                        }
                    }
                }
            },
            //设置combox对应的隐藏控件文本 - onChange事件
            setComboxHiddenText_onChange: function (hiddenName) {
                return function (newValue, oldValue, control) {
                    var data = control.combobox("getData");
                    $.each(data, function (i, item) {
                        if (item.value === newValue)
                            $(":hidden[name='" + hiddenName + "']").val(item.text);
                    });
                };
            },
            //延迟设置Combox控件值 - onLoadSuccess事件
            setComboxLazyValue_onLoadSuccess: function (id) {
                return setLazyValue(id, function (control, value) {
                    control.combobox("setValue", value);
                });
            },
            //延迟设置ComboTree控件值 - onLoadSuccess事件
            setComboTreeLazyValue_onLoadSuccess: function (id) {
                return setLazyValue(id, function (control, value) {
                    control.combotree("setValue", value);
                });
            },
            //加载子表格 - onLoadSuccess事件
            loadSubGrid_onLoadSuccess: function (options) {
                return function (data) {
                    $("#grid").datagrid("subgrid", options);
                }
            },
            //表格详细 - Html内容
            gridDetail_detailFormatter: function () {
                return function (index, row) {
                    return getDetailHtml();
                }
            },
            //表格详细 - onExpandRow事件
            gridDetail_onExpandRow: function (url, isShowBorder, fnCreateUrl, paramName, buttonsDivId) {
                return function (index, row) {
                    var grid = $(this);
                    initRows();
                    var divDetail = getDetailDiv(index);
                    if (isLoad(divDetail))
                        return;
                    load();

                    //初始化
                    function initRows() {
                        var rows = grid.datagrid("getRows");
                        $.each(rows, function (i, each) {
                            var rowIndex = grid.datagrid("getRowIndex", each);
                            if (index === rowIndex)
                                return;
                            var detail = getDetailDiv(rowIndex);
                            if (!isLoad(detail))
                                return;
                            grid.datagrid('collapseRow', rowIndex);
                            grid.datagrid('getRowDetail', rowIndex).html(getDetailHtml());
                        });
                    }

                    //获取div
                    function getDetailDiv(rowIndex) {
                        return grid.datagrid('getRowDetail', rowIndex).find('div.gridDetail');
                    }

                    //判断是否已加载
                    function isLoad(detail) {
                        if (!detail || detail.length === 0)
                            return false;
                        return detail[0].outerHTML !== getDetailHtml();
                    }

                    //加载内容
                    function load() {
                        divDetail.panel({
                            border: isShowBorder || false,
                            href: getUrl(),
                            width: '100%',
                            onLoad: function () {
                                fixButtons($(this));
                                grid.datagrid('fixDetailRowHeight', index);
                                grid.datagrid('selectRow', index);
                                setTimeout(function () {
                                    grid.datagrid('fixDetailRowHeight', index);
                                }, 1000);

                                //修正面板按钮位置
                                function fixButtons($panel) {
                                    var panel = $panel.panel("panel");
                                    buttonsDivId = buttonsDivId || $.easyui.buttonsDivId;
                                    var btn = grid.datagrid('getRowDetail', index).find('#' + buttonsDivId);
                                    if (!btn || btn.length === 0)
                                        return;
                                    btn.addClass("dialog-button").appendTo(panel);
                                }
                            },
                            onResize: function (width, height) {
                                grid.datagrid('fixDetailRowHeight', index);
                            }
                        });

                        //获取Url
                        function getUrl() {
                            if (fnCreateUrl)
                                return fnCreateUrl(row);
                            paramName = paramName || "id";
                            return $.joinUrl(url, paramName + "=" + row.Id);
                        }
                    }
                };
            },
            //加载表格列中的combox控件-将url加载方式转成本地加载方式，用于修正每次编辑表格列时都会发出请求
            loadGridColumnCombox_onBeforeLoad: function (url) {
                return loadGridColumnControl(url, function (control, data) {
                    control.combobox("loadData", data);
                });
            },
            //加载表格列中的comboTree控件-将url加载方式转成本地加载方式，用于修正每次编辑表格列时都会发出请求
            loadGridColumnComboTree_onBeforeLoad: function (url) {
                return loadGridColumnControl(url, function (control, data) {
                    control.tree("loadData", data);
                });
            },
            //点击树节点刷新面板-onClick事件
            clickTreeNodeRefreshPanel_onClick: function (panelId, url, paramName, fnCreateUrl) {
                return function (node) {
                    fnCreateUrl = fnCreateUrl || getUrl;
                    $.easyui.refreshPanel(panelId, fnCreateUrl(node));

                    //获取url
                    function getUrl(treeNode) {
                        return $.joinUrl(url, paramName + "=" + treeNode.id);
                    }
                };
            },
            //点击分页组件的按钮-onSelectPage事件
            clickPageButton_onSelectPage: function (url, fnRefresh) {
                return function (pageNumber, pageSize) {
                    var href = $.joinUrl(url, { page: pageNumber, rows: pageSize });
                    fnRefresh(href, this);
                };
            }
        };
    }());
})(jQuery);

