function KDCHART_STYLE(){
}
KDCHART_STYLE.CHART_COLUMN = 0;
KDCHART_STYLE.CHART_PIE = 1;
KDCHART_STYLE.CHART_LINE = 2;
KDCHART_STYLE.CHART_STACKED_COLUMN = 3;

function KDChart() {
}

KDChart._colors = [
    "#a8d44f", "#4386d8", "#ff9a2e", "#8560b3", "#3cbfe3",
    "#AFD8F8", "#008E8E", "#FABD0F", "#FA6E46", "#A186BE",
    "#cc6600", "#ff7e00", "#2dd277", "#d79f9e", "#c4d6a4",
    "#b7aac8", "#1e4f91", "#85221d", "#627e29", "#3a2351",
    "#187388", "#a95c18", "#2661a7", "#ab2723", "#79a63d",
    "#644682", "#2788b3", "#d27518", "#8196ab", "#b2867b",
    "#95ae86", "#978ba3", "#ff0000", "#00ff00", "#0000ff",
    "#e080c0", "#e08040", "#ff00ff", "#00e080", "#00ffff",
    "#FFFF00", "#7FFF00", "#7B68EE", "#FF7F00", "#BBFFFF",
    "#B452CD"
];

KDChart.getColor = function(index){
    var n = index % KDChart._colors.length;
    return KDChart._colors[n];
}

KDChart.getRendAsName = function(chartStyle){
    switch (chartStyle)
    {
        case KDCHART_STYLE.CHART_PIE:
            return "pie";
        case KDCHART_STYLE.CHART_COLUMN:
            return "column";
        case KDCHART_STYLE.CHART_LINE:
            return "line";
        case KDCHART_STYLE.CHART_STACKED_COLUMN:
            return "column";
    }
    return "pie";
}

KDChart.GetLabelAngle = function(labelCount){
    if (labelCount > 40)
        return -80;
    else if (labelCount > 15)
        return -60;
    else 
        return 0;
}

KDChart.createChart = function (name, title, xtitle, chartStyle, serie, serieColor, isDatePointColorEnabled, isView3DEnabled, isLabelEnabled, isTipEnabled, isShowPercent, w, h) {
    if (typeof (w) == "undefined") {
        isShowPercent = serieColor;
        w = isDatePointColorEnabled;
        h = isView3DEnabled;
        serieColor = "";
        isDatePointColorEnabled = false;
        isView3DEnabled = false;
        isLabelEnabled = true;
        isTipEnabled = false;
    }

    var div = document.getElementById("msgDiv_" + name);
    if (serie != null) {
        if (div != null) div.style.display = "none";

        var objTitle = {};
        if (title != "") {
            objTitle.text = title;
            objTitle.position = "top";
            objTitle.font = "bold 13px arial,sans-serif";
        }

        var ds = [];
        var n = 0;
        if (isLabelEnabled) {
            for (label in serie) {
                if (label != "") {
                    var v = parseFloat(serie[label]);
                    if (isNaN(v)) v = 0;
                    ds.push({
                        "label": label,
                        "value": v,
                        "color": KDChart.getColor(n)
                    });
                }
                ++n;
            }
        }
        else {
            var maxDate = null;
            var minDate = null;
            for (label in serie) {
                if (label != "") {
                    var v = parseFloat(serie[label]);
                    if (isNaN(v)) v = 0;
                    ds.push({
                        "label": label,
                        "value": v,
                        "color": KDChart.getColor(n)
                    });
                }
                ++n;
            }
        }

        var objDataSource = { data: ds };

        var objSerieLable = { visible: true, background: "transparent", template: "#: kendo.toString(value,'n0') #" };
        if (chartStyle == KDCHART_STYLE.CHART_PIE && isShowPercent) {
            objSerieLable.template += "(#: kendo.format('{0:P}', percentage) #)";
        }
        if (chartStyle == 0)
            objSerieLable.position = "outsideEnd"; // "insideEnd";

        var objSerieTooltip = { visible: false, background: "#CC3333", color: "#ffffff", border: { color: "#CC6633" }, template: "#= category #, #: kendo.format('{0:n0}', value) #" };
        if (isTipEnabled) objSerieTooltip.visible = true;
        if (chartStyle == KDCHART_STYLE.CHART_PIE && isShowPercent)
            objSerieTooltip.template += "(#: kendo.format('{0:P}', percentage) #)";

        var objSerie = {
            type: KDChart.getRendAsName(chartStyle),
            field: "value",
            categoryField: "label",
            colorField: "color",
            labels: objSerieLable,
            tooltip: objSerieTooltip,
            gap: 0.3
        };

        var labelCount = ds.length;
        var objCategoryAxis = { field: "label", font: "9px", labels: { template: "#= shortenLabel(value," + labelCount + ")#", visual: function (e) {
            return e.createVisual();
            e.rect.origin.y += 16;
            var vis = e.createVisual();
            var len = vis.children.length;
            for (var i = 0; i < len; ++i) {
                vis.children[i]._position.y += 15;
            }
            return vis;
        }
        }
        };
        if (xtitle != "") objCategoryAxis.title = { text: xtitle };
        if (ds.length > 9)
            objCategoryAxis.labels.rotation = "90";

        $("#Visifire" + name).width(w).height(h).kendoChart({
            dataSource: objDataSource,
            title: objTitle,
            legend: { visible: false, position: "bottom" },
            series: [objSerie],
            categoryAxis: objCategoryAxis,
            render: function (e) {
                return;
            }
        });
    }
    else {
        if (div != null) div.style.display = "";
    }
}

