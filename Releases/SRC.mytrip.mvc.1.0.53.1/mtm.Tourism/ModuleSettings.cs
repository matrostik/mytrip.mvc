using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core.Settings;
using System.Xml.Linq;
using System.Web;
using System.Web.Caching;

namespace mtm.Tourism
{
    public static class ModuleSetting
    {
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        public static bool unlockTours()
        {
            return bool.Parse(GeneralMethods.MytripCache("st_unlocktours", InstallAndUninstall.moduleName, "unlockTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static bool viewDescription()
        {
            return bool.Parse(GeneralMethods.MytripCache("st_viewdescription", InstallAndUninstall.moduleName, "viewDescription", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int columnTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_columntours", InstallAndUninstall.moduleName, "columnTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int styleTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_styletours", InstallAndUninstall.moduleName, "styleTours", false, null, 36000, CacheItemPriority.High).ToString());
        }



        public static string nameTours()
        {
            return GeneralMethods.MytripCache("st_nametours", InstallAndUninstall.moduleName, "nameTours", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string nameTours(string culture)
        {
            return GeneralMethods.MytripCache("st_nametours", InstallAndUninstall.moduleName, "nameTours", true,culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursKeyWords()
        {
            return GeneralMethods.MytripCache("st_tourskeywords", InstallAndUninstall.moduleName, "nameTours", "keywords", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursTitle()
        {
            return GeneralMethods.MytripCache("st_tourstitle", InstallAndUninstall.moduleName, "nameTours", "title", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursDesc()
        {
            return GeneralMethods.MytripCache("st_toursdescription", InstallAndUninstall.moduleName, "nameTours", "description", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursKeyWords(string culture)
        {
            return GeneralMethods.MytripCache("st_tourskeywords", InstallAndUninstall.moduleName, "nameTours", "keywords", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursTitle(string culture)
        {
            return GeneralMethods.MytripCache("st_tourstitle", InstallAndUninstall.moduleName, "nameTours", "title", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string ToursDesc(string culture)
        {
            return GeneralMethods.MytripCache("st_toursdescription", InstallAndUninstall.moduleName, "nameTours", "description", culture, null, 36000, CacheItemPriority.High).ToString();
        }

        public static string nameCountry()
        {
            return GeneralMethods.MytripCache("st_namecountry", InstallAndUninstall.moduleName, "nameCountry", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string nameCountry(string culture)
        {
            return GeneralMethods.MytripCache("st_namecountry", InstallAndUninstall.moduleName, "nameCountry", true, culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryKeyWords()
        {
            return GeneralMethods.MytripCache("st_countrykeywords", InstallAndUninstall.moduleName, "nameCountry", "keywords", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryTitle()
        {
            return GeneralMethods.MytripCache("st_countrytitle", InstallAndUninstall.moduleName, "nameCountry", "title", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryDesc()
        {
            return GeneralMethods.MytripCache("st_countrydescription", InstallAndUninstall.moduleName, "nameCountry", "description", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryKeyWords(string culture)
        {
            return GeneralMethods.MytripCache("st_countrykeywords", InstallAndUninstall.moduleName, "nameCountry", "keywords", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryTitle(string culture)
        {
            return GeneralMethods.MytripCache("st_countrytitle", InstallAndUninstall.moduleName, "nameCountry", "title", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string CountryDesc(string culture)
        {
            return GeneralMethods.MytripCache("st_countrydescription", InstallAndUninstall.moduleName, "nameCountry", "description", culture, null, 36000, CacheItemPriority.High).ToString();
        }


        public static string nameOrderTours()
        {
            return GeneralMethods.MytripCache("st_nameordertours", InstallAndUninstall.moduleName, "nameOrderTours", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string nameOrderTours(string culture)
        {
            return GeneralMethods.MytripCache("st_nameordertours", InstallAndUninstall.moduleName, "nameOrderTours", true, culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursKeyWords()
        {
            return GeneralMethods.MytripCache("st_ordertourskeywords", InstallAndUninstall.moduleName, "nameOrderTours", "keywords", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursTitle()
        {
            return GeneralMethods.MytripCache("st_ordertourstitle", InstallAndUninstall.moduleName, "nameOrderTours", "title", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursDesc()
        {
            return GeneralMethods.MytripCache("st_ordertoursdescription", InstallAndUninstall.moduleName, "nameOrderTours", "description", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursKeyWords(string culture)
        {
            return GeneralMethods.MytripCache("st_ordertourskeywords", InstallAndUninstall.moduleName, "nameOrderTours", "keywords", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursTitle(string culture)
        {
            return GeneralMethods.MytripCache("st_ordertourstitle", InstallAndUninstall.moduleName, "nameOrderTours", "title", culture, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string OrderToursDesc(string culture)
        {
            return GeneralMethods.MytripCache("st_ordertoursdescription", InstallAndUninstall.moduleName, "nameOrderTours", "description", culture, null, 36000, CacheItemPriority.High).ToString();
        }



        public static int MoneyProcent()
        {
            return int.Parse(GeneralMethods.MytripCache("st_moneyprocent", InstallAndUninstall.moduleName, "MoneyProcent", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int widthImgTours()
        {
            return int.Parse(GeneralMethods.MytripCache("st_widthimgtours", InstallAndUninstall.moduleName, "widthImgTours", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static int closeTour()
        {
            return int.Parse(GeneralMethods.MytripCache("st_closetour", InstallAndUninstall.moduleName, "closeTour", false, null, 36000, CacheItemPriority.High).ToString());
        }
        public static string nameMoney()
        {
            return GeneralMethods.MytripCache("st_namemoney", InstallAndUninstall.moduleName, "Money", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string keyMoney()
        {
            return ToursCache("st_keymoney", InstallAndUninstall.moduleName, "Money", null, 36000, CacheItemPriority.High).ToString();
        }
        public static string roleChiefTourManager()
        {
            return GeneralMethods.MytripCache("st_rolechieftourmanager", InstallAndUninstall.moduleName, "roleChiefTourManager", false, null, 36000, CacheItemPriority.High).ToString();
        }


        public static string partialAccordion()
        {
            return GeneralMethods.MytripCache("st_partialaccordion", InstallAndUninstall.moduleName, "partialAccordion", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialNoAccordion()
        {
            return GeneralMethods.MytripCache("st_partialnoaccordion", InstallAndUninstall.moduleName, "partialNoAccordion", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialMenuLogon()
        {
            return GeneralMethods.MytripCache("st_partialmenulogon", InstallAndUninstall.moduleName, "partialMenuLogon", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string partialMenuLogonWrap()
        {
            return GeneralMethods.MytripCache("st_partialmenulogonwrap", InstallAndUninstall.moduleName, "partialMenuLogonWrap", false, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string roleTourManager()
        {
            return GeneralMethods.MytripCache("st_roletourmanager", InstallAndUninstall.moduleName, "roleTourManager", false, null, 36000, CacheItemPriority.High).ToString();
        }
        public static object ToursCache(string key, string element, string attribute, int? absolutSek, int? spanSek, CacheItemPriority priority)
        {
            string _culture = LocalisationSetting.culture();
            if (HttpContext.Current.Cache[key + _culture] == null)
            {
                TimeSpan _spanSek = spanSek == null ? TimeSpan.Zero : TimeSpan.FromSeconds((int)spanSek);
                DateTime _absolutSek = absolutSek == null ? DateTime.MaxValue : DateTime.Now.AddSeconds((int)absolutSek);
                HttpContext.Current.Cache.Insert(key + _culture, ToursConfigurationValue(element, attribute),
                null, _absolutSek, _spanSek, priority, null);
            }
            return HttpContext.Current.Cache[key + _culture];
        }
        private static string ToursConfigurationValue(string element, string attribute)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            string localization = LocalisationSetting.culture();
            var core = _doc.Root.Elements(element).Elements("add").FirstOrDefault(x => x.Attribute("name").Value == attribute);
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == localization.ToLower());
            return _core.Attribute("key").Value;
        }
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************

        /// <summary>Строка подключения к серверу SQL
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string connectionString()
        {
            string result = string.Empty;
            if (CoreSetting.Provider() == "MSSQL")
            {
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlTours");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlTours");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
