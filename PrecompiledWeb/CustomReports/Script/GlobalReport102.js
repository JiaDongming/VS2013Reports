
$(function () {

    // 多选下拉框内容改变时调用
    $("#Select1").multiselect({
        noneSelectedText: "请选择DevTrack项目...",
        close: function (e, ui) {
            var selProjectObjs = $(this).multiselect("widget").find("input:checked");
            var selProjects = "";
            if (selProjectObjs.length > 0) {
                selProjectObjs.each(function (index, ele) {
                    if (index > 0) selProjects += ","
                    selProjects += ele.value;
                })
            }
            $("#hidprojectIDs").val(selProjects);
            if (selProjects == "") {
                $('#divGlobalReportHTML102').html("");
            }
            else {
                var checkResult = checkSearchCondition();
                if (checkResult != "false") {
                    doReportCall();
                }
            }
        }
    });

});


function checkSearchCondition() {
    var selectedProjectsStr = $('#Select1').val();
    if ((selectedProjectsStr == "") || (selectedProjectsStr == null)) {
        alert("请选择DevTrack项目！");
        return "false";
    }
    return "true";
}


function doReportCall() {

    var selectedProjectsStr = $('#Select1').val();
    var params = "{'selectedProjectsStr':'" + selectedProjectsStr + "'}"; 

    $.ajax({
        type: "POST",
        url: "ProjectReport102.aspx/RefreshGlobalReport102",
        data: params,
        contentType: "application/json; charset=utf-8",
        beforeSend: function (XMLHttpRequest) {
            $('#divGlobalReportHTML102').html("<h5 style='color:red;'>正在生成报表...</h5>");
        },
        success: function (msg) {
            $('#divGlobalReportHTML102').html("");
            if (msg.d) {
                $('#divGlobalReportHTML102').html(msg.d.toString());
            }
        },
        error: function (xhr, msg, e) {
            alert("error");
        }
    });
}


