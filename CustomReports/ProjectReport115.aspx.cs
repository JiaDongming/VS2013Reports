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

public partial class ProjectReport115 : Report
{
    private static int MyReportID = 115;
    public string htmlGlobalReportTable115;
    protected string selProjectOptions115 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();

            db.GetDropDownField(dataSet);
            selProjectOptions115 = BindChoices(dataSet.Tables["DropDownFieldsChoice"]);  // 绑定下拉框字段值
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
    public static string RefreshGlobalReport115(string selectedProjectsStr)
    {
        DBL db = new DBL();
        DataSet dataSet;

        dataSet = db.GetGlobalReportsInfo(MyReportID, selectedProjectsStr);

        return getChartReport(dataSet.Tables["ReportTable115"]);
    }


    public static string getChartReport(DataTable dt)
    {
        if (dt == null)
            return String.Empty;
        else
        {
            string strCharts = "[";

            foreach (DataRow dr in dt.Rows)
            {
                strCharts += "{ \"name\" : \"" + dr["BugTypeName"].ToString() + "\", \"data\" : " + dr["BugNo"].ToString() + "},";
            }

            strCharts = strCharts.Substring(0, strCharts.Length - 1);
            strCharts += "]";

            return strCharts;
        }
    }

}