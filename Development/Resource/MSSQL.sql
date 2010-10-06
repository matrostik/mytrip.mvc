USE [mytripmvc]
GO
/****** Object:  Table [dbo].[mytrip_users]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_users](
	[UserId] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
 CONSTRAINT [PK_mytrip_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_usersroles]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_usersroles](
	[RoleId] [int] NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_mytrip_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_votesquestion]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_votesquestion](
	[QuestionId] [int] NOT NULL,
	[Question] [nvarchar](256) NOT NULL,
	[TotalVotes] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CloseDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[OnlyForRegisterUser] [bit] NOT NULL,
	[Culture] [nvarchar](100) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_mytrip_VotesQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_storedepartment]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_storedepartment](
	[DepartmentId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[Culture] [nvarchar](50) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[SubDepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_mytrip_storedepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_rssparser]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_rssparser](
	[RssparserId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Views] [int] NOT NULL,
	[Culture] [nvarchar](100) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[RssUrl] [nvarchar](256) NOT NULL,
	[ImageUrl] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_mytrip_RssParser] PRIMARY KEY CLUSTERED 
(
	[RssparserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_gismeteo]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_gismeteo](
	[GismeteoId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[UrlXml] [nvarchar](256) NOT NULL,
	[Culture] [nvarchar](50) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[VisibleInformer] [bit] NOT NULL,
 CONSTRAINT [PK_mytrip_Gismeteo] PRIMARY KEY CLUSTERED 
(
	[GismeteoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_storeproducer]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_storeproducer](
	[ProducerId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Path] [nvarchar](256) NOT NULL,
	[Culture] [nvarchar](50) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_mytrip_storeproducer] PRIMARY KEY CLUSTERED 
(
	[ProducerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlestag]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlestag](
	[TagId] [int] NOT NULL,
	[TagName] [nvarchar](256) NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_mytrip_ArticlesTag] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlescategory]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlescategory](
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserEmail] [nvarchar](100) NOT NULL,
	[SeparateBlock] [bit] NOT NULL,
	[Blog] [bit] NOT NULL,
	[Views] [int] NOT NULL,
	[SubCategoryId] [int] NOT NULL,
	[Culture] [nvarchar](100) NOT NULL,
	[AllCulture] [bit] NOT NULL,
 CONSTRAINT [PK_mytrip_ArticleCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articles]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articles](
	[ArticleId] [int] NOT NULL,
	[SubArticleId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Abstract] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Views] [int] NOT NULL,
	[ApprovedComment] [bit] NOT NULL,
	[IncludeAnonymComment] [bit] NOT NULL,
	[ImageForAbstract] [nvarchar](256) NULL,
	[Path] [nvarchar](256) NOT NULL,
	[OnlyForRegisterUser] [bit] NOT NULL,
	[ApprovedVotes] [bit] NOT NULL,
	[CloseDate] [datetime] NOT NULL,
	[Culture] [nvarchar](100) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[TotalVotes] [decimal](4, 2) NOT NULL,
	[ModerateComments] [bit] NOT NULL,
 CONSTRAINT [PK_mytrip_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_storeproduct]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_storeproduct](
	[ProductId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[ProducerId] [int] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[Details] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Culture] [nvarchar](50) NOT NULL,
	[AllCulture] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[TotalVotes] [decimal](4, 2) NOT NULL,
	[TotalCount] [int] NOT NULL,
	[ViewPrice] [bit] NOT NULL,
	[ViewVotes] [bit] NOT NULL,
	[ViewCount] [bit] NOT NULL,
	[UrlFile] [nvarchar](256) NULL,
	[Packing] [nvarchar](256) NULL,
 CONSTRAINT [PK_mytrip_storeproduct] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_votesanswer]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_votesanswer](
	[AnswerId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Answer] [nvarchar](256) NOT NULL,
	[TotalVotes] [int] NOT NULL,
 CONSTRAINT [PK_mytrip_VotesAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_usersmembership]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_usersmembership](
	[UserId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[PasswordSalt] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[UserIP] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_mytrip_Membership] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_usersinroles]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_usersinroles](
	[UserId] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_mytrip_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_storevotes]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_storevotes](
	[VotesId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Vote] [int] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Reviews] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_mytrip_storevotes] PRIMARY KEY CLUSTERED 
(
	[VotesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_storeoptions]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_storeoptions](
	[OptionsId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[Image] [nvarchar](256) NULL,
	[Title] [nvarchar](256) NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_mytrip_storeproductvariety] PRIMARY KEY CLUSTERED 
(
	[OptionsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlesvotes]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlesvotes](
	[VotesId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Vote] [int] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_mytrip_ArticlesVotes] PRIMARY KEY CLUSTERED 
(
	[VotesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlessubscription]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlessubscription](
	[SubscribeId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_mytrip_articlessubscription] PRIMARY KEY CLUSTERED 
(
	[SubscribeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlesintags]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlesintags](
	[ArticleId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_mytrip_ArticlesInTags] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mytrip_articlescomments]    Script Date: 10/07/2010 05:27:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mytrip_articlescomments](
	[CommentId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserEmail] [nvarchar](100) NOT NULL,
	[IsAnonym] [bit] NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_mytrip_ArticleComment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_mytrip_Article_mytrip_ArticleCategory]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])
GO
ALTER TABLE [dbo].[mytrip_articles] CHECK CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]
GO
/****** Object:  ForeignKey [FK_mytrip_articles_mytrip_articles]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_articles_mytrip_articles] FOREIGN KEY([SubArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
GO
ALTER TABLE [dbo].[mytrip_articles] CHECK CONSTRAINT [FK_mytrip_articles_mytrip_articles]
GO
/****** Object:  ForeignKey [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlescategory]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])
GO
ALTER TABLE [dbo].[mytrip_articlescategory] CHECK CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]
GO
/****** Object:  ForeignKey [FK_mytrip_ArticleComment_mytrip_Article]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlescomments]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
GO
ALTER TABLE [dbo].[mytrip_articlescomments] CHECK CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]
GO
/****** Object:  ForeignKey [FK_mytrip_ArticlesInTags_mytrip_Article]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
GO
ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]
GO
/****** Object:  ForeignKey [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag] FOREIGN KEY([TagId])
REFERENCES [dbo].[mytrip_articlestag] ([TagId])
GO
ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]
GO
/****** Object:  ForeignKey [FK_mytrip_articlessubscription_mytrip_articles]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlessubscription]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_articlessubscription_mytrip_articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
GO
ALTER TABLE [dbo].[mytrip_articlessubscription] CHECK CONSTRAINT [FK_mytrip_articlessubscription_mytrip_articles]
GO
/****** Object:  ForeignKey [FK_mytrip_ArticlesVotes_mytrip_Articles]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_articlesvotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
GO
ALTER TABLE [dbo].[mytrip_articlesvotes] CHECK CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]
GO
/****** Object:  ForeignKey [FK_mytrip_storedepartment_mytrip_storedepartment]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_storedepartment]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment] FOREIGN KEY([SubDepartmentId])
REFERENCES [dbo].[mytrip_storedepartment] ([DepartmentId])
GO
ALTER TABLE [dbo].[mytrip_storedepartment] CHECK CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment]
GO
/****** Object:  ForeignKey [FK_mytrip_storeoptions_mytrip_storeproduct]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_storeoptions]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeoptions_mytrip_storeproduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[mytrip_storeproduct] ([ProductId])
GO
ALTER TABLE [dbo].[mytrip_storeoptions] CHECK CONSTRAINT [FK_mytrip_storeoptions_mytrip_storeproduct]
GO
/****** Object:  ForeignKey [FK_mytrip_storeproduct_mytrip_storedepartment]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_storeproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproduct_mytrip_storedepartment] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[mytrip_storedepartment] ([DepartmentId])
GO
ALTER TABLE [dbo].[mytrip_storeproduct] CHECK CONSTRAINT [FK_mytrip_storeproduct_mytrip_storedepartment]
GO
/****** Object:  ForeignKey [FK_mytrip_storeproduct_mytrip_storeproducer]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_storeproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproduct_mytrip_storeproducer] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[mytrip_storeproducer] ([ProducerId])
GO
ALTER TABLE [dbo].[mytrip_storeproduct] CHECK CONSTRAINT [FK_mytrip_storeproduct_mytrip_storeproducer]
GO
/****** Object:  ForeignKey [FK_mytrip_storevotes_mytrip_storeproduct]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_storevotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storevotes_mytrip_storeproduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[mytrip_storeproduct] ([ProductId])
GO
ALTER TABLE [dbo].[mytrip_storevotes] CHECK CONSTRAINT [FK_mytrip_storevotes_mytrip_storeproduct]
GO
/****** Object:  ForeignKey [FK_mytrip_UsersInRoles_mytrip_Roles]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[mytrip_usersroles] ([RoleId])
GO
ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles]
GO
/****** Object:  ForeignKey [FK_mytrip_UsersInRoles_mytrip_Users]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[mytrip_users] ([UserId])
GO
ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users]
GO
/****** Object:  ForeignKey [FK_mytrip_Membership_mytrip_Users]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_usersmembership]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Membership_mytrip_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[mytrip_users] ([UserId])
GO
ALTER TABLE [dbo].[mytrip_usersmembership] CHECK CONSTRAINT [FK_mytrip_Membership_mytrip_Users]
GO
/****** Object:  ForeignKey [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]    Script Date: 10/07/2010 05:27:53 ******/
ALTER TABLE [dbo].[mytrip_votesanswer]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[mytrip_votesquestion] ([QuestionId])
GO
ALTER TABLE [dbo].[mytrip_votesanswer] CHECK CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]
GO
