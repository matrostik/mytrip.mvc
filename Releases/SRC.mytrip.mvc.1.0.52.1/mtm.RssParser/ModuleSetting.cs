using System.Linq;
using System.Xml.Linq;
using System.Web.Caching;
using mtm.Core.Settings;

namespace mtm.RssParser
{
    public static class ModuleSetting
    {
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        /// <summary>Название модуля с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "rsp_namerssparser")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string nameRssparser()
        {
            return GeneralMethods.MytripCache("rsp_namerssparser",InstallAndUninstall.moduleName, "nameRssparser", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "rsp_unlockrssparser")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockRssparser()
        {
            return bool.Parse(GeneralMethods.MytripCache("rsp_unlockrssparser", InstallAndUninstall.moduleName, "unlockRssparser", false, null, 36000, CacheItemPriority.High).ToString());
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
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlRssparser");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlRssparser");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
