using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Web;



    public enum ReportBuilder_OutputType
    {
        Output_HTML = 0,
        Output_FILE = 1
    }
    public enum REPORT_EXPORT_TYPE
    {
        HTML = -1,
        CSV = 0,
        EXCEL = 1,
        WORLD = 2,
        PDF = 3,
        IMAGE = 4,
    }
    public class ReportBuilder
    {
        public ReportBuilder(ReportBuilder_OutputType outputType)
        {
            m_outputType = outputType;
            m_isForceRefresh = false;
        }

        protected ReportBuilder_OutputType m_outputType = ReportBuilder_OutputType.Output_FILE;

        protected bool m_enableUploadToDocServer = true;
        public bool EnableUploadToDocServer
        {
            get { return m_enableUploadToDocServer; }
            set { m_enableUploadToDocServer = value; }
        }

        protected bool m_enableReportCache = true;
        public bool EnableReportCache
        {
            get { return m_enableReportCache; }
            set { m_enableReportCache = value; }
        }

        protected int m_chartDisplayOption = 0; //0:silverlight, 1:picture
        public int ChartDisplayOption
        {
            get { return m_chartDisplayOption; }
            set { m_chartDisplayOption = value; }
        }

        protected bool m_isForceRefresh = false;
        public bool IsForceRefresh
        {
            get { return m_isForceRefresh; }
            set { m_isForceRefresh = value; }
        }

        public string BuildCustomerSpecialReportCommon(string html, int exportType)
        {
            try
            {
                return SaveToFile(html, exportType);
            }
            catch (System.Exception ex)
            {
                return string.Format("Failed to build report.\r\n {0}", ex.Message);
            }
        }

        protected string SaveToFile(StringBuilder sb, int nExportType)
        {
            return SaveToFile(sb.ToString(), nExportType);
        }
        protected string SaveToFile(string data, int nExportType)
        {
            string strPath = Path.GetTempPath();
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            string strFile = string.Empty;
            switch (nExportType)
            {
                case (int)REPORT_EXPORT_TYPE.WORLD:
                case (int)REPORT_EXPORT_TYPE.EXCEL:
                    strFile = Path.Combine(strPath, string.Format("R_E_{0}.MHT", DateTime.Now.Ticks));
                    SaveToMHT(strFile, data);
                    break;
                case (int)REPORT_EXPORT_TYPE.CSV:
                case (int)REPORT_EXPORT_TYPE.HTML:
                default:
                    strFile = Path.Combine(strPath, string.Format("R_E_{0}.HTML", DateTime.Now.Ticks));
                    using (StreamWriter sw = new StreamWriter(strFile, false, Encoding.Unicode))
                    {
                        sw.Write(data);
                    }
                    break;
            }
            return strFile;
        }

        protected string GetBaseURL()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            int pos = url.IndexOf("?");
            if (pos > 0)
            {
                url = url.Substring(0, pos);
            }

            pos = url.LastIndexOf("/");
            if (pos > 0)
            {
                return url.Substring(0, pos + 1);
            }
            else
            {
                return string.Empty;
            }
        }

        public static void SaveToMHT(string strOutputFile, string data)
        {
            string tmpFileName = string.Format("Export{0}.html", DateTime.Now.Ticks);
            string strPath = Path.GetTempPath();
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string strFilePath = Path.Combine(strPath, tmpFileName); //Server.MapPath(string.Format("./{0}", m_tmpFile));
            using (StreamWriter sw = File.CreateText(strFilePath))
            {
                sw.Write(data);
                sw.Close();
            }
            string url = "file://" + strFilePath; // GetBaseURL() + m_tmpFile;
            CDO.MessageClass message = new CDO.MessageClass();
            ADODB.Stream stream = null;

            try
            {
                message.CreateMHTMLBody(url, CDO.CdoMHTMLFlags.cdoSuppressNone, "", "");
                stream = message.GetStream();
                stream.SaveToFile(strOutputFile, ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
            }
            catch (Exception)
            {
                using (FileStream fs = File.Create(strOutputFile))
                {
                    using (BinaryWriter sw = new BinaryWriter(fs))
                    {
                        sw.Write(new byte[] { 0xFF, 0xFE });
                        sw.Write(System.Text.Encoding.Unicode.GetBytes(data)); //.Unicode. strHTML.ToCharArray()); //Write(strHTML);
                        sw.Close();
                    }
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            if (File.Exists(strFilePath))
            {
                File.Delete(strFilePath);
            }
        }

 }
