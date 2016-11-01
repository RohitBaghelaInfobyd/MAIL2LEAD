using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AdminTool.SMSService
{
    public class RingCentral
    {
        #region Declaration
        #endregion
        #region Constructor
        public RingCentral()
        {
            this.AccessToken = string.Empty;
            this.RefreshedToken = string.Empty;
            this.TokenType = string.Empty;
            this.RefreshTokenExpireDT = Convert.ToDateTime("01/01/1900");
            this.AccessTokenExpireDT = Convert.ToDateTime("01/01/1900");
        }
        public RingCentral(string ServiceURL, string AppKey, string AppSecret, string UserId, string Password)
        {
            this.ServiceURL = ServiceURL;
            this.App_Key = AppKey;
            this.App_Secret = AppSecret;
            this.UserId = UserId;
            this.Password = Password;

            this.AccessToken = string.Empty;
            this.RefreshedToken = string.Empty;
            this.TokenType = string.Empty;
            this.RefreshTokenExpireDT = Convert.ToDateTime("01/01/1900");
            this.AccessTokenExpireDT = Convert.ToDateTime("01/01/1900");
        }
        #endregion
        #region Properties
        public string ServiceURL { get; set; }
        public string App_Key { get; set; }
        public string App_Secret { get; set; }
        public string UserId { private get; set; }
        public string Password { private get; set; }
        public string Extension { private get; set; }

        private string AccessToken { get; set; }
        private string RefreshedToken { get; set; }
        private string TokenType { get; set; }
        private DateTime AccessTokenExpireDT { get; set; }
        private DateTime RefreshTokenExpireDT { get; set; }

        #endregion
        #region Methods
        public bool GetAccess()
        {
            if (!string.IsNullOrEmpty(this.AccessToken) && this.AccessTokenExpireDT > DateTime.Now) return true;

            this.AccessToken = string.Empty;
            this.RefreshedToken = string.Empty;
            this.TokenType = string.Empty;
            this.RefreshTokenExpireDT = Convert.ToDateTime("01/01/1900");
            this.AccessTokenExpireDT = Convert.ToDateTime("01/01/1900");

            string sResponse = string.Empty;
            string Data = string.Empty;

            string sURL = ServiceURL.TrimEnd('/') + "/restapi/oauth/token";
            var httpWebRequest = (System.Net.HttpWebRequest)WebRequest.Create(sURL);
            httpWebRequest.Headers.Add("Authorization", "BASIC " + Convert.ToBase64String(Encoding.Default.GetBytes(this.App_Key + ":" + this.App_Secret)));
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            httpWebRequest.Method = "POST";

            Data = "grant_type=password&username=" + this.UserId + "&password=" + this.Password;
            if (!string.IsNullOrEmpty(this.Extension)) Data = Data + "&extension=" + this.Extension;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                sResponse = streamReader.ReadToEnd();
            }

            if (sResponse.StartsWith("{") && sResponse.EndsWith("}"))
            {
                 /* JsonObject JsonObj;

                  using (JsonParser parser = new JsonParser(new StringReader(sResponse), true))
                      JsonObj = parser.ParseObject();

                  if (JsonObj.ContainsKey("access_token"))
                      this.AccessToken = ((JsonString)JsonObj["access_token"]).Value;

                  if (JsonObj.ContainsKey("token_type"))
                      this.TokenType = ((JsonString)JsonObj["token_type"]).Value;

                  if (JsonObj.ContainsKey("refresh_token"))
                      this.RefreshedToken = ((JsonString)JsonObj["refresh_token"]).Value;

                  if (JsonObj.ContainsKey("expires_in"))
                      this.AccessTokenExpireDT = DateTime.Now.AddSeconds(((JsonNumber)JsonObj["expires_in"]).Value);

                  if (JsonObj.ContainsKey("refresh_token_expires_in"))
                      this.RefreshTokenExpireDT = DateTime.Now.AddSeconds(((JsonNumber)JsonObj["refresh_token_expires_in"]).Value);
             */
            }

            if (!string.IsNullOrEmpty(this.AccessToken) && this.AccessTokenExpireDT > DateTime.Now) return true;
            return false;
        }
        public MessageStatus SendMessage(string From, string To, string Text)
        {
            MessageStatus result = new MessageStatus();
            if (string.IsNullOrEmpty(this.AccessToken) || this.AccessTokenExpireDT < DateTime.Now) throw new Exception("Token Expired!");

            string sResponse = string.Empty;
            string Data = string.Empty;

            string sURL = ServiceURL.TrimEnd('/') + "/restapi/v1.0/account/~/extension/~/sms";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + this.AccessToken);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";

            Data = "{\"to\": [{\"phoneNumber\": \"" + To + "\"}],\"from\": {\"phoneNumber\": \"" + From + "\"},\"text\": \"" + Text + "\"}";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                sResponse = streamReader.ReadToEnd();
            }

            if (sResponse.StartsWith("{") && sResponse.EndsWith("}"))
            {
                /*JsonObject JsonObj;

                using (JsonParser parser = new JsonParser(new StringReader(sResponse), true))
                    JsonObj = parser.ParseObject();

                if (JsonObj.ContainsKey("uri"))
                    result.URI = ((JsonString)JsonObj["uri"]).Value;

                if (JsonObj.ContainsKey("id"))
                    result.Id = Convert.ToString(((JsonNumber)JsonObj["id"]).Value);

                if (JsonObj.ContainsKey("type"))
                    result.Type = ((JsonString)JsonObj["type"]).Value;

                if (JsonObj.ContainsKey("creationTime"))
                    result.CreationTime = ((JsonString)JsonObj["creationTime"]).Value;

                if (JsonObj.ContainsKey("readStatus"))
                    result.ReadStatus = ((JsonString)JsonObj["readStatus"]).Value;

                if (JsonObj.ContainsKey("priority"))
                    result.Priority = ((JsonString)JsonObj["priority"]).Value;

                if (JsonObj.ContainsKey("direction"))
                    result.Direction = ((JsonString)JsonObj["direction"]).Value;

                if (JsonObj.ContainsKey("availability"))
                    result.Availablity = ((JsonString)JsonObj["availability"]).Value;

                if (JsonObj.ContainsKey("subject"))
                    result.Subject = ((JsonString)JsonObj["subject"]).Value;

                if (JsonObj.ContainsKey("messageStatus"))
                    result.MsgStatus = ((JsonString)JsonObj["messageStatus"]).Value;

                if (JsonObj.ContainsKey("smsSendingAttemptsCount"))
                    result.AttemptsCount = "1";//((JsonNumber)JsonObj["smsSendingAttemptsCount"]).Value;

                if (JsonObj.ContainsKey("conversation"))
                    result.ConversationId = ((JsonString)((JsonObject)JsonObj["conversation"])["id"]).Value;

                if (JsonObj.ContainsKey("lastModifiedTime"))
                    result.LastModifiedTime = ((JsonString)JsonObj["lastModifiedTime"]).Value;*/
            }
            return result;
        }

        public ICollection<MessageStatus> GetMessageList()
        {
            if (string.IsNullOrEmpty(this.AccessToken) || this.AccessTokenExpireDT < DateTime.Now) throw new Exception("Token Expired!");

            string sResponse = string.Empty;
            string Data = string.Empty;
            List<MessageStatus> result = new List<MessageStatus>();
            MessageStatus m = null;

            DateTime dtFrom = DateTime.UtcNow;

            string sURL = ServiceURL.TrimEnd('/') + "/restapi/v1.0/account/~/extension/~/message-store?direction=Inbound&messageType=SMS&perPage=200&dateFrom=" + dtFrom.ToString("yyyy-MM-ddTHH:mm:ss");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
            httpWebRequest.Headers.Add("Authorization", "Bearer " + this.AccessToken);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 sResponse = streamReader.ReadToEnd();
            }

            if (sResponse.StartsWith("{") && sResponse.EndsWith("}"))
            {
                /*JsonObject JsonObj;

                using (JsonParser parser = new JsonParser(new StringReader(sResponse), true))
                    JsonObj = parser.ParseObject();

                int nCount = ((JsonArray)JsonObj["records"]).Count;
                if (nCount == 0) return result;

                foreach (JsonObject obj in ((JsonArray)JsonObj["records"]))
                {
                    m = new MessageStatus();

                    if (obj.ContainsKey("uri"))
                        m.URI = ((JsonString)obj["uri"]).Value;

                    if (obj.ContainsKey("id"))
                        m.Id = Convert.ToString(((JsonNumber)obj["id"]).Value);

                    if (obj.ContainsKey("from"))
                        m.From = ((JsonString)((JsonObject)obj["from"])["phoneNumber"]).Value;

                    if (obj.ContainsKey("type"))
                        m.Type = ((JsonString)obj["type"]).Value;

                    if (obj.ContainsKey("creationTime"))
                        m.CreationTime = ((JsonString)obj["creationTime"]).Value;

                    if (obj.ContainsKey("readStatus"))
                        m.ReadStatus = ((JsonString)obj["readStatus"]).Value;

                    if (obj.ContainsKey("priority"))
                        m.Priority = ((JsonString)obj["priority"]).Value;

                    if (obj.ContainsKey("direction"))
                        m.Direction = ((JsonString)obj["direction"]).Value;

                    if (obj.ContainsKey("availability"))
                        m.Availablity = ((JsonString)obj["availability"]).Value;

                    if (obj.ContainsKey("subject"))
                        m.Subject = ((JsonString)obj["subject"]).Value;

                    if (obj.ContainsKey("messageStatus"))
                        m.MsgStatus = ((JsonString)obj["messageStatus"]).Value;

                    if (obj.ContainsKey("conversation"))
                        m.ConversationId = ((JsonString)((JsonObject)obj["conversation"])["id"]).Value;

                    if (obj.ContainsKey("lastModifiedTime"))
                        m.LastModifiedTime = ((JsonString)obj["lastModifiedTime"]).Value;
                        */
                result.Add(m);
            }


            return result;
        }

        #endregion
    }
}

public class MessageStatus
{
    public string URI { get; set; }
    public string Id { get; set; }
    public string Type { get; set; }
    public string From { get; set; }
    public string CreationTime { get; set; }
    public string ReadStatus { get; set; }
    public string Priority { get; set; }
    public string Direction { get; set; }
    public string Availablity { get; set; }
    public string Subject { get; set; }
    public string MsgStatus { get; set; }
    public string AttemptsCount { get; set; }
    public string ConversationId { get; set; }
    public string LastModifiedTime { get; set; }
}
