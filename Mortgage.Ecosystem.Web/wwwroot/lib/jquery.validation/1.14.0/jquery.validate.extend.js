/*this is basic form validation using for validation person's basic information author:Clara Guo data:2017/07/20*/
$(document).ready(function () {
    $.validator.setDefaults({
        validateHandler: function (form) {
            form.validate();
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    // Mobile phone number verification ID regular merge: (^d {15} $) | (^d {17} ([0-9] | x) $)
    jQuery.validator.addMethod("isPhone", function (value, element) {
        var length = value.length;
        //var phone = /^1[3|4|5|6|7|8][0-9]\d{8}$/; // Please fill in the correct 11-digit mobile phone number
        //var phone = /((^+)?(234){1}[0–9]{10})|((^234)[0–9]{10})|((^0)(7|8|9){1}(0|1){1}[0–9]{8})/; // Please fill in the correct 11-digit mobile phone number
        //var phone = /((^+)?\(?(\d{3})?\)?[-]?)?(((\d{3})[-]?(\d{4}))|((\d{3})[-]?(\d{3})[-]?(\d{4}))|((^0)(7|8|9){1}(0|1){1}(\d{1})[-]?(\d{3})[-]?(\d{4}))|((^0)(7|8|9){1}(0|1){1}(\d{8})))$/; // Please fill in the correct 11-digit mobile phone number
        var phone = /^\(?(\d{3})?\)?[- ]?(\d{3})[- ]?(\d{4})$/; // Please fill in the correct 11-digit mobile phone number
        return this.optional(element) || (phone.test(value));
    }, "Please fill in the correct phone number");
    // Phone number verification
    jQuery.validator.addMethod("isTel", function (value, element) {
        var tel = /^(0\d{2,3}-)?\d{7,8}$/g;//Area code 3,4-bit, number 7,8 bits
        return this.optional(element) || (tel.test(value));
    }, "Please fill in the right number");
    // Name verification
    jQuery.validator.addMethod("isName", function (value, element) {
        var name = /^[a-zA-Z0-9]{2,30}$/;
        return this.optional(element) || (name.test(value));
    }, "Name can only use characters, length 2-30 bits");
    // Check the user name
    jQuery.validator.addMethod("isUserName", function (value, element) {
        var userName = /^[a-zA-Z0-9]{2,13}$/;
        return this.optional(element) || (userName).test(value);
    }, 'Please enter numbers or letters, do not include special characters');

    // Check ID card
    jQuery.validator.addMethod("isIdentity", function (value, element) {
        var id = /^(\d{15}$|^\d{18}$|^\d{17}(\d|X))$/;
        return this.optional(element) || (id.test(value));
    }, "Please enter the correct 15 or 18-bit ID number, the end is a capital X");
    // Check the date of birth
    jQuery.validator.addMethod("isBirth", function (value, element) {
        var birth = /^(19|20)\d{2}-(1[0-2]|0?[1-9])-(0?[1-9]|[1-2][0-9]|3[0-1])$/;
        return this.optional(element) || (birth).test(value);
    }, "Example of the date of birth 2000-01-01");
    //Check whether the new and old passwords are the same
    jQuery.validator.addMethod("isdiff", function () {
        var p1 = $("#pwdOld").val();
        var p2 = $("#pwdNew").val();
        if (p1 == p2) {
            return false;
        } else {
            return true;
        }
    });
    //Check the new password and confirm whether the password is the same
    jQuery.validator.addMethod("issame", function () {
        var p3 = $("#confirm_password").val();
        var p4 = $("#pwdNew").val();
        if (p3 == p4) {
            return true;
        } else {
            return false;
        }
    });
    // Check the Basic Information Form
    $("#basicInfoForm").validate({
        errorElement: 'span',
        errorClass: 'help-block error-mes',
        rules: {
            name: {
                required: true,
                isName: true
            },
            sex: "required",
            birth: "required",
            mobile: {
                required: true,
                isPhone: true
            },
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            name: {
                required: "Please enter the name",
                isName: "Name can only be characters"
            },
            sex: {
                required: "Please enter gender"
            },
            birth: {
                required: "Please enter the year of birth"
            },
            mobile: {
                required: "Please enter phone number",
                isPhone: "Please fill in the correct 11-digit number"
            },
            email: {
                required: "please input your email",
                email: "Please fill in the correct mailbox format"
            }
        },

        errorPlacement: function (error, element) {
            element.next().remove();
            element.closest('.gg-formGroup').append(error);
        },

        highlight: function (element) {
            $(element).closest('.gg-formGroup').addClass('has-error has-feedback');
        },
        success: function (label) {
            var el = label.closest('.gg-formGroup').find("input");
            el.next().remove();
            label.closest('.gg-formGroup').removeClass('has-error').addClass("has-feedback has-success");
            label.remove();
        },
        submitHandler: function (form) {
            alert("Saved successfully!");
        }
    });

    // Check the password form
    $("#modifyPwd").validate({
        onfocusout: function (element) { $(element).valid() },
        debug: false, // Represents whether to submit the form directly after passing the verification
        onkeyup: false, // Indicates that the keys are loosened to monitor the verification and verification
        rules: {
            pwdOld: {
                required: true,
                minlength: 6
            },
            pwdNew: {
                required: true,
                minlength: 6,
                isdiff: true,
                //issame:true,
            },
            confirm_password: {
                required: true,
                minlength: 6,
                issame: true,
            }

        },
        messages: {
            pwdOld: {
                required: 'Must-have',
                minlength: $.validator.format('The password length is greater than 6')
            },
            pwdNew: {
                required: 'Must-have',
                minlength: $.validator.format('The password length is greater than 6'),
                isdiff: 'The original password and the new password cannot be repeated',

            },
            confirm_password: {
                required: 'Must-have',
                minlength: $.validator.format('The password length is greater than 6'),
                issame: 'The new password is consistent with confirming the new password',
            }

        },
        errorElement: "mes",
        errorClass: "gg-star",
        errorPlacement: function (error, element) {
            element.closest('.gg-formGroup').append(error);

        }
    });
});