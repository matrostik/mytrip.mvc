using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Mytrip.Mvc.Repository;
using System.Web;
using MySql.Data.MySqlClient;
using Mytrip.Mvc.Models;

namespace Mytrip.Mvc.StartUpSettings
{
   public class CreateDataBase
   {
       public bool TestConnectMSSQL(CreateBaseModel model)
       {
           try
           {
               SqlConnection connection = new SqlConnection(ConnectionStringMSSQL(model.Server, model.DataBase,model.User,model.Password, model.IntegratedSecurity));
               connection.Open();
               connection.Close();
               return true;
           }
           catch
           {
               return false;
           }       
       }
       public bool TestConnectMYSQL(CreateBaseModel model)
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
       public void CreateDataBaseMSSQL(string Server, string DataBase, string User, string Password, bool IntegratedSecurity)
       {
           
           string[] a = scriptMSSQL();
           StringBuilder result = new StringBuilder();
           foreach (string x in a)
           {
               result.AppendLine(x);
           }
           string queryString = string.Empty;
           if (Server.IndexOf("SQLEXPRESS") != -1)
               queryString = result.ToString();
           else
           queryString ="USE [" + DataBase + "] " + result;
           using (SqlConnection connection = new SqlConnection(ConnectionStringMSSQL(Server, DataBase,User,Password, IntegratedSecurity)))
           {
               SqlCommand command = new SqlCommand(queryString, connection);              
                   command.Connection.Open();
                   command.ExecuteNonQuery();
                   command.Connection.Close();
           }
       }
       public void CreateDataBaseMYSQL(string Server, string DataBase, string User, string Password)
       {
           
           string[] a = scriptMYSQL();
           StringBuilder result = new StringBuilder();
           foreach (string x in a)
           {
               result.AppendLine(x);
           }
           string queryString = "SET NAMES 'cp1251'; USE " + DataBase + "; " + result;
           using (MySqlConnection connection = new MySqlConnection(ConnectionStringMYSQL(Server, DataBase, User, Password)))
           {
               MySqlScript command = new MySqlScript(connection,queryString);
               command.Connection.Open();
               command.Execute();
               command.Connection.Close();

           }
       }
       public string ConnectionStringMSSQL(string Server, string DataBase,string User,string Password, bool IntegratedSecurity)
       {
           string result = string.Empty;
           if (Server.IndexOf("SQLEXPRESS") != -1)
           {
               result = "Data Source=" + Server + @"; AttachDbFilename=|DataDirectory|\"
                   + DataBase + ";Integrated Security=True;User Instance=True;";
           }
           else
           {
               if (IntegratedSecurity)
               { result = "Data Source=" + Server + "; Initial Catalog=" + DataBase + "; Integrated Security=True;"; }
               else
               {

                   result = "Data Source=" + Server + "; Initial Catalog=" + DataBase
                       + "; Persist Security Info=True; User ID=" + User + "; Password="
                       + Password + ";";
               }

           } return result;
       
       }
       public string ConnectionStringMYSQL(string Server, string DataBase, string User, string Password)
       {
           string result = "server=" + Server + "; User Id=" + User
                        + "; password=" + Password + "; Persist Security Info=True;database="
                        + DataBase + ";";
           return result;

       }
       private string[] scriptMSSQL()
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
       private string[] scriptMYSQL()
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
            "COLLATE cp1251_general_ci;"                
                             };
           return result;

       }
    }
}
