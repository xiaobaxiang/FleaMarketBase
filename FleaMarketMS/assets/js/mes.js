// 对Date的扩展，将 Date 转化为指定格式的String 
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

jQuery(function ($) {
    $.validator.addMethod("chrnum", function (value, element) {

        var chrnum = /^([0-9.]+)$/;

        return this.optional(element) || (chrnum.test(value));

    }, "只能输入数字和小數(0-9,.)");

    $.validator.addMethod("gtZero", function (value, element) {
        var num = /^\d+(\.\d+)?$/;
        var flag = num.test(value);

        if (value == 0) { flag = false; }

        return this.optional(element) || flag;
    }, "只能输入大于0的数字");

    $.validator.addMethod("gtZeroDigits", function (value, element) {
        var num = /^[1-9]\d*$/;

        return this.optional(element) || num.test(value);
    }, "只能输入大于0的整数");


    $.validator.addMethod("ipAddress", function (value, element) {

        var ipAddress = /^192.168.120.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$/;

        return this.optional(element) || (ipAddress.test(value));

    }, "请输入正确的IP格式");

    $.validator.addMethod("tel", function (value, element) {

        var tel = /((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)/;

        return this.optional(element) || (tel.test(value));

    }, "请输入正确的号码(支持手机号码，3-4位区号，7-8位直播号码，1－4位分机号)");

    $.validator.addMethod("zipcode", function (value, element) {

        var zipcode = /^[1-9][0-9]{5}$/;

        return this.optional(element) || (zipcode.test(value));

    }, "请输入正确的邮编格式");

    $.validator.addMethod("email", function (value, element) {

        var email = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;

        return this.optional(element) || (email.test(value));

    }, "请输入正确的邮箱格式");

    $.validator.addMethod("mobilephone", function (value, element) {

        var mobilephone = /^1\d{10}$/;

        return this.optional(element) || (mobilephone.test(value));

    }, "请输入正确的手机号格式");

    $.validator.addMethod("chcekValidate", function (value, element) {

        var res = false;
        if (value == "-1" || value == "")
            res = false;
        else
            res = true;
        return this.optional(element) || (res);

    }, "请选择值");

    $.datepicker.regional['zh-CN'] = {
        closeText: '关闭',
        prevText: '&#x3C;上月',
        nextText: '下月&#x3E;',
        currentText: '今天',
        monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        monthNamesShort: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
        dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
        weekHeader: '周',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        isRTL: false,
        showMonthAfterYear: true,
        onClose: function (selectedDate) {
            if ((new Date($(this).val())).toString() == "Invalid Date") { $(this).val($(this).prop('defaultValue')); }
        }
    };
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);


    //修改validate中date规则，日期格式改为yyyy-MM-dd
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || !/^((((19|20)\d{2})-(0?(1|[3-9])|1[012])-(0?[1-9]|[12]\d|30))|(((19|20)\d{2})-(0?[13578]|1[02])-31)|(((19|20)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|((((19|20)([13579][26]|[2468][048]|0[48]))|(2000))-0?2-29))$/.test(new Date(value).toString());
    };
});

//CLEAR INPUT , SELECT 
function cap_clear(obj_form) {
    $(obj_form + " .form-group input").val("");
    $(obj_form + " .form-group input[type='checkbox']").prop("checked", true);
    $(".alert").html("").hide();
    $(obj_form + " .form-group select option").each(function (index) {
        var KEY = $(this).val();
        if (KEY != "-1" && KEY != "") { $(this).remove(); }
    });

    $(obj_form).validate().resetForm();
}

//CLOSE EDIT PANEL
function cap_close(lstData, TxEdit) {
    $("#" + lstData).show();
    $("#lstMasterDetail").show();
    $("#" + TxEdit).hide();
}

//STATUS INFO DATA
function cap_status(obj_url, table_name, obj_name, SELECT_NO, SELECT_TYPE, STATUS_NO) {
    if (typeof (STATUS_NO) == "undefined") { STATUS_NO = ""; }

    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "TABLE_NAME": table_name,
            "STATUS_NO": STATUS_NO
        },
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value=''>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''> --请选择状态-- </option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    var STATUS_NO = item.STATUS_NO;
                    var STATUS_NAME_CH = item.STATUS_NAME_CH;
                    $(obj_name).append("<option value='" + STATUS_NO + "'>" + STATUS_NAME_CH + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }

            if (SELECT_NO != "") {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}

//TYPE_INFO DATA
function cap_Type(obj_url, table_name, obj_name, SELECT_NO, SELECT_TYPE, TYPE_NO,StatusNO) {
    if (typeof (TYPE_NO) == "undefined" || TYPE_NO == "") { TYPE_NO = ""; }
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "TableName": table_name,
            "TypeNO": TYPE_NO,
            "StatusNO": StatusNO,
            "ORDER_BY": "TypeNO"
        },
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value=''>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''>请选择类别</option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.TypeNO + "'>" + item.TypeName + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }

            if (SELECT_NO != "") {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}


