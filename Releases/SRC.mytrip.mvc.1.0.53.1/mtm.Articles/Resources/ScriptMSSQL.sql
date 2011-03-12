IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlestag]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_articlestag](
[TagId] [int] NOT NULL,
[TagName] [nvarchar](256) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
CONSTRAINT [PK_mytrip_ArticlesTag] PRIMARY KEY CLUSTERED 
(
[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_articlescategory](
[CategoryId] [int] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[UserEmail] [nvarchar](100) NOT NULL,
[SeparateBlock] [bit] NOT NULL,
[SeoTitle] [nvarchar](256) NOT NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
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
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articles]') AND type in (N'U'))
BEGIN
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
[SeoTitle] [nvarchar](256) NOT NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[OnlyForRegisterUser] [bit] NOT NULL,
[ApprovedVotes] [bit] NOT NULL,
[CloseDate] [datetime] NOT NULL,
[Culture] [nvarchar](100) NOT NULL,
[AllCulture] [bit] NOT NULL,
[TotalVotes] [decimal](4, 2) NOT NULL,
[ModerateComments] [bit] NOT NULL,
[CommentVotes] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_Article] PRIMARY KEY CLUSTERED 
(
[ArticleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]') AND type in (N'U'))
BEGIN
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
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlessubscription]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_articlessubscription](
[SubscribeId] [int] NOT NULL,
[ArticleId] [int] NOT NULL,
[UserName] [nvarchar](100) COLLATE Cyrillic_General_CI_AS NOT NULL,
CONSTRAINT [PK_mytrip_articlessubscription] PRIMARY KEY CLUSTERED 
(
[SubscribeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_articlesintags](
[ArticleId] [int] NOT NULL,
[TagId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_ArticlesInTags] PRIMARY KEY CLUSTERED 
(
[ArticleId] ASC,
[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_articlescomments](
[CommentId] [int] NOT NULL,
[ArticleId] [int] NOT NULL,
[Body] [nvarchar](max) NOT NULL,
[CreateDate] [datetime] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[UserEmail] [nvarchar](100) NOT NULL,
[IsAnonym] [bit] NOT NULL,
[IsApproved] [bit] NOT NULL,
[Votes] [int] NOT NULL,
CONSTRAINT [PK_mytrip_ArticleComment] PRIMARY KEY CLUSTERED 
(
[CommentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_commentvotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_commentvotes](
[Id] [int] NOT NULL,
[CommentId] [int] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_mytrip_commentvotes] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_articles_mytrip_articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_articles_mytrip_articles] FOREIGN KEY([SubArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles] CHECK CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))
ALTER TABLE [dbo].[mytrip_articlescategory]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[mytrip_articlescategory] ([CategoryId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))
ALTER TABLE [dbo].[mytrip_articlescategory] CHECK CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))
ALTER TABLE [dbo].[mytrip_articlescomments]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))
ALTER TABLE [dbo].[mytrip_articlescomments] CHECK CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag] FOREIGN KEY([TagId])
REFERENCES [dbo].[mytrip_articlestag] ([TagId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] CHECK CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_articlessubscription_mytrip_articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlessubscription]'))
ALTER TABLE [dbo].[mytrip_articlessubscription]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_articlessubscription_mytrip_articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))
ALTER TABLE [dbo].[mytrip_articlesvotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[mytrip_articles] ([ArticleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))
ALTER TABLE [dbo].[mytrip_articlesvotes] CHECK CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_commentvotes_mytrip_articlescomments]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_commentvotes]'))
ALTER TABLE [dbo].[mytrip_commentvotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_commentvotes_mytrip_articlescomments] FOREIGN KEY([CommentId])
REFERENCES [dbo].[mytrip_articlescomments] ([CommentId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_commentvotes_mytrip_articlescomments]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_commentvotes]'))
ALTER TABLE [dbo].[mytrip_commentvotes] CHECK CONSTRAINT [FK_mytrip_commentvotes_mytrip_articlescomments]
         