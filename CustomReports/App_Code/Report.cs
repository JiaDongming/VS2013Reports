using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;


public class Report : System.Web.UI.Page
{
    private string _reportHtmlStart = string.Empty;

    public Report()
	{

	}

    protected string ReportHtmlStart(int reportColumns, string reportTitle)
    {
        if (string.IsNullOrEmpty(_reportHtmlStart))
        {

            StringBuilder sb = new StringBuilder("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head><title></title><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />");
            sb.Append("<style type=\"text/css\">");
            sb.Append(@" .report_title");
            sb.Append(@" {");
            sb.Append(@" font-size:1.2em;");
            sb.Append(@" text-align:center;");
            sb.Append(@" font-weight:bold;");
            sb.Append(@" padding:6px;");
            sb.Append(@" position:absolute;");
            sb.Append(@" left:70px;");
            sb.Append(@" right:70px;");
            sb.Append(@" }");

            sb.Append(@" .report_title td .report_title th");
            sb.Append(@" {");
            sb.Append(@" font-size:1.2em;");
            sb.Append(@" text-align:center;");
            sb.Append(@" vertical-align:middle;");
            sb.Append(@" font-weight:bold;");
            sb.Append(@" padding:6px;");
            sb.Append(@" }");

            sb.Append(@" .report_subtitle");
            sb.Append(@" {");
            sb.Append(@" font-size:0.8em;");
            sb.Append(@" } ");

            sb.Append(@" .report_table");
            sb.Append(@" {");
            sb.Append(@" border-collapse:collapse;");
            sb.Append(@" background-color:#ffffff;");
            sb.Append(@" border:1px solid #2471A2;");
            sb.Append(@" width:100%;");
            sb.Append(@" }");

            sb.Append(@" .report_table td, .report_table th");
            sb.Append(@" {");
            sb.Append(@" border:1px solid #C0C0C0;");
            sb.Append(@" padding:3px 7px 2px 7px;");
            sb.Append(@" white-space: nowrap;");
            sb.Append(@" }");
            sb.Append(@" .report_table th");
            sb.Append(@" {");
            sb.Append(@" font-size:13px;");
            sb.Append(@" text-align:center;");
            sb.Append(@" vertical-align:middle;");
            sb.Append(@" height:21px;");
            sb.Append(@" }");
            sb.Append(@" .report_table td");
            sb.Append(@" {");
            sb.Append(@" font-size: 12px; ");
            sb.Append(@" text-align:center;");
            sb.Append(@" vertical-align:middle;");
            sb.Append(@" }");
            sb.Append(@" #divQueryContent");
            sb.Append(@" {");
            sb.Append(@" text-align:center;");
            sb.Append(@" padding:2px;");
            sb.Append(@" margin-bottom:8px;");
            sb.Append(@" }");
            sb.Append(@" .titleContainer");
            sb.Append(@" {");
            sb.Append(@" position:relative;");
            sb.Append(@" height:32px;");
            sb.Append(@" border:1px solid green; ");
            sb.Append(@" background-color:#eee;");
            sb.Append(@" margin-bottom:8px;");
            sb.Append(@" text-align:center;");
            sb.Append(@" } ");
            sb.Append(@" </style></head>");
            sb.Append("<body>");
            sb.Append(" <div style=\"width:100%\">");
            sb.Append(" <div class=\"titleContainer\">");
            sb.AppendFormat(" <table class=\"report_title\"><tr><th stype=\"text-align: center;\" colspan=\"{0}\">{1}</th></tr></table>", reportColumns.ToString(), reportTitle);
            sb.Append(" </div>");
            sb.Append(" <div id=\"divReportContent\">");
            sb.Append(" <table id=\"reportTable\" class=\"report_table\" border=\"1\">");
            _reportHtmlStart = sb.ToString();
        }
        return _reportHtmlStart;
    }


    protected string ReportHtmlEnd { get { return "</table></div></div></body></html>"; } }

    protected void DoExport(string filePath, int exportType)
    {
        if (exportType == 0)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=report_customer_{0}.csv", "1"));
        }
        else if (exportType == 1)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.ms-excel";
            string fileNameSubfix = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=report_customer_{0}.xls", fileNameSubfix));
        }
        else if (exportType == 2)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=report_customer_{0}.doc", "1"));
        }
        else if (exportType == 3)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=report_customer_{0}.pdf", "1"));
        }
        else if (exportType == 4)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "image/jpeg";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=report_customer_{0}.jpg", "1"));
        }

        if (File.Exists(filePath))
        {
            if (exportType == 0) // csv can't support unicode
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.Unicode))
                {
                    Response.BinaryWrite(System.Text.Encoding.Default.GetBytes(reader.ReadToEnd()));
                }
            }
            else if (exportType == 1 || exportType == 2 || exportType == 3 || exportType == 4)  //Word, Excel, PDF, JPG
            {
                using (BinaryReader reader = new BinaryReader(File.OpenRead(filePath)))
                {
                    bool isReadToEnd = false;
                    while (!isReadToEnd)
                    {
                        byte[] data = reader.ReadBytes(50 * 1024);
                        isReadToEnd = (data.Length <= 0);
                        if (data.Length > 0)
                        {
                            Response.BinaryWrite(data);
                        }
                    }
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.Unicode))
                {
                    Response.BinaryWrite(new Byte[] { 0xFF, 0xFE });
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetBytes(reader.ReadToEnd()));
                }
            }
            File.Delete(filePath);
        }
        Response.Flush();
        Response.End();
    }


}