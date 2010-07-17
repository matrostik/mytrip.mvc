using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using Mytrip.Mvc.StartUpSettings;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Mytrip.Gismeteo
{
    public class GismeteoCreateDataBase
    {
        CoreSetting core = new CoreSetting();
        CreateDataBase coreCreateBase = new CreateDataBase();
        public void CreateAndDeleteDataBase(bool create)
        {
            string provider = core.Provider();
            if (provider == "MSSQL")
                DataBaseMSSQL(create);
            else if (provider == "MYSQL")
                DataBaseMYSQL(create);

        }
        private void DataBaseMSSQL(bool create)
        {
            string Server = core.Server();
            string DataBase = core.DataBase();
            string User = core.User();
            string Password = core.Password();
            bool IntegratedSecurity = core.IntegratedSecurity();

            string[] a = { };
            if (create)
                a = scriptMSSQL();
            else
                a = deleteScriptMSSQL();
            StringBuilder result = new StringBuilder();
            foreach (string x in a)
            {
                result.AppendLine(x);
            }
            string queryString = string.Empty;
            if (Server.IndexOf("SQLEXPRESS") != -1)
                queryString = result.ToString();
            else
                queryString = "USE [" + DataBase + "] " + result;
            using (SqlConnection connection = new SqlConnection(coreCreateBase.ConnectionStringMSSQL(Server, DataBase, User, Password, IntegratedSecurity)))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
        private void DataBaseMYSQL(bool create)
        {
            string Server = core.Server();
            string DataBase = core.DataBase();
            string User = core.User();
            string Password = core.Password();
            string[] a = { };
            if (create)
                a = scriptMYSQL();
            else
                a = deleteScriptMYSQL();
            StringBuilder result = new StringBuilder();
            foreach (string x in a)
            {
                result.AppendLine(x);
            }
            string queryString = "SET NAMES 'cp1251'; USE " + DataBase + "; " + result;
            using (MySqlConnection connection = new MySqlConnection(coreCreateBase.ConnectionStringMYSQL(Server, DataBase, User, Password)))
            {
                MySqlScript command = new MySqlScript(connection, queryString);
                command.Connection.Open();
                command.Execute();
                command.Connection.Close();

            }
        }
        private string[] scriptMSSQL()
        {
            string[] result = {
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_gismeteo]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_gismeteo] (",
           "[GismeteoId] [int]  NOT NULL,",
           "[Title] [nvarchar](256)  NOT NULL,",
           "[UrlXml] [nvarchar](256)  NOT NULL,",
           "[Culture] [nvarchar](50)  NOT NULL,",
           "[AllCulture] [bit]  NOT NULL,",
           "[UserName] [nvarchar](100)  NOT NULL,",
           "[CreateDate] [datetime]  NOT NULL,",
           "[VisibleInformer] [bit]  NOT NULL,",
           "CONSTRAINT [PK_mytrip_Gismeteo] PRIMARY KEY CLUSTERED", 
           "(",
	       "[GismeteoId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END"
                              };
            return result;

        }
        private string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_gismeteo]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_gismeteo]"
                              };
            return result;
        }
        private string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_gismeteo(",
            "GismeteoId INT(11) NOT NULL,",
            "Title VARCHAR(256) NOT NULL,",
            "UrlXml VARCHAR(256) NOT NULL,",
            "Culture VARCHAR(50) NOT NULL,",
            "AllCulture BIT(1) NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",            
            "VisibleInformer BIT(1) NOT NULL,",
            "PRIMARY KEY (GismeteoId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 4096",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;"                
                             };
            return result;

        }
        private string[] deleteScriptMYSQL()
        {
            string[] result = {
            "DROP TABLE IF EXISTS mytrip_gismeteo;"
                              };
            return result;
        }

    }
}
    