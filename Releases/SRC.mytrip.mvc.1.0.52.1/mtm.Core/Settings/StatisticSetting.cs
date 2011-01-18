using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace mtm.Core.Settings
{
    public static class StatisticSetting
    {
        internal static bool statisticDefaultCity()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_statisticdefaultcity", "statistic", "defaultCity", false, null, 36000, CacheItemPriority.High).ToString());
        }
        internal static bool statisticAnonym()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_statisticanonym", "statistic", "Anonym", false, null, 36000, CacheItemPriority.High).ToString());
        }
        internal static bool statisticUser()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_statisticuser", "statistic", "User", false, null, 36000, CacheItemPriority.High).ToString());
        }
        internal static bool unlockStatistic()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_unlockstatistic", "statistic", "unlockStatistic", false, null, 36000, CacheItemPriority.High).ToString());
        }
    }
}