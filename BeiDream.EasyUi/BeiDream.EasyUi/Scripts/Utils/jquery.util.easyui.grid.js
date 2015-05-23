(function ($) {
    //创建新行
    function createNewRow(row, fn, param) {
        if (!$.easyui.newRowUrl) {
            createRow();
            return;
        }
        createRowFromUrl();

        //从客户端创建新行
        function createRow() {
            if (!row || $.isEmptyObject(row))
                row = $.extend({}, $.easyui.newRow);
            fixCreateTime();
            if (fn)
                fn(row);

            //修正创建时间
            function fixCreateTime() {
                if ($.containsProperty(row, "CreateTime"))
                    row.CreateTime = $.newIsoDate();
            }
        }

        //从服务端创建新行
        function createRowFromUrl() {
            $.easyui.ajax($.easyui.newRowUrl, param, fn, "json", "GET");
        }
    }

    //创建树型表格新行
    function createTreeGridRow(row, fn, parentId) {
        createNewRow(row, function (result) {
            if (!result.Id)
                $.extend(result, { Id: $.newGuid() });
            if (!result.ParentId)
                $.extend(result, { ParentId: parentId });
            $.extend(result, { AllowUpdateSortId: true });
            fn(result);
        }, { parentId: parentId });
    }

    //保存
    function save(grid, url, callback, fnSaveBefore, fnRefresh, fnFilter) {
        if (!fnSaveBefore())
            return;
        var changes = grid.datagrid("getChanges");
        if (changes && changes.length == 0) {
            $.easyui.warn($.easyui.saveNotChangeMessage);
            return;
        }
        var addList = filterAttributes(grid.datagrid("getChanges", "inserted"), fnFilter);
        var updateList = filterAttributes(grid.datagrid("getChanges", "updated"), fnFilter);
        var deleteList = filterAttributes(grid.datagrid("getChanges", "deleted"), fnFilter);
        ajax();

        //发送请求
        function ajax() {
            url = url || $.easyui.saveUrl;
            var token = $.getAntiForgeryToken();
            var param = { addList: $.toJSON(addList), updateList: $.toJSON(updateList), deleteList: $.toJSON(deleteList), __RequestVerificationToken: token };
            $.easyui.ajax(url, param, ajaxCallback);

            //回调
            function ajaxCallback(result) {
                $.easyui.showFormMessage(result);
                if (result.Code !== $.easyui.state.ok)
                    return;
                grid.datagrid("acceptChanges");
                if (fnRefresh)
                    fnRefresh(result);
                if (callback)
                    callback(result);
            }
        }
    }

    //过滤属性
    function filterAttributes(rows, fnFilter) {
        if (!rows || rows.length === 0)
            return rows;
        var result = [];
        $.each(rows, function (i, row) {
            var item = $.extend({}, row);
            delete item.isNewRecord;
            if (fnFilter)
                fnFilter(item);
            result.push(item);
        });
        return result;
    }

    //提交选中的Id
    function submitIds(grid, url, callback, fnSubmitBefore, fnRefresh) {
        if (!fnSubmitBefore())
            return;
        var rows = grid.datagrid("getChecked");
        if (rows.length === 0) {
            $.easyui.warn($.easyui.notCheckedMessage);
            return;
        }
        ajax();

        //发送请求
        function ajax() {
            url = url || $.easyui.submitIdsUrl;
            var ids = $.easyui.getIds(rows);
            var param = { ids: ids, __RequestVerificationToken: $.getAntiForgeryToken() };
            $.easyui.ajax(url, param, ajaxCallback);

            //回调
            function ajaxCallback(result) {
                $.easyui.showFormMessage(result);
                if (result.Code !== $.easyui.state.ok)
                    return;
                if (fnRefresh)
                    fnRefresh(result);
                if (callback)
                    callback(result);
            }
        }
    }

    //表格操作
    $.easyui.grid = function () {
        //验证表格行的表单
        function validateDetaiForm(grid) {
            var rowDetail = grid.datagrid("getRowDetail", getLastIndex(grid));
            var form = rowDetail.find("form");
            return form && form.form("validate");
        }

        //获取表格最后一行索引
        function getLastIndex(grid) {
            return grid.datagrid('getRows').length - 1;
        }

        return {
            //获取总行数
            getPagerTotal: function (grid) {
                return $.toNumber(grid.datagrid("getPager").pagination('options').total);
            },
            //更新总行数
            updatePagerTotal: function (grid, total) {
                grid.datagrid("getPager").pagination({
                    total: total
                });
            },
            //获取新行
            getNewRows: function (grid) {
                return grid.data("datagrid").insertedRows;
            },
            //获取已删除行
            getDeletedRows: function (grid) {
                return grid.data("datagrid").deletedRows;
            },
            //获取已更新行
            getUpdatedRows: function (grid) {
                return grid.data("datagrid").updatedRows;
            },
            getRows: function (gridId) {
                ///	<summary>
                ///	获取表格所有行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                grid.edatagrid("saveRow");
                return filterAttributes(grid.datagrid("getRows"));
            },
            getRowsJson: function (gridId) {
                ///	<summary>
                ///	获取表格所有行的Json数据
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var rows = $.easyui.grid.getRows(gridId);
                return $.toJSON(rows);
            },
            add: function (row, gridId, fnAddBefore) {
                ///	<summary>
                ///	创建新行
                ///	</summary>
                ///	<param name="row" type="String">
                ///	行
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                ///	<param name="fnAddBefore" type="Function">
                ///	添加前操作，返回false跳出
                ///	</param>
                if (fnAddBefore && !fnAddBefore())
                    return;
                var grid = $.easyui.getGrid(gridId);
                if (!grid.edatagrid("validateRow"))
                    return;
                createNewRow(row, function (newRow) {
                    grid.edatagrid("addRow", { row: newRow });
                });
            },
            edit: function (gridId) {
                ///	<summary>
                ///	编辑行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var row = grid.datagrid('getSelected');
                if (!row) {
                    $.easyui.warn($.easyui.editNotSelectedMessage);
                    return;
                }
                var index = grid.datagrid("getRowIndex", row);
                grid.edatagrid("editRow", index);
            },
            cancel: function (gridId) {
                ///	<summary>
                ///	取消选中的行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                grid.edatagrid('cancelRow');
            },
            deleteById: function (gridId) {
                ///	<summary>
                ///	删除行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var result = grid.datagrid("getChecked");
                if (result && result.length > 0) {
                    deleteChecked();
                    return;
                }
                var selectedRow = grid.datagrid('getSelected');
                if (!selectedRow) {
                    $.easyui.warn($.easyui.deleteNotSelectedMessage);
                    return;
                }
                grid.edatagrid("destroyRow");

                //删除复选框选中的行
                function deleteChecked() {
                    for (var i = 0; i < result.length; i++) {
                        var index = grid.datagrid("getRowIndex", result[i]);
                        grid.edatagrid("destroyRow", index);
                    }
                }
            },
            validate: function (gridId) {
                ///	<summary>
                ///	验证行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                return grid.edatagrid("validateRow");
            },
            save: function (url, callback, gridId) {
                ///	<summary>
                ///	保存表格中的修改
                ///	</summary>
                ///	<param name="url" type="String">
                ///	保存url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                save(grid, url, callback, function () {
                    if (!grid.edatagrid("validateRow"))
                        return false;
                    grid.edatagrid("saveRow");
                    return true;
                }, function () {
                    grid.datagrid("reload");
                });
            },
            submitIds: function (url, callback, gridId) {
                ///	<summary>
                ///	提交checkbox选中的Id列表
                ///	</summary>
                ///	<param name="url" type="String">
                ///	处理Url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                submitIds(grid, url, callback, function () {
                    if (!grid.edatagrid("validateRow"))
                        return false;
                    grid.edatagrid("saveRow");
                    return true;
                }, function () {
                    grid.datagrid("reload");
                });
            },
            addByDetail: function (gridId) {
                ///	<summary>
                ///	通过展开表格行创建新行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!validateDetaiForm(grid))
                    return;
                addRow();

                //添加行
                function addRow() {
                    grid.datagrid('appendRow', {});
                    var index = getLastIndex(grid);
                    grid.datagrid('expandRow', index);
                    grid.datagrid('selectRow', index);
                }
            },
            editByDetail: function (gridId) {
                ///	<summary>
                ///	通过展开表格行编辑新行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!validateDetaiForm(grid))
                    return;
                var row = grid.datagrid('getSelected');
                if (!validateSelected())
                    return;
                editRow();

                //验证是否选中行
                function validateSelected() {
                    if (row) 
                        return true;
                    $.easyui.warn($.easyui.editNotSelectedMessage);
                    return false;
                }

                //编辑行
                function editRow() {
                    var index = grid.datagrid("getRowIndex", row);
                    grid.datagrid('expandRow', index);
                }
            },
            lookup: function (value, text) {
                ///	<summary>
                ///	查找带回
                ///	</summary>
                ///	<param name="value" type="String">
                ///	值
                ///	</param>
                ///	<param name="text" type="String">
                ///	文本
                ///	</param>
                var control = $.easyui.getArray("lookups").pop();
                if (!control)
                    return;
                control.lookup('setValue', value);
            }
        }
    }();
    //树型表格操作
    $.easyui.treegrid = function () {
        //移动行
        function move(grid, id, fnMove) {
            if (!id)
                return null;
            if (typeof id == 'object')
                id = id.Id;
            var newRows = $.easyui.grid.getNewRows(grid);
            var deletedRows = $.easyui.grid.getDeletedRows(grid);
            var isNew = false;
            var total = 0;
            ready();
            var data = pop();
            updateState();
            fnMove(data);
            moveAfter();
            return data;

            //记录相关状态
            function ready() {
                var row = grid.treegrid("find", id);
                if (newRows.contains(row))
                    isNew = true;
                total = $.easyui.grid.getPagerTotal(grid);
            }

            //将节点移除
            function pop() {
                return grid.treegrid("pop", id);
            }

            //更新状态
            function updateState() {
                if (isNew)
                    newRows.push(data);
                deletedRows.remove(data);
            }

            //移动后更新状态和总行数
            function moveAfter() {
                $.easyui.treegrid.updateRow(grid, data);
                grid.datagrid("getPager").pagination({
                    total: total
                });
                grid.treegrid("select", id);
            }
        }

        //交换排序号
        function swapSortId(grid, row1, row2) {
            var sortId = row1.SortId;
            row1.SortId = row2.SortId;
            row2.SortId = sortId;
        }

        //排序
        function sort(grid, src, dest, prev) {
            if (!src || !dest)
                return;
            var nodes = $.easyui.treegrid.nextAll(grid, dest);
            nodes.remove(src);
            sortNextAll();

            //对当前节点之后的所有节点排序
            function sortNextAll() {
                $.each(nodes, function (i, node) {
                    updateSortId(grid, node, prev.SortId, 1);
                    prev = node;
                });
            }
        }

        //更新排序号
        function updateSortId(grid, row, sortId, increment) {
            sortId = getSortId(sortId, increment);
            if (sortId < 0)
                sortId = 0;
            row.SortId = sortId;
            $.easyui.treegrid.updateRow(grid, row);
        }

        //获取排序号
        function getSortId(sortId, increment) {
            return $.toNumber(sortId) + $.toNumber(increment);
        }

        //将源行排序到目标行之前，并更新目标行之后的所有排序号
        function sortToBefore(grid, src, dest) {
            if (!src || !dest)
                return;
            updateSortId(grid, src, dest.SortId);
            updateSortId(grid, dest, dest.SortId, 1);
            sort(grid, src, dest, dest);
        }

        //将源行排序到目标行之后，并更新目标行之后的所有排序号
        function sortToAfter(grid, src, dest) {
            if (!src || !dest)
                return;
            updateSortId(grid, src, src.SortId, -1);
            var prev = src;
            var childs = $.easyui.treegrid.getChilds(grid, src.ParentId);
            for (var i = 0; i < childs.length; i++) {
                if (childs[i].SortId > src.SortId && childs[i].SortId < dest.SortId) {
                    updateSortId(grid, childs[i], prev.SortId, 1);
                    prev = childs[i];
                }
            }
            updateSortId(grid, dest, dest.SortId, -1);
            updateSortId(grid, src, dest.SortId, 1);
            sort(grid, src, dest, src);
        }

        //刷新返回结果
        function refreshResult(grid, result) {
            $.each(result.Data, function (i, data) {
                delete data.children;
                if ($.easyui.treegrid.exists(grid, data.Id))
                    grid.treegrid("update", { id: data.Id, row: data });
            });
        }

        return {
            //添加行：<1> 修正总行数 <2>设置新增状态
            appendRow: function (grid, row, parentId) {
                if (!grid || !row)
                    return;
                if (!parentId)
                    parentId = row.ParentId;
                var total = $.easyui.grid.getPagerTotal(grid);
                grid.treegrid('append', {
                    parent: parentId,
                    data: [row]
                });
                if (!parentId)
                    $.easyui.grid.updatePagerTotal(grid, total + 1);
            },
            //插入行: <1> 修正总行数
            insertRow: function (grid, data) {
                if (!grid || !data)
                    return;
                var total = $.easyui.grid.getPagerTotal(grid);
                grid.treegrid("insert", data);
                if (!data.data.ParentId)
                    $.easyui.grid.updatePagerTotal(grid, total + 1);
            },
            //取消行:<1> 修正总行数
            cancelRow: function (grid, id) {
                var row = grid.treegrid("find", id);
                if (!row)
                    return;
                var total = $.easyui.grid.getPagerTotal(grid);
                grid.treegrid('cancelEdit', id);
                if (row.ParentId)
                    $.easyui.grid.updatePagerTotal(grid, total);
                else
                    $.easyui.grid.updatePagerTotal(grid, total - 1);
            },
            //更新行: <1> 设置更新状态
            updateRow: function (grid, row) {
                if (!row)
                    return;
                if (!grid.treegrid("find", row.Id))
                    return;
                grid.treegrid("update", {
                    id: row.Id,
                    row: row
                });
                var newRows = $.easyui.grid.getNewRows(grid);
                if (newRows.contains(row))
                    return;
                var updatedRows = $.easyui.grid.getUpdatedRows(grid);
                if (updatedRows.contains(row))
                    return;
                updatedRows.push(row);
            },
            //删除行: <1> 修正总行数
            deleteRow: function (grid, id, isNew) {
                var row = grid.treegrid("find", id);
                if (!row)
                    return;
                var total = $.easyui.grid.getPagerTotal(grid);
                if (isNew) {
                    grid.treegrid('beginEdit', id);
                    grid.treegrid('cancelEdit', id);
                } else {
                    grid.treegrid('cancelEdit', id);
                    grid.treegrid('remove', id);
                }
                if (row.ParentId)
                    $.easyui.grid.updatePagerTotal(grid, total);
            },
            //将源节点移动到目标节点之前
            moveToBefore: function (grid, srcRowId, destRowId, fnSort) {
                if (!destRowId)
                    return null;
                if (typeof destRowId == 'object')
                    destRowId = destRowId.Id;
                var dest = grid.treegrid("find", destRowId);
                if (!fnSort)
                    fnSort = sortToBefore;
                return move(grid, srcRowId, function (row) {
                    $.easyui.treegrid.insertRow(grid, { before: destRowId, data: row });
                    fnSort(grid, row, dest);
                });
            },
            //将源节点移动到目标节点之后
            moveToAfter: function (grid, srcRowId, destRowId, fnSort) {
                if (!destRowId)
                    return null;
                if (typeof destRowId == 'object')
                    destRowId = destRowId.Id;
                var dest = grid.treegrid("find", destRowId);
                if (!fnSort)
                    fnSort = sortToAfter;
                return move(grid, srcRowId, function (row) {
                    $.easyui.treegrid.insertRow(grid, { after: destRowId, data: row });
                    fnSort(grid, row, dest);
                });
            },
            //将源节点移动到目标节点之下
            moveToChild: function (grid, srcRowId, destRowId) {
                if (!destRowId)
                    return null;
                if (typeof destRowId == 'object')
                    destRowId = destRowId.Id;
                var dest = grid.treegrid("find", destRowId);
                if (!dest)
                    return null;
                var last = $.easyui.treegrid.lastChild(grid, dest);
                var sortId = last ? getSortId(last.SortId) + 1 : 1;
                return move(grid, srcRowId, function (row) {
                    $.easyui.treegrid.appendRow(grid, row, destRowId);
                    row.SortId = sortId;
                });
            },
            //获取下级子节点，仅包含直接子节点
            getChilds: function (grid, parentId) {
                var result = [];
                var childs = grid.treegrid("getChildren", parentId);
                $.each(childs, function (i, row) {
                    if (isParentEquals(row))
                        result.push(row);
                });
                return result;

                //判断父节点相等
                function isParentEquals(row) {
                    if (!row.ParentId && !parentId)
                        return true;
                    return row.ParentId === parentId;
                }
            },
            //获取上一行
            prev: function (grid, row) {
                if (!row)
                    row = grid.treegrid("getSelected");
                if (!row)
                    return null;
                var result = null;
                var childs = $.easyui.treegrid.getChilds(grid, row.ParentId);
                for (var i = 0; i < childs.length; i++) {
                    if (childs[i].Id === row.Id)
                        break;
                    result = childs[i];
                }
                return result;
            },
            //获取下一行
            next: function (grid, row) {
                if (!row)
                    row = grid.treegrid("getSelected");
                if (!row)
                    return null;
                var childs = $.easyui.treegrid.getChilds(grid, row.ParentId);
                var isNext = false;
                for (var i = 0; i < childs.length; i++) {
                    if (isNext)
                        return childs[i];
                    if (childs[i].Id === row.Id)
                        isNext = true;
                }
                return null;
            },
            //获取最后一个子节点
            lastChild: function (grid, row) {
                if (!row)
                    row = grid.treegrid("getSelected");
                if (!row)
                    return null;
                var childs = $.easyui.treegrid.getChilds(grid, row.Id);
                if (childs.length === 0)
                    return null;
                return childs[childs.length - 1];
            },
            //获取该行后面的所有同级节点
            nextAll: function (grid, row) {
                if (!row)
                    row = grid.treegrid("getSelected");
                if (!row)
                    return null;
                var result = [];
                var isNext = false;
                var childs = $.easyui.treegrid.getChilds(grid, row.ParentId);
                for (var i = 0; i < childs.length; i++) {
                    if (childs[i].Id === row.Id) {
                        isNext = true;
                        continue;;
                    }
                    if (isNext)
                        result.push(childs[i]);
                }
                return result;
            },
            //检测行是否存在
            exists: function (grid, id) {
                var row = grid.treegrid("find", id);
                if (row)
                    return true;
                return false;
            },
            addRoot: function (row, gridId, fnAddBefore) {
                ///	<summary>
                ///	添加根节点
                ///	</summary>
                ///	<param name="row" type="String">
                ///	行
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                ///	<param name="fnAddBefore" type="Function">
                ///	添加前操作，返回false跳出
                ///	</param>
                if (fnAddBefore && !fnAddBefore())
                    return;
                var grid = $.easyui.getGrid(gridId);
                if (!grid.etreegrid("validateRow"))
                    return;
                createTreeGridRow(row, function (newRow) {
                    grid.etreegrid("addRow", newRow);
                });
            },
            addToChild: function (row, gridId, parent) {
                ///	<summary>
                ///	添加为当前选中行的子节点
                ///	</summary>
                ///	<param name="row" type="String">
                ///	行
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                ///	<param name="parent" type="Object">
                ///	父节点
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!grid.etreegrid("validateRow"))
                    return;
                if (!parent)
                    parent = grid.treegrid("getSelected");
                var parentId = parent && parent.Id;
                grid.treegrid("expand", parentId);
                setTimeout(function () {
                    createTreeGridRow(row, function (newRow) {
                        grid.etreegrid("addRow", newRow);
                    }, parentId);
                }, 200);
            },
            addToBefore: function (row, gridId, node) {
                ///	<summary>
                ///	作为同级节点添加到指定节点的上方
                ///	</summary>
                ///	<param name="row" type="String">
                ///	行
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                ///	<param name="node" type="Object">
                ///	指定节点
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!node)
                    node = grid.treegrid("getSelected");
                if (!node)
                    return;
                createTreeGridRow(row, function (newRow) {
                    newRow.AllowUpdateSortId = false;
                    if (!grid.etreegrid("validateRow"))
                        return;
                    grid.etreegrid("insertBefore", newRow);
                    sortToBefore(grid, newRow, node);
                    grid.etreegrid("editRow", newRow.Id);
                }, node.ParentId);
            },
            addToAfter: function (row, gridId) {
                ///	<summary>
                ///	作为同级节点添加到当前选中行的下方
                ///	</summary>
                ///	<param name="row" type="String">
                ///	行
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var node = grid.treegrid("getSelected");
                if (!node)
                    return;
                var nextNode = $.easyui.treegrid.next(grid, node);
                if (!nextNode) {
                    grid.treegrid("unselectAll");
                    $.easyui.treegrid.addToChild(row, gridId, grid.treegrid("find", node.ParentId));
                    return;
                }
                grid.treegrid("select", nextNode.Id);
                $.easyui.treegrid.addToBefore(row, gridId, nextNode);
            },
            cancel: function (gridId) {
                ///	<summary>
                ///	取消选中的行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                grid.etreegrid('cancelRow');
            },
            edit: function (gridId) {
                ///	<summary>
                ///	编辑行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var row = grid.etreegrid('getSelected');
                if (!row) {
                    $.easyui.warn($.easyui.editNotSelectedMessage);
                    return;
                }
                grid.etreegrid("editRow", row.Id);
            },
            deleteById: function (gridId) {
                ///	<summary>
                ///	删除行
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var ids = getCheckedIds();
                if (ids.length > 0) {
                    deleteCheckedRows();
                    return;
                }
                var selectedRow = grid.etreegrid('getSelected');
                if (!selectedRow) {
                    $.easyui.warn($.easyui.deleteNotSelectedMessage);
                    return;
                }
                grid.etreegrid("destroyRow", selectedRow.Id);

                //获取已勾选行的Id
                function getCheckedIds() {
                    var result = [];
                    var checkedRows = grid.datagrid("getChecked");
                    $.each(checkedRows, function (i, row) {
                        result.push(row.Id);
                    });
                    return result;
                }

                //删除勾选的行
                function deleteCheckedRows() {
                    $.each(ids, function (i, id) {
                        grid.etreegrid("destroyRow", id);
                    });
                }
            },
            save: function (url, callback, gridId) {
                ///	<summary>
                ///	保存表格中的修改
                ///	</summary>
                ///	<param name="url" type="String">
                ///	保存Url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                save(grid, url, callback, function () {
                    return grid.etreegrid("saveRow");
                }, function (result) {
                    refreshResult(grid, result);
                }, function (item) {
                    delete item.state;
                    delete item._parentId;
                    delete item.children;
                    delete item.AllowUpdateSortId;
                });
            },
            submitIds: function (url, callback, gridId) {
                ///	<summary>
                ///	提交checkbox选中的Id列表
                ///	</summary>
                ///	<param name="url" type="String">
                ///	处理Url
                ///	</param>
                ///	<param name="callback" type="Function">
                ///	成功回调函数
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                submitIds(grid, url, callback, function () {
                    return grid.etreegrid("saveRow");
                }, function (result) {
                    refreshResult(grid, result);
                });
            },
            query: function (formId, gridId, fnQueryBefore) {
                ///	<summary>
                ///	刷新
                ///	</summary>
                ///	<param name="formId" type="String">
                ///	查询表单Id
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                ///	<param name="fnQueryBefore" type="Function">
                ///	查询前操作
                ///	</param>
                var param = $.easyui.getQueryForm(formId).serializeJson();
                param = $.extend(param, { QueryOperation: "Query" });
                if (fnQueryBefore)
                    fnQueryBefore(param);
                $.easyui.getGrid(gridId).treegrid({
                    pageNumber: 1,
                    queryParams: param
                });
            },
            refresh: function (formId, gridId) {
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
                $.easyui.treegrid.query(formId, gridId, function (param) {
                    param.QueryOperation = "refresh";
                });
                $.easyui.getGrid(gridId).datagrid("rejectChanges");
            },
            moveUp: function (gridId) {
                ///	<summary>
                ///	上移
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!grid.etreegrid("saveRow"))
                    return;
                var row = grid.treegrid("getSelected");
                if (!row) {
                    $.easyui.warn($.easyui.moveUpNotSelectedMessage);
                    return;
                }
                var prev = $.easyui.treegrid.prev(grid, row);
                swapSort();

                //交换排序
                function swapSort() {
                    if (!prev)
                        return;
                    $.easyui.treegrid.moveToBefore(grid, row, prev, swapSortId);
                    $.easyui.treegrid.updateRow(grid, prev);
                }
            },
            moveDown: function (gridId) {
                ///	<summary>
                ///	下移
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                if (!grid.etreegrid("saveRow"))
                    return;
                var row = grid.treegrid("getSelected");
                if (!row) {
                    $.easyui.warn($.easyui.moveDownNotSelectedMessage);
                    return;
                }
                var next = $.easyui.treegrid.next(grid, row);
                swapSort();

                //交换排序
                function swapSort() {
                    if (!next)
                        return;
                    $.easyui.treegrid.moveToAfter(grid, row, next, swapSortId);
                    $.easyui.treegrid.updateRow(grid, next);
                }
            },
            fixSortId: function (gridId) {
                ///	<summary>
                ///	修正当前行所有同级节点排序号
                ///	</summary>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                var row = grid.treegrid("getSelected");
                if (!row)
                    return;
                var childs = $.easyui.treegrid.getChilds(grid, row.ParentId);
                if (childs.length === 0)
                    return;
                var prev = null;
                $.each(childs, function (i, item) {
                    if (!prev)
                        updateFirstNode();
                    else
                        updateNextNode();
                    prev = item;

                    //更新第一个节点
                    function updateFirstNode() {
                        var option = grid.datagrid("getPager").pagination('options');
                        var number = 1;
                        if (item.Level === 1)
                            number = $.toNumber(option.pageSize) * ($.toNumber(option.pageNumber) - 1) + 1;
                        if (item.SortId === number)
                            return;
                        updateSortId(grid, item, number);
                    }

                    //更新下面的节点
                    function updateNextNode() {
                        if (getSortId(item.SortId) === getSortId(prev.SortId, 1))
                            return;
                        updateSortId(grid, item, prev.SortId, 1);
                    }
                });
            },
            dragMinLevel: function (minLevel, gridId) {
                ///	<summary>
                ///	限制允许拖动的最小级数
                ///	</summary>
                ///	<param name="minLevel" type="int">
                ///	允许拖动的最小级数,设置为2，表示第1级无法拖动
                ///	</param>
                ///	<param name="gridId" type="String">
                ///	表格Id
                ///	</param>
                var grid = $.easyui.getGrid(gridId);
                grid.etreegrid("dragMinLevel", minLevel);
            }
        }
    }();
})(jQuery);