
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
                $('#divGlobalReportHTML116').html("");
            }
            else {
                var checkResult = checkSearchCondition();
                if (checkResult != "false") {
                    doReportCall();
                }
            }
        }
    });

    // 以图片形式导出报表
    $("#btnSaveAsImage").click(function () {
        var chart = $("#divGlobalReportHTML116").getKendoChart();
        chart.exportImage().done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.png"
            });
        });
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



function createChart(strCharts) {
    $("#divGlobalReportHTML116").kendoChart({
        dataSource: {
            data: eval(strCharts)
        },
        title: {
            text: "条形图报表"
        },
        legend: {
            visible: true,
            position: "bottom"              // 设置图例在图形的上方还是下方显示，值可以是top,bottom
        },
        seriesDefaults: {
            type: "bar",
            labels: {
                visible: true,              // 设置条形图上的数字是否显示
                background: "transparent"
            }
        },
        series: [{
                name: "任务类型1",               // 图例名称
                field: "data1",                 // 字典中数据对应的字段名称
                color: KDChart.getColor(0)     // 设置bar颜色，读取KDChart.js文件中的函数
            },
            {
                name: "任务类型2",               // 图例名称
                field: "data2",                 // 字典中数据对应的字段名称
                color: KDChart.getColor(1)     // 设置bar颜色，读取KDChart.js文件中的函数
            }
        ],
        categoryAxis: {
            field: "name",                 // 设置纵轴数据读取的字段内容
            majorGridLines: {
                visible: false              // 设置是否显示网格线
            },
            line: {
                visible: false              // 纵轴标题的右边是否显示-
            },
            labels: {
                padding: { top: 5}          // 设置每列标题和条形图底部的高度差
            }
        },
        valueAxis: {
            labels: {
                format: ""              // 设置横轴数据的前后缀，比如format: "{0}%"，format: "${0}"
            },
            line: {
                visible: true           // 轴横标题的上边是否显示|
            },
            axisCrossingValue: 0        // 纵坐标轴的基线设置，大于该数字的条形图向又走，小于该数字的条形图向左走，通常是0
        },
        tooltip: {
            visible: true,              // 设置鼠标放到条形图上是否显示信息以及信息的格式
            format: "",
            template: "#= category #, #= series.name#, #: kendo.format('{0:n0}', value) #"
        }
    });
}


function doReportCall() {

    var selectedProjectsStr = $('#Select1').val();
    var params = "{'selectedProjectsStr':'" + selectedProjectsStr + "'}";

    $.ajax({
        type: "POST",
        url: "ProjectReport116.aspx/RefreshGlobalReport116",
        data: params,
        contentType: "application/json; charset=utf-8",
        beforeSend: function (XMLHttpRequest) {
            $('#divGlobalReportHTML116').html("<h5 style='color:red;'>正在生成报表...</h5>");
        },
        success: function (msg) {
            $('#divGlobalReportHTML116').html("");
            if (msg.d) {
                var strCharts = msg.d.toString();
                $("#divGlobalReportHTML116").ready(createChart(strCharts));
                $("#divGlobalReportHTML116").bind("kendo:skinChange", createChart(strCharts));
            }
        },
        error: function (xhr, msg, e) {
            alert("error");
        }
    });
}
