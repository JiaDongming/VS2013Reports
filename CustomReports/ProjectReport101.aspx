<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectReport101.aspx.cs" ValidateRequest="false" Inherits="ProjectReport101" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <meta http-equiv="content-type" content="text/html;chartset=UTF-8" /> 
        <link rel="stylesheet" href="CSS/ui-lightness/jquery-ui-1.10.3.custom.min.css" type="text/css" />
        <style type="text/css">
        #projectReport_101
        {
            /*border:1px solid green;*/
            overflow: auto;
        }
        .DivGlobalReportItem101
        {
            width: 100%;
            height: 100%;
            text-align: center;
            border: 1px solid green;
        }
        .globalReportItemHeader101
        {
            background: url("Images/rightBg.gif") repeat-x 0 0;
            height: 30px;
            vertical-align: middle;
            text-align: left;
            font-weight: bold;
            font-size: 14px;
            position: relative;
        }
        .globalHeaderLeft101
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
        .globalHeaderMiddle101
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
    </style>
    <script src="Script/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="Script/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="projectReport_101">
            <table style="width:100%;height:100%; border-collapse:separate; border-spacing:10px;">
                <tr>
                    <td style="width:100%; vertical-align:middle">
                        <div class="DivGlobalReportItem101">
                            <div class="globalReportItemHeader101">
                                <div class="globalHeaderLeft101">测试报表1</div>
                                <div class="globalHeaderMiddle101">&nbsp;</div>
                                <div style="padding-left:180px; padding-top:10px;">
                                    <asp:ImageButton  ImageAlign="Left" ImageUrl="Images/toExcel.gif" ID="btnExport" OnClick="exportReportToExcel" runat="server" ToolTip="导出Excel"  />
                                </div>
                            </div>
                            <div id="divGlobalReportHTML101"><%=htmlGlobalReportTable101%></div>
                        </div>
                    </td>               
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
