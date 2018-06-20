<%@ page language="C#" autoeventwireup="true" validaterequest="false" inherits="ProjectReport118, App_Web_ka2r34zx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta http-equiv="content-type" content="text/html;chartset=UTF-8" /> 
        <link rel="stylesheet" href="CSS/ui-lightness/jquery-ui-1.10.3.custom.min.css" type="text/css" />
        <link rel="stylesheet" href="CSS/jquery.multiselect.css" type="text/css" />
        <link rel="stylesheet" href="ReportResource/telerik.kendoui/styles/kendo.common.min.css" />
        <link rel="stylesheet" href="ReportResource/telerik.kendoui/styles/kendo.default.min.css" />
        <link rel="stylesheet" href="ReportResource/telerik.kendoui/styles/kendo.default.mobile.min.css" />
        <style type="text/css">
        #projectReport_118
        {
            /*border:1px solid green;*/
            overflow: auto;
        }
        .DivGlobalReportItem118
        {
            width: 100%;
            height: 100%;
            text-align: center;
            border: 1px solid green;
        }
        .globalReportItemHeader118
        {
            background: url("Images/rightBg.gif") repeat-x 0 0;
            height: 30px;
            vertical-align: middle;
            text-align: left;
            font-weight: bold;
            font-size: 14px;
            position: relative;
        }
        .globalHeaderLeft118
        {
            background: url("Images/leftBg.gif") no-repeat 0 0;
            padding: 6px 0 6px 6px;
            color: White;
            position: absolute;
            top: 0;
            left: 0;
            width: 150px;
            height: 17px;
        }
        .globalHeaderMiddle118
        {
            background: url("Images/middleBg.gif") no-repeat 0 0;
            position: absolute;
            top: 0;
            left: 140px;
            width: 40px;
            height: 30px;
        }
        .report_table
        {
            border-collapse: collapse;
            background-color: #ffffff;
            border: 1px solid #2471A2;
            width: 100%;
        }        
        .report_table td, .report_table th
        {
            border: 1px solid #C0C0C0;
            white-space: nowrap;
            text-align: center;
            height: 20px;
        }
        .report_table th
        {
            font-size: 12px;
            background-color: #dee3e7; /*background-color:#1ca2d5;*/
            font-weight: bold;
        }        
        .report_table td
        {
            font-size: 12px;
        }       
        #btnSaveAsImage
        {
            background: url("Images/saveAsImage.gif") no-repeat left top;
            width: 20px;
            height: 16px;
            border: none;
        }          
    </style>
    <script src="Script/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="Script/KDChart.js" type="text/javascript"></script>
    <script src="Script/jquery.multiselect.js" type="text/javascript"></script>
    <script src="Script/GlobalReport118.js" type="text/javascript"></script>
    <script src="ReportResource/telerik.kendoui/js/kendo.all.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hidprojectIDs" runat="server" />
        </div>
        <div id="projectReport_118">
            <table style="width:100%;height:100%; border-collapse:separate; border-spacing:10px;">
                <tr>
                    <td style="width:100%; vertical-align:middle">
                        <div class="DivGlobalReportItem118">
                            <div class="globalReportItemHeader118">
                                <div class="globalHeaderLeft118">设置</div>
                                <div class="globalHeaderMiddle118">&nbsp;</div>
                            </div>
                            <div>
                                <table style="height:35px";>
                                    <tr>
                                        <td style="font-size:15px;">DevTrack项目：</td>  
                                        <td>
                                            <div style="top:8px;right:160px;width:230px; float:left;">
                                                <select id="Select1" name="selLift" multiple="multiple" style="width:225px;">
                                                    <%=selProjectOptions118%>
                                                </select>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width:100%; vertical-align:middle">
                        <div class="DivGlobalReportItem118">
                            <div class="globalReportItemHeader118">
                                <div class="globalHeaderLeft118">Line报表18</div>
                                <div class="globalHeaderMiddle118">&nbsp;</div>
                                <div style="padding-left:180px; padding-top:10px;">
                                    <button type="button" id="btnSaveAsImage" value="" />
                                </div>
                            </div>
                            <div id="divGlobalReportHTML118"></div>
                        </div>
                    </td>               
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
