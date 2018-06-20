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

public partial class ProjectReport111 : Report
{
    private static int MyReportID = 111;
    public string htmlGlobalReportTable111;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();

            dataSet = db.GetGlobalReportsInfo(MyReportID);
            getChartReport(dataSet.Tables["ReportTable111"]);
        }
    }


    public void getChartReport(DataTable dt)
    {
        if (dt == null)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("var strCharts='{0}';", ""), true);
        else
        {
            chart pieChart = new chart();
            string strCharts = "[";
            int n = 0;

            foreach (DataRow dr in dt.Rows)
            {
                string color = pieChart.getColor(n++);
                strCharts += "{ \"category\" : \"" + dr["TypeName"].ToString() + "\", \"data\" : " + dr["BugNo"].ToString() + ", \"color\" : \"" + color + "\" },";
            }

            strCharts = strCharts.Substring(0, strCharts.Length - 1);
            strCharts += "]";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("var strCharts='{0}';", strCharts), true);
        }
    }



}