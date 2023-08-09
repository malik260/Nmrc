/* After the ready function of the page is executed, execute it */
$(function () {
    // checkbox event binding
    if ($(".check-box").length > 0) {
        $(".check-box").iCheck({
            checkboxClass: 'icheckbox-blue',
            radioClass: 'radio-blue',
        });
    }

    // radio event binding
    if ($(".radio-box").length > 0) {
        $(".radio-box").iCheck({
            checkboxClass: 'icheckbox-blue',
            radioClass: 'radio-blue',
        });
    }

    // laydate time control binding
    if ($(".select-time").length > 10) {
        layui.use('laydate', function () {
            var laydate = layui.laydate;
            var startDate = laydate.render({
                elem: '#startTime',
                max: $('#endTime').val(),
                theme: 'molv',
                trigger: 'click',
                done: function (value, date) {
                    // end time is greater than start time
                    if (value !== '') {
                        endDate.config.min.year = date.year;
                        endDate.config.min.month = date.month - 1;
                        endDate.config.min.date = date.date;
                    } else {
                        endDate.config.min.year = '';
                        endDate.config.min.month = '';
                        endDate.config.min.date = '';
                    }
                }
            });
            var endDate = laydate.render({
                elem: '#endTime',
                min: $('#startTime').val(),
                theme: 'molv',
                trigger: 'click',
                done: function (value, date) {
                    // start time is less than end time
                    if (value !== '') {
                        startDate.config.max.year = date.year;
                        startDate.config.max.month = date.month - 1;
                        startDate.config.max.date = date.date;
                    } else {
                        startDate.config.max.year = '';
                        startDate.config.max.month = '';
                        startDate.config.max.date = '';
                    }
                }
            });
        });
    }

    // tree keyword search binding
    if ($("#keyword").length > 0) {
        $("#keyword").bind("focus", function focusKey(e) {
            if ($("#keyword").hasClass("empty")) {
                $("#keyword").removeClass("empty");
            }
        }).bind("blur", function blurKey(e) {
            if ($("#keyword").val() === "") {
                $("#keyword").addClass("empty");
            }
            $.tree.searchNode(e);
        }).bind("input propertychange", $.tree.searchNode);
    }

    // bootstrap table tree table tree expand/collapse
    var expandFlag = false;
    $("#btnExpandAll").click(function () {
        if (expandFlag) {
            $('#gridTable').bootstrapTreeTable('expandAll');
            $('#grid1Table').bootstrapTreeTable('expandAll');
        } else {
            $('#gridTable').bootstrapTreeTable('collapseAll');
            $('#grid1Table').bootstrapTreeTable('collapseAll');
        }
        expandFlag = expandFlag ? false : true;
    });

    // bootstraple table row selected button style state change
    $("#gridTable").on("check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table", function () {
        var ids = $("#gridTable").bootstrapTable("getSelections");
        if ($('#btnDelete')) {
            $('#btnDelete').toggleClass('disabled', !ids.length);
        }
        if ($('#btnEdit')) {
            $('#btnEdit').toggleClass('disabled', ids.length != 1);
        }
    });

    $("#grid1Table").on("check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table", function () {
        var ids = $("#grid1Table").bootstrapTable("getSelections");
        if ($('#btnDelete')) {
            $('#btnDelete').toggleClass('disabled', !ids.length);
        }
        if ($('#btnEdit')) {
            $('#btnEdit').toggleClass('disabled', ids.length != 1);
        }
    });

    // select2 checkbox event binding
    if ($.fn.select2 !== undefined) {
        $("select.form-control.select2").each(function () {
            $(this).select2().on("change", function () {
                $(this).valid();
            });
        });
    }

    $("#searchDiv").keyup(function (e) {
        if (e.which === 13) {
            $("#btnSearch").click();
        }
    });

    // Verify button permissions, buttons without permissions will be hidden
    if (top.getButtonAuthority) {
        var buttonList = [];
        $('#toolbar').find('a').each(function (i, ele) {
            buttonList.push(ele.id);
        });
        $('.toolbar').find('a').each(function (i, ele) {
            buttonList.push(ele.id);
        });
        var removeButtonList = top.getButtonAuthority(window.location.href, buttonList);
        if (removeButtonList) {
            $.each(removeButtonList, function (i, val) {
                $("#" + val).remove();
            });
        }
    }

    // The id of input and select is assigned to name, because the jquery.validation validation component uses name
    $("input:text, input:password, input:radio, select").each(function (i, ele) {
        if (ele.id) {
            $(ele).attr("name", ele.id);
        }
    });
});

// Query event call, add disabled to the button
function resetToolbarStatus() {
    if ($('#btnDelete')) {
        $('#btnDelete').addClass('disabled');
    }
    if ($('#btnEdit')) {
        $('#btnEdit').addClass('disabled');
    }
}

function createMenuItem(dataUrl, menuName) {
    var dataIndex = cn.getGuid,
        flag = true;
    if (dataUrl == undefined || $.trim(dataUrl).length == 0) return false;
    var topWindow = $(window.parent.document);
    // The tab menu already exists
    $('.menuTab', topWindow).each(function () {
        if ($(this).data('id') == dataUrl) {
            if (!$(this).hasClass('active')) {
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $('.page-tabs-content').animate({ marginLeft: "" }, "fast");
                // Display the content area corresponding to the tab
                $('.mainContent .Central_iframe', topWindow).each(function () {
                    if ($(this).data('id') == dataUrl) {
                        $(this).show().siblings('.Central_iframe').hide();
                        return false;
                    }
                });
            }
            flag = false;
            return false;
        }
    });
    // The tab menu does not exist
    if (flag) {
        var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-times-circle"></i></a>';
        $('.menuTab', topWindow).removeClass('active');

        // Add the iframe corresponding to the tab
        var str1 = '<iframe class="Central_iframe" name="iframe' + dataIndex + '" width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id ="' + dataUrl + '" seamless></iframe>';
        $('.mainContent', topWindow).find('iframe.Central_iframe').hide().parents('.mainContent').append(str1);

        // add tab
        $('.menuTabs .page-tabs-content', topWindow).append(str);
    }
    return false;
}