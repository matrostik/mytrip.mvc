IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_tourscategory](
[AllCulture] [bit] NOT NULL,
[Body] [nvarchar](max) NULL,
[CategoryId] [int] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[Culture] [nvarchar](100) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
[SubCategoryId] [int] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_mytrip_tourscategory] PRIMARY KEY CLUSTERED 
(
[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_tours]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_tours](
[AllCulture] [bit] NOT NULL,
[Body] [nvarchar](max) NULL,
[CategoryId] [int] NOT NULL,
[CloseTourDate] [datetime] NOT NULL,
[CreateDate] [datetime] NOT NULL,
[Culture] [nvarchar](100) NOT NULL,
[Imige] [nvarchar](256) NULL,
[Latitude] [decimal](10, 8) NULL,
[Longitude] [decimal](10, 8) NULL,
[MinPrice] [decimal](18, 2) NOT NULL,
[MoneyId] [nvarchar](100) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
[StartDate] [datetime] NOT NULL,
[StopDate] [datetime] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[TourId] [int] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_mytrip_tours] PRIMARY KEY CLUSTERED
(
[TourId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_toursvariants](
[Hotel] [nvarchar](1000) NOT NULL,
[Latitude] [decimal](10, 8) NULL,
[Longitude] [decimal](10, 8) NULL,
[MoneyId] [nvarchar](100) NOT NULL,
[Price] [decimal](18, 2) NOT NULL,
[Services] [nvarchar](1000) NULL,
[TourId] [int] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[VariantId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_toursvariants] PRIMARY KEY CLUSTERED 
(
[VariantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))
ALTER TABLE [dbo].[mytrip_tours]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[mytrip_tourscategory] ([CategoryId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tours_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tours]'))
ALTER TABLE [dbo].[mytrip_tours] CHECK CONSTRAINT [FK_mytrip_tours_mytrip_tourscategory]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))
ALTER TABLE [dbo].[mytrip_tourscategory]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[mytrip_tourscategory] ([CategoryId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_tourscategory_mytrip_tourscategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_tourscategory]'))
ALTER TABLE [dbo].[mytrip_tourscategory] CHECK CONSTRAINT [FK_mytrip_tourscategory_mytrip_tourscategory]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))
ALTER TABLE [dbo].[mytrip_toursvariants]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours] FOREIGN KEY([TourId])
REFERENCES [dbo].[mytrip_tours] ([TourId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_toursvariants_mytrip_tours]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_toursvariants]'))
ALTER TABLE [dbo].[mytrip_toursvariants] CHECK CONSTRAINT [FK_mytrip_toursvariants_mytrip_tours]
       