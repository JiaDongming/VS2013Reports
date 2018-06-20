<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" CodeFile="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<frameset cols="220, *" name="mainFrame" id="mainframe" frameborder="yes" framespacing="1" border="2">
    <noframes> 
        <body> 
        很抱歉，馈下使用的浏览器不支援框架功能，请转用新的浏览器。 
        </body> 
    </noframes>
    <frame src="ReportTree.aspx?" name="leftFrame" id="leftFrame" scrolling="auto" noresize="noresize" />
    <frame src="<%=defaultReport4Start %>" name="rightFrame" id="rightFrame" />
</frameset>
</html>


