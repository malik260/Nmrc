/**
 * Home method package processing
 * Copyright (c) 2019
 */
$(function () {
    // MetsiMenu
    $('#side-menu').metisMenu();

    //fix the menu bar
    $(function () {
        $('.sidebar-collapse').slimScroll({
            height: '100%',
            railOpacity: 0.9,
            size: '4px'
        });
    });

    // menu toggle
    $('.navbar-minimalize').click(function () {
        $("body").toggleClass("mini-navbar");
        SmoothlyMenu();
    });

    $('#side-menu>li').click(function () {
        if ($('body').hasClass('mini-navbar')) {
            NavToggle();
        }
    });
    $('#side-menu>li li a').click(function () {
        if ($(window).width() < 769) {
            NavToggle();
        }
    });

    $('.nav-close').click(NavToggle);

    //iOS browser compatibility processing
    if (/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)) {
        $('#content-main').css('overflow-y', 'auto');
    }
});

$(window).bind("load resize", function () {
    if ($(this).width() < 769) {
        $('body').addClass('mini-navbar');
        $('.navbar-static-side').fadeIn();
    }
});

function NavToggle() {
    $('.navbar-minimalize').trigger('click');
}

function SmoothlyMenu() {
    if (!$('body').hasClass('mini-navbar')) {
        $('#side-menu').hide();
        setTimeout(function () {
            $('#side-menu').fadeIn(500);
        }, 100);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(function () {
            $('#side-menu').fadeIn(500);
        }, 300);
    } else {
        $('#side-menu').removeAttr('style');
    }
}

/**
 * iframe handling
 */
