IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesquestion]') AND type in (N'U'))
BEGIN
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
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]') AND type in (N'U'))
BEGIN
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
END
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))
ALTER TABLE [dbo].[mytrip_votesanswer]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[mytrip_votesquestion] ([QuestionId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_VotesAnswer_mytrip_VotesQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_votesanswer]'))
ALTER TABLE [dbo].[mytrip_votesanswer] CHECK CONSTRAINT [FK_mytrip_VotesAnswer_mytrip_VotesQuestion]
     