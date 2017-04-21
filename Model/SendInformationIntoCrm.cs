using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Globalization;

namespace AdminTool.Model
{
    public class SendInformationIntoCrm
    {
        static DataBaseProvider databaseProvider = new DataBaseProvider();

        public static void SendInformationIntoCRMFromDB(int UserId)
        {
            string Email, token, sRes, UniqueValueToCheckExisingValue;
            string sLeadSourceValue, Lead_Column_Header, isHaveDefaultValue, defaultValueType;
            int APILimit, exitingEntryEvent;
            try
            {
                DataTable UserInfo = databaseProvider.getUserGmailInfoById(UserId);
                if (UserInfo.Rows.Count < 0)
                { return; }

                Email = UserInfo.Rows[0]["EmailId"].ToString();
                token = UserInfo.Rows[0]["configurationAuthToken"].ToString();
                APILimit = Convert.ToInt32(UserInfo.Rows[0]["apiLimit"].ToString());
                exitingEntryEvent = Convert.ToInt32(UserInfo.Rows[0]["existingInfoEvent"].ToString());

                DataTable DTLeadsInfo = databaseProvider.getAllUnProcessedMailToPutIntoCrm(UserId);

                string sTmpDataStr = string.Empty;
                string CRLF = "\r\n";
                for (int x = 0; x < DTLeadsInfo.Rows.Count; x++)
                {
                    // Check API Access Limits
                    Boolean status = Convert.ToBoolean(databaseProvider.CheckUserApiCallCountStatusOfUser(UserId));
                    if (!status)
                    {
                        summaryEmail.sendApiLimitExceedInfoMail(UserId);
                        break;
                    }

                    int MailId = Convert.ToInt32(DTLeadsInfo.Rows[x]["id"].ToString());
                    string SubjctType = DTLeadsInfo.Rows[x]["subjectType"].ToString();

                    DataTable valueDataTable = databaseProvider.GetLeadToMailColumnValueByMailId(MailId);
                    UniqueValueToCheckExisingValue = string.Empty;
                    if (SubjctType.ToLower().Contains("lead"))
                    {
                        sTmpDataStr = "<Leads>" + CRLF;
                    }
                    else
                    {
                        sTmpDataStr = "<Contacts>" + CRLF;
                    }

                    sTmpDataStr += "<row no=" + Convert.ToChar(34) + "1" + Convert.ToChar(34) + ">" + CRLF;


                    /******* Following to add all value comes from mail ***************************/
                    for (int xy = 0; xy < valueDataTable.Rows.Count; xy++)
                    {  //Get Record Data in XML Format

                        sLeadSourceValue = valueDataTable.Rows[xy]["FiledValue"].ToString();
                        sLeadSourceValue = sLeadSourceValue.Replace('£', ' ').Trim();
                        Lead_Column_Header = valueDataTable.Rows[xy]["Lead_Column_Header"].ToString();
                        if (!string.IsNullOrEmpty(sLeadSourceValue))
                        {
                            if (Lead_Column_Header.ToLower().Contains("email"))
                            {
                                UniqueValueToCheckExisingValue = sLeadSourceValue;
                            }
                            else if (Lead_Column_Header.ToLower().Contains("price"))
                            {
                                int finalResult = 0;
                                bool output;
                                output = int.TryParse(sLeadSourceValue, out finalResult);
                                if (!output)
                                {
                                    sLeadSourceValue = "0.0";
                                }
                            }

                            sTmpDataStr += "<FL val=" + Convert.ToChar(34) + Lead_Column_Header + Convert.ToChar(34) + ">" + sLeadSourceValue + "</FL>" + CRLF;
                        }
                    }


                    /************* Following to add the all default value *******************/
                    int subjectId = Convert.ToInt32(valueDataTable.Rows[x]["subject_id"].ToString());
                    DataTable ListOfAllDefaultColumn = databaseProvider.getListOfAllDefaultLeadColumn(subjectId);

                    // Get or feed all default column value into CRM
                    if (ListOfAllDefaultColumn.Rows.Count > 0)
                    {
                        for (int xy = 0; xy < ListOfAllDefaultColumn.Rows.Count; xy++)
                        {
                            sLeadSourceValue = ListOfAllDefaultColumn.Rows[xy]["defaultValue"].ToString();
                            Lead_Column_Header = ListOfAllDefaultColumn.Rows[xy]["leadColumnHeader"].ToString();
                            isHaveDefaultValue = ListOfAllDefaultColumn.Rows[xy]["isHaveDefaultValue"].ToString();
                            defaultValueType = ListOfAllDefaultColumn.Rows[xy]["defaultValueType"].ToString();
                            if (!string.IsNullOrEmpty(sLeadSourceValue))
                            {
                                if (defaultValueType.ToLower().Contains("date"))
                                {

                                    DateTime date = DateTime.Now.Date;
                                    sLeadSourceValue = date.ToString();
                                }
                                sTmpDataStr += "<FL val=" + Convert.ToChar(34) + Lead_Column_Header + Convert.ToChar(34) + ">" + sLeadSourceValue + "</FL>" + CRLF;
                            }
                        }
                    }


                    /******** Following to Add the Lead Assignment info  *****************/
                    DataTable assignmentInfoDataTable = databaseProvider.getMailLeasAssignmentInfoToSubmitIntoCRM(UserId);
                    if (assignmentInfoDataTable.Rows.Count > 0)
                    {
                        for (int xy = 0; xy < assignmentInfoDataTable.Rows.Count; xy++)
                        {  //Get Record Data in XML Format

                            sLeadSourceValue = valueDataTable.Rows[xy]["FiledValue"].ToString();
                            Lead_Column_Header = valueDataTable.Rows[xy]["Lead_Column_Header"].ToString();
                            if (!string.IsNullOrEmpty(sLeadSourceValue))
                            {
                                sTmpDataStr += "<FL val=" + Convert.ToChar(34) + Lead_Column_Header + Convert.ToChar(34) + ">" + sLeadSourceValue + "</FL>" + CRLF;
                            }
                        }
                    }
                    /*************************************************************************/
                    sTmpDataStr += "</row>" + CRLF;

                    if (SubjctType.ToLower().Contains("lead"))
                    {
                        sTmpDataStr += "</Leads>";
                    }
                    else
                    {
                        sTmpDataStr += "</Contacts>";
                    }
                    sRes = string.Empty;

                    if (exitingEntryEvent > 1)
                    {
                        if (!string.IsNullOrEmpty(UniqueValueToCheckExisingValue))
                        {
                            bool isForLead = true;
                            if (!SubjctType.ToLower().Contains("lead"))
                            {
                                isForLead = false;
                            }
                            sRes = ZohoCRMAPI.CheckRecord(token, UniqueValueToCheckExisingValue, isForLead);

                            if (sRes.IndexOf("<code>") < 0)
                            {
                                try
                                {
                                    string record_time = string.Empty, record_id = string.Empty;
                                    DataSet ds = new DataSet();
                                    DataTable dt;

                                    XmlReader xmlReader = XmlReader.Create(new StringReader(sRes));
                                    ds.ReadXml(xmlReader);
                                    dt = ds.Tables["FL"];
                                    record_id = dt.Rows[0][1].ToString();
                                    record_time = dt.Rows[1][1].ToString();
                                    if (exitingEntryEvent > 2)
                                    {
                                        databaseProvider.InsertSubmitedMailCRMInfo(MailId, record_time, record_id, sRes, sTmpDataStr);
                                    }
                                    else
                                    {
                                        sRes = ZohoCRMAPI.UpdateAPIMethod(token, sTmpDataStr, record_id);
                                        if (sRes.IndexOf("successfully") > 0)
                                        {
                                            try
                                            {
                                                xmlReader = XmlReader.Create(new StringReader(sRes));
                                                ds.ReadXml(xmlReader);
                                                dt = ds.Tables["FL"];
                                                record_id = dt.Rows[0][1].ToString();
                                                record_time = dt.Rows[1][1].ToString();
                                                databaseProvider.InsertSubmitedMailCRMInfo(MailId, record_time, record_id, sRes, sTmpDataStr);
                                            }
                                            catch (Exception ex)
                                            {
                                                databaseProvider.logApplicationError(MailId + "__FAILED TO INSERT DATA ONTO CRM 4 " + ex.Message, "Information");
                                            }
                                            System.Threading.Thread.Sleep(30);
                                        }
                                        else
                                        {
                                            databaseProvider.updateMailContentSubmitToCrmStatusWithError(MailId, sRes, sTmpDataStr);
                                            System.Threading.Thread.Sleep(30);
                                            databaseProvider.logApplicationError(MailId + "__" + sRes, "Information");
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    databaseProvider.logApplicationError(MailId + "__FAILED TO INSERT DATA ONTO CRM 5 " + ex.Message, "Information");
                                }
                            }
                            else
                            {
                                InsertNewEntryInCRM(token, sTmpDataStr, MailId);
                            }
                        }
                        else
                        {
                            InsertNewEntryInCRM(token, sTmpDataStr, MailId);
                        }
                    }
                    else
                    {
                        InsertNewEntryInCRM(token, sTmpDataStr, MailId);
                    }
                }
            }
            catch (Exception ex)
            {
                databaseProvider.logApplicationError(ex.Message + "__FAILED TO INSERT DATA ONTO CRM 1 ", "Information");
            }

        }

        private static void InsertNewEntryInCRM(string token, string sTmpDataStr, int MailId)
        {
            string sRes = string.Empty;
            sRes = ZohoCRMAPI.APIMethod(token, sTmpDataStr);
            if (!string.IsNullOrEmpty(sRes))
            {
                if (sRes.IndexOf("added successfully") > 0)
                {
                    try
                    {
                        string record_time = string.Empty, record_id = string.Empty;
                        DataSet ds = new DataSet();
                        DataTable dt;

                        XmlReader xmlReader = XmlReader.Create(new StringReader(sRes));
                        ds.ReadXml(xmlReader);
                        dt = ds.Tables["FL"];
                        record_id = dt.Rows[0][1].ToString();
                        record_time = dt.Rows[1][1].ToString();
                        databaseProvider.InsertSubmitedMailCRMInfo(MailId, record_time, record_id, sRes, sTmpDataStr);
                    }
                    catch (Exception ex)
                    {
                        databaseProvider.logApplicationError(MailId + "__FAILED TO INSERT DATA ONTO CRM 2 " + ex.Message, "Information");
                    }
                    System.Threading.Thread.Sleep(30);
                }
                else
                {
                    databaseProvider.updateMailContentSubmitToCrmStatusWithError(MailId, sRes, sTmpDataStr);
                    System.Threading.Thread.Sleep(30);
                    databaseProvider.logApplicationError(MailId + "__" + sRes, "Information");
                }
            }
            else
            {
                databaseProvider.logApplicationError(MailId + "__FAILED TO INSERT DATA ONTO CRM 3 ", "Information");
            }
        }
    }
}