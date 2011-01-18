IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_weather]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_weather] (
[weatherId] [int]  NOT NULL,
[Title] [nvarchar](256)  NOT NULL,
[UrlXml] [nvarchar](256)  NOT NULL,
[Culture] [nvarchar](50)  NOT NULL,
[AllCulture] [bit]  NOT NULL,
[UserName] [nvarchar](100)  NOT NULL,
[CreateDate] [datetime]  NOT NULL,
[VisibleInformer] [bit]  NOT NULL,
CONSTRAINT [PK_mytrip_weather] PRIMARY KEY CLUSTERED 
(
[weatherId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END