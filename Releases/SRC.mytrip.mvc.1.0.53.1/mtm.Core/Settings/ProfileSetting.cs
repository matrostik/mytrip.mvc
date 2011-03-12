using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace mtm.Core.Settings
{
    public static class ProfileSetting
    {
        /// <summary>Gravatar (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_us_unlockgravatar")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockGravatar()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_us_unlockgravatar", "profile", "unlockGravatar", false, null, 36000, CacheItemPriority.High).ToString());
        }
        //<add name="SideBarPosition" value="right"/>
        internal static string SideBarPosition()
        {
            return GeneralMethods.MytripCache("mtm_cs_sidebarposition", "core", "SideBarPosition", false, null, 36000, CacheItemPriority.High).ToString();
        }
        //MenuPosition
        internal static string MenuPosition()
        {
            return GeneralMethods.MytripCache("mtm_cs_menuposition", "core", "MenuPosition", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static int twitterWidth()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_twitterwidth", "twitter", "width", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int twitterCount()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_twittercount", "twitter", "count", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string twitterLogin()
        {
            return GeneralMethods.MytripCache("mtm_twitterlogin", "twitter", "login", false, null, 36000, CacheItemPriority.High).ToString();
        }
    }
}