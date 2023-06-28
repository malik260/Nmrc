; (function (window) {
    "use strict";

    // Store all data dictionaries in the database, get a list of dictionary types or dictionary values ​​such as top.getDataDict('NewsType') or top.getDataDictValue('NewsType' , 1)
    var dataDict = {};
    // store the permissions of the current user
    var dataAuthority = {};

    function initDataDict() {
        cn.ajax({
            url: ctx + 'DataDict/GetDataDictListJson',
            type: "get",
            success: function (obj) {
                if (obj.Tag == 1) {
                    for (var i = 0; i < obj.Data.length; i++) {
                        dataDict[obj.Data[i].DictType] = obj.Data[i].Detail;
                    }
                }
            }
        });
    }
    function getDataDict(dictType) {
        var arr = [];
        for (var i = 0; i < dataDict[dictType].length; i++) {
            if (dataDict[dictType][i].DictStatus == 1) {
                arr.push(dataDict[dictType][i]);
            }
        }
        return arr;
    }
    function getDataDictValue(dictType, dictKey) {
        if (dataDict[dictType]) {
            for (var i = 0; i < dataDict[dictType].length; i++) {
                if (dataDict[dictType][i].DictKey == dictKey) {
                    if (dataDict[dictType][i].ListClass) {
                        return '<span class="badge badge-' + dataDict[dictType][i].ListClass + '">' + dataDict[dictType][i].DictValue + '</span>';
                    }
                    else {
                        return dataDict[dictType][i].DictValue;
                    }
                }
            }
        }
        return '';
    }

    function initDataAuthority() {
        cn.ajax({
            url: ctx + 'User/GetUserAuthorizeJson',
            type: "get",
            success: function (obj) {
                if (obj.Tag == 1) {
                    dataAuthority = obj.Data;
                }
            }
        });
    }
    function getButtonAuthority(url, buttonList) {
        var noAuthorize = [];
        if (dataAuthority) {
            // superuser does not authenticate
            if (dataAuthority.IsSystem != 1) {
                var regex = /([a-zA-Z]+)Manage\/(.*)\//; //match url like http://localhost:5000/OrganizationManage/User/UserIndex
                var matches = regex.exec(url);
                if (matches && matches.length >= 3) {
                    var module = matches[1];
                    var page = matches[2];
                    buttonList.forEach(function (btn, btnIndex) {
                        var authorize = module.toLowerCase() + ':' + page.toLowerCase() + ':' + btn.toString().replace('btn', '').toLowerCase();
                        var hasAuthority = false;

                        dataAuthority.MenuAuthorize.forEach(function (authority, authorityIndex) {
                            if (authority.Authorize == authorize) {
                                hasAuthority = true;
                                return false;
                            }
                        });

                        if (!hasAuthority) {
                            noAuthorize.push(btn);
                        }
                    });
                }
            }
        }
        return noAuthorize;
    }

    initDataDict();
    initDataAuthority();

    // public method
    window.initDataDict = initDataDict;
    window.getDataDict = getDataDict;
    window.getDataDictValue = getDataDictValue;
    window.getButtonAuthority = getButtonAuthority;

})(window);