(function ($) {
    //折叠布局面板时显示标题
    $.extend($.fn.layout.paneldefaults, {
        onBeforeCollapse: function () {
            var $this = $(this);
            var panel = $this.panel('options');
            var region = panel.region;
            var title = getTitle();
            var layoutExpand = $('.layout-button-' + convert(region)).closest('.layout-expand');
            if (region == "east" || region == "west") {
                title = title.split('').join('<br/>');
                format(layoutExpand.find('.panel-body').html(title));
            } else {
                format(layoutExpand.find('.panel-title').html(title));
            }
            //转换区域
            function convert(r) {
                switch (r) {
                    case 'north':
                        return 'down';
                    case 'south':
                        return 'up';
                    case 'east':
                        return 'left';
                    case 'west':
                        return 'right';
                }
                return false;
            }
            //获取折叠标题
            function getTitle() {
                var result = $this.attr("CollapseTitle");
                if (result)
                    return result;
                return panel.title;
            }
            //格式化
            function format(node) {
                node.css({
                    textAlign: getPosition(), lineHeight: '18px', fontWeight: 'bold'
                });
            }
            //获取折叠标题位置
            function getPosition() {
                var result = $this.attr("CollapseTitlePosition");
                if (result)
                    return result;
                return 'center';
            }
        }
    });
    //日期控件格式化
    $.fn.datebox.defaults.formatter = function (date) {
        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
    };
    $.fn.datebox.defaults.parser = function (s) {
        if (!s) return new Date();
        var ss;
        if (s.indexOf("-") > -1) {
            ss = s.split('-');
        } else {
            ss = s.split('/');
        }
        var y = parseInt(ss[0], 10);
        var m = parseInt(ss[1], 10);
        var d = parseInt(ss[2], 10);
        if (!isNaN(y) && !isNaN(m) && !isNaN(d))
            return new Date(y, m - 1, d);
        return new Date();
    };
    //扩展最小长度验证
    $.extend($.fn.validatebox.defaults.rules, {
        minLength: {
            validator: function (value, param) {
                return value.length >= param[0];
            },
            message: '该项的最小长度为{0}位'
        }
    });
    //扩展最大长度验证
    $.extend($.fn.validatebox.defaults.rules, {
        maxLength: {
            validator: function (value, param) {
                return value.length <= param[0];
            },
            message: '该项的最大长度为{0}位'
        }
    });
    //扩展远程验证,解决自定义消息的问题
    $.extend($.fn.validatebox.defaults.rules.remote, {
        validator: function (value, param) {
            var data = {};
            data[param[1]] = value;
            var result = $.ajax({ url: param[0], dataType: "json", data: data, async: false, cache: false, type: "post" }).responseText;
            $.fn.validatebox.defaults.rules.remote.message = result;
            return result == "true";
        }
    });
    //扩展相等验证
    $.extend($.fn.validatebox.defaults.rules, {
        equalTo: {
            validator: function (value, param) {
                if (param[1])
                    $.fn.validatebox.defaults.rules.equalTo.message = param[1];
                return value == $("#" + param[0]).val();
            },
            message: "您的两次输入不一致"
        }
    });
    //扩展最大值验证
    $.extend($.fn.validatebox.defaults.rules, {
        max: {
            validator: function (value, param) {
                if (param[1])
                    $.fn.validatebox.defaults.rules.max.message = param[1];
                return $.toNumber(value) <= $.toNumber(param[0]);
            },
            message: "该项不能超过{0}"
        }
    });
    //扩展最小值验证
    $.extend($.fn.validatebox.defaults.rules, {
        min: {
            validator: function (value, param) {
                if (param[1])
                    $.fn.validatebox.defaults.rules.min.message = param[1];
                return $.toNumber(value) >= $.toNumber(param[0]);
            },
            message: "该项不能小于{0}"
        }
    });
    //扩展数值范围验证
    $.extend($.fn.validatebox.defaults.rules, {
        range: {
            validator: function (value, param) {
                if (param[2])
                    $.fn.validatebox.defaults.rules.range.message = param[2];
                return $.toNumber(value) >= $.toNumber(param[0]) && $.toNumber(value) <= $.toNumber(param[1]);
            },
            message: "请输入{0}-{1}之间的数值"
        }
    });
    //扩展手机号验证
    $.extend($.fn.validatebox.defaults.rules, {
        mobilePhone: {
            validator: function (value, param) {
                var pattern = /^1[3|4|5|7|8|][0-9]{9}$/;
                return pattern.test(value);
            },
            message: "请输入有效的手机号"
        }
    });
    //扩展datagrid的查找带回
    $.extend($.fn.datagrid.defaults.editors, {
        lookup: {
            init: function (container, options) {
                var input = $('<input type="text" class="datagrid-editable-input"/>').appendTo(container);
                return input.lookup(options);
            },
            destroy: function (target) {
                $(target).lookup('destroy');
            },
            getValue: function (target) {
                return $(target).lookup('getValue');
            },
            setValue: function (target, value) {
                $(target).lookup('setValue', value);
            },
            resize: function (target, width) {
                $(target).lookup('resize', width);
            }
        }
    });
})(jQuery);

