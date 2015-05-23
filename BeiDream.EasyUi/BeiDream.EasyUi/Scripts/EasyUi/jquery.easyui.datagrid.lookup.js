//查找带回
(function ($) {
    $.fn.lookup = function (options, param) {
        if (typeof options == "string") {
            var methods = $.fn.lookup.methods[options];
            if (methods) {
                return methods(this, param);
            } else {
                return this.combo(options, param);
            }
        }
        return this.each(function () {
            initOptions();
            var control = $(this);
            createCombo();

            //初始化参数
            function initOptions() {
                options = $.extend({
                    title: '查找带回',
                    onShowPanel: showDialog,
                    closeCallback: removeControl
                }, options || {});

                //弹出窗口
                function showDialog() {
                    $.easyui.addItem("lookups", control);
                    control.combo('hidePanel');
                    $.easyui.dialog(options);
                };

                //移除控件
                function removeControl() {
                    var item = $.easyui.getItem("lookups");
                    if (item === control)
                        $.easyui.getArray("lookups").pop();
                }
            }

            //创建组合控件
            function createCombo() {
                control.combo(options);
                control.combo('textbox').unbind("keydown");
                control.combo('textbox').blur(function() {
                    control.lookup("setValue", control.combo('textbox').val());
                });
                control.data('combo').combo.addClass("combo-lookup");
            }
        });
    };

    $.fn.lookup.methods = {
        //设置值
        setValue: function (target, value) {
            return target.each(function () {
                if (!value)
                    return;
                var control = $(this);
                control.combo('setValue', value);
                control.combo('setText', value);
            });
        }
    };
})(jQuery);