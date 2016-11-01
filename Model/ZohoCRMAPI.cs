using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AdminTool.Model
{
    public class ZohoCRMAPI
    {
        private static string zohocrmurl = "https://crm.zoho.com/crm/private/xml/Leads/insertRecords?";
        private static string zohocrmupdateurl = "https://crm.zoho.com/crm/private/xml/Leads/updateRecords?";
        private static string zohocrmSearchurl = "https://crm.zoho.com/crm/private/xml/Leads/getSearchRecords?";

        internal static String APIMethod(string authToken, string xmlData)
        {
            string uri = zohocrmurl;
            /* Append your parameters here */
            string postContent = "scope=crmapi";
            postContent = postContent + "&authtoken=" + authToken;//Give your authtoken
            postContent = postContent + "&duplicateCheck=2";//Update Record if it exists
            postContent = postContent + "&wfTrigger=true";
            postContent = postContent + "&xmlData=" + HttpUtility.UrlEncode(xmlData);
            string result = AccessCRM(uri, postContent);
            return result;
        }

        internal static String UpdateAPIMethod(string authToken, string xmlData, string RecordId)
        {
            string uri = zohocrmupdateurl;
            /* Append your parameters here */
            string postContent = "scope=crmapi";
            postContent = postContent + "&authtoken=" + authToken;//Give your authtoken
            postContent = postContent + "&id=" + RecordId;
            postContent = postContent + "&wfTrigger=true";
            postContent = postContent + "&xmlData=" + HttpUtility.UrlEncode(xmlData);
            string result = AccessCRM(uri, postContent);
            return result;
        }


        internal static String CheckRecord(string authToken, string email)
        {
            string uri = zohocrmSearchurl;
            /* Append your parameters here */
            string postContent = "scope=crmapi";
            postContent = postContent + "&authtoken=" + authToken;//Give your authtoken
            postContent = postContent + "&selectColumns=Leads(Email)&searchCondition=(Email|=|" + email + ")";
            string result = AccessCRM(uri, postContent);
            return result;
        }

        private static string AccessCRM(string url, string postcontent)
        {
            string responseFromServer = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(postcontent);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception)
            { }
            return responseFromServer;
        }



    }
}