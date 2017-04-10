using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;

namespace AdminTool.Model
{
    public class summaryEmail
    {
        static DataBaseProvider databaseProvider = new DataBaseProvider();
        static string BaseUrl = "http://mail2lead.infobyd.com/frmpassword.aspx?";

        public static string sendApiLimitExceedInfoMail(int UserId)
        {
            string result = string.Empty, FirstName, Token, URL;
            string ToEmail, SubjectLine, BodyofEmail;
            try
            {
                DataTable UserInfo = databaseProvider.getUserBasicInfo(UserId);
                if (UserInfo.Rows.Count < 0)
                { return null; }
                FirstName = UserInfo.Rows[0]["FirstName"].ToString();
                ToEmail = UserInfo.Rows[0]["EmailId"].ToString();
                Token = databaseProvider.get_new_token_for_user(UserId);
                SubjectLine = "Information Mail Mail2Lead API limit Exceed";
                BodyofEmail = "Hi " + FirstName + ", \n\n Your Mail2Lead API limit Exceed. Please Check your API limit status at http://mail2lead.infobyd.com. \n\n If you required any help or query so contact us at Team@infobyd.com .\n\n Thanks, \nInfobyd Team";
                result = sendActionEmail(ToEmail, SubjectLine, BodyofEmail, null);

            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }

            return result;
        }

        public static string sendUserSignupWelcomeMail(int UserId)
        {
            string result = string.Empty, FirstName, Token, URL;
            string ToEmail, SubjectLine, BodyofEmail;
            try
            {
                DataTable UserInfo = databaseProvider.getUserBasicInfo(UserId);
                if (UserInfo.Rows.Count < 0)
                { return null; }
                FirstName = UserInfo.Rows[0]["FirstName"].ToString();
                ToEmail = UserInfo.Rows[0]["EmailId"].ToString();
                Token = databaseProvider.get_new_token_for_user(UserId);
                URL = BaseUrl + "type=121sdf16s6456fa413cc2b4cb03bdba2ed77&uid=" + Token;
                SubjectLine = "WelCome to Infobyd Team";
                BodyofEmail = "Hi " + FirstName + ", \n\n Please Click Below Link to Complete your Signup process. :- \n\n" + URL + "\n\n If you required any help or query so contact us at Team@infobyd.com. \n\n Thanks, \nInfobyd Team";
                result = sendActionEmail(ToEmail, SubjectLine, BodyofEmail, null);

            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }
            return result;
        }

        public static string sendUserMail2LeadRespotStatus(int UserId,string AppFolderPath)
        {
            string result = string.Empty, FirstName, AttachmentUrl = null;
            string ToEmail, SubjectLine, BodyofEmail;
            int numberOfDays=0;
            try
            {
                DataTable UserInfo = databaseProvider.getUserBasicInfo(UserId);
                if (UserInfo.Rows.Count < 0)
                { return null; }
                FirstName = UserInfo.Rows[0]["FirstName"].ToString();
                ToEmail = UserInfo.Rows[0]["EmailId"].ToString();
                numberOfDays =Convert.ToInt32(UserInfo.Rows[0]["Days"].ToString());
                AttachmentUrl =CaculateMail2LeadStatusReport(UserId, AppFolderPath, numberOfDays);
                SubjectLine = "Mail2Lead Status Report for" + ToEmail + " On " + DateTime.Now.ToString();
                BodyofEmail = "Hi " + FirstName + ", \n\n Please Find your Mail2Lead Status Resport for" + ToEmail + " On " + DateTime.Now.ToString() + " If you required any help or query so contact us at Team@infobyd.com. \n\n Thanks, \nInfobyd Team";
                result = sendActionEmail(ToEmail, SubjectLine, BodyofEmail, AttachmentUrl);

            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }
            return result;
        }

        public static string sendUserResetPasswordEmail(int UserId)
        {
            string result = string.Empty, FirstName, Token, URL;
            string ToEmail, SubjectLine, BodyofEmail;
            try
            {
                DataTable UserInfo = databaseProvider.getUserBasicInfo(UserId);
                if (UserInfo.Rows.Count < 0)
                { return null; }
                FirstName = UserInfo.Rows[0]["FirstName"].ToString();
                ToEmail = UserInfo.Rows[0]["EmailId"].ToString();
                Token = databaseProvider.get_new_token_for_user(UserId);
                URL = BaseUrl + "type=240410248a413cc2b4cb03bdba2ed77&uid=" + Token;
                SubjectLine = "Reset Password for Mail2Lead";
                BodyofEmail = "Hi " + FirstName + ", \n\n Please Click Below Link to Reset your password :- \n\n" + URL + "\n\n If you required any help or query so contact us at Team@infobyd.com . \n\n Thanks, \nInfobyd Team";
                result = sendActionEmail(ToEmail, SubjectLine, BodyofEmail, null);

            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }
            return result;
        }


        private static string sendActionEmail(string toEmail, string subjectLine, string bodyOfMail, string attachmentUrl)
        {
            string result = string.Empty;
            try
            {
                toEmail = "Rohitb@infobyd.com";
                Attachment attachment = null;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("rohit.baghela@outlook.com");
                mail.To.Add(toEmail);
                mail.To.Add("alok@infobyd.com");
                mail.Subject = subjectLine;
                mail.Body = bodyOfMail;
                mail.Bcc.Add("Team@infobyd.com");
                if (!string.IsNullOrEmpty(attachmentUrl))
                {
                    attachment = new Attachment(attachmentUrl);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Credentials = new System.Net.NetworkCredential("rohit.baghela@outlook.com", "888Jana*");
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Send(mail);
                if (!string.IsNullOrEmpty(attachmentUrl))
                {
                    attachment.Dispose();
                    File.Delete(attachmentUrl);
                }
                result = "SUCCESS";
            }
            catch (Exception ex)
            {
                result = "FAIL";
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }
            return result;
        }
        private static string CaculateMail2LeadStatusReport(int UserId,string AppFolderPath,int numberOfDays)
        {
            string result = null, record_id = string.Empty, DatabaseId = string.Empty, subjectLine = string.Empty, value_from_mail = string.Empty;
            string serviceTime = string.Empty, STATUS = string.Empty;
            try
            {
                DataTable UserInfo = databaseProvider.getUserAutoSyncReport(UserId, numberOfDays);
                if (UserInfo.Rows.Count < 0)
                { return null; }
                
                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = UserInfo.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in UserInfo.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
                
                result = AppFolderPath+"report_"+UserId+"_"+ DateTime.Now.ToString("dd/MM/yyyy").Replace('/','_')+".csv";
                File.WriteAllText(result, sb.ToString());
            }
            catch (Exception ex)
            {
                result = null;
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                databaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Mail Sending Fail");
            }
            return result;
        }
    }
}