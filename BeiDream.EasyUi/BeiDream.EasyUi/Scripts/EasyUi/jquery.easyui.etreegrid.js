(function ($) {
    var currTarget;
    var isEnableDnd = false;
    var level = 1;
    var isLoadChilds = false;
    $(function () {
        $(document).unbind('.etreegrid').bind('mousedown.etreegrid', function (e) {
            var p = $(e.target).closest('div.datagrid-view,div.combo-panel');
            if (p.length) {
                if (p.hasClass('datagrid-view')) {
                    var dg = p.children('table');
                    if (dg.length && currTarget != dg[0]) {
                        save();
                    }
                }
                return;
            }
            save();

            function save() {
                var dg = $(currTarget);
                if (dg.length) {
                    dg.etreegrid('saveRow');
                    currTarget = undefined;
                }
            }
        });
    });

    function buildGrid(target) {
        var opts = $.data(target, 'etreegrid').options;
        $(target).treegrid($.extend({}, opts, {
            onDblClickCell: function (field, row) {
                if (opts.editing) {
                    $(this).etreegrid('editRow', row.Id);
                    focusEditor(target, field);
                }
                if (opts.onDblClickCell) {
                    opts.onDblClickCell.call(target, field, row);
                }
            },
            onClickCell: function (field, row) {
                if (opts.editing && opts.editId) {
                    $(this).etreegrid('editRow', row.Id);
                    focusEditor(target, field);
                }
                if (opts.onClickCell) {
                    opts.onClickCell.call(target, field, row);
                }
            },
            onAfterEdit: function (row, changes) {
                opts.editId = undefined;
                if (opts.onAfterEdit) opts.onAfterEdit.call(target, row, changes);
            },
            onCancelEdit: function (row) {
                opts.editId = undefined;
                if (row.isNewRecord) {
                    $(this).treegrid('deleteRow', row.Id);
                }
                if (opts.onCancelEdit) opts.onCancelEdit.call(target, row);
            },
            onBeforeLoad: function (row, param) {
                if (opts.onBeforeLoad.call(target, param) == false) {
                    return false;
                }
                if (row != null && isLoadChilds)
                    param.QueryOperation = "LoadChilds";
                $(this).etreegrid('cancelRow');
                return true;
            },
            onLoadSuccess: function (row, data) {
                var grid = $(this);
                updateState();
                updateSortId();
                enableDnd();
                if (opts.onLoadSuccess)
                    opts.onLoadSuccess.call(target, row, data);

                //更新节点状态
                function updateState() {
                    if (!row)
                        return;
                    if (data && data.length > 0)
                        return;
                    row.state = "open";
                    grid.etreegrid('update', {
                        id: row.Id,
                        row: row
                    });
                }

                //更新排序号
                function updateSortId() {
                    if (data.rows && data.total)
                        return;
                    if (!data || data.length !== 1)
                        return;
                    $.each(data, function (i, item) {
                        if (!item.SortId || !item.AllowUpdateSortId)
                            return;
                        var rows = $.easyui.treegrid.getChilds(grid, item.ParentId);
                        var sortId = 0;
                        $.each(rows, function (index, each) {
                            if (each.Id == item.Id)
                                return;
                            if (each.SortId > sortId)
                                sortId = each.SortId;
                        });
                        if (sortId >= item.SortId)
                            item.SortId = sortId + 1;
                        item.AllowUpdateSortId = false;
                    });
                }

                //启用拖拽
                function enableDnd() {
                    if (isEnableDnd)
                        grid.treegrid('enableDnd', row ? row.Id : null);
                }
            },
            onBeforeDrag: function (row) {
                var grid = $(this);
                if (!row)
                    return false;
                if (opts.editId === row.Id)
                    return false;
                if (grid.treegrid("getLevel", row.Id) < $.toNumber(level))
                    return false;
                return true;
            },
            onDrop: function (targetRow, sourceRow, point) {
                if (!targetRow || !sourceRow)
                    return;
                var grid = $(this);
                moveToChild();
                move();

                //移动到下级
                function moveToChild() {
                    if (point !== "append")
                        return;
                    sourceRow.ParentId = targetRow.Id;
                    updateChilds();
                }

                //移动到同级
                function move() {
                    if (point === "append")
                        return;
                    if (sourceRow.ParentId == targetRow.ParentId)
                        return;
                    sourceRow.ParentId = targetRow.ParentId;
                    updateChilds();
                }

                //更新子节点
                function updateChilds() {
                    var childs = grid.treegrid("getChildren", sourceRow.Id);
                    $.each(childs, function (i, row) {
                        $.easyui.treegrid.updateRow(grid, row);
                    });
                }
            },
            onCheck: function (row) {
                if (!row)
                    return;
                var grid = $(this);
                var childs = $.easyui.treegrid.getChilds(grid, row.Id);
                $.each(childs, function (i, item) {
                    grid.treegrid("checkRow", item.Id);
                });
                if (opts.onCheck)
                    opts.onCheck.call(target, row);
            },
            onUncheck: function (row) {
                if (!row)
                    return;
                var grid = $(this);
                var childs = $.easyui.treegrid.getChilds(grid, row.Id);
                $.each(childs, function (i, item) {
                    grid.treegrid("uncheckRow", item.Id);
                });
                if (opts.onUncheck)
                    opts.onUncheck.call(target, row);
            },
            onBeforeExpand: function (row) {
                isLoadChilds = true;
                if (opts.onBeforeExpand)
                    opts.onBeforeExpand.call(target, row);
            },
            onExpand: function (row) {
                isLoadChilds = false;
                if (opts.onExpand)
                    opts.onExpand.call(target, row);
            }
        }));
    }

    function focusEditor(target, field) {
        var opts = $(target).etreegrid('options');
        var t;
        var editor = $(target).treegrid('getEditor', { id: opts.editId, field: field });
        if (editor) {
            t = editor.target;
        } else {
            var editors = $(target).treegrid('getEditors', opts.editId);
            if (editors.length) {
                t = editors[0].target;
            }
        }
        if (t) {
            if ($(t).hasClass('textbox-f')) {
                $(t).textbox('textbox').focus();
            } else {
                $(t).focus();
            }
        }
    }

    function validateRow(grid, id) {
        if (grid.treegrid('validateRow', id))
            return true;
        grid.treegrid('select', id);
        focusEditor(grid);
        return false;
    }

    function add(dg, opts, data, fnAdd) {
        if (opts.editId) {
            if (!validateRow(dg, opts.editId))
                return;
            dg.treegrid('endEdit', opts.editId);
        }
        var row;
        if (typeof data == 'object') {
            row = $.extend(data, { isNewRecord: true });
        } else {
            row = { Id: $.newGuid(), ParentId: data, isNewRecord: true };
        }
        fnAdd(row);
        $.easyui.grid.getNewRows(dg).push(row);
        opts.editId = row.Id;
        dg.treegrid('beginEdit', opts.editId);
        dg.treegrid('select', opts.editId);
        focusEditor(dg);
    }

    $.fn.etreegrid = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.etreegrid.methods[options];
            if (method) {
                return method(this, param);
            } else {
                return this.treegrid(options, param);
            }
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'etreegrid');
            if (state) {
                $.extend(state.options, options);
            } else {
                $.data(this, 'etreegrid', {
                    options: $.extend({}, $.fn.etreegrid.defaults, $.fn.etreegrid.parseOptions(this), options)
                });
                var grid = $(this);
                if (grid.attr("enableDrag")) {
                    grid.etreegrid('enableDnd');
                    level = $.toNumber(grid.attr("enableDrag"));
                }
            }
            buildGrid(this);
        });
    };

    $.fn.etreegrid.parseOptions = function (target) {
        return $.extend({}, $.fn.treegrid.parseOptions(target), {
        });
    };

    $.fn.etreegrid.methods = {
        options: function (jq) {
            var opts = $.data(jq[0], 'etreegrid').options;
            return opts;
        },
        loadData: function (jq, data) {
            return jq.each(function () {
                $(this).etreegrid('cancelRow');
                $(this).treegrid('loadData', data);
            });
        },
        enableEditing: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'etreegrid').options;
                opts.editing = true;
            });
        },
        disableEditing: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'etreegrid').options;
                opts.editing = false;
            });
        },
        editRow: function (jq, id) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'etreegrid').options;
                if (!validateRow(dg, opts.editId))
                    return;
                dg.treegrid('endEdit', opts.editId);
                dg.treegrid('beginEdit', id);
                opts.editId = id;
                focusEditor(this);
                currTarget = this;
            });
        },
        addRow: function (jq, data) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'etreegrid').options;
                add(dg, opts, data, function (row) {
                    $.easyui.treegrid.appendRow(dg, row);
                });
            });
        },
        insertBefore: function (jq, data) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'etreegrid').options;
                var node = dg.treegrid("getSelected");
                if (!node)
                    return;
                add(dg, opts, data, function (row) {
                    $.easyui.treegrid.insertRow(dg, { before: node.Id, data: row });
                });
            });
        },
        validateRow: function (jq) {
            var dg = jq.eq(0);
            var opts = $.data(jq[0], 'etreegrid').options;
            if (!opts.editId)
                return true;
            if (!validateRow(dg, opts.editId))
                return false;
            return true;
        },
        saveRow: function (jq) {
            var dg = jq.eq(0);
            var opts = $.data(jq[0], 'etreegrid').options;
            if (!opts.editId)
                return true;
            if (!validateRow(dg, opts.editId))
                return false;
            dg.treegrid('endEdit', opts.editId);
            return true;
        },
        cancelRow: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'etreegrid').options;
                if (opts.editId)
                    $.easyui.treegrid.cancelRow($(this), opts.editId);
            });
        },
        destroyRow: function (jq, id) {
            return jq.each(function () {
                var dg = $(this);
                var rows = [];
                if (id == undefined) {
                    rows = dg.treegrid('getSelections');
                } else {
                    var row = dg.treegrid("find", id);
                    if (row) {
                        rows.push(row);
                    }
                }
                if (!rows.length) {
                    return;
                }
                for (var i = 0; i < rows.length; i++) {
                    $.easyui.treegrid.deleteRow(dg, rows[i].Id, rows[i].isNewRecord);
                }
            });
        },
        enableDnd: function (jq) {
            return jq.each(function () {
                isEnableDnd = true;
            });
        },
        dragMinLevel: function (jq, minLevel) {
            level = minLevel;
        }
    };

    $.fn.etreegrid.defaults = $.extend({}, $.fn.treegrid.defaults, {
        singleSelect: true,
        editing: true,
        editId: undefined
    });

    $.parser.plugins.push('etreegrid');
})(jQuery);