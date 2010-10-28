/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web;
using System.Web.Caching;

namespace Mytrip.Mvc.Settings
{
    /// <summary>Данные из MytripConfiguration.xml, Доменное имя сайта
    /// </summary>
     public static class UsersSetting
    {
        #region Доменное имя сайта
        // **********************************************
        // Доменное имя сайта
        // **********************************************

        /// <summary>Доменное имя сайта
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string applicationName()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        }

        //****************** E N D **********************
        #endregion

        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        /// <summary>Минимальная длина пароля
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_minrequiredpasswordlength")
        /// </summary>
        /// <returns>возвращает int</returns>
        internal static int minRequiredPasswordLength()
        {
            return int.Parse(GeneralMethods.MytripCache("us_minrequiredpasswordlength", "membership", "minRequiredPasswordLength", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Максимальное количество неудачных попыток входа
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_maxinvalidpasswordattempts")
        /// </summary>
        /// <returns>возвращает int</returns>
        internal static int maxInvalidPasswordAttempts()
        {
            return int.Parse(GeneralMethods.MytripCache("us_maxinvalidpasswordattempts", "membership", "maxInvalidPasswordAttempts", false, null, 36000, CacheItemPriority.High).ToString());
        }        
        /// <summary>Проверка на уникальный Email (true = включена)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_requiresuniqueemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool requiresUniqueEmail()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_requiresuniqueemail", "membership", "requiresUniqueEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }
        internal static bool unlockVisibleProfileToSidebar()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockvisibleprofiletosidebar", "profile", "unlockVisibleProfileToSidebar", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Вход и регистрация пользователей через OpenId (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockopenid")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockOpenId()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockopenid", "membership", "unlockOpenId", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Вход и регистрация пользователей через Membership (true = включено)
        /// При выключенном входе через OpenId значение всегда true
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlocksiteid")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockSiteId()
        {
            return (!unlockOpenId())
                ? true
                : bool.Parse(GeneralMethods.MytripCache("us_unlocksiteid", "membership", "unlockSiteId", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Captcha (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockcaptcha")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockCaptcha()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockcaptcha", "membership", "unlockCaptcha", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Имя роли администратора сайта
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_roleadmin")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string roleAdmin()
        {
            return GeneralMethods.MytripCache("us_roleadmin", "membership", "roleAdmin", false, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Gravatar (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockgravatar")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockGravatar()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockgravatar", "profile", "unlockGravatar", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Регистрация пользователей через Membership (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockregistration")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockRegistration()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockregistration", "membership", "unlockRegistration", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Показ кнопки вход на сайте (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockvisiblelogon")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockVisibleLogon()
        {
            return bool.Parse(GeneralMethods.MytripCache("us_unlockvisiblelogon", "membership", "unlockVisibleLogon", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Подтверждение регистрации через Email (true = включено)
        /// При ненастроенном Email значение всегда false
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_unlockapprovedemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockApprovedEmail()
        {
           return bool.Parse(GeneralMethods.MytripCache("us_unlockapprovedemail", "membership", "unlockApprovedEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }        
        /// <summary>Имя роли главного редактора сайта
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "us_rolechiefeditor")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string roleChiefEditor()
        {
            return GeneralMethods.MytripCache("us_rolechiefeditor", "membership", "roleChiefEditor", false, null, 36000, CacheItemPriority.High).ToString();
        }

        //****************** E N D **********************
        #endregion
    }
}
