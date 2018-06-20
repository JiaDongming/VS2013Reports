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

public partial class ProjectReport104 : Report
{
    private static int MyReportID = 104;
    public string htmlGlobalReportTable104;
    protected string selProjectOptions104 = "";


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

            string strFlag = string.Format("{0}_{1}", startDate, endDate);
            dataSet = db.GetGlobalReportsInfo(MyReportID, strFlag);
            htmlGlobalReportTable104 = GetReportByHTML(dataSet.Tables["ReportTable104"]);
            
        }
    }



    [WebMethod]
    public static string RefreshGlobalReport104(string startDateStr, string endDateStr)
    {
        DBL db = new DBL();
        DataSet dataSet;
        DateTime startDate = Convert.ToDateTime(startDateStr);
        DateTime endDate = Convert.ToDateTime(endDateStr);
        string strFlag = string.Format("{0}_{1}", startDate, endDate);

        dataSet = db.GetGlobalReportsInfo(MyReportID, strFlag);
        return GetReportByHTML(dataSet.Tables["ReportTable104"]);
    }



    public static string GetReportByHTML(DataTable dt)
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
        DateTime startDate = Convert.ToDateTime(this.dateTimePicker.Value.ToString());
        DateTime endDate = Convert.ToDateTime(this.dateTimePicker2.Value.ToString());
        string strHtml = getReportTableString(startDate, endDate);

        if (strHtml.CompareTo(string.Empty) != 0)
        {
            fullHtml.Append(ReportHtmlStart(4, "测试报表4"));
            fullHtml.Append(strHtml);
            fullHtml.Append(ReportHtmlEnd);
        }

        // 获取系统的临时目录
        ReportBuilder rb = new ReportBuilder(ReportBuilder_OutputType.Output_FILE);
        string filePath = rb.BuildCustomerSpecialReportCommon(fullHtml.ToString(), 1);

        DoExport(filePath, 1);  // 导出报表
    }


    [WebMethod]
    public static string getReportTableString(DateTime startDate, DateTime endDate)
    {
        DBL db = new DBL();
        DataSet dataSet;

        string strFlag = string.Format("{0}_{1}", startDate, endDate);
        dataSet = db.GetGlobalReportsInfo(MyReportID, strFlag);
        DataTable dt = dataSet.Tables["ReportTable104"];

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