//ROLE NAME DATA
function cap_role_name(obj_url, obj_name, SELECT_NO, SELECT_TYPE, NOT_ROLE_ID) {
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "PAGE_INDEX": -1,
            "PAGE_SIZE": -1,
            "ORDER_BY": "ROLE_ID",
            "NOT_ROLE_ID": NOT_ROLE_ID
        },
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value='-1'>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''> --请选择角色名称-- </option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.ROLE_ID + "'>" + item.ROLE_NAME_CH + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }
            if (SELECT_NO > -1) {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}


//COUNTRY INFO DATA
function cap_country_info(obj_url, obj_name, SELECT_NO, SELECT_TYPE, COUNTRY_NO, STATUS_NO) {
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "PAGE_INDEX": -1,
            "PAGE_SIZE": -1,
            "ORDER_BY": "COUNTRY_NO",
            "COUNTRY_NO": COUNTRY_NO,
            "STATUS_NO": STATUS_NO
        },
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value=''>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''> --请选择国别-- </option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.COUNTRY_NO + "'>" + item.COUNTRY_NAME_CH + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }
            if (SELECT_NO != "") {

                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}

// CITY INFO DATA
function cap_city_info(obj_url, obj_name, SELECT_NO, SELECT_TYPE, COUNTRY_NO, STATUS_NO) {
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "PAGE_INDEX": -1,
            "PAGE_SIZE": -1,
            "ORDER_BY": "",
            "COUNTRY_NO": COUNTRY_NO,
            "STATUS_NO": STATUS_NO
        },
        success: function (data) {
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value=''>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''> --请选择城市-- </option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.CITY_NO + "'>" + item.CITY_NAME_CH + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }
            if (SELECT_NO != "") {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}


//GROUP_INFO DATA
function cap_group(obj_url, obj_name, SELECT_NO, SELECT_TYPE) {
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value=''>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value=''>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value=''> --请选择群组名称-- </option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.GROUP_ID + "'>" + item.GROUP_NAME + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }

            if (SELECT_NO != "") {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}

//GROUP_INFO DATA
function cap_category(obj_url, obj_name, SELECT_NO, SELECT_TYPE) {
    $.ajax({
        type: "POST",
        url: obj_url,
        async: false,
        dataType: "json",
        data: {
            "PAGE_INDEX": -1,
            "PAGE_SIZE": -1,
            "ORDER_BY": "",
            "StatusNO": "A"
        },
        beforeSend: function () { $("#divload").show(); },
        complete: function () { $("#divload").hide(); },
        success: function (data) {
            //Clear Combo Data               
            var vItem = $(obj_name + " option").length;
            $(obj_name + " option").each(function (index) {
                if (index >= 0) $(this).remove();
            });
            //Add Combo Data
            if (SELECT_TYPE == "ALL") { $(obj_name).append("<option value='-1'>ALL</option>"); }
            if (SELECT_TYPE == "NA") { $(obj_name).append("<option value='-1'>N/A</option>"); }
            if (SELECT_TYPE == "CHOOSE") { $(obj_name).append("<option value='-1'>请选择类别</option>"); }
            if (data.msg == "OK") {
                $.each(data.qrydata, function (i, item) {
                    $(obj_name).append("<option value='" + item.CategoryID + "'>" + item.CategoryName + "</option>");
                });
            }
            else {
                cap_alert(data.error_desc);
            }

            if (SELECT_NO != "") {
                $(obj_name).val(SELECT_NO);
            }
            else {
                $(obj_name + " option").eq(0).attr('selected', 'true');
            }
        }
    });
}

//下拉表验证  用上面的那种扩展方法的写法
//function validateSelect(validSelectid, errorVal, errorMsg) {
//    alert(222);
//    var valobj = $(validSelectid);
//    var valGrandParent = valobj.parent().parent();
//    if (valobj.val() === errorVal) {
//        valGrandParent.removeClass("has-success").addClass("has-error");
//        valobj.removeClass("has-success").parent().find("label").remove();
//        valobj.parent().append("<label class=\"has-error help-block\" >"+errorMsg+"</label>");
//    } else {
//        valobj.removeClass("has-error").addClass("has-success");
//        valobj.parent().find("label").remove();
//        return true;
//    }
//    return false;
//}

//jQuery.validator.addMethod("validateSelect", function (value, element, param) {
//    var res = false;
//    cosnole.dir(value);
//    return this.optional(element) || res;
//}, $.validator.format("请选择值"));
