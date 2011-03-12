IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_rssparser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_rssparser] (
[RssparserId] [int]  NOT NULL,
[Title] [nvarchar](256)  NOT NULL,
[Path] [nvarchar](256)  NOT NULL,
[SeoTitle] [nvarchar](256) NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[CreateDate] [datetime]  NOT NULL,
[UserName] [nvarchar](100)  NOT NULL,
[Views] [int]  NOT NULL,
[Culture] [nvarchar](100)  NOT NULL,
[AllCulture] [bit]  NOT NULL,
[RssUrl] [nvarchar](256)  NOT NULL,
[ImageUrl] [nvarchar](256)  NOT NULL,
CONSTRAINT [PK_mytrip_RssParser] PRIMARY KEY CLUSTERED
(
[RssparserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END