function createShortenFunction(labelCount) {
    return function (value) {
        return shortenLabel(value, labelCount);
    }
}
function shortenLabel(value, labelCount) {
    //if (typeof (labelCount) == "undefined") labelCount = 10;
    var maxLenth = 15;
    //if (labelCount > 9)
    //    maxLenth = 10;

    if (value.length > maxLenth) {
        value = value.substring(0,maxLenth-3);
        value += "...";
    }
    return value;
}

KDChart.createMultiSeriesChart = function (name, title, xtitle, chartStyle, xAxisLabels, dataSeries, w, h, serieColors, serieChartTypes) {
    var div = document.getElementById("msgDiv_" + name);
    if (dataSeries != null) {
        if (div != null) div.style.display = "none";

        var objTitle = {};
        if (title != "") {
            objTitle.text = title;
            objTitle.position = "top";
            objTitle.font = "bold 13px arial,sans-serif";
        }

        var i = 0;
        var labelCount = xAxisLabels.length;
        var objCategoryAxis = { categories: [], labels: { template: "#: shortenLabel(value," + labelCount + ")#"} };
        for (i = 0; i < labelCount; ++i) {
            var label = xAxisLabels[i];
            objCategoryAxis.categories.push(label);
        }
        if (labelCount > 5) {
            if (labelCount >9)
                objCategoryAxis.labels.rotation = "90";
            //else
            //    objCategoryAxis.labels.rotation = "0";
        }

        var objSerieLable = { visible: true, template: "#: value #", format: "{0:n0}", background: "transparent" };
        var objSerieTooltip = { visible: true, background: "#CC3333", color: "#ffffff", border: { color: "#CC6633" }, template: "#: series.name #, #= category #, #: value #" };
        var objSerieDefaults = { type: KDChart.getRendAsName(chartStyle), stack: (chartStyle == 3), tooltip: objSerieTooltip, labels: objSerieLable };
        if (chartStyle == 3) {
            objSerieLable.visual = function (e) {
                if (e.text != "0")
                    return e.createVisual();
            };
        }
        if (chartStyle == 0 || chartStyle == 3)
            objSerieLable.position = "outsideEnd"; // "insideEnd";

        var n = 0;
        var objSeries = [];
        for (serieName in dataSeries) {
            var objSerie = { name: serieName, color: KDChart.getColor(n), data: [] };
            if (serieColors != null && typeof (serieColors) != "undefined" && typeof (serieColors[serieName]) != "undefined") {
                objSerie.color = serieColors[serieName];
            }
            if (serieChartTypes != null && typeof (serieChartTypes) != "undefined" && typeof (serieChartTypes[serieName]) != "undefined") {
                objSerie.type = KDChart.getRendAsName(serieChartTypes[serieName]);
                objSerie.stack = (serieChartTypes[serieName] == 3 ? "true" : "false");
            }
            for (i = 0; i < labelCount; ++i) {
                var label = xAxisLabels[i];
                var yvalue = 0;
                if (dataSeries[serieName].hasOwnProperty(label)) {
                    yvalue = parseFloat(dataSeries[serieName][label]);
                    if (isNaN(yvalue)) yvalue = 0;
                }
                objSerie.data.push(yvalue);
            }
            objSeries.push(objSerie);
            ++n;
        }

        if (xtitle != "") objCategoryAxis.title = { text: xtitle };

        $("#Visifire" + name).width(w).height(h).kendoChart({
            seriesDefaults: objSerieDefaults,
            series: objSeries,
            title: objTitle,
            legend: { visible: true, position: "bottom" },
            categoryAxis: objCategoryAxis,
            gap: 0.3
        });
    }
}

function processStr(str) {
    return str;
    //return str.replace("{", "\\n{");
}