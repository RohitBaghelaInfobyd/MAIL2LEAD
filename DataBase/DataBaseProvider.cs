using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AdminTool.DataBase
{
    public class DataBaseProvider
    {
        public static string myConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public DataTable LoginUser(string Emailid, string Password)
        {
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_user_login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sEmailId", MySqlDbType.VarChar).Value = Emailid;
                    cmd.Parameters.Add("sPassword", MySqlDbType.VarChar).Value = Password;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable AddNewUserIntoDatabase(int CreatorId, string FirstName, string LastName, string EmailId, string Password, int sType)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_new_user", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sFirstName", MySqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@sLastName", MySqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@sEmailId", MySqlDbType.VarChar).Value = EmailId;
                    cmd.Parameters.Add("@sPassword", MySqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("@sProfileImage", MySqlDbType.VarChar).Value = "NA";
                    cmd.Parameters.Add("@sType", MySqlDbType.Int32).Value = sType;
                    cmd.Parameters.Add("@sCreatorId", MySqlDbType.Int32).Value = CreatorId;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        internal DataTable getUserMailChartInfo(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_number_of_mails_count_between_date", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    cmd.Parameters.Add("@sStartDate", MySqlDbType.VarChar).Value = "29/10/2014";
                    cmd.Parameters.Add("@sEndDate", MySqlDbType.VarChar).Value = "29/10/2017"; ;
                    cmd.Parameters.Add("@sSubjectId", MySqlDbType.Int32).Value = 0;
                    cmd.Parameters.Add("@sStatusId", MySqlDbType.Int32).Value = 0;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        internal DataTable getMailLeasAssignmentInfoToSubmitIntoCRM(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_lead_assignment_info_to_submit_in_crm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public string updateExisingUserInfoIntoDataBase(int ViewUserId, string FirstName, string LastName, string EmailId, int sType, string Password)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_user_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sFirstName", MySqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@sLastName", MySqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@sEmailId", MySqlDbType.VarChar).Value = EmailId;
                    cmd.Parameters.Add("@sType", MySqlDbType.Int32).Value = sType;
                    cmd.Parameters.Add("@sPassword", MySqlDbType.VarChar).Value = Password;
                    cmd.Parameters.Add("@sProfileImage", MySqlDbType.VarChar).Value = "Na";
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = ViewUserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }


        internal string InsertUserGmailInfo(int viewUserId, string gmailId, string gmailPassword, string configurationToken)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_update_user_gmail_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserId", MySqlDbType.Int32).Value = viewUserId;
                    cmd.Parameters.Add("sGmailId", MySqlDbType.VarChar).Value = gmailId;
                    cmd.Parameters.Add("sGmailPassword", MySqlDbType.VarChar).Value = gmailPassword;
                    cmd.Parameters.Add("sConfigurationToken", MySqlDbType.VarChar).Value = configurationToken;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public DataTable getUserInfoById(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_user_info_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getUserTemplateStatus(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_template_status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }




        public DataTable getUserBasicInfo(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_user_basic_info_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getUserAutoSyncReport(int userId, int Days)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_daily_report_data", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.Int32).Value = userId;
                    cmd.Parameters.Add("sNumberOfDays", MySqlDbType.Int32).Value = Days;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getUserGmailInfoById(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_user_gmail_info_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable getSMSUserInfoById(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_sms_user_info_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }



        public DataTable getApiSubscriptionInfo()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("get_api_subscription_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal string InsertUserSmsInfo(int viewUserId, string smsUserId, string smsUserPassword, string smsAppKey, string SmsAppSecreyKey, string smsConfigurationToken, string SmsFrom, bool UseDefault, string ModuleName)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_update_user_sms_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserId", MySqlDbType.Int32).Value = viewUserId;
                    cmd.Parameters.Add("sSmsUserID", MySqlDbType.VarChar).Value = smsUserId;
                    cmd.Parameters.Add("sSmsUserPassword", MySqlDbType.VarChar).Value = smsUserPassword;
                    cmd.Parameters.Add("sAppKey", MySqlDbType.VarChar).Value = smsAppKey;
                    cmd.Parameters.Add("sAppSecreyKey", MySqlDbType.VarChar).Value = SmsAppSecreyKey;
                    cmd.Parameters.Add("sSmsConfigurationToken", MySqlDbType.VarChar).Value = smsConfigurationToken;
                    cmd.Parameters.Add("sSmsFrom", MySqlDbType.VarChar).Value = SmsFrom;
                    if (UseDefault)
                    {
                        cmd.Parameters.Add("sUseDefault", MySqlDbType.Int32).Value = 1;
                    }
                    else
                    {
                        cmd.Parameters.Add("sUseDefault", MySqlDbType.Int32).Value = 0;
                    }
                    cmd.Parameters.Add("sModuleName", MySqlDbType.VarChar).Value = ModuleName;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public DataTable getListOfallUser(int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_user", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable getListOfallUserForCronJob()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_for_cron_job", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getListOfAllUserToSendReport()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_user_to_send_api_report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int spInsertCronJobStatus(string Description, string AutoSyncFrom)
        {
            DataTable dt = new DataTable();
            int jobId = 0;
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_cron_job", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sDescription", MySqlDbType.VarChar).Value = Description;
                    cmd.Parameters.Add("sAutoSyncFrom", MySqlDbType.VarChar).Value = AutoSyncFrom;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    jobId = Convert.ToInt32(dt.Rows[0]["result"].ToString());
                }
            }
            return jobId;
        }


        public int updateCronJobStatus(string Description, int jobId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_cron_job", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sDescription", MySqlDbType.VarChar).Value = Description;
                    cmd.Parameters.Add("sJobId", MySqlDbType.Int32).Value = jobId;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    jobId = Convert.ToInt32(dt.Rows[0]["result"].ToString());
                }
            }
            return jobId;
        }


        public DataTable getApiStatusReport(int UserId)
        {

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_api_status_report", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sUserId", MySqlDbType.VarChar).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            return dt;
        }

        public DataTable getUserPaymentHistory(int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_user_payment_history", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sUserId", MySqlDbType.VarChar).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            return dt;
        }

        public DataTable getMailReport(int UserId, string StartDate, string EndDate, int SubjectId, int StatusId)
        {

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_report_data_between_date", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("sStartDate", MySqlDbType.VarChar).Value = StartDate;
                    cmd.Parameters.Add("sEndDate", MySqlDbType.VarChar).Value = EndDate;
                    cmd.Parameters.Add("sSubjectId", MySqlDbType.Int32).Value = SubjectId;
                    cmd.Parameters.Add("sStatusId", MySqlDbType.Int32).Value = StatusId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            return dt;


        }

        public DataTable getAvailablePaymentOption(int UserId)
        {

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_all_available_payment_option", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sUserId", MySqlDbType.VarChar).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                }
            }
            return dt;
        }



        public DataTable getListOfAllUserSubject(int UserId, int IsApprovedOrAll, int subjectId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_subject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("sIsApprovedOrAll", MySqlDbType.Int32).Value = IsApprovedOrAll;
                    cmd.Parameters.Add("sSubjectId", MySqlDbType.Int32).Value = subjectId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable getListOfMailContentSplitInfo(int SubjectId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_mail_content_split_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sSubjectId", MySqlDbType.VarChar).Value = SubjectId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getListOfAllDefaultLeadColumn(int SubjectId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_default_value_column", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sSubjectId", MySqlDbType.VarChar).Value = SubjectId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable updateMailSplitContentCompleteStatus(int MailId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_is_split_mail_content_completed", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sMailId", MySqlDbType.VarChar).Value = MailId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable updateMailContentSubmitToCrmStatusWithError(int MailId, string Error, string MailXml)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_is_info_submited_into_crm_with_Error", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sMailId", MySqlDbType.VarChar).Value = MailId;
                    cmd.Parameters.Add("sInsertError", MySqlDbType.VarChar).Value = Error;
                    cmd.Parameters.Add("sMailXmlFile", MySqlDbType.VarChar).Value = MailXml;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void logApplicationError(string ErrorDetail, string ErrorType)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_error_info_of_application", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sErrorDetail", MySqlDbType.VarChar).Value = ErrorDetail;
                    cmd.Parameters.Add("sErrorType", MySqlDbType.VarChar).Value = ErrorType;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }


        public DataTable getListOfunfiledHeaderofMailContentSplit(int SubjectId, int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_unfiled_header_of_mail_content_split", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sSubjectId", MySqlDbType.VarChar).Value = SubjectId;
                    cmd.Parameters.Add("sUserId", MySqlDbType.VarChar).Value = UserId;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }



        public DataTable getListofColumnHeaderOfLeadToMailTable(int UserId, int isAll)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_mail_to_lead_column_header_list", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = UserId;
                    cmd.Parameters.Add("sIsAll", MySqlDbType.VarChar).Value = isAll;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int getLeadOwnerColumnIdForLeadAssignment(int UserId, int isAll)
        {
            int LeadOwnerColumnId = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_mail_to_lead_column_header_list_for_owner", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = UserId;
                    cmd.Parameters.Add("sIsAll", MySqlDbType.VarChar).Value = isAll;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        LeadOwnerColumnId = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            return LeadOwnerColumnId;
        }



        public DataTable getListOfAllUnUsedUniqueIdentifier(int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_list_of_all_unUsed_unique_identifier", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sUserid", MySqlDbType.VarChar).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string AddNewColumnHeaderOfLeadToMail(string LeadColumnHeader, int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_new_mail_to_lead_header_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sLeadColumnHeader", MySqlDbType.VarChar).Value = LeadColumnHeader;
                    cmd.Parameters.Add("@sUserid", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string AddNewMailContentSplitInfo(string sStartText, string sEndText, int sColumnHeaderId, int sSubjectId, string splitValueText, int splitIndex, string SplitType, Boolean isValueSplit, Boolean IsDefaultValueCheck, string IsDefaultValuetype, string defaultValue)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_mail_content_split_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sStartText", MySqlDbType.VarChar).Value = sStartText;
                    cmd.Parameters.Add("@sEndText", MySqlDbType.VarChar).Value = sEndText;
                    cmd.Parameters.Add("@sSubjectId", MySqlDbType.Int32).Value = sSubjectId;
                    cmd.Parameters.Add("@sColumnHeaderId", MySqlDbType.Int32).Value = sColumnHeaderId;
                    cmd.Parameters.Add("@sSplitValueText", MySqlDbType.VarChar).Value = splitValueText;
                    cmd.Parameters.Add("@sSplitIndex", MySqlDbType.Int32).Value = splitIndex;
                    cmd.Parameters.Add("@sSplitType", MySqlDbType.VarChar).Value = SplitType;
                    if (isValueSplit)
                        cmd.Parameters.Add("@sIsValueSplit", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sIsValueSplit", MySqlDbType.Int32).Value = 0;

                    if (IsDefaultValueCheck)
                        cmd.Parameters.Add("@sIsDefaultValueCheck", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sIsDefaultValueCheck", MySqlDbType.Int32).Value = 0;
                    
                    cmd.Parameters.Add("@sIsDefaultValuetype", MySqlDbType.VarChar).Value = IsDefaultValuetype;
                    cmd.Parameters.Add("@sDefaultValue", MySqlDbType.VarChar).Value = defaultValue;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }
        
        public string AddNewSubject(string SubjectLine, int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_new_subject", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sSubejctLine", MySqlDbType.VarChar).Value = SubjectLine;
                    cmd.Parameters.Add("@sUserid", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@UTC_TIMESTAMP", MySqlDbType.DateTime).Value = System.DateTime.UtcNow;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string UpdateSubjectInfoById(string SubjectLine, int SubjectId, bool IsApproved,int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_subject_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sSubejctLine", MySqlDbType.VarChar).Value = SubjectLine;
                    cmd.Parameters.Add("@sSubjectId", MySqlDbType.Int32).Value = SubjectId;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    
                    if (IsApproved)
                        cmd.Parameters.Add("@sIsApproved", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sIsApproved", MySqlDbType.Int32).Value = 0;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string DeleteSubjectById(int SubjectId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_subject_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sSubjectId", MySqlDbType.VarChar).Value = SubjectId;
                    cmd.Parameters.Add("@UTC_TIMESTAMP", MySqlDbType.DateTime).Value = System.DateTime.UtcNow;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string DeleteMailContentSplitInfo(int RowId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_mail_content_split_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sMailContentId", MySqlDbType.VarChar).Value = RowId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }


        public string DeleteColumnHeaderOfLeadToMail(int MailToLeadColumnRowId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_mail_to_lead_column_header_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sColumnHeaderId", MySqlDbType.VarChar).Value = MailToLeadColumnRowId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }


        public string UpdateColumnHeaderOfLeadToMailInfo(int MailToLeadColumnRowId, string LeadColumnHeader, int UserId, bool isSubscribe)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_mail_to_lead_column_header_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sColumnHeaderId", MySqlDbType.Int32).Value = MailToLeadColumnRowId;
                    cmd.Parameters.Add("@sLeadColumnHeader", MySqlDbType.VarChar).Value = LeadColumnHeader;
                    if (isSubscribe)
                        cmd.Parameters.Add("@sIsSubscribe", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sIsSubscribe", MySqlDbType.Int32).Value = 0;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string UpdateLeadColumnUpdateColumnValue(int MailToLeadColumnRowId, Boolean isToBeUpdate)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_lead_column_action_update_value", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sColumnHeaderId", MySqlDbType.Int32).Value = MailToLeadColumnRowId;
                    if (isToBeUpdate)
                        cmd.Parameters.Add("@sIsToBeUpdate", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sIsToBeUpdate", MySqlDbType.Int32).Value = 0;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public DataTable GetLastOneMonthReport(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_last_one_month_report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public string deleteUser(int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_user_by_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public string updateUserPaymentStatus(int PaymentID, int IsActive)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_user_payment_status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("sPaymentID", MySqlDbType.VarChar).Value = PaymentID;
                    cmd.Parameters.Add("sIsActive", MySqlDbType.VarChar).Value = IsActive;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();

                }
            }
        }

        /************** Following For Automation */

        public DateTime GetLastProcessedTime(int userId)
        {
            DateTime result = DateTime.Now.ToUniversalTime();
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_user_mail_last_processed_time", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        if (DateTime.TryParse(dt.Rows[0]["result"].ToString(), out result))
                        {
                            result = Convert.ToDateTime(dt.Rows[0]["result"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        public int InsertMailIntoDataBase(int SubjectId, string mailBody, DateTime mailTime, string SyncType, uint mailUid)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_mail_detail_info_of_user", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sSubject_id", MySqlDbType.Int32).Value = SubjectId;
                    cmd.Parameters.Add("@sMail_content_body", MySqlDbType.Text).Value = mailBody;
                    cmd.Parameters.Add("@sMail_actual_date_time", MySqlDbType.DateTime).Value = mailTime;
                    cmd.Parameters.Add("@sSyncType", MySqlDbType.Text).Value = SyncType;
                    cmd.Parameters.Add("@sMailUid", MySqlDbType.UInt32).Value = mailUid;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return int.Parse(dt.Rows[0]["result"].ToString().ToUpper());
                }
            }
        }

        public string get_new_token_for_user(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_generate_new_token", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString();
                }
            }
        }

        public string InsertUserMail2LeadReportStatus(int userId, int status)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_report_status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    cmd.Parameters.Add("@sStatus", MySqlDbType.Int32).Value = status;

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString();
                }
            }
        }

        public string sp_reset_new_password(int userId, string password)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_reset_user_password", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    cmd.Parameters.Add("@sPassword", MySqlDbType.VarChar).Value = password;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
        }

        public DataTable CheckUserToken(string Token)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_check_user_token", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sToken", MySqlDbType.VarChar).Value = Token;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string InsertMailSplitInfoFromBody(int MailDataBaseId, string Value, int contentSplitId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_lead_to_mail_column_content_value", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUser_mail_detail_id", MySqlDbType.Int32).Value = MailDataBaseId;
                    cmd.Parameters.Add("@sValue_from_mail", MySqlDbType.Text).Value = Value;
                    cmd.Parameters.Add("@sLead_to_mail_column_header_id", MySqlDbType.Int32).Value = contentSplitId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    result = dt.Rows[0]["result"].ToString().ToUpper();
                }
            }

            return result;
        }

        public DataTable getAllUnProcessedMailToPutIntoCrm(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_all_unprocessed_mail_to_insert_into_crm", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable getUserInfoByEmailID(string emailId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_user_info_by_emailid", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sEmailId", MySqlDbType.VarChar).Value = emailId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public bool CheckUserApiCallCountStatusOfUser(int userId)
        {
            bool result = false;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_check_use_api_call_count_status_of_user", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    result = bool.Parse(dt.Rows[0]["result"].ToString().ToUpper());
                }
            }

            return result;
        }

        public DataTable GetLeadToMailColumnValueByMailId(int mailId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_lead_to_mail_column_value_by_mail_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sMailId", MySqlDbType.Int32).Value = mailId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetOverViewOfAllApiCountStatus(int UserId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_over_all_api_status_report", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetYearlyReportForUser(int UserId, int Year)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_yearly_report_data", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sYear", MySqlDbType.Int32).Value = Year;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetUserMailReportByUserId(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("get_mail_report_by_user_id", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        internal void InsertSubmitedMailCRMInfo(int MailId, string record_time, string record_id, string Xml, string MailXml)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_mail_submit_crm_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sRecord_id", MySqlDbType.VarChar).Value = record_id;
                    cmd.Parameters.Add("@sRecord_time", MySqlDbType.VarChar).Value = record_time;
                    cmd.Parameters.Add("@sDatabaseMailId", MySqlDbType.Int32).Value = MailId;
                    cmd.Parameters.Add("@sServiceResponse", MySqlDbType.VarChar).Value = Xml;
                    cmd.Parameters.Add("@sMailXmlFile", MySqlDbType.VarChar).Value = MailXml;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }

        public DataTable GetUserTempMail()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("temp_sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetExistingEntryEvent()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_existing_entry_event", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
        public DataTable GetReportType()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_report_types", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }


        public DataTable GetListOfAllMailUids(int userId, string type)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("get_list_of_all_mail_uids", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = userId;
                    if (type.ToLower().Contains("auto"))
                        cmd.Parameters.Add("@sType", MySqlDbType.Int32).Value = 1;
                    else
                        cmd.Parameters.Add("@sType", MySqlDbType.Int32).Value = 0;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal string UpdatePaymentInfoByUserId(int UserId, string TransactionId, string PaymentSource, string PaymentAmount, int ApiCount)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_payment_status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sTransactionId", MySqlDbType.VarChar).Value = TransactionId;
                    cmd.Parameters.Add("@sPaymentSource", MySqlDbType.VarChar).Value = PaymentSource;
                    cmd.Parameters.Add("@sPaymentAmount", MySqlDbType.VarChar).Value = PaymentAmount;
                    cmd.Parameters.Add("@sApiCount", MySqlDbType.Int32).Value = ApiCount;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    result = dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
            return result;
        }

        internal string InsertNewMailUidIntoDataBase(int UserId, uint MailUId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_new_mail_uid", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sUid", MySqlDbType.Int32).Value = MailUId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    result = dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
            return result;
        }

        internal string UpdateMailUidIntoDataBase(int UserId, uint MailUId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_mail_uid_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sUid", MySqlDbType.Int32).Value = MailUId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    result = dt.Rows[0]["result"].ToString().ToUpper();
                }
            }
            return result;
        }



        internal DataTable GetLeadAssignmentInfo(int UserId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_lead_assignment_info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DataTable InstertLeadAssignmentInfo(string AssignmentName, int UserId, int AssignmentType, int LeadColumnHeaderId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_lead_assignment_detail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sAssignmentName", MySqlDbType.VarChar).Value = AssignmentName;
                    cmd.Parameters.Add("@sAssignmentType", MySqlDbType.Int32).Value = AssignmentType;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sLeadColumnHeaderId", MySqlDbType.Int32).Value = LeadColumnHeaderId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DataTable InsertNewUniqueIdentifier(int LeadColumnHeaderId, int UserId, string ActionType)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_insert_new_unique_identifier", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sLeadColumnHeaderId", MySqlDbType.VarChar).Value = LeadColumnHeaderId;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sActionType", MySqlDbType.VarChar).Value = ActionType;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DataTable getListOfAllUniqueIdentifier(int UserId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("get_list_of_all_unique_identifier", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal string DeleteUniqueIdentifierInfo(int UniqueIdentifierId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_unique_identifier", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUniqueIdentifierId", MySqlDbType.Int32).Value = UniqueIdentifierId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = dt.Rows[0][0].ToString();
                }
            }
            return result;
        }

        internal string UpdateLeadAssignmentInfo(string AssignmentName, int AssignmentId, int Status)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_lead_assignment_detail", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sAssignmentName", MySqlDbType.VarChar).Value = AssignmentName;
                    cmd.Parameters.Add("@sAssignmentId", MySqlDbType.Int32).Value = AssignmentId;
                    cmd.Parameters.Add("@sStatus", MySqlDbType.Int32).Value = Status;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = dt.Rows[0][0].ToString();
                }
            }
            return result;
        }

        internal string UpdateUniqueIdentifierId(int IdentifierId, string ActionType)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_unique_identifier", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sIdentifierId", MySqlDbType.Int32).Value = IdentifierId;
                    cmd.Parameters.Add("@sActionType", MySqlDbType.String).Value = ActionType;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = dt.Rows[0][0].ToString();
                }
            }
            return result;
        }

        internal string DeleteLeadAssignmentInfo(int AssignmentId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_delete_lead_assignment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sAssignmentId", MySqlDbType.Int32).Value = AssignmentId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        result = dt.Rows[0][0].ToString();
                }
            }
            return result;
        }


        internal DataTable UpdateUserAssignmentType(int UserId, int AssignmentType)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_lead_assignment_type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sAssignmentType", MySqlDbType.Int32).Value = AssignmentType;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DataTable UpdateExistingRecordType(int UserId, int actionType)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update_existing_record_type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    cmd.Parameters.Add("@sExistingInfoType", MySqlDbType.Int32).Value = actionType;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        internal DataTable GetUserAssignmentType(int UserId)
        {
            string result = string.Empty;
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(myConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_get_user_assignment_type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.Add("@sUserId", MySqlDbType.Int32).Value = UserId;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }



        internal DataTable GetPendingReceivedMessage()
        {
            throw new NotImplementedException();
        }

        internal string GetSubject(string ConversationId)
        {
            throw new NotImplementedException();
        }

        internal void UpdateReceivedSMSStatus(int Id, int nStatus)
        {
            throw new NotImplementedException();
        }

        internal void InsertSMS(string p, string sModuleId, string sName, string p_2, string p_3, int nModuleType, DateTime dateTime, DateTime dateTime_2, DateTime dateTime_3, string p_4)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetNewLeadId()
        {
            throw new NotImplementedException();
        }

        internal void InsertLeadInfo(string p, string p_2, string p_3, string p_4, string p_5)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetNewContactId()
        {
            throw new NotImplementedException();
        }

        internal DataTable getUserSmsConfigurationinfo(int UserId)
        {
            throw new NotImplementedException();
        }

        internal void UpdateSMSStatus(string SMSId, string p)
        {
            throw new NotImplementedException();
        }

        internal void InsertSMSStatus(string SMSId, string p, string p_2, string p_3, string p_4, string p_5, string p_6, string p_7, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        internal void InsertReceivedSMS(string p, string p_2, string p_3, string p_4, string p_5, string p_6, string p_7, string p_8, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        internal DataTable GetPendingMessage()
        {
            throw new NotImplementedException();
        }

        internal void InsertContactInfo(string p, string p_2, string p_3, string p_4, string p_5)
        {
            throw new NotImplementedException();
        }
    }
}
