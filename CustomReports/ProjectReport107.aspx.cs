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

public partial class ProjectReport107 : Report
{
    private static int MyReportID = 107;
    protected string selProjectOptions107 = "";
    public string htmlGlobalReportTable107;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker.Value = startDate.ToShortDateString();
            dateTimePicker2.Value = endDate.ToShortDateString();
            hidOrderBy.Value = listType.Items[0].Value.ToString();

            db.GetDropDownField(dataSet);
            selProjectOptions107 = BindChoices(dataSet.Tables["DropDownFieldsChoice"]);
            htmlGlobalReportTable107 = "";
        }
    }


    [WebMethod]
    public static string RefreshGlobalReport107(string startDateStr, string endDateStr, string selectedProjectsStr, string sortTypeStr)
    {
        DBL db = new DBL();
        DataSet dataSet;
        DateTime startDate = Convert.ToDateTime(startDateStr);
        DateTime endDate = Convert.ToDateTime(endDateStr);

        string strFlag = string.Format("{0}_{1}_{2}_{3}", startDate, endDate, selectedProjectsStr, sortTypeStr);
        dataSet = db.GetGlobalReportsInfo(MyReportID, strFlag);
        return GetReportByHTML(dataSet.Tables["ReportTable107"]);
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
        string selectedProjectsStr = hidprojectIDs.Value.ToString();
        string sortTypeStr = hidOrderBy.Value.ToString();
        DateTime startDate = Convert.ToDateTime(this.dateTimePicker.Value.ToString());
        DateTime endDate = Convert.ToDateTime(this.dateTimePicker2.Value.ToString());
        StringBuilder fullHtml = new StringBuilder();
        string strHtml = getReportTableString(startDate, endDate, selectedProjectsStr, sortTypeStr);

        if (strHtml.CompareTo(string.Empty) != 0)
        {
            fullHtml.Append(ReportHtmlStart(4, "测试报表7")); 
            fullHtml.Append(strHtml);
            fullHtml.Append(ReportHtmlEnd);
        }

        // 获取系统的临时目录
        ReportBuilder rb = new ReportBuilder(ReportBuilder_OutputType.Output_FILE);
        string filePath = rb.BuildCustomerSpecialReportCommon(fullHtml.ToString(), 1);

        DoExport(filePath, 1);  // 导出报表
    }


    [WebMethod]
    public static string getReportTableString(DateTime startDate, DateTime endDate, string selectedProjectsStr, string sortTypeStr)
    {
        DBL db = new DBL();
        DataSet dataSet;

        string strFlag = string.Format("{0}_{1}_{2}_{3}", startDate, endDate, selectedProjectsStr, sortTypeStr);
        dataSet = db.GetGlobalReportsInfo(MyReportID, strFlag);
        DataTable dt = dataSet.Tables["ReportTable107"];

        if (dt == null)
        {
            return string.Empty;
        }

        //写报表头部
        StringBuilder sbReportHTML = new StringBuilder();
        sbReportHTML.Append("<tr><th>项目名称</th><th>任务编号</th><th>任务名称</th><th>任务状态</th></tr>");
        sbReportHTML.Append("<tbody>");

        foreach (DataRow dr in dt.Rows)
        {
            sbReportHTML.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", dr["ProjectName"].ToString(), dr["BugID"].ToString(), dr["BugTitle"].ToString(), dr["ProgressStatusName"].ToString());
        }

        sbReportHTML.AppendFormat("</tbody></table>");
        return sbReportHTML.ToString();
    }



}