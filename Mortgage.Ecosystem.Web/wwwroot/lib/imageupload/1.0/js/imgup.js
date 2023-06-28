
(function ($) {
    "use strict";

    var deleteParent;
    var deleteDisplay = 'none';
    var defaults = {
        fileType: ["jpg", "png", "bmp", "jpeg"],   // type of uploaded file
        fileSize: 1024 * 1024 * 10                 // The size of the uploaded file is 10M
    };

    $.fn.imageUpload = function (option, param) {
        if (typeof option == 'string') {
            return $.fn.imageUpload.methods[option](this, param);
        }
        var _option = $.extend({}, $.fn.imageUpload.defaults, option || {});
        var target = $(this);
        var id = target.attr("id");
        var inputFileId = id + "_file";
        var html = '';
        html += '<section class="img-section">';
        html += '    <div class="z_photo upimg-div clear">';
        html += '       <section class="z_file fl">';
        if (_option.canAdd == 1) {
            html += '           <img src="' + _option.context + 'lib/imageupload/1.0/img/add.png" class="add-img">';
            deleteDisplay = 'block';
        }
        html += '           <input type="file" name="' + inputFileId + '" id="' + inputFileId + '" class="file-image" callback="' + _option.uploadImage + '" context="' + _option.context + '" limit="' + _option.limit + '"  value="" accept="image/jpg,image/jpeg,image/png,image/bmp" />';
        html += '       </section>';
        html += '   </div>';
        html += '</section>';
        html += '<aside class="mask works-mask">';
        html += '   <div class="mask-content">';
        html += '       <div class="del-p">Are you sure you want to delete the picture?</div>';
        html += '       <div class="check-p"><span class="del-com wsdel-ok">Sure</span><span class="wsdel-no">Cancel</span></div>';
        html += '   </div>';
        html += '</aside>';
        target.append(html);

        target.find(".wsdel-ok").click(function () {
            $(".works-mask").hide();
            var numUp = deleteParent.siblings().length;
            if (numUp < 6) {
                deleteParent.parent().find(".z_file").show();
            }
            deleteParent.remove();
        });

        target.find(".wsdel-no").click(function () {
            $(".works-mask").hide();
        });

        $("#" + inputFileId).change(function () {
            prepareUploadImage(inputFileId);
        });
    };

    $.fn.imageUpload.defaults = {
        uploadImage: '',    // upload image callback
        limit: 10,          // upload limit
        context: '',        // current page root directory
        canPreview: 1,      // Is it possible to preview (0 is not, 1 is)
        canAdd: 1           // Whether it can be added (0 is not, 1 is)
    };

    $.fn.imageUpload.methods = {
        getImageUrl: function (target) {
            var imageUrl = '';
            var list = $(target).find('.up-section').find('.up-img');
            for (var i = 0; i < list.length; i++) {
                if (i == 0) {
                    imageUrl += $(list[i]).attr("src");
                } else {
                    imageUrl += ';';
                    imageUrl += $(list[i]).attr("src");
                }
            }
            return imageUrl;
        },
        setImageUrl: function (target, imageUrl) {
            if (imageUrl) {
                var id = $(target).attr("id");
                var inputFileId = id + "_file";
                var context = $("#" + inputFileId).attr("context");

                var urlArr = imageUrl.split(';');
                for (var i = 0; i < urlArr.length; i++) {
                    if (urlArr[i] != "") {
                        var deleteId = ys.getGuid();
                        var imageName = urlArr[i].substring(urlArr[i].lastIndexOf('/') + 1);
                        var html = '';
                        html += '<section class="up-section fl">';
                        html += '   <span class="up-span"></span>';

                        html += '   <img id="' + deleteId + '" class="close-upimg" style="display:' + deleteDisplay + '" src="' + context + 'lib/imageupload/1.0/img/delete.png" />';
                        if (urlArr[i].indexOf('http') > -1) {
                            html += '   <img class="up-img" src="' + urlArr[i] + '" />';
                        }
                        else {
                            html += '   <img class="up-img" src="' + context + ys.trimStart(urlArr[i], '/') + '" />';
                        }
                        html += '   <p class="img-name-p">"' + imageName + '"</p>';
                        html += '</section>';
                        $(html).insertBefore($(target).find(".z_file"));
                        $("#" + deleteId).on("click", function () {
                            $("#" + inputFileId).imageUpload("deleteImage", deleteId)
                        });
                    }
                }
                $(".up-span").Huipreview();
                $("#" + inputFileId).imageUpload("checkImageLimit")
            }
        },
        deleteImage: function (target, deleteId) {
            var _target = $(target);
            var inputFileId = _target.attr("id");
            if (!!event) {
                event.preventDefault();
                event.stopPropagation();
            }
            $(".works-mask").show();
            deleteParent = $("#" + deleteId).parent();
            $("#" + inputFileId).imageUpload("checkImageLimit");
        },
        checkImageLimit: function (target) {
            var _target = $(target);
            var num = _target.parents(".z_photo").find(".up-section").length;
            var limit = _target.attr("limit");
            if (num >= limit) {
                _target.parent().hide();
            } else {
                _target.parent().show();
            }
        }
    };

    function prepareUploadImage(inputFileId) {
        var target = $("#" + inputFileId);
        var callback = target.attr("callback");
        var limit = target.attr("limit");
        if (!callback || callback == '') {
            alert("Please provide upload method");
            return;
        }
        var inputFile = document.getElementById(inputFileId);
        var imgContainer = target.parents(".z_photo"); // Father elements that store pictures
        var fileList = inputFile.files; // The picture file obtained
        // The picture file obtained through the traversal
        var num = imgContainer.find(".up-section").length;
        var totalNum = num + fileList.length;  // Total quantity
        if (fileList.length > limit || totalNum > limit) {
            alert("The number of upload pictures cannot be exceeded" + limit + "Please choose again");  // Choose to upload more than 5 at a time or the total number of uploaded and uploaded this time cannot exceed 5
        }
        else if (num < limit) {

            validateUpload(fileList);

            var uploadSuccess = function (imgPath) {
                var section = $("<section class='up-section fl loading'>");
                imgContainer.prepend(section);
                var span = $("<span class='up-span'>");
                span.appendTo(section);

                var context = $("#" + inputFileId).attr("context");
                var deleteId = ys.getGuid();
                var deleteImg = $("<img id='" + deleteId + "' class='close-upimg'> style='display:" + deleteDisplay + "'").on("click", function () {
                    $("#" + inputFileId).imageUpload("deleteImage", deleteId)
                });
                deleteImg.attr("src", context + "lib/imageupload/1.0/img/delete.png").appendTo(section);

                var realImg = $("<img class='up-img up-opcity'>");
                realImg.attr("src", imgPath);
                realImg.appendTo(section);
                var p = $("<p class='img-name-p'>");
                p.html(imgPath.substring(imgPath.lastIndexOf('/') + 1)).appendTo(section);

                setTimeout(function () {
                    $(".up-section").removeClass("loading");
                    $(".up-img").removeClass("up-opcity");
                }, 450);

                $("#" + inputFileId).imageUpload("checkImageLimit");
            };
            var currentFile = fileList[fileList.length - 1];
            doCallback(eval(callback), [currentFile, uploadSuccess]);
        }
    }

    function validateUpload(files) {
        var arrFiles = [];// The file array replaced
        for (var i = 0, file; file = files[i]; i++) {
            // Get the suffix name uploaded by the file
            var newStr = file.name.split("").reverse().join("");
            if (newStr.split(".")[0] != null) {
                var type = newStr.split(".")[0].split("").reverse().join("");
                console.log(type + "===type===");
                if (jQuery.inArray(type, defaults.fileType) > -1) {
                    // The type matches and can be uploaded
                    if (file.size >= defaults.fileSize) {
                        alert(file.size);
                        alert('You"' + file.name + '"The file size is too large');
                    } else {
                        // Here you need to judge all current files
                        arrFiles.push(file);
                    }
                } else {
                    alert('You"' + file.name + '"The upload type does not meet');
                }
            } else {
                alert('You"' + file.name + '"No type, unrecognized');
            }
        }
        return arrFiles;
    }

    // Dynamically call the method and pass parameters
    function doCallback(fn, args) {
        fn.apply(this, args);
    }
})(jQuery);

