using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;
using Common;

public partial class ProjectReport121 : Report
{
    private static int MyReportID = 121;
    public string htmlGlobalReportTable121;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();

            dataSet = db.GetGlobalReportsInfo(MyReportID);
            htmlGlobalReportTable121 = GetReportByHTML(dataSet.Tables["ReportTable121"]);

        }
    }


    public static string GetReportByHTML(DataTable dt)
    {
        if (dt == null)
            return string.Empty;

        //写报表头部
        StringBuilder sbReportHTML = new StringBuilder(@"<table class='report_table' style='width:100%; text-align:left' >");
        sbReportHTML.Append("<tr><th>项目名称</th><th>负责人</th></tr>");
        sbReportHTML.Append("<tbody>");

        foreach (DataRow dr in dt.Rows)
        {
            sbReportHTML.AppendFormat("<tr><td>{0}</td><td>{1}</td>", dr["标题"].ToString(), dr["负责人"].ToString());
        }

        sbReportHTML.AppendFormat("</tbody></table>");

        return sbReportHTML.ToString();
    }


    // 导出报表使用
    public void exportReportToExcel(Object sender, EventArgs e)
    {
        StringBuilder fullHtml = new StringBuilder();
        string strHtml = getReportTableString();

        if (strHtml.CompareTo(string.Empty) != 0)
        {
            fullHtml.Append(ReportHtmlStart(2, "项目报表121"));
            fullHtml.Append(strHtml);
            fullHtml.Append(ReportHtmlEnd);
        }

        // 获取系统的临时目录
        ReportBuilder rb = new ReportBuilder(ReportBuilder_OutputType.Output_FILE);
        string filePath = rb.BuildCustomerSpecialReportCommon(fullHtml.ToString(), 1);

        DoExport(filePath, 1);  // 导出报表
    }


    [WebMethod]
    public static string getReportTableString()
    {
        DBL db = new DBL();
        DataSet dataSet;

        dataSet = db.GetGlobalReportsInfo(MyReportID);
        DataTable dt = dataSet.Tables["ReportTable121"];

        if (dt == null)
        {
            return string.Empty;
        }

        StringBuilder sbReportHTML = new StringBuilder();
        sbReportHTML.Append("<thead>");
        sbReportHTML.Append("<tr><th>项目名称</th><th>负责人</th></tr>");
        sbReportHTML.Append("</thead><tbody>");

        foreach (DataRow dr in dt.Rows)
        {
            sbReportHTML.AppendFormat("<tr><td>{0}</td><td>{1}</td>", dr["标题"].ToString(), dr["负责人"].ToString());
        }

        sbReportHTML.Append("</tbody>");
        return sbReportHTML.ToString();
    }

}