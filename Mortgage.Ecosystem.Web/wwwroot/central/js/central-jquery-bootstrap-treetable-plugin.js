(function ($) {
    "use strict";
    $.fn.cnTreeTable = function (option, param) {
        //if the method is called
        if (typeof option == 'string') {
            return $.fn.cnTreeTable.methods[option](this, param);
        }

        //If it is initializing the component
        var _option = $.extend({}, $.fn.cnTreeTable.defaults, option || {});
        var target = $(this);
        target.bootstrapTreeTable(_option);
        return target;
    };

    $.fn.cnTreeTable.methods = {
        search: function (target, param) {
            return target.bootstrapTreeTable('refresh', param);
        },
        getRowById: function (target, id) {
            var tree = target.data('bootstrap.tree.table');
            for (var row in tree.data_obj) {
                if (tree.data_obj[row]) {
                    if (tree.data_obj[row].Id == id) {
                        return tree.data_obj[row];
                    }
                }
            }
        },
        expandRowById: function (target, id) {
            return target.bootstrapTreeTable('expandRow', id);
        }
    };

    $.fn.cnTreeTable.defaults = {
        method: 'GET',
        code: "Id",
        parentCode: "Parent",
        bordered: true,
        expandColumn: '1',
        expandAll: false,
        expandFirst: true
    };
})(jQuery);