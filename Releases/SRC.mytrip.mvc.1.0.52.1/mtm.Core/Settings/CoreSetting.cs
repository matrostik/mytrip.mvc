/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.Web.Caching;
using mtm.Core.Install;
using System.Web;

namespace mtm.Core.Settings
{
    /// <summary>Данные из mtm.Config.xml, Данные для подключения к серверу SQL 
    /// </summary>
    public static class CoreSetting
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
        #region Данные из mtm.Config.xml
        // **********************************************
        // Данные из mtm.Config.xml
        // **********************************************

        #region public

        /// <summary>Название титла страницы с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_nametitlepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string NameTitlePage()
        {
            return GeneralMethods.MytripCache("mtm_cs_nametitlepage", "core", "nameTitle", true, null, 36000, CacheItemPriority.High).ToString();
        }
        public static string nameLogo()
        {
            return GeneralMethods.MytripCache("mtm_cs_namelogo", "core", "nameLogo", true, null, 36000, CacheItemPriority.High).ToString();
        }
        /// <summary>Тип сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_provider")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string Provider()
        {
            return GeneralMethods.MytripCache("mtm_cs_provider", "connection", "Provider", false, null, 36000, CacheItemPriority.High).ToString();
        }

        #endregion

        #region internal

        /// <summary>Переключает сайт в режим первого включения и показом лицензионного соглашения (true = включено)
        /// После регистрации администратора сайта автоматически принимает значение false
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_development")
        /// </summary>
        /// <returns>возвращает bool</returns>
        public static bool Development()
        {
            
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_development", "core", "Development", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>Показ панели управления в SideBar (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_ControlPanelSideBar")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static int CountLatestUsers()
        {
            return int.Parse(GeneralMethods.MytripCache("mtm_cs_countlatestusers", "core", "CountLatestUsers", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>Название главной страницы с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_namehomepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string NameHomePage()
        {
            return GeneralMethods.MytripCache("mtm_cs_namehomepage", "core", "nameHome", true,  null, 36000, CacheItemPriority.High).ToString();
        }
        
        /// <summary>Адрес сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_server")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string Server()
        {
            string result= GeneralMethods.MytripCache("mtm_cs_server", "connection", "Server", false,  null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }

        /// <summary>База Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_database")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string DataBase()
        {
            string result = GeneralMethods.MytripCache("mtm_cs_database", "connection", "DataBase", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }

        /// <summary>Имя пользователя Базы Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_user")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string User()
        {
            string result = GeneralMethods.MytripCache("mtm_cs_user", "connection", "User", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }

        /// <summary>Пароль пользователя Базы Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_password")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string Password()
        {
            string result = GeneralMethods.MytripCache("mtm_cs_password", "connection", "Password", false, null, 36000, CacheItemPriority.High).ToString();
            return Crypto.Decrypt(result);
        }

        /// <summary>Интегрированная индентификация сервера SQL (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "mtm_cs_integratedsecurity")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool IntegratedSecurity()
        {
            return bool.Parse(GeneralMethods.MytripCache("mtm_cs_integratedsecurity", "connection", "IntegratedSecurity", false,  null, 36000, CacheItemPriority.High).ToString());
        }        
        
        // <add name="DownloadGeo" value="False"/>
        
        
        #endregion
        //<add name="Anonym" value="True"/>
      //<add name="User" value="True"/>
        //****************** E N D **********************
        #endregion

        #region Данные для подключения к серверу SQL
        // **********************************************
        // Данные для подключения к серверу SQL
        // **********************************************

        #region public

        /// <summary>Строка подключения к серверу MSSQL
        /// </summary>
        /// <param name="entitiesName">имя маппинга Entity Framework (вносить включая вложения без расширения .edmx, пример = "Repository.DataEntities.MssqlModel")</param>
        /// <returns>возвращает string</returns>
        public static string connectionStringMSSQL(string entitiesName)
        {
            string result = (Server().IndexOf("SQLEXPRESS") != -1)
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1}.mdf;Integrated Security=True;User Instance=True;", Server(), DataBase())
            : (!IntegratedSecurity()
            ? string.Format("Data Source={0}; Initial Catalog={1}; Persist Security Info=True; User ID={2}; Password={3};", Server(), DataBase(), User(), Password())
            : string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=True;", Server(), DataBase()));
            return string.Format("metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string=\"{1} MultipleActiveResultSets=True\"", entitiesName, result);
        }
        /// <summary>Строка подключения к серверу MySql
        /// </summary>
        /// <param name="entitiesName">имя маппинга Entity Framework (вносить включая вложения без расширения .edmx, пример = "Repository.DataEntities.MysqlModel")</param>
        /// <returns>возвращает string</returns>
        public static string connectionStringMYSQL(string entitiesName)
        {
            string result = string.Format("server={0}; User Id={1}; password={2}; Persist Security Info=True;database={3};", Server(), User(), Password(), DataBase());
            return string.Format("metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=MySql.Data.MySqlClient;provider connection string=\"{1}\"", entitiesName, result);
        }

        #endregion

        #region internal

        /// <summary>Словарь типов серверов SQL (key = type, value = type)
        /// </summary>
        /// <returns>возвращает IDictionary &lt; string, string &gt;</returns>
        internal static IDictionary<int, string> allProviderDictionary()
        {
            IDictionary<int, string> result = new Dictionary<int, string>();
            result.Add(1, "MSSQL");
            result.Add(2, "MySql");
            return result;
        }
        /// <summary>Строка подключения к серверу SQL
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string connectionString()
        {
            return (Provider() == "MSSQL")
            ? connectionStringMSSQL("Repository.DataEntities.MssqlModel")
            : ((Provider() == "MySql")
            ? connectionStringMYSQL("Repository.DataEntities.MysqlModel")
            : string.Empty);
        }

        #endregion


        //****************** E N D **********************
        #endregion

    }
}
