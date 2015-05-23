(function ($) {
    var currTarget;
    $(function () {
        $(document).unbind('.edatagrid').bind('mousedown.edatagrid', function (e) {
            var p = $(e.target).closest('div.datagrid-view,div.combo-panel');
            if (p.length) {
                if (p.hasClass('datagrid-view')) {
                    var dg = p.children('table');
                    if (dg.length && currTarget != dg[0]) {
                        _save();
                    }
                }
                return;
            }
            _save();

            function _save() {
                var dg = $(currTarget);
                if (dg.length) {
                    dg.edatagrid('saveRow');
                    currTarget = undefined;
                }
            }
        });
    });

    function buildGrid(target) {
        var opts = $.data(target, 'edatagrid').options;
        $(target).datagrid($.extend({}, opts, {
            onDblClickCell: function (index, field, value) {
                if (opts.editing) {
                    $(this).edatagrid('editRow', index);
                    focusEditor(target, field);
                }
                if (opts.onDblClickCell) {
                    opts.onDblClickCell.call(target, index, field, value);
                }
            },
            onClickCell: function (index, field, value) {
                if (opts.editing && opts.editIndex >= 0) {
                    $(this).edatagrid('editRow', index);
                    focusEditor(target, field);
                }
                if (opts.onClickCell) {
                    opts.onClickCell.call(target, index, field, value);
                }
            },
            onAfterEdit: function (index, row) {
                opts.editIndex = -1;
                opts.onSave.call(target, index, row);
                if (opts.onAfterEdit) opts.onAfterEdit.call(target, index, row);
            },
            onCancelEdit: function (index, row) {
                opts.editIndex = -1;
                if (row.isNewRecord) {
                    $(this).datagrid('deleteRow', index);
                }
                if (opts.onCancelEdit) opts.onCancelEdit.call(target, index, row);
            },
            onBeforeLoad: function (param) {
                if (opts.onBeforeLoad.call(target, param) == false) { return false }
                $(this).edatagrid('cancelRow');
                return true;
            }
        }));
    }

    function focusEditor(target, field) {
        var opts = $(target).edatagrid('options');
        var t;
        var editor = $(target).datagrid('getEditor', { index: opts.editIndex, field: field });
        if (editor) {
            t = editor.target;
        } else {
            var editors = $(target).datagrid('getEditors', opts.editIndex);
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

    function validateRow(grid, index) {
        if (grid.datagrid('validateRow', index))
            return true;
        grid.datagrid('selectRow', index);
        focusEditor(grid);
        return false;
    }

    $.fn.edatagrid = function (options, param) {
        if (typeof options == 'string') {
            var method = $.fn.edatagrid.methods[options];
            if (method) {
                return method(this, param);
            } else {
                return this.datagrid(options, param);
            }
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'edatagrid');
            if (state) {
                $.extend(state.options, options);
            } else {
                $.data(this, 'edatagrid', {
                    options: $.extend({}, $.fn.edatagrid.defaults, $.fn.edatagrid.parseOptions(this), options)
                });
            }
            buildGrid(this);
        });
    };

    $.fn.edatagrid.parseOptions = function (target) {
        return $.extend({}, $.fn.datagrid.parseOptions(target), {
        });
    };

    $.fn.edatagrid.methods = {
        options: function (jq) {
            var opts = $.data(jq[0], 'edatagrid').options;
            return opts;
        },
        loadData: function (jq, data) {
            return jq.each(function () {
                $(this).edatagrid('cancelRow');
                $(this).datagrid('loadData', data);
            });
        },
        enableEditing: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'edatagrid').options;
                opts.editing = true;
            });
        },
        disableEditing: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'edatagrid').options;
                opts.editing = false;
            });
        },
        editRow: function (jq, index) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'edatagrid').options;
                var editIndex = opts.editIndex;
                if (editIndex != index) {
                    if (dg.datagrid('validateRow', editIndex)) {
                        if (editIndex >= 0) {
                            if (opts.onBeforeSave.call(this, editIndex) == false) {
                                setTimeout(function () {
                                    dg.datagrid('selectRow', editIndex);
                                }, 0);
                                return;
                            }
                        }
                        dg.datagrid('endEdit', editIndex);
                        dg.datagrid('beginEdit', index);
                        opts.editIndex = index;
                        focusEditor(this);
                        currTarget = this;
                        var rows = dg.datagrid('getRows');
                        opts.onEdit.call(this, index, rows[index]);
                    } else {
                        setTimeout(function () {
                            dg.datagrid('selectRow', editIndex);
                        }, 0);
                    }
                }
            });
        },
        addRow: function (jq, index) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'edatagrid').options;
                if (opts.editIndex >= 0) {
                    if (!dg.datagrid('validateRow', opts.editIndex)) {
                        dg.datagrid('selectRow', opts.editIndex);
                        return;
                    }
                    if (opts.onBeforeSave.call(this, opts.editIndex) == false) {
                        setTimeout(function () {
                            dg.datagrid('selectRow', opts.editIndex);
                        }, 0);
                        return;
                    }
                    dg.datagrid('endEdit', opts.editIndex);
                }
                var rows = dg.datagrid('getRows');
                if (typeof index == 'object') {
                    add(index.index, $.extend(index.row, { isNewRecord: true }));
                } else {
                    add(index, { isNewRecord: true });
                }

                dg.datagrid('beginEdit', opts.editIndex);
                dg.datagrid('selectRow', opts.editIndex);
                focusEditor(this);
                currTarget = this;

                function add(index, row) {
                    if (index == undefined) {
                        dg.datagrid('appendRow', row);
                        opts.editIndex = rows.length - 1;
                    } else {
                        dg.datagrid('insertRow', { index: index, row: row });
                        opts.editIndex = index;
                    }
                }

                opts.onAdd.call(this, opts.editIndex, rows[opts.editIndex]);
            });
        },
        validateRow: function (jq) {
            var dg = jq.eq(0);
            var opts = $.data(jq[0], 'edatagrid').options;
            if (opts.editIndex < 0)
                return true;
            if (!validateRow(dg, opts.editIndex))
                return false;
            return true;
        },
        saveRow: function (jq) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'edatagrid').options;
                if (opts.editIndex < 0)
                    return;
                if (!validateRow(dg, opts.editIndex))
                    return;
                if (opts.onBeforeSave.call(this, opts.editIndex) == false) {
                    setTimeout(function () {
                        dg.datagrid('selectRow', opts.editIndex);
                    }, 0);
                    return;
                }
                $(this).datagrid('endEdit', opts.editIndex);
            });
        },
        cancelRow: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, 'edatagrid').options;
                if (opts.editIndex >= 0) {
                    $(this).datagrid('cancelEdit', opts.editIndex);
                }
            });
        },
        destroyRow: function (jq, index) {
            return jq.each(function () {
                var dg = $(this);
                var opts = $.data(this, 'edatagrid').options;

                var rows = [];
                if (index == undefined) {
                    rows = dg.datagrid('getSelections');
                } else {
                    var rowIndexes = $.isArray(index) ? index : [index];
                    for (var i = 0; i < rowIndexes.length; i++) {
                        var row = opts.finder.getRow(this, rowIndexes[i]);
                        if (row) {
                            rows.push(row);
                        }
                    }
                }

                if (!rows.length) {
                    return;
                }

                for (var i = 0; i < rows.length; i++) {
                    _del(rows[i]);
                }
                dg.datagrid('clearSelections');

                function _del(row) {
                    var index = dg.datagrid('getRowIndex', row);
                    if (index == -1) { return }
                    if (row.isNewRecord) {
                        dg.datagrid('beginEdit', index);
                        dg.datagrid('cancelEdit', index);
                    } else {
                        dg.datagrid('cancelEdit', index);
                        dg.datagrid('deleteRow', index);
                        opts.onDestroy.call(dg[0], index, row);
                    }
                }
            });
        }
    };

    $.fn.edatagrid.defaults = $.extend({}, $.fn.datagrid.defaults, {
        singleSelect: true,
        editing: true,
        editIndex: -1,

        autoSave: false,	// auto save the editing row when click out of datagrid
        url: null,	// return the datagrid data



        onAdd: function (index, row) { },
        onEdit: function (index, row) { },
        onBeforeSave: function (index) { },
        onSave: function (index, row) { },
        onDestroy: function (index, row) { },
        onError: function (index, row) { }
    });

    $.parser.plugins.push('edatagrid');
})(jQuery);