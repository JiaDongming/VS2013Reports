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


    // 单选下拉框内容改变时调用
    $("#Select1").change(function () {
        var checkResult = checkSearchCondition();
        if (checkResult != "false") {
            $("#hidprojectIDs").val($("#Select1").val());
            doReportCall();
        }
    });

});


function checkSearchCondition() {
    var startDateStr = $("#dateTimePicker").val();
    var endDateStr = $("#dateTimePicker2").val();
    var startDate = new Date(startDateStr.replace(/-/g, "/"));
    var endDate = new Date(endDateStr.replace(/-/g, "/"));
    var selectedProjectsStr = $('#Select1').val();

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

    if ((selectedProjectsStr == "") || (selectedProjectsStr == null)) {
        alert("请选择DevTrack项目！");
        dateTimePickerValue = startDateStr;
        dateTimePicker2Value = endDateStr;
        return "false";
    }

    dateTimePickerValue = startDateStr;
    dateTimePicker2Value = endDateStr;

    return "true";
}



function doReportCall() {

    var startDateStr = $("#dateTimePicker").val();
    var endDateStr = $("#dateTimePicker2").val();
    var selectedProjectsStr = $('#Select1').val();

    var params = "{'startDateStr':'" + startDateStr;
    params += "','endDateStr':'" + endDateStr;
    params += "','selectedProjectsStr':'" + selectedProjectsStr;
    params += "'}";

    $.ajax({
        type: "POST",
        url: "ProjectReport106.aspx/RefreshGlobalReport106",
        data: params,
        contentType: "application/json; charset=utf-8",
        beforeSend: function (XMLHttpRequest) {
            $('#divGlobalReportHTML106').html("<h5 style='color:red;'>正在生成报表...</h5>");
        },
        success: function (msg) {
            $('#divGlobalReportHTML106').html("");
            if (msg.d) {
                $('#divGlobalReportHTML106').html(msg.d.toString());
            }
        },
        error: function (xhr, msg, e) {
            alert("error");
        }
    });
}


