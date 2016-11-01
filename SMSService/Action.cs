using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace AdminTool.SMSService
{
    public class Action
    {
        DataBaseProvider databaseProvider = new DataBaseProvider();

        #region Declaration
        Thread tZoho = null;
        Thread tRC = null;

        Thread tRcv = null;

        int nZoho_Sync_Time = 600000;
        int nRC_Sync_Time = 600000;
        int LoggedInuserId, UserId;
        #endregion
        #region Constrcutor
        public Action()
        { }
        #endregion
        #region Properties
        #endregion
        #region Methods
        public void Start()
        {
            nZoho_Sync_Time = 120000;
            nRC_Sync_Time = 120000;

            if (nRC_Sync_Time == 0) nRC_Sync_Time = 600000;
            if (nZoho_Sync_Time == 0) nZoho_Sync_Time = 600000;

            tZoho = new Thread(new ThreadStart(Zoho_Sync));
            tZoho.IsBackground = true;
            tZoho.Start();

            tRC = new Thread(new ThreadStart(RC_Sync));
            tRC.IsBackground = true;
            tRC.Start();

            tRcv = new Thread(new ThreadStart(Received_Sync));
            tRcv.IsBackground = true;
            tRcv.Start();

            Console.ReadKey();
        }

        private void Zoho_Sync()
        {
            Zoho zoho = new Zoho();
            DataTable UserInfo = databaseProvider.getUserInfoById(UserId);
            if (UserInfo.Rows.Count > 0)
            {
                zoho.AuthToken = UserInfo.Rows[0]["configurationAuthToken"].ToString();
            }
            DateTime dtRecord = Convert.ToDateTime("01/01/1900");

            int Id = 0;
            string From = string.Empty;
            string Subject = string.Empty;
            DateTime dtRcv = Convert.ToDateTime("01/01/1900");
            string ConversationId = string.Empty;
            string ModuleSubject = string.Empty;

            int nModuleType = 0;
            string sModuleId = string.Empty;
            string sName = string.Empty;
            DataTable data = null;
            while (true)
            {
                try
                {
                    DataTable Received = databaseProvider.GetPendingReceivedMessage();
                    //Insert Data into Zoho
                    for (int i = 0; i < Received.Rows.Count; i++)
                    {
                        try
                        {
                            Id = Convert.ToInt32(Received.Rows[i]["Id"]);
                            nModuleType = Convert.ToInt32(Received.Rows[i]["ModuleType"]);
                            From = Convert.ToString(Received.Rows[i]["From"]);
                            Subject = Convert.ToString(Received.Rows[i]["Subject"]);
                            sModuleId = Convert.ToString(Received.Rows[i]["ModuleId"]);
                            dtRcv = Convert.ToDateTime(Received.Rows[i]["ReceivedDT"]);
                            ConversationId = Convert.ToString(Received.Rows[i]["ConversationId"]);
                            ModuleSubject = databaseProvider.GetSubject(ConversationId);
                            if (string.IsNullOrEmpty(ModuleSubject)) ModuleSubject = From;

                            Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t1 SMS Received from " + From);
                            int nStatus = zoho.InsertSMS(nModuleType, sModuleId, ModuleSubject, Subject, dtRcv);
                            databaseProvider.UpdateReceivedSMSStatus(Id, nStatus);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
                        }
                        finally
                        {
                            System.Threading.Thread.Sleep(2000);
                        }
                    }


                    Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\tRetrieving data from zoho.");
                    DataSet ds = zoho.GetSMS();
                    if (ds == null)
                    {
                        Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\tRetrieving data from zoho failed.");
                        continue;
                    }
                    if (!ds.Tables.Contains("FL"))
                    {
                        Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\tNo Pending Messages.");
                        continue;
                    }

                    DataTable dtTemp = ds.Tables["FL"];
                    data = Pivot(dtTemp, dtTemp.Columns["val"], dtTemp.Columns["FL_Text"]);

                    if (!data.Columns.Contains("Lead_ID")) data.Columns.Add("Lead_ID");
                    if (!data.Columns.Contains("Lead")) data.Columns.Add("Lead");

                    if (!data.Columns.Contains("Client_ID")) data.Columns.Add("Client_ID");
                    if (!data.Columns.Contains("Client")) data.Columns.Add("Client");

                    if (!data.Columns.Contains("Status")) data.Columns.Add("Status");

                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        try
                        {
                            if (Convert.ToString(data.Rows[i]["Status"]) != "Pending") continue;

                            if (i == 0)
                                dtRecord = DateTime.ParseExact(Convert.ToString(data.Rows[i]["Created Time"]), "yyyy-MM-dd HH:mm:ss", null);
                            else if (dtRecord < DateTime.ParseExact(Convert.ToString(data.Rows[i]["Created Time"]), "yyyy-MM-dd HH:mm:ss", null))
                                dtRecord = DateTime.ParseExact(Convert.ToString(data.Rows[i]["Created Time"]), "yyyy-MM-dd HH:mm:ss", null);

                            if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i]["Lead_ID"])))
                            {
                                nModuleType = 1;
                                sModuleId = Convert.ToString(data.Rows[i]["Lead_ID"]);
                                sName = Convert.ToString(data.Rows[i]["Lead"]);
                            }
                            else if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[i]["Client_ID"])))
                            {
                                nModuleType = 2;
                                sModuleId = Convert.ToString(data.Rows[i]["Client_ID"]);
                                sName = Convert.ToString(data.Rows[i]["Client"]);
                            }
                            else
                            {
                                continue;
                            }

                            databaseProvider.InsertSMS(Convert.ToString(data.Rows[i]["CUSTOMMODULE2_ID"]), sModuleId,
                                    sName, Convert.ToString(data.Rows[i]["Template Message"]),
                                    Convert.ToString(data.Rows[i]["Message"]), nModuleType,
                                    DateTime.ParseExact(Convert.ToString(data.Rows[i]["Created Time"]), "yyyy-MM-dd HH:mm:ss", null),
                                    DateTime.ParseExact(Convert.ToString(data.Rows[i]["Modified Time"]), "yyyy-MM-dd HH:mm:ss", null),
                                    DateTime.ParseExact(Convert.ToString(data.Rows[i]["Last Activity Time"]), "yyyy-MM-dd HH:mm:ss", null), Convert.ToString(data.Rows[i]["Subject"]));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
                        }
                    }



                    //Retrieve Lead Details if any Lead is new
                    data = databaseProvider.GetNewLeadId();
                    if (data == null) continue;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        try
                        {
                            Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\tRetriveing Lead Details for Id " + Convert.ToString(data.Rows[0]["ModuleId"]));
                            ds = zoho.GetLeadInfo(Convert.ToString(data.Rows[0]["ModuleId"]));

                            if (ds == null)
                            {
                                continue;
                            }
                            if (!ds.Tables.Contains("FL"))
                            {
                                continue;
                            }

                            dtTemp = ds.Tables["FL"];
                            DataTable LeadInfo = Pivot(dtTemp, dtTemp.Columns["val"], dtTemp.Columns["FL_Text"]);
                            databaseProvider.InsertLeadInfo(Convert.ToString(LeadInfo.Rows[0]["LEADID"]), Convert.ToString(LeadInfo.Rows[0]["First Name"]), Convert.ToString(LeadInfo.Rows[0]["Last Name"]), Convert.ToString(LeadInfo.Rows[0]["Email"]), Convert.ToString(LeadInfo.Rows[0]["Phone"]));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
                        }
                    }

                    //Retrieve Contact Details if any Contact is new
                    data = databaseProvider.GetNewContactId();
                    if (data == null) continue;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        try
                        {
                            Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\tRetriveing Contact Details for Id " + Convert.ToString(data.Rows[0]["ModuleId"]));
                            ds = zoho.GetContactInfo(Convert.ToString(data.Rows[0]["ModuleId"]));

                            if (ds == null)
                            {
                                continue;
                            }
                            if (!ds.Tables.Contains("FL"))
                            {
                                continue;
                            }

                            dtTemp = ds.Tables["FL"];
                            DataTable ContactInfo = Pivot(dtTemp, dtTemp.Columns["val"], dtTemp.Columns["FL_Text"]);
                            databaseProvider.InsertContactInfo(Convert.ToString(ContactInfo.Rows[0]["CONTACTID"]), Convert.ToString(ContactInfo.Rows[0]["First Name"]), Convert.ToString(ContactInfo.Rows[0]["Last Name"]), Convert.ToString(ContactInfo.Rows[0]["Email"]), Convert.ToString(ContactInfo.Rows[0]["Phone"]));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tZOHO\t\t" + ex.Message);
                }
                finally
                {
                    System.Threading.Thread.Sleep(nZoho_Sync_Time);
                }
            }
        }

        private void RC_Sync()
        {
            RingCentral rc = new RingCentral();
            int nWaitTime = 25000;
            int nExpireTime = 60;
            string CountryCode = "+1";

            string SMSId = string.Empty;
            string FullName = string.Empty;
            string Template = string.Empty;
            string CustomMsg = string.Empty;
            string FirstName = string.Empty;
            string LastName = string.Empty;
            string Email = string.Empty;
            string Phone = string.Empty;
            DateTime CreateDT = Convert.ToDateTime("01/01/1900");
            MessageStatus SendStatus = null;

            string From = string.Empty;
            string To = string.Empty;
            string Msg = string.Empty;


            Zoho zoho = new Zoho();
            DataTable UserInfo = databaseProvider.getUserInfoById(UserId);
            if (UserInfo.Rows.Count > 0)
            {
                zoho.AuthToken = UserInfo.Rows[0]["configurationAuthToken"].ToString();
            }

            while (true)
            {
                try
                {
                    /*
                   [ringcentral]
                   url=https://platform.devtest.ringcentral.com
                   app_key=pySpy2OiScuRvdwrtQE4Zg
                   app_secret=BslJ2D5DQu-W3GsR9_c2XQn2gTKWWnRtOcQbBGqZOZwg
                   uid=16475599844
                   pwd=Bankprop100
                   extension=101
                   from=+16475599844
                   smspermin=4
                   sync_time=120000
                   [zoho]
                   zsc=909d6eb4aa5924dfb2d27274c34e9c5c
                   tz=(UTC-07:00) Mountain Time (US & Canada)
                   sync_time=120000
                   [syncstatus]
                   zoho=05/14/2016 00:00:00
                   rc=05/14/2016 00:00:00
                   [db]
                   src=SMS.dat
                   [info]
                   smsexpire=60
                   countrycode=+1

                */
                    DataTable dt = new DataTable();
                    dt = databaseProvider.getUserSmsConfigurationinfo(UserId);
                    rc.ServiceURL = dt.Rows[0]["url"].ToString();
                    rc.App_Key = dt.Rows[0]["app_key"].ToString();
                    rc.App_Secret = dt.Rows[0]["app_secret"].ToString();
                    rc.UserId = dt.Rows[0]["uid"].ToString();
                    rc.Password = dt.Rows[0]["pwd"].ToString();
                    rc.Extension = dt.Rows[0]["extension"].ToString();

                    From = dt.Rows[0]["from"].ToString(); ;


                    nExpireTime = Convert.ToInt32(dt.Rows[0]["smsexpire"].ToString());
                    CountryCode = dt.Rows[0]["countrycode"].ToString();

                    DataTable DT = databaseProvider.GetPendingMessage();
                    if (DT == null) continue;

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        try
                        {
                            SMSId = Convert.ToString(DT.Rows[i]["SMSId"]);
                            FullName = Convert.ToString(DT.Rows[i]["FullName"]);
                            Template = Convert.ToString(DT.Rows[i]["Template"]);
                            CustomMsg = Convert.ToString(DT.Rows[i]["CustomMsg"]);
                            FirstName = Convert.ToString(DT.Rows[i]["FirstName"]);
                            LastName = Convert.ToString(DT.Rows[i]["LastName"]);
                            Email = Convert.ToString(DT.Rows[i]["Email"]);
                            Phone = Convert.ToString(DT.Rows[i]["Phone"]);
                            CreateDT = Convert.ToDateTime(DT.Rows[i]["CreateDT"]);
                            SendStatus = null;

                            if (GetCurrentDateTime().Subtract(CreateDT).TotalMinutes > nExpireTime)
                            {
                                Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRC\t\tSMS ID " + SMSId + " expired.");
                                databaseProvider.UpdateSMSStatus(SMSId, "Expire");
                                zoho.UpdateStatus(SMSId, Zoho.MessageStatus.Failed);
                                continue;
                            }

                            Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRC\t\tAuthenticating.");
                            if (!rc.GetAccess())
                                throw new Exception("Authorization failed with ring central.");

                            if (Template.Contains("None") || String.IsNullOrEmpty(Template))
                            {
                                Msg = CustomMsg;
                            }
                            else
                            {
                                Msg = Template;
                                Msg = Msg.Replace("(custom_msg)", CustomMsg);
                                Msg = Msg.Replace("(leadname)", FullName);
                                Msg = Msg.Replace("(contactname)", FullName);
                                Msg = Msg.Replace("(fullname)", FullName);
                                Msg = Msg.Replace("(firstname)", FirstName);
                                Msg = Msg.Replace("(lastname)", LastName);
                                Msg = Msg.Replace("(email)", Email);
                                Msg = Msg.Replace("(phone)", Phone);
                            }

                            To = GetNumericString(Phone);
                            if (To.Length < 10)
                            {
                                databaseProvider.UpdateSMSStatus(SMSId, "Failed");
                                zoho.UpdateStatus(SMSId, Zoho.MessageStatus.Failed);
                                continue;
                            }

                            To = To.PadLeft(11, '0');
                            To = CountryCode + To.Substring(1);

                            SendStatus = rc.SendMessage(From, To, Msg);
                            if (SendStatus != null)
                            {
                                Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRC\t\tSMS sent to " + To + " [" + FullName + "].");
                                databaseProvider.UpdateSMSStatus(SMSId, "Processed");
                                databaseProvider.InsertSMSStatus(SMSId, SendStatus.Id, SendStatus.ConversationId, SendStatus.MsgStatus, SendStatus.ReadStatus, SendStatus.Priority, SendStatus.AttemptsCount, SendStatus.CreationTime, GetCurrentDateTime());
                                zoho.UpdateStatus(SMSId, Zoho.MessageStatus.Sent);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRC\t\t" + ex.Message);
                        }
                        finally
                        {
                            System.Threading.Thread.Sleep(nWaitTime);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRC\t\t" + ex.Message);
                }
                finally
                {
                    System.Threading.Thread.Sleep(nRC_Sync_Time);
                }
            }
        }

        private void Received_Sync()
        {
            try
            {
                RingCentral rc = new RingCentral();
                DataTable dt = new DataTable();

                /*
                    [ringcentral]
                    url=https://platform.devtest.ringcentral.com
                    app_key=pySpy2OiScuRvdwrtQE4Zg
                    app_secret=BslJ2D5DQu-W3GsR9_c2XQn2gTKWWnRtOcQbBGqZOZwg
                    uid=16475599844
                    pwd=Bankprop100
                    extension=101
                    from=+16475599844
                    smspermin=4
                    sync_time=120000
                    [zoho]
                    zsc=909d6eb4aa5924dfb2d27274c34e9c5c
                    tz=(UTC-07:00) Mountain Time (US & Canada)
                    sync_time=120000
                    [syncstatus]
                    zoho=05/14/2016 00:00:00
                    rc=05/14/2016 00:00:00
                    [db]
                    src=SMS.dat
                    [info]
                    smsexpire=60
                    countrycode=+1

                 */
                dt = databaseProvider.getUserSmsConfigurationinfo(UserId);
                rc.ServiceURL = dt.Rows[0]["url"].ToString();
                rc.App_Key = dt.Rows[0]["app_key"].ToString();
                rc.App_Secret = dt.Rows[0]["app_secret"].ToString();
                rc.UserId = dt.Rows[0]["uid"].ToString();
                rc.Password = dt.Rows[0]["pwd"].ToString();
                rc.Extension = dt.Rows[0]["extension"].ToString();

                while (true)
                {
                    try
                    {
                        Console.WriteLine("Info:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRCV\t\tAuthenticating.");
                        if (!rc.GetAccess())
                            throw new Exception("Authorization failed with ring central.");

                        var msgs = rc.GetMessageList();
                        foreach (MessageStatus m in msgs)
                        {
                            databaseProvider.InsertReceivedSMS(m.Id, m.ConversationId, m.From, m.Subject, m.MsgStatus, m.ReadStatus, m.Priority, m.LastModifiedTime, GetCurrentDateTime());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRCV\t\t" + ex.Message);
                    }
                    finally
                    {
                        System.Threading.Thread.Sleep(nRC_Sync_Time);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Err:" + DateTime.Now.ToString("yyyyMMddHHmmss") + ":\tRCV\t\t" + ex.Message);
            }
        }

        DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            // find primary key columns 
            //(i.e. everything but pivot column and pivot value)
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            // prep results table
            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();
            dt.AsEnumerable()
                .Select(r => r[pivotColumn.ColumnName].ToString())
                .Distinct().ToList()
                .ForEach(c => result.Columns.Add(c, typeof(string)));

            // load it
            foreach (DataRow row in dt.Rows)
            {
                // find row to update
                DataRow aggRow = result.Rows.Find(
                    pkColumnNames
                        .Select(c => row[c])
                        .ToArray());
                // the aggregate used here is LATEST 
                // adjust the next line if you want (SUM, MAX, etc...)
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];
            }
            return result;
        }
        private DateTime GetCurrentDateTime()
        {
            string sTemp = DateTime.UtcNow.ToString();
            string sTimeZone = "(UTC-07:00) Mountain Time (US & Canada)";
            TimeZoneInfo timeZoneinfo = null;
            List<TimeZoneInfo> listTimeZone = TimeZoneInfo.GetSystemTimeZones().ToList();
            foreach (TimeZoneInfo tz in listTimeZone)
            {
                if (tz.DisplayName == sTimeZone)
                    timeZoneinfo = tz;
            }
            if (timeZoneinfo == null)
                timeZoneinfo = TimeZoneInfo.Local;

            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneinfo);
        }
        public string GetNumericString(string Data)
        {
            string sTemp = string.Empty;
            int nTemp = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                if (int.TryParse(Data.Substring(i, 1), out nTemp))
                    sTemp += Data.Substring(i, 1);
            }
            return sTemp;
        }
        #endregion
    }
}