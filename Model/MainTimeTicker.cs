using AdminTool.DataBase;
using AdminTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;

namespace AdminTool.Model
{
    public class MainTimeTicker
    {
        private static DateTime dtProcessed = DateTime.Now.ToUniversalTime();
        private static DateTime dtAppStart = DateTime.Now.ToUniversalTime();
        static DataBaseProvider databaseProvider = new DataBaseProvider();

        public static void SubmitEmailFromMailToCRM(int UserId, int numberOfDays, int IsApprovedOrAll, int ViewSubjectId, string SyncType)
        {
            try
            {
                string Email, Password;
                int APILimit;
                DataTable UserInfo = databaseProvider.getUserGmailInfoById(UserId);
                if (UserInfo.Rows.Count < 0)
                { return; }

                Email = UserInfo.Rows[0]["EmailId"].ToString();
                Password = UserInfo.Rows[0]["password"].ToString();
                APILimit = Convert.ToInt32(UserInfo.Rows[0]["apiLimit"].ToString());
                getListOfMailUidAndInsertIntoDataBase(UserId, numberOfDays, SyncType);
                getMailFromGmailbyUid(UserId, numberOfDays, IsApprovedOrAll, ViewSubjectId, SyncType);
                submitInformationIntoCrm(UserId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_9_" + SyncType);

            }
        }

        public static void submitInformationIntoCrm(int UserId)
        {
            try
            {
                SendInformationIntoCrm.SendInformationIntoCRMFromDB(UserId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_789");
            }
        }

        public static void getListOfMailUidAndInsertIntoDataBase(int UserId, int numberOfDays, string SyncType)
        {
            try
            {
                string Email, Password;
                int APILimit;
                DataTable UserInfo = databaseProvider.getUserGmailInfoById(UserId);
                if (UserInfo.Rows.Count < 0)
                { return; }

                Email = UserInfo.Rows[0]["EmailId"].ToString();
                Password = UserInfo.Rows[0]["password"].ToString();
                APILimit = Convert.ToInt32(UserInfo.Rows[0]["apiLimit"].ToString());

                MailHelper mailHelper = new MailHelper();
                if (mailHelper.connect(Email, Password))
                {
                    if (numberOfDays > 0)
                    {
                        dtProcessed = DateTime.Now.AddDays(-numberOfDays);
                    }
                    else
                    {
                        //Get last Processed Mail Date in GMT HelperClass.Format
                        dtProcessed = databaseProvider.GetLastProcessedTime(UserId);

                    }

                    IEnumerable<uint> uids = mailHelper.getAllMailsUid(dtProcessed);
                    foreach (uint uid in uids)
                    {
                        databaseProvider.InsertNewMailUidIntoDataBase(UserId, uid);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_10_" + SyncType);
            }
        }

        public static void getMailFromGmailbyUid(int UserId, int numberOfDays, int IsApprovedOrAll, int ViewSubjectId, string SyncType)
        {
            try
            {
                string Email, Password;
                int APILimit;
                DataTable UserInfo = databaseProvider.getUserGmailInfoById(UserId);
                if (UserInfo.Rows.Count < 0)
                { return; }

                Email = UserInfo.Rows[0]["EmailId"].ToString();
                Password = UserInfo.Rows[0]["password"].ToString();
                APILimit = Convert.ToInt32(UserInfo.Rows[0]["apiLimit"].ToString());

                MailHelper mailHelper = new MailHelper();
                DateTime dtEmailTime;
                DataTable uidsList = databaseProvider.GetListOfAllMailUids(UserId, SyncType);
                DataTable ListOfAllSubject = databaseProvider.getListOfAllUserSubject(UserId, IsApprovedOrAll, ViewSubjectId);

                if (mailHelper.connect(Email, Password))
                {
                    for (int i = 0; i <= uidsList.Rows.Count; i++)
                    {
                        //uint item = Convert.ToUInt32(uidsList.Rows[i][0].ToString());
                        uint item = 4259;
                        IEnumerable <uint> uids = (new[] { item });

                        IEnumerable<MailMessage> mailMessages = mailHelper.getMailByUids(uids);

                        foreach (MailMessage mailMessage in mailMessages)
                        {
                            try
                            {
                                databaseProvider.UpdateMailUidIntoDataBase(UserId, item);

                                string sMailContent = mailMessage.Body;

                                sMailContent = mailMessage.Subject + " " + sMailContent;
                                string tempDate = mailMessage.Headers["Date"];

                                if (DateTime.TryParse(tempDate, out dtEmailTime))
                                    dtEmailTime = Convert.ToDateTime(tempDate);
                                else
                                    dtEmailTime = DateTime.Now;

                                sMailContent = HelperClass.StripHTML(sMailContent);  //remove html tags form content

                                if (ListOfAllSubject.Rows.Count < 0) { return; }

                                // Insert MailInto DataBase

                                for (int SubjectRowNo = 0; SubjectRowNo < ListOfAllSubject.Rows.Count; SubjectRowNo++)
                                {
                                    string SubjectLine = ListOfAllSubject.Rows[SubjectRowNo]["subjectLine"].ToString();


                                    if (sMailContent.Contains(SubjectLine))
                                    {
                                        int SubjectId = Convert.ToInt32(ListOfAllSubject.Rows[SubjectRowNo]["id"].ToString());
                                        int DataBaseMailId = databaseProvider.InsertMailIntoDataBase(SubjectId, sMailContent, dtEmailTime, SyncType, item);
                                        SplitTheInformationFromMail(SubjectId, DataBaseMailId, sMailContent, SyncType);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_1_" + SyncType);
                            }
                        }
                    }
                }
                mailHelper.disconnect();
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_2_" + SyncType);

            }

        }

        public static void SplitTheInformationFromMail(int SubjectId, int DataBaseMailId, string sMailContent, string SyncType)
        {
            try
            {
                DataTable UserMailToLeadColumnHeader = databaseProvider.getListOfMailContentSplitInfo(SubjectId);
                bool mailSplitStatus = true;
                for (int LeadRowNo = 0; LeadRowNo < UserMailToLeadColumnHeader.Rows.Count; LeadRowNo++)
                {
                    string splitStartText, splitEndText;
                    int columnHeaderId, contentSplitId;
                    Boolean isHaveDefaultValue;
                    splitStartText = UserMailToLeadColumnHeader.Rows[LeadRowNo]["startText"].ToString();
                    splitEndText = UserMailToLeadColumnHeader.Rows[LeadRowNo]["endText"].ToString();
                    columnHeaderId = Convert.ToInt32(UserMailToLeadColumnHeader.Rows[LeadRowNo]["columnHeaderId"].ToString());
                    contentSplitId = Convert.ToInt32(UserMailToLeadColumnHeader.Rows[LeadRowNo]["id"].ToString());
                    isHaveDefaultValue = !Convert.ToBoolean(UserMailToLeadColumnHeader.Rows[LeadRowNo]["isHaveDefaultValue"].ToString());
                    if (isHaveDefaultValue)
                    {
                        if (sMailContent.Contains(splitStartText))
                        {
                            string[] Temp = sMailContent.Split(new string[] { splitStartText }, StringSplitOptions.None);
                            if (Temp.Length > 0)
                            {
                                string[] Temp1 = Temp[1].Split(new string[] { splitEndText }, StringSplitOptions.None);
                                if (Temp1.Length > 0)
                                {
                                    string ValueForColumnHeader = Temp1[0];
                                    if (ValueForColumnHeader.Trim().Length > 0)
                                    {
                                        string result = string.Empty;
                                        if (Convert.ToInt32(UserMailToLeadColumnHeader.Rows[LeadRowNo]["IsValueSplit"].ToString()) > 0)
                                        {
                                            Temp1 = null;
                                            if (UserMailToLeadColumnHeader.Rows[LeadRowNo]["splitType"].ToString().Trim().ToLower() == "space")
                                            {
                                                Temp1 = ValueForColumnHeader.Split(' ');
                                            }
                                            else if (UserMailToLeadColumnHeader.Rows[LeadRowNo]["splitType"].ToString().Trim().ToLower() == "newline")
                                            {
                                                Temp1 = ValueForColumnHeader.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                            }
                                            else
                                            {
                                                string text = UserMailToLeadColumnHeader.Rows[LeadRowNo]["splitValueText"].ToString().Trim().ToLower();
                                                Temp1 = ValueForColumnHeader.Split(new string[] { text }, StringSplitOptions.None);
                                            }
                                            if (Temp1.Length > 1)
                                            {
                                                int splitIndex = Convert.ToInt32(UserMailToLeadColumnHeader.Rows[LeadRowNo]["splitIndex"].ToString());
                                                if (splitIndex > 0)
                                                {
                                                    if (Temp1.Length > splitIndex)
                                                    {
                                                        ValueForColumnHeader = Temp1[splitIndex].ToString().Trim();
                                                    }
                                                    else
                                                    {
                                                        ValueForColumnHeader = Temp1[Temp1.Length - 1].ToString().Trim();
                                                    }

                                                    if (string.IsNullOrEmpty(ValueForColumnHeader))
                                                    {
                                                        for (int te = Temp1.Length - 1; te > -1; te--)
                                                        {
                                                            if (!string.IsNullOrEmpty(Temp1[te].ToString().Trim()))
                                                            {
                                                                ValueForColumnHeader = Temp1[te].ToString().Trim();
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ValueForColumnHeader = Temp1[0].ToString();
                                                }
                                            }
                                        }
                                        ValueForColumnHeader = ValueForColumnHeader.Replace('£', ' ');
                                        try
                                        {
                                            result = databaseProvider.InsertMailSplitInfoFromBody(DataBaseMailId, ValueForColumnHeader, columnHeaderId);
                                            if (result.ToLower() != "success")
                                            {
                                                // mailSplitStatus = false;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                                            databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_3_" + SyncType);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (mailSplitStatus)
                {
                    databaseProvider.updateMailSplitContentCompleteStatus(DataBaseMailId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_4_" + SyncType);
            }
        }
    }
}