using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Mytrip.Mvc;
using Mytrip.Mvc.StartUpSettings;

namespace Mytrip.Articles
{
    public class ArticlesCreateDataBase
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
            string Server=core.Server();
            string DataBase=core.DataBase();
            string User=core.User();
            string Password=core.Password();
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
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlestag]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articlestag](",
	       "[TagId] [int] NOT NULL,",
	       "[TagName] [nvarchar](256) NOT NULL,",
	       "[Path] [nvarchar](256) NOT NULL,",
           "CONSTRAINT [PK_mytrip_ArticlesTag] PRIMARY KEY CLUSTERED", 
           "(",
	       "[TagId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articlescategory](",
	       "[CategoryId] [int] NOT NULL,",
	       "[Title] [nvarchar](256) NOT NULL,",
	       "[Path] [nvarchar](256) NOT NULL,",
	       "[CreateDate] [datetime] NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
	       "[UserEmail] [nvarchar](100) NOT NULL,",
	       "[SeparateBlock] [bit] NOT NULL,",
	       "[Blog] [bit] NOT NULL,",
	       "[Views] [int] NOT NULL,",
	       "[SubCategoryId] [int] NOT NULL,",
	       "[Culture] [nvarchar](100) NOT NULL,",
	       "[AllCulture] [bit] NOT NULL,",
           "CONSTRAINT [PK_mytrip_ArticleCategory] PRIMARY KEY CLUSTERED", 
           "(",
	       "[CategoryId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articles]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articles](",
	       "[ArticleId] [int] NOT NULL,",
	       "[CategoryId] [int] NOT NULL,",
	       "[Title] [nvarchar](256) NOT NULL,",
	       "[Abstract] [nvarchar](max) NULL,",
	       "[Body] [nvarchar](max) NOT NULL,",
	       "[CreateDate] [datetime] NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
	       "[Views] [int] NOT NULL,",
	       "[ApprovedComment] [bit] NOT NULL,",
	       "[IncludeAnonymComment] [bit] NOT NULL,",
	       "[ImageForAbstract] [nvarchar](256) NULL,",
	       "[Path] [nvarchar](256) NOT NULL,",
	       "[OnlyForRegisterUser] [bit] NOT NULL,",
	       "[ApprovedVotes] [bit] NOT NULL,",
	       "[CloseDate] [datetime] NOT NULL,",
	       "[Culture] [nvarchar](100) NOT NULL,",
	       "[AllCulture] [bit] NOT NULL,",
	       "[TotalVotes] [decimal](4, 2) NOT NULL,",
           "CONSTRAINT [PK_mytrip_Article] PRIMARY KEY CLUSTERED", 
           "(",
	       "[ArticleId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articlesvotes](",
	       "[VotesId] [int] NOT NULL,",
	       "[ArticleId] [int] NOT NULL,",
	       "[Vote] [int] NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
           "CONSTRAINT [PK_mytrip_ArticlesVotes] PRIMARY KEY CLUSTERED", 
           "(",
	       "[VotesId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articlesintags](",
	       "[ArticleId] [int] NOT NULL,",
	       "[TagId] [int] NOT NULL,",
           "CONSTRAINT [PK_mytrip_ArticlesInTags] PRIMARY KEY CLUSTERED", 
           "(",
	       "[ArticleId] ASC,",
	       "[TagId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]') AND type in (N'U'))",
           "BEGIN",
           "CREATE TABLE [dbo].[mytrip_articlescomments](",
	       "[CommentId] [int] NOT NULL,",
	       "[ArticleId] [int] NOT NULL,",
	       "[Body] [nvarchar](max) NOT NULL,",
	       "[CreateDate] [datetime] NOT NULL,",
	       "[UserName] [nvarchar](100) NOT NULL,",
	       "[UserEmail] [nvarchar](100) NOT NULL,",
	       "[IsAnonym] [bit] NOT NULL,",
           "CONSTRAINT [PK_mytrip_ArticleComment] PRIMARY KEY CLUSTERED", 
           "(",
	       "[CommentId] ASC",
           ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]",
           ") ON [PRIMARY]",
           "END",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))",
           "ALTER TABLE [dbo].[mytrip_articles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory] FOREIGN KEY([CategoryId])",
           "REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))",
           "ALTER TABLE [dbo].[mytrip_articles] CHECK CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))",
           "ALTER TABLE [dbo].[mytrip_articlescategory]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory] FOREIGN KEY([SubCategoryId])",
           "REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))",
           "ALTER TABLE [dbo].[mytrip_articlescategory] CHECK CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))",
           "ALTER TABLE [dbo].[mytrip_articlescomments]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article] FOREIGN KEY([ArticleId])",
           "REFERENCES [dbo].[mytrip_articles] ([ArticleId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))",
           "ALTER TABLE [dbo].[mytrip_articlescomments] CHECK CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
           "ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article] FOREIGN KEY([ArticleId])",
           "REFERENCES [dbo].[mytrip_articles] ([ArticleId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
           "ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
           "ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag] FOREIGN KEY([TagId])",
           "REFERENCES [dbo].[mytrip_articlestag] ([TagId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
           "ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]",
           "",
           "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))",
           "ALTER TABLE [dbo].[mytrip_articlesvotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles] FOREIGN KEY([ArticleId])",
           "REFERENCES [dbo].[mytrip_articles] ([ArticleId])",
           "",
           "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))",
           "ALTER TABLE [dbo].[mytrip_articlesvotes] CHECK CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]"
           };
            return result;

        }
        private string[] deleteScriptMSSQL()
        {
            string[] result = {
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))",
            "ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))",
            "ALTER TABLE [dbo].[mytrip_articlescategory] DROP CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))",
            "ALTER TABLE [dbo].[mytrip_articlescomments] DROP CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
            "ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
            "ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))",
            "ALTER TABLE [dbo].[mytrip_articlesvotes] DROP CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))",
            "ALTER TABLE [dbo].[mytrip_articlescomments] DROP CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articlescomments]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
            "ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))",
            "ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articlesintags]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))",
            "ALTER TABLE [dbo].[mytrip_articlesvotes] DROP CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articlesvotes]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))",
            "ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articles]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articles]",
            "IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))",
            "ALTER TABLE [dbo].[mytrip_articlescategory] DROP CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articlescategory]",
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlestag]') AND type in (N'U'))",
            "DROP TABLE [dbo].[mytrip_articlestag]"
                              };
            return result;
        }
        private string[] scriptMYSQL()
        {
            string[] result = {
            "CREATE TABLE IF NOT EXISTS mytrip_articlescategory(",
            "CategoryId INT(11) NOT NULL,",
            "Title VARCHAR(256) NOT NULL,",
            "Path VARCHAR(256) NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "UserEmail VARCHAR(100) NOT NULL,",
            "SeparateBlock BIT(1) NOT NULL,",
            "Blog BIT(1) NOT NULL,",
            "Views INT(11) NOT NULL,",
            "SubCategoryId INT(11) NOT NULL,",
            "Culture VARCHAR(100) NOT NULL,",
            "AllCulture BIT(1) NOT NULL,",
            "PRIMARY KEY (CategoryId),",
            "INDEX IX_mytrip_ArticleCategory_mytrip_ArticleCategory (SubCategoryId),",
            "CONSTRAINT FK_mytrip_ArticleCategory_mytrip_ArticleCategory FOREIGN KEY (SubCategoryId)",
            "REFERENCES mytrip_articlescategory (CategoryId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 2048",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_articlestag(",
            "TagId INT(11) NOT NULL,",
            "TagName VARCHAR(256) NOT NULL,",
            "Path VARCHAR(256) NOT NULL,",
            "PRIMARY KEY (TagId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 8192",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_articles(",
            "ArticleId INT(11) NOT NULL,",
            "CategoryId INT(11) NOT NULL,",
            "Title VARCHAR(256) NOT NULL,",
            "Abstract TEXT DEFAULT NULL,",
            "Body TEXT NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "Views INT(11) NOT NULL,",
            "ApprovedComment BIT(1) NOT NULL,",
            "IncludeAnonymComment BIT(1) NOT NULL,",
            "ImageForAbstract VARCHAR(256) DEFAULT NULL,",
            "Path VARCHAR(256) NOT NULL,",
            "OnlyForRegisterUser BIT(1) NOT NULL,",
            "ApprovedVotes BIT(1) NOT NULL,",
            "CloseDate DATETIME NOT NULL,",
            "Culture VARCHAR(100) NOT NULL,",
            "AllCulture BIT(1) NOT NULL,",
            "TotalVotes DECIMAL (4, 2) NOT NULL,",
            "PRIMARY KEY (ArticleId),",
            "INDEX IX_mytrip_Article_mytrip_ArticleCategory (CategoryId),",
            "CONSTRAINT FK_mytrip_Article_mytrip_ArticleCategory FOREIGN KEY (CategoryId)",
            "REFERENCES mytrip_articlescategory (CategoryId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 1365",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_articlescomments(",
            "CommentId INT(11) NOT NULL,",
            "ArticleId INT(11) NOT NULL,",
            "Body TEXT NOT NULL,",
            "CreateDate DATETIME NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "UserEmail VARCHAR(100) NOT NULL,",
            "IsAnonym BIT(1) NOT NULL,",
            "PRIMARY KEY (CommentId),",
            "INDEX IX_mytrip_ArticleComment_mytrip_Article (ArticleId),",
            "CONSTRAINT FK_mytrip_ArticleComment_mytrip_Article FOREIGN KEY (ArticleId)",
            "REFERENCES mytrip_articles (ArticleId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 5461",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_articlesintags(",
            "ArticleId INT(11) NOT NULL,",
            "TagId INT(11) NOT NULL,",
            "PRIMARY KEY (ArticleId, TagId),",
            "INDEX IX_mytrip_ArticlesInTags_mytrip_ArticlesTag (TagId),",
            "CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_Article FOREIGN KEY (ArticleId)",
            "REFERENCES mytrip_articles (ArticleId),",
            "CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_ArticlesTag FOREIGN KEY (TagId)",
            "REFERENCES mytrip_articlestag (TagId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 16384",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;",
            "",
            "CREATE TABLE IF NOT EXISTS mytrip_articlesvotes(",
            "VotesId INT(11) NOT NULL,",
            "ArticleId INT(11) NOT NULL,",
            "Vote INT(11) NOT NULL,",
            "UserName VARCHAR(100) NOT NULL,",
            "PRIMARY KEY (VotesId),",
            "INDEX IX_mytrip_ArticlesVotes_mytrip_Articles (ArticleId),",
            "CONSTRAINT FK_mytrip_ArticlesVotes_mytrip_Articles FOREIGN KEY (ArticleId)",
            "REFERENCES mytrip_articles (ArticleId)",
            ")",
            "ENGINE = INNODB",
            "AVG_ROW_LENGTH = 8192",
            "CHARACTER SET cp1251",
            "COLLATE cp1251_general_ci;"                
                             };
            return result;

        }
        private string[] deleteScriptMYSQL()
        {
            string[] result = {
            "DROP TABLE IF EXISTS mytrip_articlesvotes;",                 
            "DROP TABLE IF EXISTS mytrip_articlesintags;",
            "DROP TABLE IF EXISTS mytrip_articlescomments;", 
            "DROP TABLE IF EXISTS mytrip_articles;",
            "DROP TABLE IF EXISTS mytrip_articlestag;",
            "DROP TABLE IF EXISTS mytrip_articlescategory;"
                              };
            return result;
        }
    }
}
