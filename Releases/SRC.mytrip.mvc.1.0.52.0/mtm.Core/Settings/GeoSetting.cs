using System.Web.Caching;

namespace mtm.Core.Settings
{
    public static class GeoSetting
    {
        internal static bool DownloadGeo()
        {
            return bool.Parse(GeneralMethods.MytripConfigurationValue("geo", "DownloadGeo"));
        }
        public static bool unlockGeo()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_unlockgeo", "geo", "unlockGeo", false, null, 36000, CacheItemPriority.High).ToString());
        }

        internal static string keyBingMap()
        {
            return GeneralMethods.MytripCache("mtm_cs_keybingmap", "geo", "keyBingMap", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string UrlGeo()
        {
            return GeneralMethods.MytripConfigurationValue("geo", "Url");
        }
    }
}