IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_users](
[UserId] [nvarchar](50) NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[LastActivityDate] [datetime] NOT NULL,
CONSTRAINT [PK_mytrip_Users] PRIMARY KEY CLUSTERED 
(
[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_usersroles](
[RoleId] [int] NOT NULL,
[RoleName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_mytrip_Roles] PRIMARY KEY CLUSTERED 
(
[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]') AND type in (N'U'))
BEGIN
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
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_usersinroles](
[UserId] [nvarchar](50) NOT NULL,
[RoleId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_UsersInRoles] PRIMARY KEY CLUSTERED 
(
[UserId] ASC,
[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_corepages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_corepages](
[PageId] [int] NOT NULL,
[Title] [nvarchar](256) NULL,
[Path] [nvarchar](256) NULL,
[SeoTitle] [nvarchar](256) NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[Body] [nvarchar](max) NULL,
[ViewOnlyHomePage] [bit] NOT NULL,
[SideBar] [bit] NOT NULL,          
[EmailForm] [bit] NOT NULL,
[AddMenu] [bit] NOT NULL,
[AddHomePage] [bit] NOT NULL,
[SubPagesId] [int] NOT NULL,
[Culture] [nvarchar](100) NOT NULL,
[AllCulture] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_corepages] PRIMARY KEY CLUSTERED 
(
[PageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_geocountry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_geocountry](
[CountryId] [int] NOT NULL,
[Code] [nvarchar](50) NOT NULL,
[Name] [nvarchar](256) NOT NULL,
[Longitude] [decimal](18, 8) NULL,
[Latitude] [decimal](18, 8) NULL,
[EditAdmin] [bit] NOT NULL,
[EditUser] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_geocountry] PRIMARY KEY CLUSTERED
(
[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_georegion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_georegion](
[RegionId] [int] NOT NULL,
[CountryId] [int] NOT NULL,
[Code] [nvarchar](50) NOT NULL,
[Name] [nvarchar](256) NOT NULL,
[Longitude] [decimal](18, 8) NULL,
[Latitude] [decimal](18, 8) NULL,
[EditAdmin] [bit] NOT NULL,
[EditUser] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_georegion] PRIMARY KEY CLUSTERED 
(
[RegionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_geocity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_geocity](
[CityId] [int] NOT NULL,
[RegionId] [int] NOT NULL,
[Name] [nvarchar](256) NOT NULL,
[Longitude] [decimal](18, 8) NULL,
[Latitude] [decimal](18, 8) NULL,
[EditAdmin] [bit] NOT NULL,
[EditUser] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_geocity] PRIMARY KEY CLUSTERED 
(
[CityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_usersprofile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_usersprofile](
[UserId] [nvarchar](50) NOT NULL,
[FirstName] [nvarchar](256) NULL,
[LastName] [nvarchar](256) NULL,
[Phone] [nvarchar](256) NULL,
[Longitude] [decimal](18, 8) NULL,
[Latitude] [decimal](18, 8) NULL,
[Site] [nvarchar](256) NULL,
[Description] [nvarchar](max) NULL,
[icq] [nvarchar](100) NULL,
[skype] [nvarchar](100) NULL,
[CityId] [int] NOT NULL,
[ProfileClose] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_usersprofile] PRIMARY KEY CLUSTERED 
(
[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_corestatistic]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_corestatistic](
[StatisticId] [int] NOT NULL,
[UserIP] [nvarchar](max) NULL,
[Date] [datetime] NOT NULL,
[Page] [nvarchar](max) NULL,
[Referrer] [nvarchar](max) NULL,
[Browser] [nvarchar](max) NULL,
[OS] [nvarchar](max) NULL,
[CityId] [int] NOT NULL,
[UserCount] [int] NOT NULL,
[ViewCount] [int] NOT NULL,
[Day] [bit] NOT NULL,
[Month] [bit] NOT NULL,
[Year] [bit] NOT NULL,
[Time] [int] NULL,
CONSTRAINT [PK_mytrip_corestatistic] PRIMARY KEY CLUSTERED 
(
[StatisticId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))
ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[mytrip_usersroles] ([RoleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))
ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Roles]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))
ALTER TABLE [dbo].[mytrip_usersinroles]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[mytrip_users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_UsersInRoles_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersinroles]'))
ALTER TABLE [dbo].[mytrip_usersinroles] CHECK CONSTRAINT [FK_mytrip_UsersInRoles_mytrip_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Membership_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]'))
ALTER TABLE [dbo].[mytrip_usersmembership]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_Membership_mytrip_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[mytrip_users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_Membership_mytrip_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersmembership]'))
ALTER TABLE [dbo].[mytrip_usersmembership] CHECK CONSTRAINT [FK_mytrip_Membership_mytrip_Users]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_corepages_mytrip_corepages]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_corepages]'))
ALTER TABLE [dbo].[mytrip_corepages]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_corepages_mytrip_corepages] FOREIGN KEY([SubPagesId])
REFERENCES [dbo].[mytrip_corepages] ([PageId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_corepages_mytrip_corepages]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_corepages]'))
ALTER TABLE [dbo].[mytrip_corepages] CHECK CONSTRAINT [FK_mytrip_corepages_mytrip_corepages]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_corestatistic_mytrip_geocity]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_corestatistic]'))
ALTER TABLE [dbo].[mytrip_corestatistic]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_corestatistic_mytrip_geocity] FOREIGN KEY([CityId])
REFERENCES [dbo].[mytrip_geocity] ([CityId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_corestatistic_mytrip_geocity]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_corestatistic]'))
ALTER TABLE [dbo].[mytrip_corestatistic] CHECK CONSTRAINT [FK_mytrip_corestatistic_mytrip_geocity]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_geocity_mytrip_georegion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_geocity]'))
ALTER TABLE [dbo].[mytrip_geocity]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_geocity_mytrip_georegion] FOREIGN KEY([RegionId])
REFERENCES [dbo].[mytrip_georegion] ([RegionId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_geocity_mytrip_georegion]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_geocity]'))
ALTER TABLE [dbo].[mytrip_geocity] CHECK CONSTRAINT [FK_mytrip_geocity_mytrip_georegion]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_georegion_mytrip_geocountry]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_georegion]'))
ALTER TABLE [dbo].[mytrip_georegion]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_georegion_mytrip_geocountry] FOREIGN KEY([CountryId])
REFERENCES [dbo].[mytrip_geocountry] ([CountryId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_georegion_mytrip_geocountry]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_georegion]'))
ALTER TABLE [dbo].[mytrip_georegion] CHECK CONSTRAINT [FK_mytrip_georegion_mytrip_geocountry]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_usersprofile_mytrip_geocity]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersprofile]'))
ALTER TABLE [dbo].[mytrip_usersprofile]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_usersprofile_mytrip_geocity] FOREIGN KEY([CityId])
REFERENCES [dbo].[mytrip_geocity] ([CityId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_usersprofile_mytrip_geocity]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersprofile]'))
ALTER TABLE [dbo].[mytrip_usersprofile] CHECK CONSTRAINT [FK_mytrip_usersprofile_mytrip_geocity]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_usersprofile_mytrip_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersprofile]'))
ALTER TABLE [dbo].[mytrip_usersprofile]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_usersprofile_mytrip_users] FOREIGN KEY([UserId])
REFERENCES [dbo].[mytrip_users] ([UserId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_usersprofile_mytrip_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_usersprofile]'))
ALTER TABLE [dbo].[mytrip_usersprofile] CHECK CONSTRAINT [FK_mytrip_usersprofile_mytrip_users]                      
           