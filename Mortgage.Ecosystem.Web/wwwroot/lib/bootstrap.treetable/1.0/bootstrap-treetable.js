/**
 * bootstrapTreeTable
 *
 * @author swifly YiShaAdmin
 */
(function ($) {
    "use strict";

    $.fn.bootstrapTreeTable = function (options, param) {
        var target = $(this).data('bootstrap.tree.table');
        target = target ? target : $(this);
        // if calling method
        if (typeof options == 'string') {
            return $.fn.bootstrapTreeTable.methods[options](target, param);
        }
        // If it is an initialized component
        options = $.extend({}, $.fn.bootstrapTreeTable.defaults, options || {});
        target.hasSelectItem = false;// Whether there is radio or checkbox
        target.data_list = null; // Data for cache format-according to the father group
        target.data_obj = null; // Data for the formatting of cache-according to the ID of the ID
        target.hiddenColumns = []; // Used to store the hidden Field
        target.lastAjaxParams; // The parameters of the user's last request
        target.isFixWidth = false; // Whether there is a fixed width
        // initialization
        var init = function () {
            // Initialize the container
            initContainer();
            // init toolbar
            initToolbar();
            // Initialize header
            initHeader();
            // initialize table body
            initBody();
            // Initialize the data service
            initServer();
            // dynamically set header width
            autoTheadWidth(true);
            // Cache the target object
            target.data('bootstrap.tree.table', target);
        }
        // Initialize the container
        var initContainer = function () {
            // Wrap the div in the outer layer, the bootstrap-table style is used
            var $main_div = $("<div class='bootstrap-tree-table'></div>");
            var $treetable = $("<div class='treetable-table'></div>");
            target.before($main_div);
            $main_div.append($treetable);
            $treetable.append(target);
            target.addClass("table");
            if (options.striped) {
                target.addClass('table-striped');
            }
            if (options.bordered) {
                target.addClass('table-bordered');
            }
            if (options.hover) {
                target.addClass('table-hover');
            }
            if (options.condensed) {
                target.addClass('table-condensed');
            }
            target.html("");
        }
        // Initialize toolbar
        var initToolbar = function () {
            var $toolbar = $("<div class='treetable-bars'></div>");
            if (options.toolbar) {
                $(options.toolbar).addClass('tool-left');
                $toolbar.append($(options.toolbar));
            }
            var $rightToolbar = $('<div class="btn-group tool-right">');
            $toolbar.append($rightToolbar);
            target.parent().before($toolbar);
            // Whether to display the refresh button
            if (options.showRefresh) {
                var $refreshBtn = $('<button class="btn btn-default btn-outline" type="button" aria-label="refresh" title="Refresh"><i class="glyphicon glyphicon-repeat"></i></button>');
                $rightToolbar.append($refreshBtn);
                registerRefreshBtnClickEvent($refreshBtn);
            }
            // Whether to display column options
            if (options.showColumns) {
                var $columns_div = $('<div class="btn-group pull-right" title="List"><button type="button" aria-label="columns" class="btn btn-default btn-outline dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><i class="glyphicon glyphicon-list"></i> <span class="caret"></span></button></div>');
                var $columns_ul = $('<ul class="dropdown-menu columns" role="menu"></ul>');
                $.each(options.columns, function (i, column) {
                    if (column.field != 'selectItem') {
                        var _li = null;
                        if (typeof column.visible == "undefined" || column.visible == true) {
                            _li = $('<li role="menuitem"><label><input type="checkbox" checked="checked" data-field="' + column.field + '" value="' + column.field + '" > ' + column.title + '</label></li>');
                        } else {
                            _li = $('<li role="menuitem"><label><input type="checkbox" data-field="' + column.field + '" value="' + column.field + '" > ' + column.title + '</label></li>');
                            target.hiddenColumns.push(column.field);
                        }
                        $columns_ul.append(_li);
                    }
                });
                $columns_div.append($columns_ul);
                $rightToolbar.append($columns_div);
                // register column options event
                registerColumnClickEvent();
            } else {
                $.each(options.columns, function (i, column) {
                    if (column.field != 'selectItem') {
                        if (!(typeof column.visible == "undefined" || column.visible == true)) {
                            target.hiddenColumns.push(column.field);
                        }
                    }
                });
            }
        }
        // Initialize hidden columns
        var initHiddenColumns = function () {
            $.each(target.hiddenColumns, function (i, field) {
                target.find("." + field + "_cls").hide();
            });
        }
        // Initialize header
        var initHeader = function () {
            var $thr = $('<tr></tr>');
            $.each(options.columns, function (i, column) {
                var $th = null;
                // Determine whether there is a selected column
                if (i == 0 && column.field == 'selectItem') {
                    target.hasSelectItem = true;
                    $th = $('<th style="width:36px;"></th>');
                } else {
                    $th = $('<th style="' + ((column.width) ? ('width:' + column.width) : '') + '" class="' + column.field + '_cls"></th>');
                }
                if ((!target.isFixWidth) && column.width) {
                    target.isFixWidth = column.width.indexOf("px") > -1 ? true : false;
                }
                $th.text(column.title);
                $thr.append($th);
            });
            var $thead = $('<thead class="treetable-thead"></thead>');
            $thead.append($thr);
            target.append($thead);
        }
        // initialize table body
        var initBody = function () {
            var $tbody = $('<tbody class="treetable-tbody"></tbody>');
            target.append($tbody);
            // 默认高度
            if (options.height) {
                $tbody.css("height", options.height);
            }
        }
        // Initialize the data service
        var initServer = function (parms) {
            // Clear before loading data
            target.data_list = {};
            target.data_obj = {};
            var $tbody = target.find("tbody");
            // Add loading loading
            var $loading = '<tr><td colspan="' + options.columns.length + '"><div style="display: block;text-align: center;">Strive in the data, please wait ...</div></td></tr>'
            $tbody.html($loading);
            if (options.url) {
                $.ajax({
                    type: options.method,
                    url: options.url,
                    data: parms ? parms : options.ajaxParams,
                    dataType: "JSON",
                    success: function (data, textStatus, jqXHR) {
                        var dataJson = data;
                        if (data.Data) {
                            dataJson = data.Data;
                        }
                        renderTable(dataJson);
                    },
                    error: function (xhr, textStatus) {
                        var _errorMsg = '<tr><td colspan="' + options.columns.length + '"><div style="display: block;text-align: center;">' + xhr.responseText + '</div></td></tr>'
                        $tbody.html(_errorMsg);
                    },
                });
            } else {
                renderTable(options.data);
            }
        }
        // Render the table after loading the data
        var renderTable = function (data) {
            var $tbody = target.find("tbody");
            // clear first
            $tbody.html("");
            if (!data || data.length <= 0) {
                var _empty = '<tr><td colspan="' + options.columns.length + '"><div style="display: block;text-align: center;">No record of matching</div></td></tr>'
                $tbody.html(_empty);
                return;
            }
            // cache and format data
            formatData(data);
            // get all root nodes
            var rootNode = target.data_list["_root_"];
            // start drawing
            if (rootNode) {
                $.each(rootNode, function (i, item) {
                    var _child_row_id = "row_id_" + i;
                    recursionNode(item, 1, _child_row_id, "row_root");
                });
            }
            // The following operations are mainly for displaying some nodes without root nodes when querying
            $.each(data, function (i, item) {
                var _defaultRootFlag = target.getRootFlag(item);
                if (_defaultRootFlag) {
                    if (!item.isShow) {
                        var tr = renderRow(item, false, 1, "", "");
                        $tbody.append(tr);
                    }
                }
            });
            target.append($tbody);
            registerExpanderEvent();
            registerRowClickEvent();
            initHiddenColumns();
            if ($.isFunction(options.onLoadSuccess)) {
                options.onLoadSuccess();
            }
            // dynamically set header width
            autoTheadWidth();
        }
        // dynamically set header width
        var autoTheadWidth = function (initFlag) {
            if (options.height > 0) {
                var $thead = target.find("thead");
                var $tbody = target.find("tbody");
                var borderWidth = parseInt(target.css("border-left-width")) + parseInt(target.css("border-right-width"))

                $thead.css("width", $tbody.children(":first").width());
                if (initFlag) {
                    var resizeWaiter = false;
                    $(window).resize(function () {
                        if (!resizeWaiter) {
                            resizeWaiter = true;
                            setTimeout(function () {
                                if (!target.isFixWidth) {
                                    $tbody.css("width", target.parent().width() - borderWidth);
                                }
                                $thead.css("width", $tbody.children(":first").width());
                                resizeWaiter = false;
                            }, 300);
                        }
                    });
                }
            }

        }
        // cache and format data
        var formatData = function (data) {
            if (!target.data_list["_root_"]) {
                target.data_list["_root_"] = [];
            }
            var tempRoot = [];  // The root directory is sorted according to the original order of the data
            var _root = options.rootIdValue ? options.rootIdValue : null
            $.each(data, function (index, item) {
                // Add a default attribute to determine whether the current node is displayed
                item.isShow = false;
                // This is compatible with several common Root node writing methods
                // Several default judgments
                var _defaultRootFlag = target.getRootFlag(item);
                if (!item[options.parentCode] || (_root ? (item[options.parentCode] == options.rootIdValue) : _defaultRootFlag)) {
                    if (!target.data_obj["id_" + item[options.code]]) {
                        tempRoot.push({ 'index': index, 'node': item })
                    }
                } else {
                    var rootNode = recursionQueryRootNode(data, 0, item);
                    if (rootNode && rootNode.node) {
                        if (!target.data_obj["id_" + rootNode.node[options.code]]) {
                            target.data_obj["id_" + rootNode.node[options.code]] = rootNode.node

                            tempRoot.push(rootNode)
                        }
                    }
                    if (!target.data_list["_n_" + item[options.parentCode]]) {
                        target.data_list["_n_" + item[options.parentCode]] = [];
                    }
                    if (!target.data_obj["id_" + item[options.code]]) {
                        target.data_list["_n_" + item[options.parentCode]].push(item);
                    }
                }
                if (!target.data_obj["id_" + item[options.code]]) {
                    target.data_obj["id_" + item[options.code]] = item;
                }
            });
            tempRoot = tempRoot.sort(function (a, b) {
                return a.index - b.index
            });
            $.each(tempRoot, function (index, item) {
                target.data_list["_root_"].push(item.node);
            })
        }
        // Recursively get the root node of the node
        var recursionQueryRootNode = function (data, index, node) {
            for (let i = 0; i < data.length; i++) {
                if (data[i][options.code] == node[options.parentCode]) {
                    return recursionQueryRootNode(data, i, data[i]);
                }
                if (i == data.length - 1) {
                    return { 'index': index, 'node': node };
                }
            }
        }
        // Recursively get child nodes and set child nodes
        var recursionNode = function (parentNode, lv, row_id, p_id) {
            var $tbody = target.find("tbody");
            var _ls = target.data_list["_n_" + parentNode[options.code]];
            var $tr = renderRow(parentNode, _ls ? true : false, lv, row_id, p_id);
            $tbody.append($tr);
            if (_ls) {
                if (options.expandAll) {
                    $.each(_ls, function (i, item) {
                        var _child_row_id = row_id + "_" + i;
                        recursionNode(item, (lv + 1), _child_row_id, row_id);
                    });
                }
                else if (options.expandFirst) {
                    if (lv == 1) {
                        $.each(_ls, function (i, item) {
                            var _child_row_id = row_id + "_" + i;
                            recursionNode(item, (lv + 1), _child_row_id, row_id);
                        });
                    }
                }
            }
        };
        // draw the line
        var renderRow = function (item, isP, lv, row_id, p_id, isShow) {
            // marker is displayed
            item.isShow = true;
            item.row_id = row_id;
            item.p_id = p_id;
            item.lv = lv;
            var $tr = $('<tr id="' + row_id + '" pid="' + p_id + '"></tr>');
            var _icon = options.expanderCollapsedClass;
            if (options.expandAll) {
                $tr.css("display", "table");
                _icon = options.expanderExpandedClass;
            } else if (isShow) {
                $tr.css("display", "table");
                _icon = options.expanderCollapsedClass;
            }
            else if (lv == 1) {
                $tr.css("display", "table");
                _icon = (options.expandFirst) ? options.expanderExpandedClass : options.expanderCollapsedClass;
            } else if (lv == 2) {
                if (options.expandFirst) {
                    $tr.css("display", "table");
                } else {
                    $tr.css("display", "none");
                }
                _icon = options.expanderCollapsedClass;
            } else {
                $tr.css("display", "none");
                _icon = options.expanderCollapsedClass;
            }
            $.each(options.columns, function (index, column) {
                // Determine whether there is a selected column
                if (column.field == 'selectItem') {
                    target.hasSelectItem = true;
                    var $td = $('<td style="text-align:center;width:36px;"></td>');
                    if (column.radio) {
                        var _ipt = $('<input name="select_item" type="radio" value="' + item[options.code] + '"></input>');
                        $td.append(_ipt);
                    }
                    if (column.checkbox) {
                        var _ipt = $('<input name="select_item" type="checkbox" value="' + item[options.code] + '"></input>');
                        $td.append(_ipt);
                    }
                    $tr.append($td);
                } else {
                    var $td = $('<td name="' + column.field + '" class="' + column.field + '_cls"></td>');
                    if (column.width) {
                        $td.css("width", column.width);
                    }
                    if (column.align) {
                        $td.css("text-align", column.align);
                    }
                    if (options.expandColumn == index) {
                        $td.css("text-align", "left");
                    }
                    if (column.valign) {
                        $td.css("vertical-align", column.valign);
                    }
                    if (options.showTitle) {
                        $td.addClass("ellipsis");
                    }
                    // Add formatter rendering
                    if (column.formatter) {
                        $td.html(column.formatter.call(this, item[column.field], item, index));
                    } else {
                        if (options.showTitle) {
                            // Only add the title attribute if the field does not have a formatter
                            $td.attr("title", item[column.field]);
                        }
                        $td.text(item[column.field]);
                    }
                    if (options.expandColumn == index) {
                        if (!isP) {
                            $td.prepend('<span class="treetable-expander"></span>')
                        } else {
                            $td.prepend('<span class="treetable-expander ' + _icon + '"></span>')
                        }
                        for (var int = 0; int < (lv - 1); int++) {
                            $td.prepend('<span class="treetable-indent"></span>')
                        }
                    }
                    $tr.append($td);
                }
            });
            return $tr;
        }
        // Register refresh button click event
        var registerRefreshBtnClickEvent = function (btn) {
            $(btn).off('click').on('click', function () {
                target.refresh();
            });
        }
        // register column options event
        var registerColumnClickEvent = function () {
            $(".bootstrap-tree-table .treetable-bars .columns label input").off('click').on('click', function () {
                var $this = $(this);
                if ($this.prop('checked')) {
                    target.showColumn($(this).val());
                } else {
                    target.hideColumn($(this).val());
                }
            });
        }
        // register row click event
        var registerRowClickEvent = function () {
            target.find("tbody").find("tr").unbind();
            target.find("tbody").find("tr").click(function () {
                target.rowClickHandler(this);
            });
        }
        // Register the small icon click event -- expand and shrink
        var registerExpanderEvent = function () {
            target.find("tbody").find("tr").find(".treetable-expander").unbind();
            target.find("tbody").find("tr").find(".treetable-expander").click(function () {
                target.rowExpandHandler(this);
            });
        }
        target.getRootFlag = function (item) {
            var _defaultRootFlag = item[options.parentCode] == '0' ||
                item[options.parentCode] == 0 ||
                item[options.parentCode] == null ||
                item[options.parentCode] == '';
            return _defaultRootFlag;
        }
        // row click event
        target.rowClickHandler = function (tr) {
            if (target.hasSelectItem) {
                var _ipt = $(tr).find("input[name='select_item']");
                if (_ipt.attr("type") == "radio") {
                    _ipt.prop('checked', true);
                    target.find("tbody").find("tr").removeClass("treetable-selected");
                    $(tr).addClass("treetable-selected");
                } else {
                    if (_ipt.prop('checked')) {
                        _ipt.prop('checked', false);
                        $(tr).removeClass("treetable-selected");
                    } else {
                        _ipt.prop('checked', true);
                        $(tr).addClass("treetable-selected");
                    }
                }
            }
        }
        // small icon click event
        target.rowExpandHandler = function (span) {
            var _isExpanded = $(span).hasClass(options.expanderExpandedClass);
            var _isCollapsed = $(span).hasClass(options.expanderCollapsedClass);
            if (_isExpanded || _isCollapsed) {
                var tr = $(span).parent().parent();
                var row_id = tr.attr("id");
                var _ls = target.find("tbody").find("tr[id^='" + row_id + "_']"); //下所有
                if (_isExpanded) {
                    $(span).removeClass(options.expanderExpandedClass);
                    $(span).addClass(options.expanderCollapsedClass);
                    if (_ls && _ls.length > 0) {
                        $.each(_ls, function (index, item) {
                            $(item).css("display", "none");
                        });
                    }
                } else {
                    $(span).removeClass(options.expanderCollapsedClass);
                    $(span).addClass(options.expanderExpandedClass);
                    if (_ls && _ls.length == 0) {
                        var _key = tr.find("input[type='radio']").val();
                        var lv = row_id.replace("row_id_", "").split('_').length;
                        var _children = target.data_list["_n_" + _key];
                        var lastRow = null;
                        $.each(_children, function (i, item) {
                            var _child_row_id = row_id + "_" + i;
                            var hasChildren = target.data_list["_n_" + item[options.code]] ? true : false;
                            var $tr = renderRow(item, hasChildren, lv + 1, _child_row_id, row_id, true);
                            if (lastRow == null) {
                                tr.after($tr);
                            }
                            else {
                                if (i > 0) {
                                    lastRow = $("#" + row_id + "_" + (i - 1).toString());
                                }
                                lastRow.after($tr);
                            }
                            var childRow = $("#" + _child_row_id);
                            childRow.click(function () {
                                target.rowClickHandler(this);
                            });
                            childRow.find(".treetable-expander").click(function () {
                                target.rowExpandHandler(this);
                            });
                            if (lastRow == null) {
                                lastRow = childRow;
                            }
                        });
                    } else {
                        if (_ls && _ls.length > 0) {
                            $.each(_ls, function (index, item) {
                                // 父icon
                                var _p_icon = $("#" + $(item).attr("pid")).children().eq(options.expandColumn).find(".treetable-expander");
                                if (_p_icon.hasClass(options.expanderExpandedClass)) {
                                    $(item).css("display", "table");
                                }
                            });
                        }
                    }
                }
            }
        }
        // Refresh data
        target.refresh = function (parms) {
            if (parms) {
                target.lastAjaxParams = parms;
            }
            initServer(target.lastAjaxParams);
        }
        // add data refresh table
        target.appendData = function (data) {
            // The following operations are mainly for displaying some nodes without root nodes when querying
            $.each(data, function (i, item) {
                var _data = target.data_obj["id_" + item[options.code]];
                var _p_data = target.data_obj["id_" + item[options.parentCode]];
                var _c_list = target.data_list["_n_" + item[options.parentCode]];
                var row_id = ""; // line ID
                var p_id = ""; // Father Xing ID
                var _lv = 1; // If there is no father, it is 1 default display
                var tr; // To add a line object
                if (_data && _data.row_id && _data.row_id != "") {
                    row_id = _data.row_id; // If it already exists, directly refer to the original
                }
                if (_p_data) {
                    p_id = _p_data.row_id;
                    if (row_id == "") {
                        var _tmp = 0
                        if (_c_list && _c_list.length > 0) {
                            _tmp = _c_list.length;
                        }
                        row_id = _p_data.row_id + "_" + _tmp;
                    }
                    _lv = _p_data.lv + 1; // If you have a father
                    // draw the line
                    tr = renderRow(item, false, _lv, row_id, p_id);

                    var _p_icon = $("#" + _p_data.row_id).children().eq(options.expandColumn).find(".treetable-expander");
                    var _isExpanded = _p_icon.hasClass(options.expanderExpandedClass);
                    var _isCollapsed = _p_icon.hasClass(options.expanderCollapsedClass);
                    // Does the parent node have expand and contract buttons?
                    if (_isExpanded || _isCollapsed) {
                        // The expanded state of the parent node displays new rows
                        if (_isExpanded) {
                            tr.css("display", "table");
                        }
                    } else {
                        // If the parent node does not have an expand and contract button, add it
                        _p_icon.addClass(options.expanderCollapsedClass);
                    }

                    if (_data) {
                        $("#" + _data.row_id).before(tr);
                        $("#" + _data.row_id).remove();
                    } else {
                        // Compute parent's sibling next row
                        var _tmp_ls = _p_data.row_id.split("_");
                        var _p_next = _p_data.row_id.substring(0, _p_data.row_id.length - 1) + (parseInt(_tmp_ls[_tmp_ls.length - 1]) + 1);
                        // draw on
                        $("#" + _p_next).before(tr);
                    }
                } else {
                    tr = renderRow(item, false, _lv, row_id, p_id);
                    if (_data) {
                        $("#" + _data.row_id).before(tr);
                        $("#" + _data.row_id).remove();
                    } else {
                        // 画上
                        var tbody = target.find("tbody");
                        tbody.append(tr);
                    }
                }
                item.isShow = true;
                // cache and format data
                formatData([item]);
            });
            registerExpanderEvent();
            registerRowClickEvent();
            initHiddenColumns();
        }
        // expand/collapse the specified row
        target.toggleRow = function (id) {
            var _rowData = target.data_obj["id_" + id];
            var $row_expander = $("#" + _rowData.row_id).find(".treetable-expander");
            $row_expander.trigger("click");
        }
        // Expand the specified row
        target.expandRow = function (id) {
            var destArr = [];
            ys.recursion(target.data_obj, id, destArr, options.code, options.parentCode);
            // Expand layer by layer
            for (var i = destArr.length - 1; i >= 0; i--) {
                if (destArr[i].row_id) {
                    var $row_expander = $("#" + destArr[i].row_id).find(".treetable-expander");
                    var _isCollapsed = $row_expander.hasClass(options.expanderCollapsedClass);
                    if (_isCollapsed) {
                        $row_expander.trigger("click");
                    }
                }
            }
        }
        // fold the specified line
        target.collapseRow = function (id) {
            var _rowData = target.data_obj["id_" + id];
            var $row_expander = $("#" + _rowData.row_id).find(".treetable-expander");
            var _isExpanded = $row_expander.hasClass(options.expanderExpandedClass);
            if (_isExpanded) {
                $row_expander.trigger("click");
            }
        }
        // expand all rows
        target.expandAll = function () {
            target.find("tbody").find("tr").find(".treetable-expander").each(function (i, n) {
                var _isCollapsed = $(n).hasClass(options.expanderCollapsedClass);
                if (_isCollapsed) {
                    $(n).trigger("click");
                }
            })
        }
        // collapse all rows
        target.collapseAll = function () {
            target.find("tbody").find("tr").find(".treetable-expander").each(function (i, n) {
                var _isExpanded = $(n).hasClass(options.expanderExpandedClass);
                if (_isExpanded) {
                    $(n).trigger("click");
                }
            })
        }
        // display the specified column
        target.showColumn = function (field, flag) {
            var _index = $.inArray(field, target.hiddenColumns);
            if (_index > -1) {
                target.hiddenColumns.splice(_index, 1);
            }
            target.find("." + field + "_cls").show();
            // Whether to update the column option status
            if (flag && options.showColumns) {
                var $input = $(".bootstrap-tree-table .treetable-bars .columns label").find("input[value='" + field + "']")
                $input.prop("checked", 'checked');
            }
        }
        // hide the specified column
        target.hideColumn = function (field, flag) {
            target.hiddenColumns.push(field);
            target.find("." + field + "_cls").hide();
            // Whether to update the column option status
            if (flag && options.showColumns) {
                var $input = $(".bootstrap-tree-table .treetable-bars .columns label").find("input[value='" + field + "']")
                $input.prop("checked", '');
            }
        }
        // initialization
        init();
        return target;
    };

    // Component method encapsulation........
    $.fn.bootstrapTreeTable.methods = {
        // In order to be compatible with the writing method of bootstrap-table, the array is returned uniformly, and the data of the column displayed in the table is returned here
        getSelections: function (target, data) {
            // All selected records input
            var _ipt = target.find("tbody").find("tr").find("input[name='select_item']:checked");
            var chk_value = [];
            // if it is radio
            if (_ipt.attr("type") == "radio") {
                var _data = target.data_obj["id_" + _ipt.val()];
                chk_value.push(_data);
            } else {
                _ipt.each(function (_i, _item) {
                    var _data = target.data_obj["id_" + $(_item).val()];
                    chk_value.push(_data);
                });
            }
            return chk_value;
        },
        // refresh record
        refresh: function (target, parms) {
            if (parms) {
                target.refresh(parms);
            } else {
                target.refresh();
            }
        },
        // add data to table
        appendData: function (target, data) {
            if (data) {
                target.appendData(data);
            }
        },
        // expand/collapse the specified row
        toggleRow: function (target, id) {
            target.toggleRow(id);
        },
        // Expand the specified row
        expandRow: function (target, id) {
            target.expandRow(id);
        },
        // fold the specified line
        collapseRow: function (target, id) {
            target.collapseRow(id);
        },
        // expand all rows
        expandAll: function (target) {
            target.expandAll();
        },
        // collapse all rows
        collapseAll: function (target) {
            target.collapseAll();
        },
        // display the specified column
        showColumn: function (target, field) {
            target.showColumn(field, true);
        },
        // hide the specified column
        hideColumn: function (target, field) {
            target.hideColumn(field, true);
        }
        // Other methods of components can also be similarly encapsulated........
    };

    $.fn.bootstrapTreeTable.defaults = {
        code: 'code',              // Select the value returned by the record to set the parent-child relationship
        parentCode: 'parentCode',  // Used to set parent-child relationship
        rootIdValue: null,         // Set the root node id value ---- you can specify the root node, the default is null,"",0,"0"
        data: null,                // Construct the data collection of the table
        method: "GET",             // Ajax type of request data
        url: null,                 // The url of ajax requesting data
        ajaxParams: {},            // The data attribute of ajax requesting data
        expandColumn: 0,           // Which column to display the expand button on
        expandAll: false,          // Whether to expand all
        expandFirst: true,         // Whether the first level of expansion is defaulted to take effect when --expand All is false
        striped: false,            // Whether each row has a gradient color
        bordered: true,            // Whether to display the border
        hover: true,               // Whether the mouse is hovering
        condensed: false,          // Whether to compact the table
        columns: [],               // List
        toolbar: null,             // top toolbar
        height: 0,                 // table height
        showTitle: true,           // Whether to use the title attribute to display the field content (the field formatted by the formatter will not be displayed）
        showColumns: true,         // Whether to display the content column drop-down box
        showRefresh: true,         // Whether to display the refresh button
        expanderExpandedClass: 'glyphicon glyphicon-chevron-down', // icon for the expanded button
        expanderCollapsedClass: 'glyphicon glyphicon-chevron-right', // Icon for the shrink button
        onLoadSuccess: null          // Called after loading is complete
    };
    

})(jQuery);