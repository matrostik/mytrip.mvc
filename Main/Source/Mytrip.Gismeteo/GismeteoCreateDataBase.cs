using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mytrip.Mvc.Install;
using Mytrip.Mvc.Interface;

namespace Mytrip.Gismeteo
{
    public class GismeteoCreateDataBase : ICreateDataBase
    {
        public void CreateAndDeleteDataBase(bool create)
        {
            CreateDataBase.CreateDataBaseMSSQL(scriptMSSQL(), deleteScriptMSSQL(), create);
            CreateDataBase.CreateDataBaseMYSQL(scriptMYSQL(), deleteScriptMYSQL(), create);
        }
        public string[] scriptMSSQL()
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
        public string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_gismeteo]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_gismeteo]"
                              };
            return result;
        }
        public string[] scriptMYSQL()
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
        public string[] deleteScriptMYSQL()
        {
            string[] result = {
            "DROP TABLE IF EXISTS mytrip_gismeteo;"
                              };
            return result;
        }

    }
}
