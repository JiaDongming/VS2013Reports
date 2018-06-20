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

public partial class ProjectReport102 : Report
{
    private static int MyReportID = 102;
    protected string selProjectOptions102 = "";
    public string htmlGlobalReportTable102;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();

            db.GetDropDownField(dataSet);
            selProjectOptions102 = BindChoices(dataSet.Tables["DropDownFieldsChoice"]);  // 绑定下拉框字段值

            htmlGlobalReportTable102 = "";
        }
    }


    // 绑定下拉框字段值
    private string BindChoices(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
            return string.Empty;

        StringBuilder sb = new StringBuilder();

        foreach (DataRow dr in dt.Rows)
        {
            sb.AppendFormat("<option value=\"{0}\">{1}</option>", dr["ProjectID"], dr["ProjectName"]);
        }
        return sb.ToString();
    }


    [WebMethod]
    public static string RefreshGlobalReport102(string selectedProjectsStr)
    {
        DBL db = new DBL();
        DataSet dataSet;

        dataSet = db.GetGlobalReportsInfo(MyReportID, selectedProjectsStr);

        return GetReportByHTML(dataSet.Tables["ReportTable102"]);
    }

    
    private static string GetReportByHTML(DataTable dt)
    {
        if (dt == null)
            return string.Empty;

        //写报表头部
        StringBuilder sbReportHTML = new StringBuilder(@"<table class='report_table' style='width:100%; text-align:left' >");
        sbReportHTML.Append("<tr><th>项目名称</th><th>任务编号</th><th>任务名称</th><th>任务状态</th></tr>");
        sbReportHTML.Append("<tbody>");

        foreach (DataRow dr in dt.Rows)
        {
            sbReportHTML.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", dr["ProjectName"].ToString(), dr["BugID"].ToString(), dr["BugTitle"].ToString(), dr["ProgressStatusName"].ToString());
        }

        sbReportHTML.AppendFormat("</tbody></table>");

        return sbReportHTML.ToString();
    }


    // 导出报表使用
    public void exportReportToExcel(Object sender, EventArgs e)
    {
        StringBuilder fullHtml = new StringBuilder();
        string strHtml = getReportTableString(hidprojectIDs.Value.ToString());

        if (strHtml.CompareTo(string.Empty) != 0)
        {
            fullHtml.Append(ReportHtmlStart(4, "测试报表2"));
            fullHtml.Append(strHtml);
            fullHtml.Append(ReportHtmlEnd);
        }

        // 获取系统的临时目录
        ReportBuilder rb = new ReportBuilder(ReportBuilder_OutputType.Output_FILE);
        string filePath = rb.BuildCustomerSpecialReportCommon(fullHtml.ToString(), 1);

        DoExport(filePath, 1);  // 导出报表
    }


    [WebMethod]
    public static string getReportTableString(string multiSelectFieldValue)
    {
        DBL db = new DBL();
        DataSet dataSet;

        dataSet = db.GetGlobalReportsInfo(MyReportID, multiSelectFieldValue);
        DataTable dt = dataSet.Tables["ReportTable102"];

        if (dt == null)
        {
            return string.Empty;
        }

        StringBuilder sbReportHTML = new StringBuilder();
        sbReportHTML.Append("<thead>");
        sbReportHTML.Append("<tr><th>项目名称</th><th>任务编号</th><th>任务名称</th><th>任务状态</th></tr>");
        sbReportHTML.Append("</thead><tbody>");

        foreach (DataRow dr in dt.Rows)
        {
            sbReportHTML.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", dr["ProjectName"].ToString(), dr["BugID"].ToString(), dr["BugTitle"].ToString(), dr["ProgressStatusName"].ToString());
        }

        sbReportHTML.Append("</tbody>");
        return sbReportHTML.ToString();
    }



}