using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;


public partial class DBL
{
    SqlConnection sqlCon;
    DataSet ds;
	public DBL()
	{
        sqlCon = new SqlConnection();
        sqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["TechExcelDBconnection"].ConnectionString;
        ds = new DataSet();
	}


    public DataSet GetGlobalReportsInfo(int ReportID)
    {
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dapt = new SqlDataAdapter();
        try
        {
            sqlCon.Open();
            switch (ReportID)
            {
                case 101:
                    GetScheduleData101(sqlCon, cmd, dapt, ds);
                    break;
                case 108:
                    GetScheduleData108(sqlCon, cmd, dapt, ds);
                    break;
                case 109:
                    GetScheduleData109(sqlCon, cmd, dapt, ds);
                    break;
                case 110:
                    GetScheduleData110(sqlCon, cmd, dapt, ds);
                    break;
                case 111:
                    GetScheduleData111(sqlCon, cmd, dapt, ds);
                    break;
                case 121:
                    GetScheduleData121(sqlCon, cmd, dapt, ds);
                    break;
            }
            return ds;
        }
        finally
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
            cmd = null;
            dapt = null;
        }

    }


    public DataSet GetGlobalReportsInfo(int ReportID, string iFlag)
    {
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dapt = new SqlDataAdapter();
        try
        {
            sqlCon.Open();
            switch (ReportID)
            {
                case 102:
                    GetScheduleData102(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 103:
                    GetScheduleData103(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 104:
                    GetScheduleData104(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 105:
                    GetScheduleData105(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 106:
                    GetScheduleData106(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 107:
                    GetScheduleData107(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 112:
                    GetScheduleData112(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 113:
                    GetScheduleData113(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 114:
                    GetScheduleData114(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 115:
                    GetScheduleData115(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 116:
                    GetScheduleData116(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 117:
                    GetScheduleData117(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 118:
                    GetScheduleData118(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 119:
                    GetScheduleData119(sqlCon, cmd, dapt, ds, iFlag);
                    break;
                case 120:
                    GetScheduleData120(sqlCon, cmd, dapt, ds, iFlag);
                    break;
            }                 
            return ds;
        }
        finally
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
            cmd = null;
            dapt = null;
        }

    }


    // 根据iFlag的值，将字符串根据_，分别存入startDate，endDate
    private void ParseFlag4Schedule(string iFlag, ref DateTime startDate, ref DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(iFlag))
            return;

        string[] p = iFlag.Split('_');

        if (p.Length > 0 && !string.IsNullOrWhiteSpace(p[0]))
        {
            startDate = Convert.ToDateTime(p[0]);
        }

        if (p.Length > 1 && !string.IsNullOrWhiteSpace(p[1]))
        {
            endDate = Convert.ToDateTime(p[1]);
        }
    }


    // 根据iFlag的值，将字符串根据_，分别存入startDate，endDate, choiceNames
    private void ParseFlag4Schedule(string iFlag, ref DateTime startDate, ref DateTime endDate, ref string choiceNames)
    {
        if (string.IsNullOrWhiteSpace(iFlag))
            return;

        string[] p = iFlag.Split('_');

        if (p.Length > 0 && !string.IsNullOrWhiteSpace(p[0]))
        {
            startDate = Convert.ToDateTime(p[0]);
        }

        if (p.Length > 1 && !string.IsNullOrWhiteSpace(p[1]))
        {
            endDate = Convert.ToDateTime(p[1]);
        }

        if (p.Length > 2 && !string.IsNullOrWhiteSpace(p[2]))
        {
            choiceNames = Convert.ToString(p[2]);
        }
    }


    // 根据iFlag的值，将字符串根据_，分别存入startDate，endDate, selectedProjectsStr, sortTypeStr
    private void ParseFlag4Schedule(string iFlag, ref DateTime startDate, ref DateTime endDate, ref string selectedProjectsStr, ref string sortTypeStr)
    {
        if (string.IsNullOrWhiteSpace(iFlag))
            return;

        string[] p = iFlag.Split('_');

        if (p.Length > 0 && !string.IsNullOrWhiteSpace(p[0]))
        {
            startDate = Convert.ToDateTime(p[0]);
        }

        if (p.Length > 1 && !string.IsNullOrWhiteSpace(p[1]))
        {
            endDate = Convert.ToDateTime(p[1]);
        }

        if (p.Length > 2 && !string.IsNullOrWhiteSpace(p[2]))
        {
            selectedProjectsStr = Convert.ToString(p[2]);
        }
        if (p.Length > 3 && !string.IsNullOrWhiteSpace(p[3]))
        {
            sortTypeStr = Convert.ToString(p[3]);
        }
    }


}





