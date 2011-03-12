/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web;
using System.Web.Caching;

namespace mtm.Core.Settings
{
    /// <summary>Данные из mtm.Config.xml, Доменное имя сайта
    /// </summary>
     public static class UsersSetting
    {
        

        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        /// <summary>Минимальная длина пароля
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_minrequiredpasswordlength")
        /// </summary>
        /// <returns>возвращает int</returns>
        internal static int minRequiredPasswordLength()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_us_minrequiredpasswordlength", "membership", "minRequiredPasswordLength", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Максимальное количество неудачных попыток входа
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_maxinvalidpasswordattempts")
        /// </summary>
        /// <returns>возвращает int</returns>
        internal static int maxInvalidPasswordAttempts()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_us_maxinvalidpasswordattempts", "membership", "maxInvalidPasswordAttempts", false, null, 36000, CacheItemPriority.High).ToString());
        }        
        /// <summary>Проверка на уникальный Email (true = включена)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_requiresuniqueemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool requiresUniqueEmail()
        {
            if (EmailasUsername())
                return true;
            else
                return bool.Parse(GeneralMethods.MytripCache("mtm_us_requiresuniqueemail", "membership", "requiresUniqueEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }
        
        /// <summary>Вход и регистрация пользователей через OpenId (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockopenid")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockOpenId()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockopenid", "membership", "unlockOpenId", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Вход и регистрация пользователей через Membership (true = включено)
        /// При выключенном входе через OpenId значение всегда true
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlocksiteid")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockSiteId()
        {
            return (!unlockOpenId())
                ? true
                : bool.Parse(GeneralMethods.MytripCache("mtm_us_unlocksiteid", "membership", "unlockSiteId", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Captcha (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockcaptcha")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockCaptcha()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockcaptcha", "membership", "unlockCaptcha", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Имя роли администратора сайта
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_roleadmin")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string roleAdmin()
        {
            return GeneralMethods.MytripCache("mtm_us_roleadmin", "membership", "roleAdmin", false, null, 36000, CacheItemPriority.High).ToString();
        }
        
        /// <summary>Регистрация пользователей через Membership (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockregistration")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockRegistration()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockregistration", "membership", "unlockRegistration", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Показ кнопки вход на сайте (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockvisiblelogon")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockVisibleLogon()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockvisiblelogon", "membership", "unlockVisibleLogon", false, null, 36000, CacheItemPriority.High).ToString());
        }
        /// <summary>Подтверждение регистрации через Email (true = включено)
        /// При ненастроенном Email значение всегда false
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockapprovedemail")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool unlockApprovedEmail()
        {
           return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockapprovedemail", "membership", "unlockApprovedEmail", false, null, 36000, CacheItemPriority.High).ToString());
        }        
        /// <summary>Имя роли главного редактора сайта
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_rolechiefeditor")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string roleChiefEditor()
        {
            return GeneralMethods.MytripCache("mtm_us_rolechiefeditor", "membership", "roleChiefEditor", false, null, 36000, CacheItemPriority.High).ToString();
        }
        internal static bool EmailasUsername()
        {
           return bool.Parse(GeneralMethods.MytripCache("mtm_us_emailasusername", "membership", "EmailasUsername", false, null, 36000, CacheItemPriority.High).ToString());
        } 
        internal static bool AutoGenerateUserName()
        {
           return bool.Parse(GeneralMethods.MytripCache("mtm_us_autogenerateusername", "membership", "AutoGenerateUserName", false, null, 36000, CacheItemPriority.High).ToString());
        } 
        //****************** E N D **********************
        #endregion
    }
}
