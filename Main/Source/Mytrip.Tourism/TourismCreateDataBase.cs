using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Interface;
using Mytrip.Mvc.Install;

namespace Mytrip.Tourism
{
    public class TourismCreateDataBase : ICreateDataBase
    {
        public void CreateAndDeleteDataBase(bool e)
        {
            CreateDataBase.CreateDataBaseMSSQL(scriptMSSQL(), deleteScriptMSSQL(), e);
            CreateDataBase.CreateDataBaseMYSQL(scriptMYSQL(), deleteScriptMYSQL(), e);
        }
        private string[] scriptMSSQL()
        {
            string[] result = {
            "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]') AND type in (N'U'))",
            "BEGIN",
            "CREATE TABLE [dbo].[mytrip_tourscategory](",
	        "[AllCulture] [bit] NOT NULL,",
	        "[Body] [nvarchar](max) NULL,",
        	"[CategoryId] [int] NOT NULL,",
	        "[CreateDate] [datetime] NOT NULL,",
	        "[Culture] [nvarchar](100) NOT NULL,",
	        "[Path] [nvarchar](256) NOT NULL,",
	        "[SubCategoryId] [int] NOT NULL,",
	        "[Title] [nvarchar](256) NOT NULL,",
	        "[UserName] [nvarchar](100) NOT NULL,",
            "CONSTRAINT [PK_mytrip_tourscategory] PRIMARY KEY CLUSTERED", 
            "(",
	        "[CategoryId] ASC",
            ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
            ") ON [PRIMARY]",
            "END",
            "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tours]') AND type in (N'U'))",
            "BEGIN",
            "CREATE TABLE [dbo].[mytrip_tours](",
	        "[AllCulture] [bit] NOT NULL,",
	        "[Body] [nvarchar](max) NULL,",
	        "[CategoryId] [int] NOT NULL,",
	        "[CloseTourDate] [datetime] NOT NULL,",
	        "[CreateDate] [datetime] NOT NULL,",
	        "[Culture] [nvarchar](100) NOT NULL,",
	        "[Imige] [nvarchar](256) NULL,",
	        "[Latitude] [decimal](10, 4) NULL,",
	        "[Longitude] [decimal](10, 4) NULL,",
	        "[MinPrice] [decimal](18, 2) NOT NULL,",
	        "[MoneyId] [nvarchar](100) NOT NULL,",
	        "[Path] [nvarchar](256) NOT NULL,",
	        "[StartDate] [datetime] NOT NULL,",
	        "[StopDate] [datetime] NOT NULL,",
	        "[Title] [nvarchar](256) NOT NULL,",
	        "[TourId] [int] NOT NULL,",
	        "[UserName] [nvarchar](100) NOT NULL,",
            "CONSTRAINT [PK_mytrip_tours] PRIMARY KEY CLUSTERED",
            "(",
	        "[TourId] ASC",
            ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
            ") ON [PRIMARY]",
            "END",
            "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]') AND type in (N'U'))",
            "BEGIN",
            "CREATE TABLE [dbo].[mytrip_toursvariants](",
	        "[Hotel] [nvarchar](1000) NOT NULL,",
	        "[Latitude] [decimal](10, 4) NULL,",
	        "[Longitude] [decimal](10, 4) NULL,",
	        "[MoneyId] [nvarchar](100) NOT NULL,",
	        "[Price] [decimal](18, 2) NOT NULL,",
	        "[Services] [nvarchar](1000) NULL,",
	        "[TourId] [int] NOT NULL,",
	        "[UserName] [nvarchar](100) NOT NULL,",
	        "[VariantId] [int] NOT NULL,",
            "CONSTRAINT [PK_mytrip_toursvariants] PRIMARY KEY CLUSTERED", 
            "(",
	        "[VariantId] ASC",
            ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
            ") ON [PRIMARY]",
            "END",
            "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))",
            "ALTER TABLE [dbo].[mytrip_tours]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory] FOREIGN KEY([CategoryId])",
            "REFERENCES [dbo].[mytrip_tourscategory] ([CategoryId])",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))",
            "ALTER TABLE [dbo].[mytrip_tours] CHECK CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory]",
            "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))",
            "ALTER TABLE [dbo].[mytrip_tourscategory]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory] FOREIGN KEY([SubCategoryId])",
            "REFERENCES [dbo].[mytrip_tourscategory] ([CategoryId])",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))",
            "ALTER TABLE [dbo].[mytrip_tourscategory] CHECK CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory]",
            "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))",
            "ALTER TABLE [dbo].[mytrip_toursvariants]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours] FOREIGN KEY([TourId])",
            "REFERENCES [dbo].[mytrip_tours] ([TourId])",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))",
            "ALTER TABLE [dbo].[mytrip_toursvariants] CHECK CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours]"
            };
            return result;

        }
        private string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))",
            "ALTER TABLE [dbo].[mytrip_tours] DROP CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))",
            "ALTER TABLE [dbo].[mytrip_tourscategory] DROP CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))",
            "ALTER TABLE [dbo].[mytrip_toursvariants] DROP CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_toursvariants]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tours]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_tours]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_tourscategory]"
           };
            return result;
        }
        private string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_tourscategory(",
            "CategoryId INT (11) NOT NULL,",
            "Title VARCHAR (256) NOT NULL,",
            "Path VARCHAR (256) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "Culture VARCHAR (100) NOT NULL,",
            "AllCulture BIT (1) NOT NULL,",
            "Body TEXT DEFAULT NULL,",
            "UserName VARCHAR (100) NOT NULL,",
            "SubCategoryId INT (11) NOT NULL,",
            "PRIMARY KEY (CategoryId),",
            "INDEX mytrip_tourscategory_FK1 USING BTREE (SubCategoryId),",
            "CONSTRAINT mytrip_tourscategory_FK1 FOREIGN KEY (SubCategoryId)",
            "REFERENCES mytrip_tourscategory (CategoryId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 5461",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "CREATE TABLE IF NOT EXISTS mytrip_tours(",
            "TourId INT (11) NOT NULL,",
            "CategoryId INT (11) NOT NULL,",
            "Title VARCHAR (256) NOT NULL,",
            "Path VARCHAR (256) NOT NULL,",
            "Body TEXT DEFAULT NULL,",
            "Imige VARCHAR (256) DEFAULT NULL,",
            "MinPrice DECIMAL (18, 2) NOT NULL,",
            "MoneyId VARCHAR (256) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "StopDate DATETIME NOT NULL,",
            "Latitude DOUBLE (10, 4) DEFAULT NULL,",
            "Longitude DOUBLE (10, 4) DEFAULT NULL,",
            "CloseTourDate DATETIME NOT NULL,",
            "UserName VARCHAR (100) NOT NULL,",
            "Culture VARCHAR (100) NOT NULL,",
            "AllCulture BIT (1) NOT NULL,",
            "StartDate DATETIME NOT NULL,",
            "PRIMARY KEY (TourId),",
            "INDEX mytrip_tours_FK1 USING BTREE (CategoryId),",
            "CONSTRAINT mytrip_tours_FK1 FOREIGN KEY (CategoryId)",
            "REFERENCES mytrip_tourscategory (CategoryId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2730",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "CREATE TABLE IF NOT EXISTS mytrip_toursvariants(",
            "VariantId INT (11) NOT NULL,",
            "TourId INT (11) NOT NULL,",
            "Price DECIMAL (18, 2) NOT NULL,",
            "MoneyId VARCHAR (100) NOT NULL,",
            "Hotel VARCHAR (1000) NOT NULL,",
            "Services VARCHAR (1000) DEFAULT NULL,",
            "Latitude DOUBLE (10, 4) DEFAULT NULL,",
            "Longitude DOUBLE (10, 4) DEFAULT NULL,",
            "UserName VARCHAR (100) NOT NULL,",
            "PRIMARY KEY (VariantId),",
            "INDEX mytrip_toursvariants_FK1 USING BTREE (TourId),",
            "CONSTRAINT mytrip_toursvariants_FK1 FOREIGN KEY (TourId)",
            "REFERENCES mytrip_tours (TourId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2730",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;"
                              };
            return result;

        }
        private string[] deleteScriptMYSQL()
        {
            string[] result = {
            "DROP TABLE IF EXISTS mytrip_toursvariants;",                 
            "DROP TABLE IF EXISTS mytrip_tours;",
            "DROP TABLE IF EXISTS mytrip_tourscategory;"
                              };
            return result;
        }
    }
}
