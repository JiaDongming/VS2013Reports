var dateTimePickerValue;
var dateTimePicker2Value;

$(function () {

    {
        dateTimePickerValue = $("#dateTimePicker").val();
        dateTimePicker2Value = $("#dateTimePicker2").val();
    }

    $("#dateTimePicker").change(function () {
        var checkResult = checkSearchCondition();
        if (checkResult != "false") {
            doReportCall();
        }
    });

    $("#dateTimePicker2").change(function () {
        var checkResult = checkSearchCondition();
        if (checkResult != "false") {
            doReportCall();
        }
    });

});


function checkSearchCondition() {
    var startDateStr = $("#dateTimePicker").val();
    var endDateStr = $("#dateTimePicker2").val();
    var startDate = new Date(startDateStr.replace(/-/g, "/"));
    var endDate = new Date(endDateStr.replace(/-/g, "/"));

    if (startDate > endDate) {
        alert("开始时间不能超过终止时间，请重新选择。");
        $("#dateTimePicker").val(dateTimePickerValue);
        $("#dateTimePicker2").val(dateTimePicker2Value);
        return "false";
    }
    if (startDateStr == '') {
        alert("开始时间需要填写");
        return "false";
    }
    if (endDateStr == '') {
        alert("结束时间需要填写");
        return "false";
    }

    dateTimePickerValue = startDateStr;
    dateTimePicker2Value = endDateStr;

    return "true";
}



function doReportCall() {

    var startDateStr = $("#dateTimePicker").val();
    var endDateStr = $("#dateTimePicker2").val();

    var params = "{'startDateStr':'" + startDateStr;
    params += "','endDateStr':'" + endDateStr;
    params += "'}";

    $.ajax({
        type: "POST",
        url: "ProjectReport104.aspx/RefreshGlobalReport104",
        data: params,
        contentType: "application/json; charset=utf-8",
        beforeSend: function (XMLHttpRequest) {
            $('#divGlobalReportHTML104').html("<h5 style='color:red;'>正在生成报表...</h5>");
        },
        success: function (msg) {
            $('#divGlobalReportHTML104').html("");
            if (msg.d) {
                $('#divGlobalReportHTML104').html(msg.d.toString());
            }
        },
        error: function (xhr, msg, e) {
            alert("error");
        }
    });
}

