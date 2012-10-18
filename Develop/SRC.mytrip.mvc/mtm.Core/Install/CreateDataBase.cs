/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using mtm.Core.Models;
using mtm.Core.Settings;
using System.Web;
using mtm.Core.Files;

namespace mtm.Core.Install
{
    /// <summary>Методы для создания и удаления таблиц в базе данных
    /// </summary>
    public static class CreateDataBase
    {
        #region Публичные методы для создания и удаления таблиц в базе данных
        // **********************************************
        // Публичные методы для создания и удаления таблиц в базе данных
        // **********************************************

        /// <summary>Исполнение скрипта для базы данных MSSQL
        /// </summary>
        /// <param name="createScript">скрипт для создания таблиц в базе данных</param>
        /// <param name="deleteScript">скрипт для удаления таблиц из базы данных</param>
        /// <param name="create">true - выполнится createScript</param>
        public static void CreateDataBaseMSSQL(string[] createScript, string[] deleteScript, bool create)
        {
            if (CoreSetting.Provider() == "MSSQL")
            {
                string result = create ? string.Join("\n", createScript) : string.Join("\n", deleteScript);
                string queryString = (CoreSetting.Server().Contains("SQLEXPRESS"))
                    ? result
                    : string.Format("USE [{0}] {1}", CoreSetting.DataBase(), result);
                using (SqlConnection connection = new SqlConnection(ConnectionStringMSSQL()))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
        }
        public static void CreateDataBaseMSSQL(string createScript, string deleteScript, bool create)
        {
            if (CoreSetting.Provider() == "MSSQL")
            {
                string result = create ? createScript : deleteScript;
                string queryString = (CoreSetting.Server().Contains("SQLEXPRESS"))
                    ? result
                    : string.Format("USE [{0}] {1}", CoreSetting.DataBase(), result);
                using (SqlConnection connection = new SqlConnection(ConnectionStringMSSQL()))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
        }
        /// <summary>Исполнение скрипта для базы данных MySql
        /// </summary>
        /// <param name="createScript">скрипт для создания таблиц в базе данных</param>
        /// <param name="deleteScript">скрипт для удаления таблиц из базы данных</param>
        /// <param name="create">true - выполнится createScript</param>
        public static void CreateDataBaseMYSQL(string[] createScript, string[] deleteScript, bool create)
        {
            if (CoreSetting.Provider() == "MySql")
            {
                string result = create ? string.Join("\n", createScript) : string.Join("\n", deleteScript);
                string queryString = string.Format("SET NAMES 'cp1251'; USE {0}; {1}", CoreSetting.DataBase(), result);
                using (MySqlConnection connection = new MySqlConnection(ConnectionStringMYSQL()))
                {
                    MySqlScript command = new MySqlScript(connection, queryString);
                    command.Connection.Open();
                    command.Execute();
                    command.Connection.Close();

                }
            }
        }
        public static void CreateDataBaseMYSQL(string createScript, string deleteScript, bool create)
        {
            if (CoreSetting.Provider() == "MySql")
            {
                string result = create ? createScript : deleteScript;
                string queryString = string.Format("SET NAMES 'cp1251'; USE {0}; {1}", CoreSetting.DataBase(), result);
                using (MySqlConnection connection = new MySqlConnection(ConnectionStringMYSQL()))
                {
                    MySqlScript command = new MySqlScript(connection, queryString);
                    command.Connection.Open();
                    command.Execute();
                    command.Connection.Close();

                }
            }
        }
        //****************** E N D **********************
        #endregion

        #region Методы для создания и удаления таблиц в базе данных доступные только внутри сборки
        // **********************************************
        // Методы для создания и удаления таблиц в базе 
        // данных доступные только внутри сборки
        // **********************************************

        /// <summary>Тест подключения к базе данных MSSQL
        /// </summary>
        /// <param name="model">mtm.Core.Models.CreateBaseModel</param>
        /// <returns>возвращает bool</returns>
        internal static bool TestConnectMSSQL(CreateBaseModel model)
        {
            bool res = false;
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionStringMSSQL(model.Server, model.DataBase, model.User, model.Password, model.IntegratedSecurity));
                connection.Open();
                connection.Close();
                res = true;
            }
            catch
            {
                if (!res && model.Server.Contains("SQLEXPRESS"))
                {
                    string directory = GeneralMethods.GetPath("App_Data");
                    string filename = directory + "\\" + model.DataBase + ".mdf";
                    string databaseName = System.IO.Path.GetFileNameWithoutExtension(filename);
                    using (var connection = new SqlConnection(
                        @"Data Source=.\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=true;User Instance=True;"))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText =
                                "CREATE DATABASE " + databaseName +
                                " ON PRIMARY (NAME=" + databaseName +
                                ", FILENAME='" + filename + "')";
                            command.ExecuteNonQuery();

                            command.CommandText =
                                "EXEC sp_detach_db '" + databaseName + "', 'true'";
                            command.ExecuteNonQuery();
                        }
                    }                    
                    res = true;
                }
                
            }
            return res;
        }

        /// <summary>Тест подключения к базе данных MySql
        /// </summary>
        /// <param name="model">mtm.Core.Models.CreateBaseModel</param>
        /// <returns>возвращает bool</returns>
        internal static bool TestConnectMYSQL(CreateBaseModel model)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionStringMYSQL(model.Server, model.DataBase, model.User, model.Password));
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Исполнение скрипта для базы данных MSSQL
        /// </summary>
        internal static void CreateDataBaseMSSQL()
        {
            string result = ScriptSql.ScriptMSSQL;
            string queryString = (CoreSetting.Server().Contains("SQLEXPRESS"))
                ? result
                : string.Format("USE [{0}] {1}", CoreSetting.DataBase(), result);
            
            using (SqlConnection connection = new SqlConnection(ConnectionStringMSSQL()))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>Исполнение скрипта для базы данных MySql
        /// </summary>
        internal static void CreateDataBaseMYSQL()
        {

            string result = ScriptSql.ScriptMySql;
            string queryString = string.Format("SET NAMES 'cp1251'; USE {0}; {1}", CoreSetting.DataBase(), result);
            using (MySqlConnection connection = new MySqlConnection(ConnectionStringMYSQL()))
            {
                MySqlScript command = new MySqlScript(connection, queryString);
                command.Connection.Open();
                command.Execute();
                command.Connection.Close();

            }
        }

        //****************** E N D **********************
        #endregion

        #region Приватные методы для создания и удаления таблиц в базе данных
        // **********************************************
        // Приватные методы для создания и удаления таблиц в базе данных
        // **********************************************

        /// <summary>Строка подключения к базе данных MSSQL
        /// </summary>
        /// <returns>возвращает string</returns>
        private static string ConnectionStringMSSQL()
        {
            return CoreSetting.Server().Contains("SQLEXPRESS")
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1}.mdf;Integrated Security=True;User Instance=True;",
            CoreSetting.Server(), CoreSetting.DataBase())
            : (CoreSetting.IntegratedSecurity()
            ? string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=True;",
            CoreSetting.Server(), CoreSetting.DataBase())
            : string.Format("Data Source={0}; Initial Catalog={1}; Persist Security Info=True; User ID={2}; Password={3};",
            CoreSetting.Server(), CoreSetting.DataBase(), CoreSetting.User(), CoreSetting.Password()));
        }

        /// <summary>Строка подключения к базе данных MySql
        /// </summary>
        /// <returns>возвращает string</returns>
        private static string ConnectionStringMYSQL()
        {
            return string.Format("server={0}; User Id={1}; password={2}; Persist Security Info=True;database={3};", 
                CoreSetting.Server(), CoreSetting.User(), CoreSetting.Password(), CoreSetting.DataBase());
        }

        /// <summary>Строка подключения к базе данных MSSQL
        /// </summary>
        /// <param name="Server">Адрес SQL сервера</param>
        /// <param name="DataBase">Имя базы данных</param>
        /// <param name="User">Имя пользователя SQL сервера</param>
        /// <param name="Password">Пароль пользователя SQL сервера</param>
        /// <param name="IntegratedSecurity">Интегрированная система аутентификации (true - включена)</param>
        /// <returns>возвращает string</returns>
        private static string ConnectionStringMSSQL(string Server, string DataBase, string User, string Password, bool IntegratedSecurity)
        {
            return Server.Contains("SQLEXPRESS")
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1}.mdf;Integrated Security=True;User Instance=True;",
            Server, DataBase)
            : (IntegratedSecurity
            ? string.Format("Data Source={0}; Initial Catalog={1}; Integrated Security=True;",
            Server, DataBase)
            : string.Format("Data Source={0}; Initial Catalog={1}; Persist Security Info=True; User ID={2}; Password={3};",
            Server, DataBase, User, Password));
        }

        /// <summary>Строка подключения к базе данных MySql
        /// </summary>
        /// <param name="Server">Адрес SQL сервера</param>
        /// <param name="DataBase">Имя базы данных</param>
        /// <param name="User">Имя пользователя SQL сервера</param>
        /// <param name="Password">Пароль пользователя SQL сервера</param>
        /// <returns>возвращает string</returns>
        private static string ConnectionStringMYSQL(string Server, string DataBase, string User, string Password)
        {
            return string.Format("server={0}; User Id={1}; password={2}; Persist Security Info=True;database={3};",
                Server, User, Password, DataBase);

        }

        private static string[] CreateDataBaseSQLEXPRESS(string name)
        {
            string directory = GeneralMethods.GetPath("App_Data");
            string[] result = {

            "USE [tempdb]",
            "CREATE DATABASE [" + name + "] ON  PRIMARY", 
            "( NAME = N'"+name+"', FILENAME = N'"+directory+"\\"+name+".mdf' , SIZE = 3072KB , FILEGROWTH = 1024KB )",
            "LOG ON", 
            "( NAME = N'"+name+"_log', FILENAME = N'"+directory+"\\"+name+"_log.ldf' , SIZE = 1024KB , FILEGROWTH = 10%)"
           ,
            "EXEC sp_detach_db '" + name + "', 'true'"
                              };
            return result;
        }
        //****************** E N D **********************
        #endregion
    }
}
