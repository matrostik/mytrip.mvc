/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mytrip.Mvc.Models;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Install
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

        //****************** E N D **********************
        #endregion

        #region Методы для создания и удаления таблиц в базе данных доступные только внутри сборки
        // **********************************************
        // Методы для создания и удаления таблиц в базе 
        // данных доступные только внутри сборки
        // **********************************************

        /// <summary>Тест подключения к базе данных MSSQL
        /// </summary>
        /// <param name="model">Mytrip.Mvc.Models.CreateBaseModel</param>
        /// <returns>возвращает bool</returns>
        internal static bool TestConnectMSSQL(CreateBaseModel model)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionStringMSSQL(model.Server, model.DataBase, model.User, model.Password, model.IntegratedSecurity));
                connection.Open();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Тест подключения к базе данных MySql
        /// </summary>
        /// <param name="model">Mytrip.Mvc.Models.CreateBaseModel</param>
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
            string result = string.Join("\n", scriptMSSQL());
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

        /// <summary>Исполнение скрипта для базы данных MySql
        /// </summary>
        internal static void CreateDataBaseMYSQL()
        {

            string result = string.Join("\n", scriptMYSQL());
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
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;User Instance=True;",
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
            ? string.Format(@"Data Source={0}; AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;User Instance=True;",
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

        /// <summary>Скрипт создания таблиц ядра в базе данных MSSQL
        /// </summary>
        /// <returns>возвращает string[]</returns>
        private static string[] scriptMSSQL()
        {
            string[] result = {
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_users]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_users](",
	       "[UserId] [nvarchar](50) NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
	       "[LastActivityDate] [datetime] NOT NULL,",
           "CONSTRAINT [PK_mytrip_Users] PRIMARY KEY CLUSTERED", 
           "(",
	       "[UserId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersroles]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_usersroles](",
	       "[RoleId] [int] NOT NULL,",
	       "[RoleName] [nvarchar](100) NOT NULL,",
           "CONSTRAINT [PK_mytrip_Roles] PRIMARY KEY CLUSTERED", 
           "(",
	       "[RoleId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_usersmembership](",
	       "[UserId] [nvarchar](50) NOT NULL,",
	       "[Password] [nvarchar](100) NOT NULL,",
	       "[PasswordSalt] [nvarchar](100) NOT NULL,",
	       "[Email] [nvarchar](100) NOT NULL,",
	       "[IsApproved] [bit] NOT NULL,",
	       "[CreationDate] [datetime] NOT NULL,",
	       "[LastLockoutDate] [datetime] NOT NULL,",
	       "[LastLoginDate] [datetime] NOT NULL,",
	       "[LastPasswordChangedDate] [datetime] NOT NULL,",
	       "[UserIP] [nvarchar](50) NOT NULL,",
           "CONSTRAINT [PK_mytrip_Membership] PRIMARY KEY CLUSTERED", 
           "(",
	       "[UserId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_usersinroles](",
	       "[UserId] [nvarchar](50) NOT NULL,",
	       "[RoleId] [int] NOT NULL,",
           "CONSTRAINT [PK_mytrip_UsersInRoles] PRIMARY KEY CLUSTERED", 
           "(",
	       "[UserId] ASC,",
	       "[RoleId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))",
           "ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles] FOREIGN KEY([RoleId])",
           "REFERENCES [dbo].[mytrip_usersroles] ([RoleId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))",
           "ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))",
           "ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users] FOREIGN KEY([UserId])",
           "REFERENCES [dbo].[mytrip_users] ([UserId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))",
           "ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Membership_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]'))",
           "ALTER TABLE [dbo].[mytrip_usersmembership]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Membership_mytrip_Users] FOREIGN KEY([UserId])",
           "REFERENCES [dbo].[mytrip_users] ([UserId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Membership_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]'))",
           "ALTER TABLE [dbo].[mytrip_usersmembership] CHECK CONSTRAINT [FK_mytrip_Membership_mytrip_Users]"                 
           };
            return result;

        }

        /// <summary>Скрипт создания таблиц ядра в базе данных MySql
        /// </summary>
        /// <returns>возвращает string[]</returns>
        private static string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_users(",
            "UserId VARCHAR(50),",
            "UserName VARCHAR(100) NOT NULL,",
            "LastActivityDate DATETIME NOT NULL,",
            "PRIMARY KEY (UserId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2340",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_usersroles(",
            "RoleId INT(11) NOT NULL,",
            "RoleName VARCHAR(100) NOT NULL,",
            "PRIMARY KEY (RoleId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 4096",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_usersinroles(",
            "UserId VARCHAR(50),",
            "RoleId INT(11) NOT NULL,",
            "PRIMARY KEY (UserId, RoleId),",
            "INDEX IX_mytrip_UsersInRoles_mytrip_Users (RoleId),",
            "CONSTRAINT FK_mytrip_UsersInRoles_mytrip_Users FOREIGN KEY (UserId)",
            "REFERENCES mytrip_users (UserId),",
            "CONSTRAINT FK_mytrip_UsersInRoles_mytrip_Roles FOREIGN KEY (RoleId)",
            "REFERENCES mytrip_usersroles (RoleId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 8192",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_usersmembership(",
            "UserId VARCHAR(50),",
            "`Password` VARCHAR(100) NOT NULL,",
            "PasswordSalt VARCHAR(100) NOT NULL,",
            "Email VARCHAR(100) NOT NULL,",
            "IsApproved BIT(1) NOT NULL,",
            "CreationDate DATETIME NOT NULL,",
            "LastLockoutDate DATETIME NOT NULL,",
            "LastLoginDate DATETIME NOT NULL,",
            "LastPasswordChangedDate DATETIME NOT NULL,",
            "UserIP VARCHAR(50) NOT NULL,",
            "PRIMARY KEY (UserId),",
            "CONSTRAINT FK_mytrip_Membership_mytrip_Users FOREIGN KEY (UserId)",
            "REFERENCES mytrip_users (UserId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2340",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_corepages(",
            "PageId INT (11) NOT NULL,",
            "Title VARCHAR (256) DEFAULT NULL,",
            "Path VARCHAR (256) DEFAULT NULL,",
            "Body TEXT DEFAULT NULL,",
            "ViewOnlyHomePage BIT (1) NOT NULL,",
            "SideBar BIT (1) NOT NULL,",
            "EmailForm BIT (1) NOT NULL,",
            "AddMenu BIT (1) NOT NULL,",
            "AddHomePage BIT (1) NOT NULL,",
            "Culture VARCHAR (100) NOT NULL,",
            "AllCulture BIT (1) NOT NULL,",
            "SubPagesId INT (11) NOT NULL,",
            "PRIMARY KEY (PageId),",
            "INDEX mytrip_corepages_FK1 USING BTREE (SubPagesId),",
            "CONSTRAINT mytrip_corepages_FK1 FOREIGN KEY (SubPagesId)",
            "REFERENCES mytrip_corepages (PageId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 8192",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;"    
            };
            return result;

        }

        //****************** E N D **********************
        #endregion
    }
}
