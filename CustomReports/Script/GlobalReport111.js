
$(function () {

    // 显示图形报表
    {
        $("#divGlobalReportHTML111").ready(createChart(strCharts));
        $("#divGlobalReportHTML111").bind("kendo:skinChange", createChart(strCharts));
    }

    // 以图片形式导出报表
    $("#btnSaveAsImage").click(function () {
        var chart = $("#divGlobalReportHTML111").getKendoChart();
        chart.exportImage().done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.png"
            });
        });
    });


});



function createChart(strCharts) {
    $("#divGlobalReportHTML111").kendoChart({
        dataSource: {
            data: eval(strCharts)
        },
        title: {
            text: "饼图报表",
            position: "top" 
        },
        legend: {
            visible: true,
            position: "bottom"              // 设置图例在图形的上方还是下方显示，值可以是top,bottom
        },
        seriesDefaults: {
            labels: {
                visible: true,              // 设置饼图上的数字是否显示
                format: "{0}%",
                background: "transparent",
                template : "#: kendo.toString(value,'n0') #" + ' ' + "(#: kendo.format('{0:P}', percentage) #)"
            }
        },
        series: [{
            type: "pie",
            field: "data",                  // 字典中数据对应的字段名称
            overlay: {
                gradient: "roundedBevel"    // 饼图的样式，值可以是none, roundedBevel, sharpBevel
            }
        }],
        categoryAxis: {
            field: "category"               // 字典中关键字对应的字段名称
        },
        tooltip: {
            visible: true,
            template: "#= category #, #: kendo.format('{0:n0}', value) #" + ' ' + "(#: kendo.format('{0:P}', percentage) #)"
        }
    });
}
