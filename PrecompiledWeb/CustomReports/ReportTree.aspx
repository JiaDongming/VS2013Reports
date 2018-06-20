<%@ page language="C#" autoeventwireup="true" inherits="ReportTree, App_Web_ka2r34zx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报表布局</title>
    <style type="text/css">
        .mainMenu
        {
            width:100%;
            height:40px;
            border-bottom:1px solid #ccc;
            /*position:relative;*/
        }
        .menuReportIcon
        {
            background:url("Images/leftBg1.gif") no-repeat 0 0;
            left:4px;
            width:20px;
            height:40px;
            position:absolute;
        }
        .menuReportText
        {
            position:absolute;
            left:24px;
            right:4px;
            background:url("Images/mainBg.gif") repeat-x 0 0;
            height:28px;
            padding-top:12px;
            cursor:pointer;
            font-weight:bold;
        }
        .ProjectItem,.ProjectItem a
        {
            cursor:pointer;
            margin-bottom:10px;
            font-weight:bold;
            color:#666;
            font-size:14px;
            line-height:1.5em;
        }        
        .ReportItem a
        {
            cursor:pointer;
            margin-bottom:10px;
            font-size:13px;
            line-height:1.8em;
            color:#666;
            font-weight:normal;
        }        
        #globalReportList
        {
            list-style-type:circle;
            padding-left:16px;
        }       
        .highlightItem
        {
            color:#eb8f00 !important;
            font-weight:bold !important;
        }
        ul
        {
            margin-top:4px;
            margin-bottom:4px;
            padding-left:10px;
        }
    </style>
    <script src="Script/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="Script/ReportLeftTree.js" type="text/javascript"></script>
</head>

<body id="leftBody">
  <div id="accordion">
      <div class="mainMenu"  id="globleReport">
          <div class="menuReportIcon"></div>
          <div class="menuReportText highlightItem" onclick="navReport(-1,<%=defaultReportID4Start %>,this)"><a>全局报表</a></div>
      </div>
      <div id="divGlobalReports">
          <ul id="globalReportList">
                <li itemId="101" <%=IfShowReport(-1, 101) ? "" : "style=\"display:none;\""%>><a>测试报表1</a></li>
                <li itemId="102" <%=IfShowReport(-1, 102) ? "" : "style=\"display:none;\""%>><a>测试报表2</a></li>
                <li itemId="103" <%=IfShowReport(-1, 103) ? "" : "style=\"display:none;\""%>><a>测试报表3</a></li>
                <li itemId="104" <%=IfShowReport(-1, 104) ? "" : "style=\"display:none;\""%>><a>测试报表4</a></li>
                <li itemId="105" <%=IfShowReport(-1, 105) ? "" : "style=\"display:none;\""%>><a>测试报表5</a></li>
                <li itemId="106" <%=IfShowReport(-1, 106) ? "" : "style=\"display:none;\""%>><a>测试报表6</a></li>
                <li itemId="107" <%=IfShowReport(-1, 107) ? "" : "style=\"display:none;\""%>><a>测试报表7</a></li>
                <li itemId="108" <%=IfShowReport(-1, 108) ? "" : "style=\"display:none;\""%>><a>Column报表8</a></li>
                <li itemId="109" <%=IfShowReport(-1, 109) ? "" : "style=\"display:none;\""%>><a>Bar报表9</a></li>
                <li itemId="110" <%=IfShowReport(-1, 110) ? "" : "style=\"display:none;\""%>><a>Line报表10</a></li>
                <li itemId="111" <%=IfShowReport(-1, 111) ? "" : "style=\"display:none;\""%>><a>Pie报表11</a></li>
                <li itemId="112" <%=IfShowReport(-1, 112) ? "" : "style=\"display:none;\""%>><a>Column报表12</a></li>
                <li itemId="113" <%=IfShowReport(-1, 113) ? "" : "style=\"display:none;\""%>><a>Column报表13</a></li>
                <li itemId="114" <%=IfShowReport(-1, 114) ? "" : "style=\"display:none;\""%>><a>Column报表14</a></li>
                <li itemId="115" <%=IfShowReport(-1, 115) ? "" : "style=\"display:none;\""%>><a>Bar报表15</a></li>
                <li itemId="116" <%=IfShowReport(-1, 116) ? "" : "style=\"display:none;\""%>><a>Bar报表16</a></li>
                <li itemId="117" <%=IfShowReport(-1, 117) ? "" : "style=\"display:none;\""%>><a>Bar报表17</a></li>
                <li itemId="118" <%=IfShowReport(-1, 118) ? "" : "style=\"display:none;\""%>><a>Line报表18</a></li>
                <li itemId="119" <%=IfShowReport(-1, 119) ? "" : "style=\"display:none;\""%>><a>Line报表19</a></li>
                <li itemId="120" <%=IfShowReport(-1, 120) ? "" : "style=\"display:none;\""%>><a>Column_Line报表20</a></li>
          </ul>
      </div>
</div>
</body>
</html>
