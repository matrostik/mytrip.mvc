/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.Web.Caching;

namespace Mytrip.Mvc.Settings
{
    /// <summary>Данные из MytripConfiguration.xml, Данные для подключения к серверу SQL 
    /// </summary>
    public static class CoreSetting
    {
        #region Данные из MytripConfiguration.xml
        // **********************************************
        // Данные из MytripConfiguration.xml
        // **********************************************

        #region public

        /// <summary>Название титла страницы с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_nametitlepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string NameTitlePage()
        {
            return GeneralMethods.MytripCache("cs_nametitlepage", "core", "nameTitle", true, null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Тип сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_provider")
        /// </summary>
        /// <returns>возвращает string</returns>
        public static string Provider()
        {
            return GeneralMethods.MytripCache("cs_provider", "connection", "Provider", false, null, 36000, CacheItemPriority.High).ToString();
        }

        #endregion

        #region internal

        /// <summary>Переключает сайт в режим первого включения и показом лицензионного соглашения (true = включено)
        /// После регистрации администратора сайта автоматически принимает значение false
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_development")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool Development()
        {
            
            return bool.Parse(GeneralMethods.MytripCache("cs_development", "core", "Development", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>Показ панели управления в SideBar (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_controlpanelsidebare")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool ControlPanelSideBare()
        {
            return bool.Parse(GeneralMethods.MytripCache("cs_controlpanelsidebare", "core", "ControlPanelSideBare", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>Показ Donate в SideBar (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_donatesidesare")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool DonateSideBare()
        {
            return bool.Parse(GeneralMethods.MytripCache("cs_donatesidesare", "core", "DonateSideBare", false,  null, 36000, CacheItemPriority.High).ToString());
        }

        /// <summary>Название главной страницы с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_namehomepage")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string NameHomePage()
        {
            return GeneralMethods.MytripCache("cs_namehomepage", "core", "nameHome", true,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Название страницы About с учетом культуры
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_nameaboutpage")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string NameAboutPage()
        {
            return GeneralMethods.MytripCache("cs_nameaboutpage", "core", "nameAbout", true,  null, 36000, CacheItemPriority.High).ToString();
        }
        
        /// <summary>Адрес сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_server")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string Server()
        {
            return GeneralMethods.MytripCache("cs_server", "connection", "Server", false,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>База Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_database")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string DataBase()
        {
            return GeneralMethods.MytripCache("cs_database", "connection", "DataBase", false,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Имя пользователя Базы Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_user")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string User()
        {
            return GeneralMethods.MytripCache("cs_user", "connection", "User", false,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Пароль пользователя Базы Данных сервера SQL
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_password")
        /// </summary>
        /// <returns>возвращает string</returns>
        internal static string Password()
        {
            return GeneralMethods.MytripCache("cs_password", "connection", "Password", false,  null, 36000, CacheItemPriority.High).ToString();
        }

        /// <summary>Интегрированная индентификация сервера SQL (true = включено)
        /// (данные закешированы, приоритет "High", скользящее 36000 секунд, key = "cs_integratedsecurity")
        /// </summary>
        /// <returns>возвращает bool</returns>
        internal static bool IntegratedSecurity()
        {
            return bool.Parse(GeneralMethods.MytripCache("cs_integratedsecurity", "connection", "IntegratedSecurity", false,  null, 36000, CacheItemPriority.High).ToString());
        }        

        #endregion

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
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;User Instance=True;", Server(), DataBase())
            : (IntegratedSecurity()
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
        /// <returns>возвращает IDictionary[string, string]</returns>
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
