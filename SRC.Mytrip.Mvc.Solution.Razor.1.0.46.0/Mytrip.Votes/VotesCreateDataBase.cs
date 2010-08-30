using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mytrip.Mvc.Install;
using Mytrip.Mvc.Interface;

namespace Mytrip.Votes
{
    public class VotesCreateDataBase : ICreateDataBase
    {
        public void CreateAndDeleteDataBase(bool create)
        {
            CreateDataBase.CreateDataBaseMSSQL(scriptMSSQL(), deleteScriptMSSQL(), create);
            CreateDataBase.CreateDataBaseMYSQL(scriptMYSQL(), deleteScriptMYSQL(), create);

        }
        public string[] scriptMSSQL()
        {
            string[] result = {
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesquestion]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_votesquestion](",
	       "[QuestionId] [int] NOT NULL,",
	       "[Question] [nvarchar](256) NOT NULL,",
	       "[TotalVotes] [int] NOT NULL,",
	       "[Active] [bit] NOT NULL,",
	       "[CreateDate] [datetime] NOT NULL,",
	       "[CloseDate] [datetime] NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
	       "[OnlyForRegisterUser] [bit] NOT NULL,",
	       "[Culture] [nvarchar](100) NOT NULL,",
	       "[AllCulture] [bit] NOT NULL,",
	       "[Path] [nvarchar](256) NOT NULL,",
           "CONSTRAINT [PK_mytrip_VotesQuestion] PRIMARY KEY CLUSTERED", 
           "(",
	       "[QuestionId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_votesanswer](",
	       "[AnswerId] [int] NOT NULL,",
	       "[QuestionId] [int] NOT NULL,",
	       "[Answer] [nvarchar](256) NOT NULL,",
	       "[TotalVotes] [int] NOT NULL,",
           "CONSTRAINT [PK_mytrip_VotesAnswer] PRIMARY KEY CLUSTERED", 
           "(",
	       "[AnswerId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))",
           "ALTER TABLE [dbo].[mytrip_votesanswer]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion] FOREIGN KEY([QuestionId])",
           "REFERENCES [dbo].[mytrip_votesquestion] ([QuestionId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))",
           "ALTER TABLE [dbo].[mytrip_votesanswer] CHECK CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]"
           };
            return result;

        }
        public string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))",
            "ALTER TABLE [dbo].[mytrip_votesanswer] DROP CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))",
            "ALTER TABLE [dbo].[mytrip_votesanswer] DROP CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_votesanswer]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesquestion]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_votesquestion]"
                              };
            return result;
        }
        public string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_votesquestion(",
            "QuestionId INT(11) NOT NULL,",
            "Question VARCHAR(256) NOT NULL,",
            "TotalVotes INT(11) NOT NULL,",
            "Active BIT(10) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "CloseDate DATETIME NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "OnlyForRegisterUser BIT(10) NOT NULL,",
            "Culture VARCHAR(100) NOT NULL,",
            "AllCulture BIT(10) NOT NULL,",
            "Path VARCHAR(256) NOT NULL,",
            "PRIMARY KEY (QuestionId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 8192",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_votesanswer(",
            "AnswerId INT(11) NOT NULL,",
            "QuestionId INT(11) NOT NULL,",
            "Answer VARCHAR(256) NOT NULL,",
            "TotalVotes INT(11) NOT NULL,",
            "PRIMARY KEY (AnswerId),",
            "INDEX IX_mytrip_VotesAnswer_mytrip_VotesQuestion (QuestionId),",
            "CONSTRAINT FK_mytrip_VotesAnswer_mytrip_VotesQuestion FOREIGN KEY (QuestionId)",
            "REFERENCES mytrip_votesquestion (QuestionId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2048",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;"                
                             };
            return result;

        }
        public string[] deleteScriptMYSQL()
        {
            string[] result = {
            "DROP TABLE IF EXISTS mytrip_votesanswer;",                 
            "DROP TABLE IF EXISTS mytrip_votesquestion;"
                              };
            return result;
        }
    }
}
