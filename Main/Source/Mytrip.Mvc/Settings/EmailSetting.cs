/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Caching;
using Mytrip.Mvc.Repository;
using System.Net.Mail;
using System.Collections.Generic;

namespace Mytrip.Mvc.Settings
{
    /// <summary>
    /// Данные из MytripConfiguration.xml
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

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        /// <summary>
        /// Подтверждение настройки Email (true = включено)
        /// При удачной настройке автоматически принимает значение true
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_unlocksendemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockSendEmail()
        {
            return bool.Parse(GeneralMethods.MytripCache("es_unlocksendemail", "email", "unlockSendEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_from_email")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string from_email()
        {
            return GeneralMethods.MytripCache("es_from_email", "email", "EmailAdress", false, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// Smtp для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_smtp")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string smtp()
        {
            return GeneralMethods.MytripCache("es_smtp", "email", "Smtp", false, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// Port для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_port")
        /// </summary>
        /// <returns>возвращает int</returns>
        public static int port()
        {
            return int.Parse(GeneralMethods.MytripCache("es_port", "email", "Port", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Ssl для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_ssl")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool ssl()
        {
            return bool.Parse(GeneralMethods.MytripCache("es_ssl", "email", "Ssl", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>
        /// Имя пользователя для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_login_email")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string login_email()
        {
            return GeneralMethods.MytripCache("es_login_email", "email", "Login", false, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// Пароль для Email с которого осуществляется отправка писем
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "es_pass_email")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string pass_email()
        {
            return GeneralMethods.MytripCache("es_pass_email", "email", "Password", false, null, 36000, CacheItemPriority.High).ToString();
        }

        //****************** E N D **********************
        #endregion
    }
}