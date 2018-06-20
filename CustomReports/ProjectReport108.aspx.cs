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

public partial class ProjectReport108 : Report
{
    private static int MyReportID = 108;
    public string htmlGlobalReportTable108;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBL db = new DBL();
            DataSet dataSet = new DataSet();

            dataSet = db.GetGlobalReportsInfo(MyReportID);
            getChartReport(dataSet.Tables["ReportTable108"]);
        }
    }


    public void getChartReport(DataTable dt)
    {
        if (dt == null)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("var strCharts='{0}';", ""), true);
        else
        {
            string strCharts = "[";

            foreach (DataRow dr in dt.Rows)
            {
                strCharts += "{ \"name\" : \"" + dr["TypeName"].ToString() + "\", \"data\" : " + dr["BugNo"].ToString() + "},";
            }

            strCharts = strCharts.Substring(0, strCharts.Length - 1);
            strCharts += "]";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "", string.Format("var strCharts='{0}';", strCharts), true);
        }
    }

}