$(function () {
    // Calculate the total width of the set of elements
    function calSumWidth(elements) {
        var width = 0;
        $(elements).each(function () {
            width += $(this).outerWidth(true);
        });
        return width;
    }

    //Scroll to the specified tab
    function scrollToTab(element) {
        var marginLeftVal = calSumWidth($(element).prevAll()),
            marginRightVal = calSumWidth($(element).nextAll());
        // The visible area is not tab width
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".menuTabs"));
        //viewable area tab width
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        // Actual scroll width
        var scrollVal = 0;
        if ($(".page-tabs-content").outerWidth() < visibleWidth) {
            scrollVal = 0;
        } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
            if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                scrollVal = marginLeftVal;
                var tabElement = element;
                while ((scrollVal - $(tabElement).outerWidth()) > ($(".page-tabs-content").outerWidth() - visibleWidth)) {
                    scrollVal -= $(tabElement).prev().outerWidth();
                    tabElement = $(tabElement).prev();
                }
            }
        } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
            scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
        }
        $('.page-tabs-content').animate({ marginLeft: 0 - scrollVal + 'px' }, "fast");
    }

    // scroll to the specified menu
    function scrollToMenu(element) {
        var menuTabUrl = $(element).data('id');
        $(".nav ul, .nav li").removeClass("selected").removeClass("active").removeClass("in");
        $(".nav ul, .nav li").each(function () {
            if ($(this).children().length > 0) {
                var link = $(this).children()[0];
                if (link) {
                    var menuUrl = $(link).data('url');
                    if (menuUrl == menuTabUrl) {
                        var dataType = "[data-type=menu]";
                        var parent1_li = $(link).parent(dataType);
                        parent1_li.addClass("active");

                        var parent2_ul = parent1_li.parent(dataType);
                        parent2_ul.addClass("in").addClass("active");

                        var parent3_li = parent2_ul.parent(dataType);
                        parent3_li.addClass("active");

                        var parent4_ul = parent3_li.parent(dataType);
                        if (parent4_ul) {
                            parent4_ul.addClass("in").addClass("active");

                            var parent5_li = parent4_ul.parent(dataType);
                            parent5_li.addClass("active");
                        }
                        return false; // terminate the loop
                    }
                }
            }
        });
    }

    // View hidden tabs on the left
    function scrollTabLeft() {
        var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
        // The visible area is not tab width
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".menuTabs"));
        //viewable area tab width
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        // Actual scroll width
        var scrollVal = 0;
        if ($(".page-tabs-content").width() < visibleWidth) {
            return false;
        } else {
            var tabElement = $(".menuTab:first");
            var offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) { //find the element closest to the current tab
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            offsetVal = 0;
            if (calSumWidth($(tabElement).prevAll()) > visibleWidth) {
                while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).prev();
                }
                scrollVal = calSumWidth($(tabElement).prevAll());
            }
        }
        $('.page-tabs-content').animate({ marginLeft: 0 - scrollVal + 'px' }, "fast");
    }

    // View hidden tabs on the right
    function scrollTabRight() {
        var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
        // The visible area is not tab width
        var tabOuterWidth = calSumWidth($(".content-tabs").children().not(".menuTabs"));
        //viewable area tab width
        var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
        // Actual scroll width
        var scrollVal = 0;
        if ($(".page-tabs-content").width() < visibleWidth) {
            return false;
        } else {
            var tabElement = $(".menuTab:first");
            var offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) { //find the element closest to the current tab
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            offsetVal = 0;
            while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                offsetVal += $(tabElement).outerWidth(true);
                tabElement = $(tabElement).next();
            }
            scrollVal = calSumWidth($(tabElement).prevAll());
            if (scrollVal > 0) {
                $('.page-tabs-content').animate({ marginLeft: 0 - scrollVal + 'px' }, "fast");
            }
        }
    }

    // Add the data-index attribute to the menu item by traversing
    $(".menuItem").each(function (index) {
        if (!$(this).attr('data-index')) {
            $(this).attr('data-index', index);
        }
    });

    $('.menuItem').on('click', function menuItem() {
        // get identification data
        var dataUrl = $(this).data('url'),
            dataIndex = $(this).data('index'),
            menuName = $.trim($(this).text()),
            href = $(this).attr("href"),
            addMenuTab = true;

        if (href !== "#" && href !== "") {
            cn.openLink(href, '_blank');
            return false;
        }
        if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
            return false;
        }
        // The tab menu already exists
        $('.menuTab').each(function () {
            if ($(this).data('id') == dataUrl) {
                if (!$(this).hasClass('active')) {
                    $(this).addClass('active').siblings('.menuTab').removeClass('active');
                    scrollToTab(this);
                    // Display the content area corresponding to the tab
                    $('.mainContent .Central_iframe').each(function () {
                        if ($(this).data('id') == dataUrl) {
                            $(this).show().siblings('.Central_iframe').hide();
                            return false;
                        }
                    });
                }
                addMenuTab = false;
                return false;
            }
        });
        // The tab menu does not exist
        if (addMenuTab) {
            var menuTabStr = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-times-circle"></i></a>';
            $('.menuTab').removeClass('active');

            // Add the iframe corresponding to the tab
            var ifrmaeStr = '<iframe class="Central_iframe" name="iframe' + dataIndex + '" width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id ="' + dataUrl + '" seamless></iframe>';
            $('.mainContent').find('iframe.Central_iframe').hide().parents('.mainContent').append(ifrmaeStr);
            cn.showLoading("Data loading, please wait...");

            $('.mainContent iframe:visible').load(function () {
                cn.closeLoading();
            });

            // add tab
            $('.menuTabs .page-tabs-content').append(menuTabStr);
            scrollToTab($('.menuTab.active'));
        }
        scrollToMenu($('.menuTab.active'));
        return false;
    });

    // close the tab menu
    function closeTab() {
        var closeTabId = $(this).parents('.menuTab').data('id');
        var currentWidth = $(this).parents('.menuTab').width();

        // the current element is active
        if ($(this).parents('.menuTab').hasClass('active')) {

            // There is a sibling element behind the current element, making the next element active
            if ($(this).parents('.menuTab').next('.menuTab').size()) {

                var activeId = $(this).parents('.menuTab').next('.menuTab:eq(0)').data('id');
                $(this).parents('.menuTab').next('.menuTab:eq(0)').addClass('active');

                $('.mainContent .Central_iframe').each(function () {
                    if ($(this).data('id') == activeId) {
                        $(this).show().siblings('.Central_iframe').hide();
                        return false;
                    }
                });

                var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                if (marginLeftVal < 0) {
                    $('.page-tabs-content').animate({
                        marginLeft: (marginLeftVal + currentWidth) + 'px'
                    }, "fast");
                }

                // remove the current tab
                $(this).parents('.menuTab').remove();

                // Remove the content area corresponding to the tab
                $('.mainContent .Central_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
            }

            // There is no sibling element behind the current element, making the previous element of the current element active
            if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                $('.mainContent .Central_iframe').each(function () {
                    if ($(this).data('id') == activeId) {
                        $(this).show().siblings('.Central_iframe').hide();
                        return false;
                    }
                });

                // remove the current tab
                $(this).parents('.menuTab').remove();

                // Remove the content area corresponding to the tab
                $('.mainContent .Central_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
            }
        }
        // The current element is not active
        else {
            // remove the current tab
            $(this).parents('.menuTab').remove();

            // Remove the content area corresponding to the corresponding tab
            $('.mainContent .Central_iframe').each(function () {
                if ($(this).data('id') == closeTabId) {
                    $(this).remove();
                    return false;
                }
            });
            scrollToTab($('.menuTab.active'));
        }
        return false;
    }

    $('.menuTabs').on('click', '.menuTab i', closeTab);

    //close other tabs
    $('.tabCloseOther').on('click', function closeOtherTabs() {
        $('.page-tabs-content').children("[data-id]").not(":first").not(".active").each(function () {
            $('.Central_iframe[data-id="' + $(this).data('id') + '"]').remove();
            $(this).remove();
        });
        $('.page-tabs-content').css("margin-left", "0");
    });

    //scroll to active tab
    $('.tabShowActive').on('click', function showActiveTab() {
        scrollToTab($('.menuTab.active'));
    });

    // click on the tab menu
    $('.menuTabs').on('click', '.menuTab', function activeTab() {
        if (!$(this).hasClass('active')) {
            var currentId = $(this).data('id');
            // Display the content area corresponding to the tab
            $('.mainContent .Central_iframe').each(function () {
                if ($(this).data('id') == currentId) {
                    $(this).show().siblings('.Central_iframe').hide();
                    return false;
                }
            });
            $(this).addClass('active').siblings('.menuTab').removeClass('active');
            scrollToTab(this);
        }
        scrollToMenu(this);
    });

    // refresh iframe
    function refreshTab() {
        var currentId = $('.page-tabs-content').find('.active').attr('data-id');
        var target = $('.Central_iframe[data-id="' + currentId + '"]');
        var url = target.attr('src');
        target.attr('src', url).ready();
    }

    // full-screen display
    $('#fullScreen').on('click', function () {
        $('#wrapper').fullScreen();
    });

    // refresh button
    $('.tabReload').on('click', refreshTab);

    $('.menuTabs').on('dblclick', '.menuTab', refreshTab);

    // left button
    $('.tabLeft').on('click', scrollTabLeft);

    // right shift button
    $('.tabRight').on('click', scrollTabRight);

    // close the current
    $('.tabCloseCurrent').on('click', function () {
        $('.page-tabs-content').find('.active i').trigger("click");
    });

    // close all
    $('.tabCloseAll').on('click', function () {
        $('.page-tabs-content').children("[data-id]").not(":first").each(function () {
            $('.Central_iframe[data-id="' + $(this).data('id') + '"]').remove();
            $(this).remove();
        });
        $('.page-tabs-content').children("[data-id]:first").each(function () {
            $('.Central_iframe[data-id="' + $(this).data('id') + '"]').show();
            $(this).addClass("active");
        });
        $('.page-tabs-content').css("margin-left", "0");
    });
});

