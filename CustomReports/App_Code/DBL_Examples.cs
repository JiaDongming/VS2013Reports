using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
using Common;


public partial class DBL
{

    // 获取DevTrack项目名称
    public void GetDropDownField(DataSet ds)
    {
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dapt = new SqlDataAdapter();
        try
        {
            sqlCon.Open();
            GetDropDownFieldSQL(sqlCon, cmd, dapt, ds);
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


    // 获取DevTrack项目名称SQL语句
    private void GetDropDownFieldSQL(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"select ProjectID,ProjectName from Project where ProjectID>0 AND ProjectTypeID=1 AND IsActiveProject=1 and projectid>180");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "DropDownFieldsChoice");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 101 报表使用
    private void GetScheduleData101(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport101(sqlCon, cmd, dapt, ds);
    }


    // 101 报表SQL语句
    private void GetReport101(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID=181");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable101");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 102 报表使用
    private void GetScheduleData102(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport102(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 102 报表SQL语句
    private void GetReport102(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += ")";

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable102");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 103 报表使用
    private void GetScheduleData103(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport103(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 103 报表SQL语句
    private void GetReport103(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID=");
        sqlStr += multiSelectFieldChoice;

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable103");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 104 报表使用
    private void GetScheduleData104(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        ParseFlag4Schedule(strFlag, ref startDate, ref endDate);
        GetReport104(sqlCon, cmd, dapt, ds, startDate, endDate);
    }


    // 104 报表SQL语句
    private void GetReport104(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, DateTime startDate, DateTime endDate)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID=181 AND DATEDIFF(DD,@startDate,Bug.DateCreated)>=0 AND DATEDIFF(DD,@endDate,Bug.DateCreated)<=0");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        // 添加起始日期参数
        SqlParameter pStartDate = new SqlParameter();
        pStartDate.ParameterName = "@startDate";
        pStartDate.SqlDbType = SqlDbType.DateTime;
        pStartDate.Value = startDate;
        cmd.Parameters.Add(pStartDate);

        // 添加结束日期参数
        SqlParameter pEndDate = new SqlParameter();
        pEndDate.ParameterName = "@endDate";
        pEndDate.SqlDbType = SqlDbType.DateTime;
        pEndDate.Value = endDate;
        cmd.Parameters.Add(pEndDate);

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable104");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 105 报表使用
    private void GetScheduleData105(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        string selectedProjectStr = "";

        ParseFlag4Schedule(strFlag, ref startDate, ref endDate, ref selectedProjectStr);
        GetReport105(sqlCon, cmd, dapt, ds, startDate, endDate, selectedProjectStr);
    }


    // 105 报表SQL语句
    private void GetReport105(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, DateTime startDate, DateTime endDate, string selectedProjectStr)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where DATEDIFF(DD,@startDate,Bug.DateCreated)>=0 AND DATEDIFF(DD,@endDate,Bug.DateCreated)<=0 AND Bug.ProjectID IN(");
        sqlStr += selectedProjectStr;
        sqlStr += ")";

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        // 添加起始日期参数
        SqlParameter pStartDate = new SqlParameter();
        pStartDate.ParameterName = "@startDate";
        pStartDate.SqlDbType = SqlDbType.DateTime;
        pStartDate.Value = startDate;
        cmd.Parameters.Add(pStartDate);

        // 添加结束日期参数
        SqlParameter pEndDate = new SqlParameter();
        pEndDate.ParameterName = "@endDate";
        pEndDate.SqlDbType = SqlDbType.DateTime;
        pEndDate.Value = endDate;
        cmd.Parameters.Add(pEndDate);

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable105");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 106 报表使用
    private void GetScheduleData106(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        string selectedProjectStr = "";

        ParseFlag4Schedule(strFlag, ref startDate, ref endDate, ref selectedProjectStr);
        GetReport106(sqlCon, cmd, dapt, ds, startDate, endDate, selectedProjectStr);
    }


    // 106 报表SQL语句
    private void GetReport106(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, DateTime startDate, DateTime endDate, string selectedProjectStr)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where DATEDIFF(DD,@startDate,Bug.DateCreated)>=0 AND DATEDIFF(DD,@endDate,Bug.DateCreated)<=0 AND Bug.ProjectID=");
        sqlStr += selectedProjectStr;

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        // 添加起始日期参数
        SqlParameter pStartDate = new SqlParameter();
        pStartDate.ParameterName = "@startDate";
        pStartDate.SqlDbType = SqlDbType.DateTime;
        pStartDate.Value = startDate;
        cmd.Parameters.Add(pStartDate);

        // 添加结束日期参数
        SqlParameter pEndDate = new SqlParameter();
        pEndDate.ParameterName = "@endDate";
        pEndDate.SqlDbType = SqlDbType.DateTime;
        pEndDate.Value = endDate;
        cmd.Parameters.Add(pEndDate);

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable106");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 107 报表使用
    private void GetScheduleData107(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        string selectedProjectStr = "";
        string sortTypeStr = "";

        ParseFlag4Schedule(strFlag, ref startDate, ref endDate, ref selectedProjectStr, ref sortTypeStr);
        GetReport107(sqlCon, cmd, dapt, ds, startDate, endDate, selectedProjectStr, sortTypeStr);
    }


    // 107 报表SQL语句
    private void GetReport107(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, DateTime startDate, DateTime endDate, string selectedProjectStr, string sortTypeStr)
    {
        string sortStr = "";
        if(sortTypeStr == "0")
            sortStr = "ORDER BY Bug.ProjectID,Bug.BugID ASC";
        else
            sortStr = "ORDER BY Bug.ProjectID,Bug.BugID DESC";

        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Bug.ProjectID,Project.ProjectName,Bug.BugID,Bug.BugTitle,ProgressStatusTypes.ProgressStatusName from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where DATEDIFF(DD,@startDate,Bug.DateCreated)>=0 AND DATEDIFF(DD,@endDate,Bug.DateCreated)<=0 AND Bug.ProjectID IN(");
        sqlStr += selectedProjectStr;
        sqlStr += ") " + sortStr;

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        // 添加起始日期参数
        SqlParameter pStartDate = new SqlParameter();
        pStartDate.ParameterName = "@startDate";
        pStartDate.SqlDbType = SqlDbType.DateTime;
        pStartDate.Value = startDate;
        cmd.Parameters.Add(pStartDate);

        // 添加结束日期参数
        SqlParameter pEndDate = new SqlParameter();
        pEndDate.ParameterName = "@endDate";
        pEndDate.SqlDbType = SqlDbType.DateTime;
        pEndDate.Value = endDate;
        cmd.Parameters.Add(pEndDate);

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable107");
        }
        catch (Exception)
        {
            return;
        }
    }
    

    // 108 报表使用
    private void GetScheduleData108(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport108(sqlCon, cmd, dapt, ds);
    }


    // 108 报表SQL语句
    private void GetReport108(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT Bug.CrntBugTypeID,IssueTypes.TypeName,COUNT(*) BugNo FROM Bug 
                        JOIN IssueTypes ON(Bug.ProjectID=IssueTypes.ProjectID AND Bug.CrntBugTypeID=IssueTypes.TypeID)
                        WHERE Bug.ProjectID=181 
                        GROUP BY Bug.CrntBugTypeID,IssueTypes.TypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable108");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 109 报表使用
    private void GetScheduleData109(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport109(sqlCon, cmd, dapt, ds);
    }


    // 109 报表SQL语句
    private void GetReport109(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT Bug.CrntBugTypeID,IssueTypes.TypeName,COUNT(*) BugNo FROM Bug 
                        JOIN IssueTypes ON(Bug.ProjectID=IssueTypes.ProjectID AND Bug.CrntBugTypeID=IssueTypes.TypeID)
                        WHERE Bug.ProjectID=181 
                        GROUP BY Bug.CrntBugTypeID,IssueTypes.TypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable109");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 110 报表使用
    private void GetScheduleData110(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport110(sqlCon, cmd, dapt, ds);
    }


    // 110 报表SQL语句
    private void GetReport110(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT CONVERT(NVARCHAR(10),DateCreated,23) AS DateCreated,COUNT(*) AS BugCounts FROM Bug
                        WHERE ProjectID=181 AND DATEDIFF(DD,'2017-01-01',DateCreated)>=0 AND DATEDIFF(DD,'2017-06-30',DateCreated)<=0
                        GROUP BY CONVERT(NVARCHAR(10),DateCreated,23)
                        ORDER BY CONVERT(NVARCHAR(10),DateCreated,23)");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable110");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 111 报表使用
    private void GetScheduleData111(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport111(sqlCon, cmd, dapt, ds);
    }


    // 111 报表SQL语句
    private void GetReport111(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT Bug.CrntBugTypeID,IssueTypes.TypeName,COUNT(*) BugNo FROM Bug 
                        JOIN IssueTypes ON(Bug.ProjectID=IssueTypes.ProjectID AND Bug.CrntBugTypeID=IssueTypes.TypeID)
                        WHERE Bug.ProjectID=181 
                        GROUP BY Bug.CrntBugTypeID,IssueTypes.TypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable111");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 112 报表使用
    private void GetScheduleData112(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport112(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 112 报表SQL语句
    private void GetReport112(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Project.ProjectName,ProgressStatusTypes.ProgressStatusName,COUNT(*) AS BugNo from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY Project.ProjectName,ProgressStatusTypes.ProgressStatusName
                        ORDER BY Project.ProjectName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable112");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 113 报表使用
    private void GetScheduleData113(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport113(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 113 报表SQL语句
    private void GetReport113(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Project.ProjectName,ProgressStatusTypes.ProgressStatusName,COUNT(*) AS BugNo,
                        case when COUNT(*) - 3 < 0 then 0 else COUNT(*) - 3 end as SecondNo from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY Project.ProjectName,ProgressStatusTypes.ProgressStatusName
                        ORDER BY Project.ProjectName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable113");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 114 报表使用
    private void GetScheduleData114(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport114(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 114 报表SQL语句
    private void GetReport114(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Project.ProjectName,ProgressStatusTypes.ProgressStatusName,COUNT(*) AS BugNo,
                        case when COUNT(*) - 3 < 0 then 0 else COUNT(*) - 3 end as SecondNo from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY Project.ProjectName,ProgressStatusTypes.ProgressStatusName
                        ORDER BY Project.ProjectName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable114");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 115 报表使用
    private void GetScheduleData115(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport115(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 115 报表SQL语句
    private void GetReport115(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT BugTypes.BugTypeName,COUNT(*) BugNo FROM Bug 
                        JOIN BugTypes ON(Bug.ProjectID=BugTypes.ProjectID AND Bug.CrntBugTypeID=BugTypes.BugTypeID)
                        WHERE Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@") GROUP BY BugTypes.BugTypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable115");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 116 报表使用
    private void GetScheduleData116(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport116(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 116 报表SQL语句
    private void GetReport116(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT BugTypes.BugTypeName,COUNT(*) BugNo,
                        case when COUNT(*) - 3 < 0 then 0 else COUNT(*) - 3 end as SecondNo
                        FROM Bug 
                        JOIN BugTypes ON(Bug.ProjectID=BugTypes.ProjectID AND Bug.CrntBugTypeID=BugTypes.BugTypeID)
                        WHERE Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@") GROUP BY BugTypes.BugTypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable116");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 117 报表使用
    private void GetScheduleData117(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport117(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 117 报表SQL语句
    private void GetReport117(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT BugTypes.BugTypeName,COUNT(*) BugNo,
                        case when COUNT(*) - 3 < 0 then 0 else COUNT(*) - 3 end as SecondNo
                        FROM Bug 
                        JOIN BugTypes ON(Bug.ProjectID=BugTypes.ProjectID AND Bug.CrntBugTypeID=BugTypes.BugTypeID)
                        WHERE Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@") GROUP BY BugTypes.BugTypeName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable117");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 118 报表使用
    private void GetScheduleData118(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport118(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 118 报表SQL语句
    private void GetReport118(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT CONVERT(NVARCHAR(10),DateCreated,23) AS DateCreated,COUNT(*) AS BugCounts FROM Bug
                        WHERE DATEDIFF(DD,'2017-01-01',DateCreated)>=0 AND DATEDIFF(DD,'2017-06-30',DateCreated)<=0
						AND ProjectID IN (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY CONVERT(NVARCHAR(10),DateCreated,23)
                        ORDER BY CONVERT(NVARCHAR(10),DateCreated,23)
                        ");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable118");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 119 报表使用
    private void GetScheduleData119(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport119(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 119 报表SQL语句
    private void GetReport119(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        SELECT CONVERT(NVARCHAR(10),DateCreated,23) AS DateCreated,COUNT(*) AS BugCounts,
                        case when COUNT(*) - 2 < 0 then 0 else COUNT(*) - 2 end as SecondNo
                        FROM Bug
                        WHERE DATEDIFF(DD,'2017-01-01',DateCreated)>=0 AND DATEDIFF(DD,'2017-06-30',DateCreated)<=0
						AND ProjectID IN (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY CONVERT(NVARCHAR(10),DateCreated,23)
                        ORDER BY CONVERT(NVARCHAR(10),DateCreated,23)
                        ");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable119");
        }
        catch (Exception)
        {
            return;
        }
    }


    // 120 报表使用
    private void GetScheduleData120(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string strFlag)
    {
        GetReport120(sqlCon, cmd, dapt, ds, strFlag);
    }


    // 120 报表SQL语句
    private void GetReport120(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds, string multiSelectFieldChoice)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select Project.ProjectName,ProgressStatusTypes.ProgressStatusName,COUNT(*) AS BugNo,
                        cast(cast(count(*)*100 as decimal(10,2))/(cast(count(*) + 50 as decimal(10,2))) as decimal(10,2)) AS BugPercent
                        from Bug
                        join Project on(Bug.ProjectID=Project.ProjectID)
                        join ProgressStatusTypes on(Bug.ProjectID=ProgressStatusTypes.ProjectID and Bug.ProgressStatusID=ProgressStatusTypes.ProgressStatusID)
                        where Bug.ProjectID in (");
        sqlStr += multiSelectFieldChoice;
        sqlStr += string.Format(@")
                        GROUP BY Project.ProjectName,ProgressStatusTypes.ProgressStatusName
                        ORDER BY Project.ProjectName");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable120");
        }
        catch (Exception)
        {
            return;
        }
    }



    // 121 报表使用
    private void GetScheduleData121(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        GetReport121(sqlCon, cmd, dapt, ds);
    }


    // 121 报表SQL语句
    private void GetReport121(SqlConnection sqlCon, SqlCommand cmd, SqlDataAdapter dapt, DataSet ds)
    {
        cmd.CommandType = CommandType.Text;
        string sqlStr = string.Format(@"
                        select SubProject.Title as 标题, (Login.FName+' '+Login.LName) as 负责人 from SubProject left join LogIn on(subproject.currentowner=LogIn.PersonID) 
                         join SubProjectTree on (SubProject.SubProjectID=SubProjectTree.ChildID)
                        where  SubProjectTree.ParentID=0 and SubProjectTree.ProjectID=186");

        cmd.CommandText = sqlStr;
        cmd.Connection = sqlCon;
        cmd.Parameters.Clear();

        dapt.SelectCommand = cmd;
        try
        {
            dapt.SelectCommand.ExecuteNonQuery();
            dapt.Fill(ds, "ReportTable121");
        }
        catch (Exception)
        {
            return;
        }
    }

}