using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mtm.Core;
using System.Web;
using System.Xml.Linq;
using mtm.Core.Repository;
using System.Web.Caching;
using mtm.Core.Settings;

namespace mtm.Votes
{
    public static class ModuleSetting
    {
       #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        /// <summary>
        /// Название блока в сайдбаре с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_namehomepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string NameVotesPage()
        {
            return GeneralMethods.MytripCache("vs_namevotes", InstallAndUninstall.moduleName, "nameVotes", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>
        /// Статус модуля - включен или отключен  (true = включен)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_integratedsecurity")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool unlockVotes()
        {
            return bool.Parse(GeneralMethods.MytripCache("vs_unlockvotes", InstallAndUninstall.moduleName, "unlockVotes", false, null, 36000, CacheItemPriority.High).ToString());
        }
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************

        /// <summary>
        /// Строка подключения к серверу SQL
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string connectionString()
        {
            string result = string.Empty;
            if (CoreSetting.Provider() == "MSSQL")
            {
                result = CoreSetting.connectionStringMSSQL("Repository.DataEntities.MssqlVotes");
            }
            else if (CoreSetting.Provider() == "MySql")
            {
                result = CoreSetting.connectionStringMYSQL("Repository.DataEntities.MysqlVotes");
            }
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
