
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
                $('#divGlobalReportHTML113').html("");
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
        var chart = $("#divGlobalReportHTML113").getKendoChart();
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
    $("#divGlobalReportHTML113").kendoChart({
        dataSource: {
            data: eval(strCharts)
        },
        title: {
            text: "柱状图报表"
        },
        legend: {
            visible: true,
            position: "bottom"              // 设置图例在图形的上方还是下方显示，值可以是top,bottom
        },
        seriesDefaults: {
            type: "column",
            labels: {
                visible: true,              // 设置柱状图上的数字是否显示
                background: "transparent"
            }
        },
        series: [{
                name: "任务状态1",               // 图例名称
                field: "data1",                 // 字典中数据对应的字段名称
                //color: "#44CEF6"               // 设置column颜色
                color: KDChart.getColor(3)     // 设置column颜色，读取KDChart.js文件中的函数
            },
            {
                name: "任务状态2",               // 图例名称
                field: "data2",                 // 字典中数据对应的字段名称
                //color: "#44CEF6"               // 设置column颜色
                color: KDChart.getColor(4)     // 设置column颜色，读取KDChart.js文件中的函数
            }
        ],
        categoryAxis: {
            field: "name",                 // 设置横轴数据读取的字段内容
            majorGridLines: {
                visible: false          // 设置是否显示网格线
            },
            line: {
                visible: false
            },
            labels: {
                padding: { top: 5}     // 设置每列标题和柱状图底部的高度差
            }
        },
        valueAxis: {
            labels: {
                format: ""              // 设置纵坐标轴数据的前后缀，比如format: "{0}%"，format: "${0}"
            },
            line: {
                visible: false
            },
            axisCrossingValue: 0        // 纵坐标轴的基线设置，大于该数字的柱状图向上走，小于该数字的柱状图向下走
        },
        tooltip: {
            visible: true,              // 设置鼠标放到柱状图上是否显示信息以及信息的格式
            format: "",
            template: "#= category #, #= series.name#, #: kendo.format('{0:n0}', value) #"     // n2表示保留小数点后2位，还可以是n0,n1,...值
        }
    });
}


function doReportCall() {

    var selectedProjectsStr = $('#Select1').val();
    var params = "{'selectedProjectsStr':'" + selectedProjectsStr + "'}";

    $.ajax({
        type: "POST",
        url: "ProjectReport113.aspx/RefreshGlobalReport113",
        data: params,
        contentType: "application/json; charset=utf-8",
        beforeSend: function (XMLHttpRequest) {
            $('#divGlobalReportHTML113').html("<h5 style='color:red;'>正在生成报表...</h5>");
        },
        success: function (msg) {
            $('#divGlobalReportHTML113').html("");
            if (msg.d) {
                var strCharts = msg.d.toString();
                $("#divGlobalReportHTML113").ready(createChart(strCharts));
                $("#divGlobalReportHTML113").bind("kendo:skinChange", createChart(strCharts));
            }
        },
        error: function (xhr, msg, e) {
            alert("error");
        }
    });
}


