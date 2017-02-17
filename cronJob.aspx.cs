using AdminTool.DataBase;
using AdminTool.Model;
using AdminTool.SMSService;
using System;
using System.Data;
using System.Reflection;

namespace AdminTool
{
    public partial class cronJob : System.Web.UI.Page
    {

        /*  https://pingability.com/
         * UserId Rohit@infobyd.com
         * Password 123456789
         *  
         *  */
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        static int jobId;
        static string UserIds;
        static int AutoSyncUserId = 0;
        static string SyncFrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    MainTimeTicker.getMailFromGmailbyUid(73, 2, 1, 0, "AutoSync_" + SyncFrom + "_" + jobId);
                    int ActionType = 0;
                    UserIds = null;
                    if (Request.QueryString.Count > 0)
                    {
                        string QueryString = Request.QueryString[0];
                        /*http://localhost:59663/cronJob.aspx?q=all_mail_uptimerobot_240410248a413cc2b4cb03bdba2ed77&uid=93*/
                        if (QueryString.Contains("240410248a413cc2b4cb03bdba2ed77"))
                        {
                            if (Request.QueryString.Count > 1)
                            {
                                ActionType = 5;
                                int result;
                                if (int.TryParse(Request.QueryString[1], out result))
                                {
                                    AutoSyncUserId = Convert.ToInt32(Request.QueryString[1]);
                                }

                                SyncFrom = "UserViseAutoSync_" + AutoSyncUserId + "_uptimerobot";
                            }
                            else if (QueryString.Equals("all_mail_uptimerobot_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 1;
                                SyncFrom = "all_mail_uptimerobot";
                            }
                            else if (QueryString.Equals("all_mail_pingability_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 1;
                                SyncFrom = "all_mail_pingability";
                            }
                            else if (QueryString.Equals("send_status_report_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 6;
                                SyncFrom = "send_user_mail_status_report";
                            }
                            else if (QueryString.Equals("get_list_mail_uid_uptimerobot_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 2;
                                SyncFrom = "get_list_mail_uid_uptimerobot";
                            }
                            else if (QueryString.Equals("get_list_mail_uid_pingability_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 2;
                                SyncFrom = "get_list_mail_uid_pingability";
                            }
                            else if (QueryString.Equals("get_mail_by_uid_uptimerobot_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 3;
                                SyncFrom = "get_mail_by_uid_uptimerobot";
                            }
                            else if (QueryString.Equals("get_mail_by_uid_pingability_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 3;
                                SyncFrom = "get_mail_by_uid_pingability";
                            }
                            else if (QueryString.Equals("submit_information_into_crm_uptimerobot_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 4;
                                SyncFrom = "submit_information_into_crm_uptimerobot";
                            }
                            else if (QueryString.Equals("submit_information_into_crm_pingability_240410248a413cc2b4cb03bdba2ed77"))
                            {
                                ActionType = 4;
                                SyncFrom = "submit_information_into_crm_pingability";
                            }
                            jobId = dataBaseProvider.spInsertCronJobStatus("Cron job run Started.", SyncFrom);
                            SyncFrom = SyncFrom + "_" + jobId;
                            switch (ActionType)
                            {

                                case 1:
                                    GetAllUserList();
                                    break;
                                case 2:
                                    GetListOfAllMailUserID(SyncFrom);
                                    break;
                                case 3:
                                    GetAllMailByUid();
                                    break;
                                case 4:
                                    submitInformationIntoCRM();
                                    break;
                                case 5:
                                    AytoSyncForSingleUser(AutoSyncUserId);
                                    break;
                                case 6:
                                    sendUserEmailStatusResport();
                                    break;
                            }

                            if (jobId > 0)
                            {
                                dataBaseProvider.updateCronJobStatus("Cron job run Successfully." + UserIds, jobId);
                            }
                            Response.Redirect("~/default.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }

                }
                catch (Exception ex)
                {
                    if (jobId > 0)
                    {
                        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                        dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_5");
                        dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
                    }
                    Response.Write(ex.Message);
                }
            }
        }

        public void sendUserEmailStatusResport()
        {
            try
            {
                string appDataFolder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");

                DataTable UserInfo;
                int ViewUserId = 0;
                UserInfo = dataBaseProvider.getListOfAllUserToSendReport();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    UserIds = UserIds + "_" + ViewUserId;
                    string result = summaryEmail.sendUserMail2LeadRespotStatus(ViewUserId, appDataFolder);
                    if (result.ToUpper().Equals("SUCCESS"))
                    {
                        dataBaseProvider.InsertUserMail2LeadReportStatus(ViewUserId, 1);
                    }
                    else
                    {
                        dataBaseProvider.InsertUserMail2LeadReportStatus(ViewUserId, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_7");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void AytoSyncForSingleUser(int user)
        {
            if (user > 0)
            {
                SendInformationIntoCrm.SendInformationIntoCRMFromDB(user);
                MainTimeTicker.SubmitEmailFromMailToCRM(user, 2, 1, 0, "AutoSync_" + SyncFrom + "_" + jobId);
            }

        }

        public void GetAllUserList()
        {
            try
            {
                DataTable UserInfo;
                int ViewUserId = 0;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    UserIds = UserIds + "_" + ViewUserId;
                    MainTimeTicker.SubmitEmailFromMailToCRM(ViewUserId, 2, 1, 0, "AutoSync_" + SyncFrom + "_" + jobId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_6");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void GetListOfAllMailUserID(string SyncFrom)
        {
            try
            {                
                DataTable UserInfo;
                int ViewUserId = 0;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    UserIds = UserIds + "_" + ViewUserId;
                    MainTimeTicker.getListOfMailUidAndInsertIntoDataBase(ViewUserId, 2, SyncFrom);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_7");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void submitInformationIntoCRM()
        {
            try
            {
                DataTable UserInfo;
                int ViewUserId = 0;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    UserIds = UserIds + "_" + ViewUserId;
                    MainTimeTicker.submitInformationIntoCrm(ViewUserId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_8");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void GetAllMailByUid()
        {
            try
            {
                DataTable UserInfo;
                int ViewUserId = 0;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    UserIds = UserIds + "_" + ViewUserId;
                    MainTimeTicker.getMailFromGmailbyUid(ViewUserId, 2, 1, 0, "AutoSync_" + SyncFrom + "_" + jobId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_1");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void SendTheSmsFromPortal()
        {
            try
            {
                DataTable UserInfo;
                int ViewUserId = 0;

                string ServiceURL, AppKey, AppSecret, UserId, Password;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["ServiceURL"].ToString());
                    ServiceURL = UserInfo.Rows[i]["ServiceURL"].ToString();
                    AppKey = UserInfo.Rows[i]["AppKey"].ToString();
                    AppSecret = UserInfo.Rows[i]["AppSecret"].ToString();
                    UserId = UserInfo.Rows[i]["AppKey"].ToString();
                    Password = UserInfo.Rows[i]["Password"].ToString();

                    RingCentral RCentral = new RingCentral(ServiceURL, AppKey, AppSecret, UserId, Password);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_2");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }

        public void GetTheAllSmsFromCrmToPortal()
        {


            //summaryEmail.sendApiLimitExceedInfoMail1(ViewUserId);

            /* Zoho zo = new Zoho("240410248a413cc2b4cb03bdba2ed77c");
             zo.GetLeadInfo("1950638000000623001");
            string url = "https://platform.devtest.ringcentral.com";
             string app_key = "pySpy2OiScuRvdwrtQE4Zg";
            string app_secret = "BslJ2D5DQu - W3GsR9_c2XQn2gTKWWnRtOcQbBGqZOZwg";
            string uid = "16475599844";
            string pwd = "Bankprop100";
            RingCentral rc = new RingCentral(url, app_key, app_secret, uid, pwd);
            rc.SendMessage("55637", "8982245289", "Hello Test Message");
            aLOK lEAD Id 2015002000000259011
*/
            try
            {
                DataTable UserInfo;
                int ViewUserId = 0;
                string configurationAuthToken;
                UserInfo = dataBaseProvider.getListOfallUserForCronJob();
                for (int i = 0; i < UserInfo.Rows.Count; i++)
                {
                    ViewUserId = Convert.ToInt32(UserInfo.Rows[i]["id"].ToString());
                    configurationAuthToken = UserInfo.Rows[i]["configurationAuthToken"].ToString();
                    configurationAuthToken = "9af24929848c6730d5d3ccae057a8c98";
                    SMSService.Zoho zo = new SMSService.Zoho(configurationAuthToken);
                    DataSet ds = zo.GetSMS();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Information_3");
                dataBaseProvider.updateCronJobStatus("Cron job have error " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), jobId);
            }
        }
    }
}
