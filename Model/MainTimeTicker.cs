﻿using AdminTool.DataBase;
using AdminTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace AdminTool.Model
{
    public class MainTimeTicker
    {
        private static DateTime dtProcessed = DateTime.Now.ToUniversalTime();
        private static DateTime dtAppStart = DateTime.Now.ToUniversalTime();
        static DataBaseProvider databaseProvider = new DataBaseProvider();

        public static void SendEmailStarted(int UserId, int numberOfDays, int IsApprovedOrAll, int ViewSubjectId,string SyncType)
        {
            string Email, Password;
            int APILimit;
            bool isAPIMailSend = false;
            DateTime dtEmailTime, dtLastProcessed;
            numberOfDays = -(numberOfDays);
            try
            {
                DataTable UserInfo = databaseProvider.getUserGmailInfoById(UserId);
                if (UserInfo.Rows.Count < 0)
                { return; }

                Email = UserInfo.Rows[0]["EmailId"].ToString();
                Password = UserInfo.Rows[0]["password"].ToString();
                APILimit = Convert.ToInt32(UserInfo.Rows[0]["apiLimit"].ToString());

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    return;

                MailHelper mailHelper = new MailHelper();
                if (mailHelper.connect(Email, Password))
                {
                    try
                    {
                        if (numberOfDays > 0)
                        {
                            dtProcessed = DateTime.Now.AddDays(numberOfDays);
                        }
                        else
                        {
                            //Get last Processed Mail Date in GMT HelperClass.Format
                            dtProcessed = databaseProvider.GetLastProcessedTime(UserId);

                        }
                        StringBuilder oSB = new StringBuilder();
                        //Check Every Mail

                      //  IEnumerable<MailMessage> mailMessages = mailHelper.getMsgSentAfter(dtProcessed);
                        IEnumerable<MailMessage> mailMessages = mailHelper.getMsgWithSubjectContaingAndSentAfter("Sales enquiry: London ", dtProcessed);
                        mailHelper.disconnect();

                        DataTable ListOfAllSubject = databaseProvider.getListOfAllUserSubject(UserId, IsApprovedOrAll, ViewSubjectId);

                        foreach (MailMessage mailMessage in mailMessages)
                        {
                            try
                            {
                                string sMailContent = mailMessage.Body;

                                sMailContent = mailMessage.Subject + " " + sMailContent;
                                string tempDate = mailMessage.Headers["Date"];

                                if (DateTime.TryParse(tempDate, out dtEmailTime))
                                    dtEmailTime = Convert.ToDateTime(tempDate);
                                else
                                    dtEmailTime = DateTime.Now;


                                dtLastProcessed = dtEmailTime;
                                sMailContent = HelperClass.StripHTML(sMailContent);  //remove html tags form content

                                if (ListOfAllSubject.Rows.Count < 0) { return; }

                                // Insert MailInto DataBase

                                for (int SubjectRowNo = 0; SubjectRowNo < ListOfAllSubject.Rows.Count; SubjectRowNo++)
                                {
                                    string SubjectLine = ListOfAllSubject.Rows[SubjectRowNo]["subjectLine"].ToString();


                                    if (sMailContent.Contains(SubjectLine))
                                    {
                                        int SubjectId = Convert.ToInt32(ListOfAllSubject.Rows[SubjectRowNo]["id"].ToString());

                                        if (APILimit < 1)
                                        {
                                            if (!isAPIMailSend)
                                            {
                                                summaryEmail.sendApiLimitExceedInfoMail(UserId);
                                                isAPIMailSend = true;
                                            }
                                            return;
                                        }

                                        // Insert MailInto DataBase
                                        int DataBaseMailId = databaseProvider.InsertMailIntoDataBase(SubjectId, sMailContent, dtEmailTime, SyncType);


                                        if (DataBaseMailId < 0)
                                            return;
                                        else
                                            APILimit--;


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

                                                                result = databaseProvider.InsertMailSplitInfoFromBody(DataBaseMailId, ValueForColumnHeader, columnHeaderId);
                                                                if (result.ToLower() != "success")
                                                                {
                                                                    // mailSplitStatus = false;
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
                                }


                            }
                            catch (Exception ex)
                            {
                                databaseProvider.logApplicationError("ERROR WHILE PROCESSING MAIL INTO DATABASE " + ex.Message, "Information");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        databaseProvider.logApplicationError("ERROR WHILE PROCESSING MAIL INTO DATABASE " + ex.Message, "Information");
                    }
                }


                //Fetch Leads Info From Database and Update same into CRM
                SendInformationIntoCrm.SendInformationIntoCRMFromDB(UserId);

                // Send SumaryMail to team
                if (DateTime.Now.Hour <= 1)
                    summaryEmail.SendSumaryMail(UserId);
            }
            catch (Exception ex)
            {
                databaseProvider.logApplicationError("ERROR WHILE PROCESSING MAIL INTO DATABASE " + ex.Message, "Information");
            }
        }
    }
}