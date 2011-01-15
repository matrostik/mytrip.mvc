/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Caching;
using mtm.Core.Repository;
using System.Net.Mail;
using System.Collections.Generic;
using mtm.Core.Install;

namespace mtm.Core.Settings
{
    /// <summary>
    /// Данные из mtm.Config.xml
    /// </summary>
      public static class EmailSetting
    {
        #region Методы по отправке писем с сайта
        // **********************************************
        // Методы по отправке писем с сайта
        // **********************************************

        /// <summary>
        /// Отправка письма с сайта
        /// </summary>
        /// <param name="to_email">адрес</param>
        /// <param name="subject">тема</param>
        /// <param name="body">письмо</param>
        public static void SendEmail(string to_email, string subject, string body)
        {
            EmailRepository email = new EmailRepository();
            email.SendEmail(to_email, subject, body);
        }

        /// <summary>
        /// Отправка письма с сайта
        /// </summary>
        /// <param name="to_email">адрес</param>
        /// <param name="subject">тема</param>
        /// <param name="body">письмо</param>
        public static void SendEmail(MailAddress to_email, string subject, string body)
        {
            EmailRepository email = new EmailRepository();
            email.SendEmail(to_email, subject, body);
        }
        /// <summary>
        /// Send email in background using new thread
        /// </summary>
        /// <param name="mail_msg">MailMessage mail_msg</param>
        public static void SendEmail(MailMessage mail_msg)
        {
            EmailRepository email = new EmailRepository();
            email.SendEmail(mail_msg);
        }
        /// <summary>
        /// Send emails in background using new thread
        /// </summary>
        /// <param name="msgs"></param>
        public static void SendEmail(List<MailMessage> msgs)
        {
            EmailRepository email = new EmailRepository();
            email.SendEmail(msgs);
        }

        //****************** E N D **********************
        #endregion

        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        /// <summary>
        /// Подтверждение настройки Email (true = включено)
        /// При удачной настройке автоматически принимает значение true
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_unlocksendemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockSendEmail()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_es_unlocksendemail", "email", "unlockSendEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_fromemail")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string from_email()
        {
            string result=GeneralMethods.MytripCache("mtm_es_fromemail", "email", "EmailAdress", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }
        /// <summary>
        /// Smtp для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_smtp")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string smtp()
        {
            string result = GeneralMethods.MytripCache("mtm_es_smtp", "email", "Smtp", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }
        /// <summary>
        /// Port для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_port")
        /// </summary>
        /// <returns>возвращает int</returns>
        public static int port()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_es_port", "email", "Port", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Ssl для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_ssl")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool ssl()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_es_ssl", "email", "Ssl", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Имя пользователя для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_user")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string login_email()
        {
            string result = GeneralMethods.MytripCache("mtm_es_user", "email", "Login", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }
        /// <summary>
        /// Пароль для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_es_password")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string pass_email()
        {
            string result = GeneralMethods.MytripCache("mtm_es_password", "email", "Password", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }

        //****************** E N D **********************
        #endregion
    }
}