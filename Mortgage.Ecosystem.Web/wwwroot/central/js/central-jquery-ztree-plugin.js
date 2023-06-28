(function ($) {
    "use strict";
    $.fn.cnTree = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.cnTree.methods[option](this, param);
        }
        var _option = $.extend({}, $.fn.cnTree.defaults, option || {});
        var target = $(this);
        var id = target.attr("id");

        // show vertical scroll bar
        target.css("overflow-y", "auto").css("max-height", _option.maxHeight);

        cn.ajax({
            url: _option.url,
            async: _option.async,
            success: function (data) {
                var tree = $.fn.zTree.init($("#" + id), _option, data.Data);
                for (var level = 0; level <= _option.expandLevel; level++) {
                    var nodes = tree.getNodesByParam("level", level);
                    for (var i = 0; i < nodes.length; i++) {
                        tree.expandNode(nodes[i], true, false, false);
                    }
                }
            }
        });
        return target;
    };
    $.fn.cnTree.methods = {
        getCheckedNodes: function (target, column) {
            var zTreeObj = $.fn.zTree.getZTreeObj($(target).attr("id"));
            var _column = cn.isNullOrEmpty(column) ? "id" : column;
            var nodes = zTreeObj.getCheckedNodes(true);
            return $.map(nodes, function (row) {
                return row[_column];
            }).join();
        },
        setCheckedNodes: function (target, ids) {
            if (!cn.isNullOrEmpty(ids)) {
                var _ids = ids.split(',');
                var zTreeObj = $.fn.zTree.getZTreeObj($(target).attr("id"));
                zTreeObj.cancelSelectedNode();//First cancel all selected states
                $.each(_ids, function (i, id) {
                    var node = zTreeObj.getNodeByParam("id", id);
                    zTreeObj.checkNode(node, true, false, true);
                });
            }
        },
        setCheckedNodesByName: function (target, names) {
            if (!cn.isNullOrEmpty(names)) {
                var _names = names.split(',');
                var zTreeObj = $.fn.zTree.getZTreeObj($(target).attr("id"));
                zTreeObj.cancelSelectedNode();//First cancel all selected states
                $.each(_names, function (i, name) {
                    var node = zTreeObj.getNodeByParam("name", name);
                    zTreeObj.checkNode(node, true, false, true);
                });
            }
        }
    };
    $.fn.cnTree.defaults = {
        url: '',
        async: false,
        maxHeight: "300px",
        expandLevel: 0,
        check: { "enable": false },
        view: { selectedMulti: false, nameIsHTML: true },
        data: { simpleData: { enable: true } },
        callback: {}
    };

    // The drop-down box, the data displayed in it is a tree, corresponding to cnComboBox
    $.fn.cnComboBoxTree = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.cnComboBoxTree.methods[option](this, param);
        }
        var _option = $.extend({}, $.fn.cnComboBoxTree.defaults, option || {});

        var target = $(this);
        var id = target.attr("id");

        var eleInputId = id + "_input";
        var eleTreeId = id + "_tree";

        // style needs to be changed to generic
        target.css("position", "relative");
        var html = "<input id='" + eleInputId + "' name='" + eleInputId + "' readonly='readonly' type='text' class='form-control' />";
        html += "<div id='" + eleTreeId + "' class='ztree treeSelect-panel' style='overflow-y: auto;max-height:" + _option.maxHeight + ";border:1px solid #e5e6e7 ;margin-top:1px;display:none'></div>";
        $(html).appendTo(target);

        $("#" + eleInputId).click(function () {
            var targetTree = $("#" + eleTreeId);
            if (targetTree.is(":hidden")) {
                targetTree.show();
            }
            else {
                targetTree.hide();
            }
        });

        cn.ajax({
            url: _option.url,
            async: _option.async,
            success: function (data) {
                var targetTree = $("#" + eleTreeId);
                var targetInput = $("#" + eleInputId);

                // user defined onClick callback
                var customOnClick = _option.callback.customOnClick;
                // OnClick callback
                _option.callback.onClick = function (event, treeId, treeNode) {
                    var wholeName = '';
                    var wholeId = '';
                    var parentNode = treeNode;
                    while (parentNode != null) {
                        wholeName = parentNode.name + '>' + wholeName;
                        wholeId = parentNode.id + ',' + wholeId;
                        parentNode = parentNode.getParentNode();
                    }
                    wholeName = cn.trimEnd(wholeName, '>');
                    wholeId = cn.trimEnd(wholeId, ',');

                    target.attr("data-key", wholeId);
                    target.attr("data-value", wholeName);

                    targetInput.val(wholeName);
                    targetTree.hide();

                    if (customOnClick) {
                        customOnClick(event, treeId, treeNode);
                    }
                };

                target.ztree = $.fn.zTree.init($("#" + eleTreeId), _option, data.Data);
                if (_option.expandLevel >= 0) {
                    for (var level = 0; level <= _option.expandLevel; level++) {
                        var nodes = target.ztree.getNodesByParam("level", level);
                        for (var i = 0; i < nodes.length; i++) {
                            target.ztree.expandNode(nodes[i], true, false, false);
                        }
                    }
                }
            }
        });

        $(document).click(function (e) {
            var e = e ? e : window.event;
            var tar = e.srcElement || e.target;
            if (!$(tar).hasClass('form-control')) {
                var tarId = $(tar).attr("id");
                if (cn.isNullOrEmpty(tarId) || tarId.indexOf("_tree") == -1) {
                    var targetTree = $("#" + eleTreeId);
                    targetTree.hide();
                    e.stopPropagation();
                }
            }
        });

        return target;
    };
    $.fn.cnComboBoxTree.methods = {
        getValue: function (target) {
            return $(target).attr("data-key");
        },
        setValue: function (target, value) {
            var lastId = '0'; // take the lowest value
            if (value) {
                var arr = value.toString().split(',');
                lastId = arr[arr.length - 1];
            }
            var id = target.attr("id");
            var eleTreeId = id + "_tree";
            var zTreeObj = $.fn.zTree.getZTreeObj(eleTreeId);
            var node = zTreeObj.getNodeByParam("id", lastId);
            if (node != null) {
                zTreeObj.cancelSelectedNode();//First cancel all selected states
                zTreeObj.selectNode(node, true);//Select the node with the specified ID
                zTreeObj.expandNode(node, true, false);//Expand the specified ID node
                zTreeObj.setting.callback.onClick('setValue', zTreeObj.setting.treeId, node); //trigger onclick
            }
            return $(target);
        }
    };
    $.fn.cnComboBoxTree.defaults = {
        url: '',
        async: false,
        maxHeight: "200px",
        expandLevel: 0,
        check: { "enable": false },
        view: { selectedMulti: false, nameIsHTML: true },
        data: { simpleData: { enable: true } },
        callback: {}
    };

})(jQuery);