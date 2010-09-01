using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mytrip.Mvc;
using Mytrip.Mvc.Install;
using Mytrip.Mvc.Interface;

namespace Mytrip.Rssparser
{
    public class RssparserCreateDataBase : ICreateDataBase
    {
        public void CreateAndDeleteDataBase(bool create)
        {
                CreateDataBase.CreateDataBaseMSSQL(scriptMSSQL(),deleteScriptMSSQL(),create);
                CreateDataBase.CreateDataBaseMYSQL(scriptMYSQL(),deleteScriptMYSQL(),create);

        }
        public string[] scriptMSSQL()
        {
            string[] result = {
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_rssparser]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_rssparser] (",
           "[RssparserId] [int]  NOT NULL,",
           "[Title] [nvarchar](256)  NOT NULL,",
           "[Path] [nvarchar](256)  NOT NULL,",
           "[CreateDate] [datetime]  NOT NULL,",
           "[UserName] [nvarchar](100)  NOT NULL,",
           "[Views] [int]  NOT NULL,",
           "[Culture] [nvarchar](100)  NOT NULL,",
           "[AllCulture] [bit]  NOT NULL,",
           "[RssUrl] [nvarchar](256)  NOT NULL,",
           "[ImageUrl] [nvarchar](256)  NOT NULL,",
           "CONSTRAINT [PK_mytrip_RssParser] PRIMARY KEY CLUSTERED", 
           "(",
	       "[RssparserId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END"
                              };
            return result;

        }
        public string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_rssparser]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_rssparser]"
                              };
            return result;
        }
        public string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_rssparser(",
            "RssparserId INT(11) NOT NULL,",
            "Title VARCHAR(256) NOT NULL,",
            "Path VARCHAR(256) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "Views INT(11) NOT NULL,",
            "Culture VARCHAR(100) NOT NULL,",
            "AllCulture BIT(1) NOT NULL,",
            "RssUrl VARCHAR(256) NOT NULL,",
            "ImageUrl VARCHAR(256) DEFAULT NULL,",
            "PRIMARY KEY (RssparserId)",
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
            "DROP TABLE IF EXISTS mytrip_rssparser;"
                              };
            return result;
        }

    }
}
