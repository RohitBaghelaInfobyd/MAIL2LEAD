using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AdminTool.SMSService
{
    public class Zoho
    {
        #region Declaration
        string ServiceURL = "https://crm.zoho.com/crm/private/xml/";
        DateTime dtlastSync = DateTime.Now;
        TimeZoneInfo timeZoneinfo = null;
        #endregion
        #region Enums
        public enum MessageStatus
        {
            Pending,
            Sent,
            Failed,
            Received
        }
        #endregion
        #region Constructor
        public Zoho()
        {
            string sTemp = "05/14/2016 00:00:00";
            string sTimeZone = "(UTC-07:00) Mountain Time (US & Canada)";

            List<TimeZoneInfo> listTimeZone = TimeZoneInfo.GetSystemTimeZones().ToList();
            foreach (TimeZoneInfo tz in listTimeZone)
            {
                if (tz.DisplayName == sTimeZone)
                    timeZoneinfo = tz;
            }
            if (timeZoneinfo == null)
                timeZoneinfo = TimeZoneInfo.Local;

        }
        public Zoho(string AuthToken)
        {
            this.AuthToken = AuthToken;
            string sTemp = "05/14/2016 00:00:00";
            string sTimeZone = "(UTC-07:00) Mountain Time (US & Canada)";

            List<TimeZoneInfo> listTimeZone = TimeZoneInfo.GetSystemTimeZones().ToList();
            foreach (TimeZoneInfo tz in listTimeZone)
            {
                if (tz.DisplayName == sTimeZone)
                    timeZoneinfo = tz;
            }
            if (timeZoneinfo == null)
                timeZoneinfo = TimeZoneInfo.Local;

        }
        #endregion
        #region Properties
        public string AuthToken { private get; set; }
        #endregion
        #region Methods
        public DataSet GetSMS()
        {
            DataSet result = null;
            try
            {
                dtlastSync = DateTime.UtcNow;
                string uri = ServiceURL + "CustomModule2/getRecords?";
                string postContent = "scope=crmapi";
                postContent = postContent + "&authtoken=" + this.AuthToken;
                postContent = postContent + "&newFormat=1&selectColumns=All&lastModifiedTime=" + dtlastSync.ToString("yyyy-MM-dd HH:mm:ss");
                string xmlData = AccessCRM(uri, postContent);
                result = new DataSet();
                using (StringReader theReader = new StringReader(xmlData))
                {
                    result.ReadXml(theReader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
            }
            return result;
        }

        public DataSet GetLeadInfo(string LeadId)
        {
            DataSet result = null;
            try
            {
                string uri = ServiceURL + "Leads/getRecordById?";
                string postContent = "scope=crmapi";
                postContent = postContent + "&authtoken=" + this.AuthToken;
                postContent = postContent + "&newFormat=1&selectColumns=Leads(First Name,Last Name,Email,Phone)&id=" + LeadId;
                string xmlData = AccessCRM(uri, postContent);
                result = new DataSet();
                using (StringReader theReader = new StringReader(xmlData))
                {
                    result.ReadXml(theReader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
            }
            return result;
        }

        public DataSet GetContactInfo(string ContactId)
        {
            DataSet result = null;
            try
            {
                string uri = ServiceURL + "Contacts/getRecordById?";
                string postContent = "scope=crmapi";
                postContent = postContent + "&authtoken=" + this.AuthToken;
                postContent = postContent + "&newFormat=1&selectColumns=Contacts(First Name,Last Name,Email,Phone)&id=" + ContactId;
                string xmlData = AccessCRM(uri, postContent);
                result = new DataSet();
                using (StringReader theReader = new StringReader(xmlData))
                {
                    result.ReadXml(theReader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
            }
            return result;
        }

        public int InsertSMS(int ModuleType, string ModuleId, string From, string Msg, DateTime dtRcv)
        {
            string sTmpDataStr1 = string.Empty;
            string CRLF = "\r\n";

            sTmpDataStr1 = "<CustomModule2>" + CRLF;
            sTmpDataStr1 += "<row no=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + ">" + CRLF;
            sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Subject" + Convert.ToChar(34) + ">" + From + "</FL>" + CRLF;
            //sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "From" + Convert.ToChar(34) + ">" + From + "</FL>" + CRLF;
            sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Message" + Convert.ToChar(34) + ">" + Msg + "</FL>" + CRLF;

            if (ModuleType == 1)//Lead
                sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Lead_ID" + Convert.ToChar(34) + ">" + ModuleId + "</FL>" + CRLF;
            else if (ModuleType == 2)//Client
                sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Client_ID" + Convert.ToChar(34) + ">" + ModuleId + "</FL>" + CRLF;

            sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Status" + Convert.ToChar(34) + ">Received</FL>" + CRLF;
            sTmpDataStr1 += "</row>" + CRLF;
            sTmpDataStr1 += "</CustomModule2>";

            string uri = ServiceURL + "CustomModule2/insertRecords?";
            string postContent = "scope=crmapi";
            postContent = postContent + "&authtoken=" + this.AuthToken;
            postContent = postContent + "&xmlData=" + HttpUtility.UrlEncode(sTmpDataStr1);
            string sRes = AccessCRM(uri, postContent);
            if (!string.IsNullOrEmpty(sRes))
            {
                if (sRes.IndexOf("added successfully") > 0)
                {
                    return 1;
                }
            }
            return 2;
        }

        public int UpdateStatus(string SMSId, MessageStatus status)
        {
            string sTmpDataStr1 = string.Empty;
            string CRLF = "\r\n";

            sTmpDataStr1 = "<CustomModule2>" + CRLF;
            sTmpDataStr1 += "<row no=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + ">" + CRLF;
            sTmpDataStr1 += "<FL val=" + Convert.ToChar(34) + "Status" + Convert.ToChar(34) + ">" + Convert.ToString(status) + "</FL>" + CRLF;
            sTmpDataStr1 += "</row>" + CRLF;
            sTmpDataStr1 += "</CustomModule2>";

            string uri = ServiceURL + "CustomModule2/updateRecords?";
            string postContent = "scope=crmapi";
            postContent = postContent + "&authtoken=" + this.AuthToken;
            postContent = postContent + "&id=" + SMSId;
            postContent = postContent + "&xmlData=" + HttpUtility.UrlEncode(sTmpDataStr1);
            string sRes = AccessCRM(uri, postContent);
            if (!string.IsNullOrEmpty(sRes))
            {
                if (sRes.IndexOf("updated successfully") > 0)
                {
                    return 1;
                }
            }
            return 2;
        }

        private string AccessCRM(string url, string postcontent)
        {
            string responseFromServer = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(postcontent);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                using (var dataStream = request.GetRequestStream())
                    dataStream.Write(byteArray, 0, byteArray.Length);
                WebResponse response = request.GetResponse();
                using (var dataStream = new StreamReader(response.GetResponseStream()))
                    responseFromServer = dataStream.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
            }
            return responseFromServer;
        }

        #endregion
    }
}