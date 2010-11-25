/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Net.Mail;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Settings;
using System.Collections.Generic;
using System.Threading;

namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// 
    /// </summary>
    internal class EmailRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        internal void SendEmail(string to_email, string subject, string body)
        {
            try
            {
                MailMessage Message = new MailMessage();
                Message.Subject = subject;
                Message.Body = body;
                Message.IsBodyHtml = true;
                Message.From = new System.Net.Mail.MailAddress(EmailSetting.from_email());
                Message.To.Add(new MailAddress(to_email));
                SmtpClient Smtp = new SmtpClient(EmailSetting.smtp(), EmailSetting.port());
                Smtp.Host = EmailSetting.smtp();
                Smtp.EnableSsl = EmailSetting.ssl();
                Smtp.Credentials = new System.Net.NetworkCredential(EmailSetting.login_email(), EmailSetting.pass_email());
                //Smtp.Send(Message);
                EmailThread ethread = new EmailThread(Message, Smtp);
                Thread t = new Thread(new ThreadStart(ethread.SendEmail));
                t.IsBackground = true;
                t.Start();
            }
            catch
            { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="to_email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        internal void SendEmail(MailAddress to_email, string subject, string body)
        {
            try
            {
                MailMessage Message = new MailMessage();
                Message.Subject = subject;
                Message.Body = body;
                Message.IsBodyHtml = true;
                Message.From = new System.Net.Mail.MailAddress(EmailSetting.from_email());
                Message.To.Add(to_email);
                SmtpClient Smtp = new SmtpClient(EmailSetting.smtp(), EmailSetting.port());
                Smtp.Host = EmailSetting.smtp();
                Smtp.EnableSsl = EmailSetting.ssl();
                Smtp.Credentials = new System.Net.NetworkCredential(EmailSetting.login_email(), EmailSetting.pass_email());
                //Smtp.Send(Message);
                EmailThread ethread = new EmailThread(Message, Smtp);
                Thread t = new Thread(new ThreadStart(ethread.SendEmail));
                t.IsBackground = true;
                t.Start();
            }
            catch
            { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail_msg"></param>
        internal void SendEmail(MailMessage mail_msg)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient(EmailSetting.smtp(), EmailSetting.port());
                Smtp.Host = EmailSetting.smtp();
                Smtp.EnableSsl = EmailSetting.ssl();
                Smtp.Credentials = new System.Net.NetworkCredential(EmailSetting.login_email(), EmailSetting.pass_email());

                EmailThread ethread = new EmailThread(mail_msg, Smtp);
                Thread t = new Thread(new ThreadStart(ethread.SendEmail));
                t.IsBackground = true;
                t.Start();
            }
            catch
            { }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgs"></param>
        internal void SendEmail(List<MailMessage> msgs)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient(EmailSetting.smtp(), EmailSetting.port());
                Smtp.Host = EmailSetting.smtp();
                Smtp.EnableSsl = EmailSetting.ssl();
                Smtp.Credentials = new System.Net.NetworkCredential(EmailSetting.login_email(), EmailSetting.pass_email());

                EmailThread ethread = new EmailThread(msgs, Smtp);
                Thread t = new Thread(new ThreadStart(ethread.SendEmails));
                t.IsBackground = true;
                t.Start();
            }
            catch
            { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        internal bool approvedSendEmail(EmailModel m)
        {
            try
            {
                MailMessage Message = new MailMessage();
                Message.Subject = m.FromEmail;
                Message.Body = m.FromEmail;
                Message.From = new System.Net.Mail.MailAddress(m.FromEmail);
                Message.To.Add(new MailAddress(m.FromEmail));
                SmtpClient Smtp = new SmtpClient(m.Smtp, m.Port);
                Smtp.Host = m.Smtp;
                Smtp.EnableSsl = m.Ssl;
                Smtp.Credentials = new System.Net.NetworkCredential(m.LoginEmail, m.PasswordEmail);
                Smtp.Send(Message);
                return true;
            }
            catch { return false; }
        }
    }
    #region EmailThread
    /// <summary>
    /// 
    /// </summary>
    class EmailThread
    {
        List<MailMessage> mail_msgs;
        MailMessage mail_msg;
        SmtpClient smtp_client;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="smtp"></param>
        public EmailThread(MailMessage message, SmtpClient smtp)
        {
            mail_msg = message;
            smtp_client = smtp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="smtp"></param>
        public EmailThread(List<MailMessage> messages, SmtpClient smtp)
        {
            mail_msgs = messages;
            smtp_client = smtp;
        }
        /// <summary>
        /// 
        /// </summary>
        public void SendEmail()
        {
            try
            {
                smtp_client.Send(mail_msg);
            }
            catch
            { }
        }
        /// <summary>
        /// 
        /// </summary>
        public void SendEmails()
        {
            foreach (var m in mail_msgs)
            {
                try
                {
                    smtp_client.Send(m);
                }
                catch
                { }
            }
        }
    }
    #endregion
}
