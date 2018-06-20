
$(function () {

    // 显示图形报表
    {
        $("#divGlobalReportHTML108").ready(createChart(strCharts));
        $("#divGlobalReportHTML108").bind("kendo:skinChange", createChart(strCharts));
    }

    // 以图片形式导出报表
    $("#btnSaveAsImage").click(function () {
        var chart = $("#divGlobalReportHTML108").getKendoChart();
        chart.exportImage().done(function (data) {
            kendo.saveAs({
                dataURI: data,
                fileName: "chart.png"
            });
        });
    });


});



function createChart(strCharts) {
    $("#divGlobalReportHTML108").kendoChart({
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
            name: "任务类型",               // 图例名称
            field: "data",                 // 字典中数据对应的字段名称
            //color: "#44CEF6"               // 设置column颜色
            color: KDChart.getColor(3)     // 设置column颜色，读取KDChart.js文件中的函数
        }],
        categoryAxis: {
            field: "name",                 // 设置横轴数据读取的字段内容
            majorGridLines: {
                visible: false          // 设置是否显示网格线
            },
            line: {
                visible: false
            },
            labels: {
                padding: { top: 5 }     // 设置每列标题和柱状图底部的高度差
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
            template: "#= category #, #: kendo.format('{0:n0}', value) #"     // n2表示保留小数点后2位，还可以是n0,n1,...值
        }
    });
}
