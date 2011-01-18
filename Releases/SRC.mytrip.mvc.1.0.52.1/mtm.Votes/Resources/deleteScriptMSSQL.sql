IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))
ALTER TABLE [dbo].[mytrip_votesanswer] DROP CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))
ALTER TABLE [dbo].[mytrip_votesanswer] DROP CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_votesanswer]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesquestion]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_votesquestion]