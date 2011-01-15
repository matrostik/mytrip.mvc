IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_rssparser]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_rssparser]