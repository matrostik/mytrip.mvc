IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))
ALTER TABLE [dbo].[mytrip_tours] DROP CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscountry]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))
ALTER TABLE [dbo].[mytrip_tours] DROP CONSTRAINT [FK_mytrip_tours_mytrip_tourscountry]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))
ALTER TABLE [dbo].[mytrip_tourscategory] DROP CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))
ALTER TABLE [dbo].[mytrip_toursvariants] DROP CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_toursvariants]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tours]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_tours]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_tourscategory]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tourscountry]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_tourscountry]