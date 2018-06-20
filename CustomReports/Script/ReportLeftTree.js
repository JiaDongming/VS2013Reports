var currentSelReport;
var needToggleGlobleContainer = false;
var isLoading = false;

function navReport(projectID, reportID, target, typeid) {
    if (typeof (typeid) == "undefined") typeid = -1;
    setProjectSelStatus(projectID, target);
    var url;
    if (projectID == -1) {
        if (target.tagName == "DIV" && needToggleGlobleContainer) {
            toggleSubMenuVisible("#divGlobalReports");
            return;
        }
        if (currentSelReport == projectID + "_" + reportID)
            return;
        if (reportID == 0)
            url = "Default.aspx";

        if (reportID == 10000)
            url = "null.htm";

        //"ProjectReport" + reportID+".aspx"
        if (reportID == 101)
            url = "ProjectReport101.aspx";
        if (reportID == 102)
            url = "ProjectReport102.aspx";
        if (reportID == 103)
            url = "ProjectReport103.aspx";
        if (reportID == 104)
            url = "ProjectReport104.aspx";
        if (reportID == 105)
            url = "ProjectReport105.aspx";
        if (reportID == 106)
            url = "ProjectReport106.aspx";
        if (reportID == 107)
            url = "ProjectReport107.aspx";
        if (reportID == 108)
            url = "ProjectReport108.aspx";
        if (reportID == 109)
            url = "ProjectReport109.aspx";
        if (reportID == 110)
            url = "ProjectReport110.aspx";
        if (reportID == 111)
            url = "ProjectReport111.aspx";
        if (reportID == 112)
            url = "ProjectReport112.aspx";
        if (reportID == 113)
            url = "ProjectReport113.aspx";
        if (reportID == 114)
            url = "ProjectReport114.aspx";
        if (reportID == 115)
            url = "ProjectReport115.aspx";
        if (reportID == 116)
            url = "ProjectReport116.aspx";
        if (reportID == 117)
            url = "ProjectReport117.aspx";
        if (reportID == 118)
            url = "ProjectReport118.aspx";
        if (reportID == 119)
            url = "ProjectReport119.aspx";
        if (reportID == 120)
            url = "ProjectReport120.aspx";
        if (reportID == 121)
            url = "ProjectReport121.aspx";

    }

    url += "?reportID=" + reportID;
    url += "&projectID=" + projectID;

    setTimeout(function () {
        parent.rightFrame.location = url;
        isLoading = true;
    }, 400);

    currentSelReport = projectID + "_" + reportID;

    if (currentSelReport == "-1_0")
        needToggleGlobleContainer = true;
    else
        needToggleGlobleContainer = false;
}

function setProjectSelStatus(projectID, target) {

    $(".highlightItem").removeClass("highlightItem");
    setTimeout(function () {
        if (target.tagName == undefined && typeof target.addClass == "function")
            target.addClass("highlightItem");

        if (target.tagName == "A") {
            if (!$(target).hasClass("highlightItem"))
                $(target).addClass("highlightItem");
        }
        else {
            if (!$(target).find("a").hasClass("highlightItem"))
                $(target).find("a").addClass("highlightItem");
        }
    }, 200);
}

function toggleSubMenuVisible(target) {
    if ($(target).css("display") == "block" || $(target).css("display") == "")
        $(target).slideUp();
    else
        $(target).slideDown();
}

$(function () {

    $("#globalReportList li").addClass("ReportItem").each(function (i, e) {
        $(e).bind("click", function () {
            var reportId = $(e).attr("itemId") || i + 1;
            navReport(-1, reportId, e);
        });
    });

});


