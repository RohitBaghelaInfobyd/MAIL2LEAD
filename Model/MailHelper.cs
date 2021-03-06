﻿using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AdminTool.Model
{
    public class MailHelper
    {
        private string defaultImapHostName = "imap.gmail.com";
        private int defaultPort = 993;
        private ImapClient client;
        private bool isConnected = false;
        
        public bool connect(string userName, string password)
        {
            try
            {
                client = new ImapClient(defaultImapHostName, defaultPort, userName, password, AuthMethod.Login, true);

                Console.WriteLine("We are conneted with " + userName);
                isConnected = true;
                return true;
            }
            catch (InvalidCredentialsException ex)
            {
                Console.WriteLine("Invalid credential Exception " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                return false;
            }
        }


        public void disconnect()
        {
            //client.Logout();
            client.Dispose();
            isConnected = false;
            Console.WriteLine("disconnected from mail");
        }
        public IEnumerable<MailMessage> getMsgSentAfter(DateTime dateTime)
        {
            IEnumerable<MailMessage> mailMessages = Enumerable.Empty<MailMessage>();
            try
            {
                if (!isConnected)
                {
                    return null;
                }

                IEnumerable<uint> uids = client.Search(

                        SearchCondition.SentSince(dateTime), "[Gmail]/All Mail"   //select if Mail Sent after dateTime specified

                        );
                foreach (uint item in uids)
                {
                    IEnumerable<uint> uids1 = (new[] { item });
                    IEnumerable<MailMessage> mailMessages1 = client.GetMessages(uids1, false, "[Gmail]/All Mail"); // get the messages without setting status as read
                    mailMessages = mailMessages.Concat(mailMessages1.AsEnumerable());
                }
            }
            catch (Exception ex)
            {
            }
            return mailMessages;
        }

        public IEnumerable<uint> getAllMailsUid(DateTime dateTime)
        {
            IEnumerable<uint> uids = null;
            try
            {
                if (!isConnected)
                {
                    return null;
                }

                uids = client.Search(

                        SearchCondition.SentSince(dateTime), "[Gmail]/All Mail"   //select if Mail Sent after dateTime specified

                        );
            }
            catch (Exception ex)
            { }
            return uids;
        }

        public IEnumerable<MailMessage> getMailByUids(IEnumerable<uint> uids)
        {
            IEnumerable<MailMessage> mailMessages = Enumerable.Empty<MailMessage>();
            try
            {
                if (!isConnected)
                {
                    return null;
                }
                mailMessages = client.GetMessages(uids, FetchOptions.Normal, false, "[Gmail]/All Mail"); // get the messages without setting status as read
            }
            catch (Exception ex)
            {
            }
            return mailMessages;
        }

        public int getNoOfMsg()
        {
            if (!isConnected)
            {
                return -1;
            }
            MailboxInfo mailboxInfo = client.GetMailboxInfo();
            Console.WriteLine("No of Msg in mailbox : " + mailboxInfo.Messages);
            return mailboxInfo.Messages;
        }

        public int getNoOfUnreadMsg()
        {
            if (!isConnected)
            {
                return -1;
            }

            MailboxInfo mailboxInfo = client.GetMailboxInfo();
            Console.WriteLine("No of unread Msg in mailbox : " + mailboxInfo.Unread);
            return mailboxInfo.Unread;
        }

        public IEnumerable<MailMessage> getUnreadMsgWithSubjectContaining(string searchStr)
        {
            if (!isConnected)
            {
                return null;
            }
            IEnumerable<uint> uids = client.Search(
                    SearchCondition.Unseen().  //select if mail is undread
                    And(SearchCondition.Subject(searchStr))  //select if subject contains specified words
                    );

            IEnumerable<MailMessage> mailMessages = client.GetMessages(uids, false); // get the messages without setting status as read
                                                                                     //IEnumerable<MailMessage> mailMessages = client.GetMessages(uids, true); // get the message and set status as read

            return mailMessages;
        }


        public IEnumerable<MailMessage> getMsgWithSubjectContaingAndSentAfter(string subjectContaining, DateTime time)
        {
            if (!isConnected)
            {
                return null;
            }
            IEnumerable<uint> uids = client.Search(

                    SearchCondition.Subject(subjectContaining).And(SearchCondition.SentSince(time))  //select if subject contains specified words
                    );

            IEnumerable<MailMessage> mailMessages = client.GetMessages(uids, false); // get the messages without setting status as read
                                                                                     //IEnumerable<MailMessage> mailMessages = client.GetMessages(uids, true); // get the message and set status as read

            return mailMessages;
        }

    }
}