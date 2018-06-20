using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ReportTree : System.Web.UI.Page
{
    public int defaultReportID4Start
    {
        get { return ReportCustomerInfo.GetDefaultReportID4Start(); }
    }


    public bool IfShowReport(int typeId, int reportId)
    {
        List<string> reportIds = ReportCustomerInfo.GetShowReportIDs();
        string key = typeId.ToString() + "_" + reportId;
        return reportIds.Contains(key);
    }

}