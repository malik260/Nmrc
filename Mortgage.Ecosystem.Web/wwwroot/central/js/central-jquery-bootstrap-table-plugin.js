; (function ($) {
    "use strict";
    $.fn.cnTable = function (option, param) {
        //if the method is called
        if (typeof option == 'string') {
            return $.fn.cnTable.methods[option](this, param);
        }

        //If it is initializing the component
        var _option = $.extend({}, $.fn.cnTable.defaults, option || {});
        var target = $(this);
        target.bootstrapTable(_option);
        return target;
    };

    $.fn.cnTable.methods = {
        search: function (target) {
            // start from the first page
            target.bootstrapTable('refresh', { pageNumber: 1 });
        },
        getPagination: function (target, params) {
            var pagination = {
                pageSize: params.limit, //page size
                pageIndex: (params.offset / params.limit) + 1, //page number
                sort: params.sort, //sort column name
                sortType: params.order //Ranking order (desc, asc)
            };
            return pagination;
        }
    };

    $.fn.cnTable.defaults = {
        method: 'GET', // request method (*)
        toolbar: '#toolbar', // which container to use for tool buttons
        striped: true, // whether to display line spacing color
        cache: false, // Whether to use the cache, the default is true, so in general, you need to set this property (*)
        pagination: true, // whether to display pagination (*)
        sortable: true, // whether to enable sorting
        sortStable: true, // set to true to get a stable sort
        sortName: 'Id', // sort column name
        sortOrder: "desc", // sort order
        sidePagination: "server", // Paging method: client paging, server server paging (*)
        pageNumber: 1, // Initially load the first page, the default first page, and record
        pageSize: 10, // The number of record rows per page (*)
        pageList: "10, 25, 50, 100", // number of rows per page to choose from (*)
        search: false, // whether to display table search
        strictSearch: true,
        showColumns: true, // Whether to show all columns (select the displayed columns)
        showRefresh: true, // whether to show the refresh button
        showToggle: true, // Whether to show the toggle buttons of the detail view and list view
        minimumCountColumns: 2, // minimum allowed number of columns
        clickToSelect: true, // Whether to enable click to select rows
        height: undefined, // The row height, if the height property is not set, the table will automatically feel the table height according to the number of records
        uniqueId: "Id", // The unique identifier of each row, generally the primary key column
        cardView: false, // whether to show the detail view
        detailView: false, // Whether to display the parent and child table
        totalField: 'Total',
        dataField: 'Data',
        columns: [],
        queryParams: {},
        onLoadSuccess: function (obj) {
            if (obj) {
                if (obj.Tag != 1) {
                    cn.alertError(obj.Message);
                }
            }
        },
        onLoadError: function (status, s) {
            if (s.statusText != "abort") {
                cn.alertError("Data load failed!");
            }
        }
    };
   
})(window.jQuery);