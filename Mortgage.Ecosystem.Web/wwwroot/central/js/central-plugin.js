// Add to the Jquery object and access it with $("#id").
; (function ($) {
    "use strict";
    $.fn.cnRadioBox = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.cnRadioBox.methods[option](this, param);
        }
        var target = $(this);
        var targetId = target.attr('id');
        if (!targetId) {
            return false;
        }
        var _option = $.extend({
            url: null,
            key: "Key",
            value: "Value",
            data: null, // data source
            dataName: 'Data', // data name
            default: undefined
        }, option);

        var dom = {
            loadData: function () {
                if (_option.url) {
                    $.ajax({
                        url: _option.url,
                        type: 'get',
                        dataType: 'json',
                        async: false,
                        cache: false,
                        success: function (data) {
                            _option.data = data;
                            if (_option.dataName) {
                                if (_option.data != null) {
                                    _option.data = _option.data[_option.dataName];
                                }
                            }
                        },
                        error: function (xhr, status, obj) {
                            throw exception;
                        }
                    });
                }
            },
            render: function (setting) {
                if (setting.data && setting.data.length >= 0) {
                    var ref = target.attr('ref');
                    var name = targetId + "_radiobox";
                    var html = "";
                    $.each(setting.data, function (i) {
                        var row = setting.data[i];
                        html += "<label class='radio-box'>";
                        html += "<input type='radio' name='" + name + "' value='" + row[setting.key] + "' ref='" + ref + "' /> " + row[setting.value];
                        html += "</label>";

                        if (row.IsDefault == 1) {
                            setting.default = row[setting.key];
                        }
                    });
                    target.append(html);
                }
                if (setting.default != undefined) {
                    target.cnRadioBox("setValue", setting.default);
                }
            }
        };
        dom.loadData();
        dom.render(_option);
        return target;
    };
    $.fn.cnRadioBox.methods = {
        getValue: function (target) {
            var value = "";
            $(target).find("div.checked").each(function (i, ele) {
                value += $(ele).find("input[type=radio]").val();
                value += ",";
            });
            if (value.indexOf(",") >= 0) {
                value = value.substring(0, value.length - 1);
            }
            return value;
        },
        setValue: function (target, value) {
            if (cn.isNullOrEmpty(value)) {
                return;
            }
            if (typeof value != 'string') {
                value = value.toString();
            }
            $(target).find("div").each(function (i, ele) {
                $(ele).removeClass('checked');
            });
            var ids = value.split(',');
            $.each(ids, function (i, val) {
                var radiobox = $(target).find('input[type=radio][value=' + val + ']');
                radiobox.attr("checked", true);
                radiobox.parent().addClass("checked");
            });
        }
    };

    $.fn.cnCheckBox = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.cnCheckBox.methods[option](this, param);
        }
        var target = $(this);
        var targetId = target.attr('id');
        if (!targetId) {
            return false;
        }
        var _option = $.extend({
            url: null,
            key: "Key",
            value: "Value",
            data: null, //data source
            dataName: 'Data', //data name
            default: undefined
        }, option);

        var dom = {
            loadData: function () {
                if (_option.url) {
                    $.ajax({
                        url: _option.url,
                        type: 'get',
                        dataType: 'json',
                        async: false,
                        cache: false,
                        success: function (data) {
                            _option.data = data;
                        },
                        error: function (xhr, status, obj) {
                            throw exception;
                        }
                    });
                    if (_option.dataName) {
                        if (_option.data != null) {
                            _option.data = _option.data[_option.dataName];
                        }
                    }
                }
            },
            render: function (setting) {
                if (setting.data && setting.data.length >= 0) {
                    var name = targetId + "_checkbox";
                    var html = "";
                    $.each(setting.data, function (i) {
                        var row = setting.data[i];
                        html += "<label class='check-box'>";
                        html += "<input name='" + name + "' type='checkbox' value='" + row[setting.key] + "'>" + row[setting.value] + "</input>";
                        html += "</label>";

                        if (row.IsDefault == 1) {
                            setting.default = row[setting.key];
                        }
                    });
                    target.append(html);
                }
                if (setting.default != undefined) {
                    target.cnCheckBox("setValue", setting.default);
                }
            }
        };
        dom.loadData();
        dom.render(_option);
        return target;
    };
    $.fn.cnCheckBox.methods = {
        getValue: function (target) {
            var value = "";
            $(target).find("div.checked").each(function (i, ele) {
                value += $(ele).find("input[type=checkbox]").val();
                value += ",";
            });
            if (value.indexOf(",") >= 0) {
                value = value.substring(0, value.length - 1);
            }
            return value;
        },
        setValue: function (target, value) {
            if (cn.isNullOrEmpty(value)) {
                return;
            }
            if (typeof value != 'string') {
                value = value.toString();
            }
            var ids = value.split(',');
            $.each(ids, function (i, val) {
                var checkbox = $(target).find('input[type=checkbox][value=' + val + ']');
                checkbox.attr("checked", true);
                checkbox.parent().addClass("checked");
            });
        }
    };

    // Drop-down box selection, corresponding to cnComboxBoxTree
    $.fn.cnComboBox = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.cnComboBox.methods[option](this, param);
        }
        var target = $(this);
        var targetId = target.attr('id');
        if (!targetId) {
            return false;
        }
        var _option = $.extend({
            url: null,
            key: "Key",
            value: "Value",
            maxHeight: "160px",
            class: null,
            multiple: false,
            data: null, // data source
            dataName: 'Data', // data name
            onChange: null,
            default: undefined
        }, option);

        var dom = {
            loadData: function () {
                if (_option.url) {
                    $.ajax({
                        url: _option.url,
                        type: 'get',
                        dataType: 'json',
                        async: false,
                        cache: false,
                        success: function (data) {
                            _option.data = data;
                        },
                        error: function (xhr, status, obj) {
                            throw exception;
                        }
                    });
                    if (_option.dataName) {
                        if (_option.data != null) {
                            _option.data = _option.data[_option.dataName];
                        }
                    }
                }
            },
            render: function (setting) {
                if (setting.data && setting.data.length >= 0) {
                    var id = targetId + "_select";

                    var multiple = '';
                    if (setting.multiple) {
                        multiple = 'multiple=""';
                    }
                    var html = "<select id='" + id + "' name='" + id + "' class='" + cn.toString(setting.class) + " select2' " + multiple + " maxheight=" + setting.maxHeight + ">";
                    var selectExist = $("#" + id).length > 0;
                    if (selectExist) {
                        $("#" + id).empty();
                    }

                    var option = '';

                    var groupOption = false; // whether there is a group
                    if (setting.data.length > 0) {
                        groupOption = setting.data[0][setting.value] instanceof Array;
                    }

                    if (!groupOption) {
                        if (!setting.class) {
                            // If there is no form-control class, add a full option, which should be the query condition
                            option += "<option value='0'>Please select</option>";
                        } else {
                            if (!setting.multiple) {
                                option += "<option value='' selected='selected'>Please select</option>";
                            }
                        }
                    }

                    $.each(setting.data, function (i) {
                        var row = setting.data[i];
                        if (typeof row == 'string') {
                            option += "<option value='" + row + "'>" + row + "</option>";
                        } else {
                            if (row[setting.value] instanceof Array) {
                                // grouping options
                                option += "<optgroup label='--" + row[setting.key] + "--'>";
                                $.each(row[setting.value], function (j) {
                                    var childRow = row[setting.value][j];
                                    option += "<option value='" + childRow[setting.key] + "'>" + childRow[setting.value] + "</option>";

                                    if (row.IsDefault == 1) {
                                        setting.default = row[setting.key];
                                    }
                                });
                            }
                            else {
                                option += "<option value='" + row[setting.key] + "'>" + row[setting.value] + "</option>";

                                if (row.IsDefault == 1) {
                                    setting.default = row[setting.key];
                                }
                            }
                        }
                    });
                    if (selectExist) {
                        $("#" + id).append(option);
                    }
                    else {
                        html += option;
                        html += "</select>";
                        target.append(html);

                        if (setting.onChange) {
                            $("#" + id).change(setting.onChange);
                        }
                    }
                    $("#" + id).select2();

                    // The hack searched select keeps the same width as other elements
                    if (!setting.class) {
                        $("#" + targetId).find(".select2-container").width(280);
                    }

                    if (setting.default != undefined && setting.class) {
                        target.cnComboBox("setValue", setting.default);
                    }
                }
            }
        };
        dom.loadData();
        dom.render(_option);
        return target;
    };
    $.fn.cnComboBox.methods = {
        getValue: function (target) {
            var valArray = $("#" + $(target).attr("id") + "_select").select2("val");
            if (valArray == null) {
                return "";
            }
            else {
                var val = valArray.toString();
                // -1 means all query conditions, leave this query condition empty
                return val;
            }
        },
        setValue: function (target, value) {
            if (cn.isNullOrEmpty(value)) {
                return;
            }
            if (typeof value != 'string') {
                value = value.toString();
            }
            $("#" + $(target).attr("id") + "_select").val(value.split(',')).trigger("change");
        }
    };

    $.fn.getWebControls = function (value) {
        var data = {};
        if (value && typeof value == 'object') {
            data = value;
        }
        $(this).find("[col]").each(function (i, control) {
            var id = $(control).attr("id");
            var field = $(control).attr("col");

            if (control.tagName == "INPUT") {
                if (control.type == "checkbox") {
                    if ($(control).prop("checked")) {
                        if (data[field]) {
                            data[field] = data[field] + "," + $(control).val();
                        } else {
                            data[field] = $(control).val();
                        }
                    }
                }
                else if (control.type == "radio") {
                    if ($(control).prop("checked")) {
                        data[field] = $(control).val();
                    }
                }
                else {
                    data[field] = $(control).val();
                }
            }
            else if (control.tagName == "SELECT") {
                data[field] = $(control).val();
            }
            else if (control.tagName == "DIV") {
                if ($(control).find("#" + id + "_tree").length > 0) {
                    data[field] = $(control).cnComboBoxTree("getValue");
                } else if ($(control).find("#" + id + "_select").length > 0) {
                    data[field] = $(control).cnComboBox("getValue");
                }
                else if ($(control).find("input[type=checkbox]").length > 0) {
                    data[field] = $(control).cnCheckBox("getValue");
                } else if ($(control).find("input[type=radio]").length > 0) {
                    data[field] = $(control).cnRadioBox("getValue");
                } else {
                    data[field] = $(control).html();
                }
            }
            else if (control.tagName == "IMG") {
                data[field] = $(control).prop("src");
            }
            else if (control.tagName == "SPAN") {
                if ($(control).find("#" + id + "_select").length > 0) {
                    data[field] = $(control).cnComboBox("getValue");
                } else {
                    data[field] = $(control).html();
                }
            }
            else if (control.tagName == "TEXTAREA") {
                data[field] = $(control).val();
            }
        });
        return data;
    };
    $.fn.setWebControls = function (data) {
        $(this).find("[col]").each(function (i, control) {
            var id = $(control).attr("id");
            var field = $(control).attr("col");
            if (control.tagName == "INPUT") {
                if (control.type == "checkbox") {
                    if ($(control).val() == data[field]) {
                        $(control).prop("checked", "checked");
                    }
                }
                else if (control.type == "radio") {
                    if ($(control).val() == data[field]) {
                        if ($(control).iCheck) {
                            $(control).iCheck('check');
                        }
                        else {
                            $(control).prop("checked", true);
                        }
                    }
                }
                else {
                    $(control).val(data[field]);
                }
            }
            else if (control.tagName == "SELECT") {
                $(control).val(data[field]);
            }
            else if (control.tagName == "DIV") {
                if ($(control).find("#" + id + "_tree").length > 0) {
                    $(control).cnComboBoxTree("setValue", data[field]);
                } else if ($(control).find("#" + id + "_select").length > 0) {
                    $(control).cnComboBox("setValue", data[field]);
                } else if ($(control).find("input[type=checkbox]").length > 0) {
                    $(control).cnCheckBox("setValue", data[field]);
                } else if ($(control).find("input[type=radio]").length > 0) {
                    $(control).cnRadioBox("setValue", data[field]);
                } else {
                    $(control).html(data[field]);
                }
            }
            else if (control.tagName == "SPAN") {
                $(control).html(data[field]);
            }
            else if (control.tagName == "TEXTAREA") {
                $(control).val(data[field]);
            }
        });
        return data;
    }
})(window.jQuery);