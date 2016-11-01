using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AdminTool.Model
{
    public class summaryEmail
    {
        public static void SendSumaryMail(int UserId)
        {
            try
            {
                string Email = string.Empty;
                string sFileName = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\M2L\\" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + ".txt";
                if (!File.Exists(sFileName)) return;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("Newone1automation@gmail.com");
                //mail.To.Add("ayushmaheshwari@outlook.com");
                mail.To.Add("devkumar2486@gmail.com");
                mail.To.Add("zohoalok@gmail.com");
                mail.To.Add("apurva.21singh@gmail.com");
                mail.To.Add("aloklnct1985@gmail.com");
                mail.Subject = "Newone1-Report:";
                mail.Body = "Newone1-Report for mail ac: " + Email + ", Please find attachment.";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(sFileName);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Newone1automation", "Newone1automation@123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                attachment.Dispose();
                File.Delete(sFileName);
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }
        public static void sendApiLimitExceedInfoMail(int UserId)
        {
        }
        public static void sendApiLimitExceedInfoMail1(int UserId)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("noreply@infobyd.com");
            //mail.To.Add("ayushmaheshwari@outlook.com");
            mail.To.Add("Rohitb@infobyd.com");
            mail.Subject = "Test Email";
            mail.Body = "Test Email";

           /* Attachment attachment;
            attachment = new System.Net.Mail.Attachment(sFileName);
            mail.Attachments.Add(attachment);*/

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("Rohitb@infobyd.com", "555Baghela*");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            //attachment.Dispose();
        }
    }
}