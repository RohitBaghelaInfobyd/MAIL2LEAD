using AdminTool.DataBase;
using AdminTool.Model;
using AdminTool.SMSService;
using System;
using System.Data;

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
        static string SyncFrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UserIds = null;
                    string QueryString = Request.QueryString[0];
                    if (QueryString.Contains("240410248a413cc2b4cb03bdba2ed77"))
                    {
                        if (QueryString.Equals("240410248a413cc2b4cb03bdba2ed77c"))
                        {
                            SyncFrom = "Pingability";
                        }
                        if (QueryString.Equals("240410248a413cc2b4cb03bdba2ed77d"))
                        {
                            SyncFrom = "UPtimeRobot";
                        }
                        jobId = dataBaseProvider.spInsertCronJobStatus("Cron job run Started.", SyncFrom);
                        GetAllUserList();
                        if (jobId > 0)
                        {
                            dataBaseProvider.updateCronJobStatus("Cron job run Successfully." + UserIds, jobId);
                        }
                        Response.Write("Done");
                    }
                    else if (QueryString.Contains("GetSmsFromCrm230410248a413cc2b4cb03bdba2ed77"))
                    {
                      //  GetTheAllSmsFromCrmToPortal();
                    }
                    else if (QueryString.Contains("SendSmsFromPortal230410248a413cc2b4cb03bdba2ed77"))
                    {
                       // SendTheSmsFromPortal();
                    }
                    else
                    {
                        Response.Write("Wrong Request");
                    }

                }
                catch (Exception exc)
                {
                    if (jobId > 0)
                    {
                        dataBaseProvider.updateCronJobStatus("Cron job have error " + exc.Message, jobId);
                    }
                    Response.Write(exc.Message);
                }
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
                    MainTimeTicker.SendEmailStarted(ViewUserId, 2, 1, 0, "AutoSync_" + SyncFrom + "_" + jobId);
                }
            }
            catch (Exception ex)
            {
                dataBaseProvider.updateCronJobStatus("Cron job have error " + ex.Message, jobId);
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
                dataBaseProvider.updateCronJobStatus("Cron job have error " + ex.Message, jobId);
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
                dataBaseProvider.updateCronJobStatus("Cron job have error " + ex.Message, jobId);
            }
        }
    }
}
