IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_articles_mytrip_articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_articles_mytrip_articles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))
ALTER TABLE [dbo].[mytrip_articlescategory] DROP CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))
ALTER TABLE [dbo].[mytrip_articlescomments] DROP CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))
ALTER TABLE [dbo].[mytrip_articlesvotes] DROP CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleComment_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]'))
ALTER TABLE [dbo].[mytrip_articlescomments] DROP CONSTRAINT [FK_mytrip_ArticleComment_mytrip_Article]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_articlessubscription_mytrip_articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlessubscription]'))
ALTER TABLE [dbo].[mytrip_articlessubscription] DROP CONSTRAINT [FK_mytrip_articlessubscription_mytrip_articles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlessubscription]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlessubscription]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_commentvotes_mytrip_articlescomments]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_commentvotes]'))
ALTER TABLE [dbo].[mytrip_commentvotes] DROP CONSTRAINT [FK_mytrip_commentvotes_mytrip_articlescomments]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_commentvotes]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_commentvotes]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescomments]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlescomments]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_Article]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]'))
ALTER TABLE [dbo].[mytrip_articlesintags] DROP CONSTRAINT [FK_mytrip_ArticlesInTags_mytrip_ArticlesTag]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesintags]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlesintags]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticlesVotes_mytrip_Articles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]'))
ALTER TABLE [dbo].[mytrip_articlesvotes] DROP CONSTRAINT [FK_mytrip_ArticlesVotes_mytrip_Articles]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlesvotes]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlesvotes]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articles]'))
ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articles]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articles]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_ArticleCategory_mytrip_ArticleCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]'))
ALTER TABLE [dbo].[mytrip_articlescategory] DROP CONSTRAINT [FK_mytrip_ArticleCategory_mytrip_ArticleCategory]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlescategory]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlescategory]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_articlestag]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_articlestag]