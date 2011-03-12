IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeseller]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeseller](
[SellerId] [int] NOT NULL,
[Organization] [nvarchar](256) NULL,
[Address] [nvarchar](max) NULL,
[Phone] [nvarchar](256) NULL,
[Email] [nvarchar](256) NULL,
[OrganizationINN] [nvarchar](256) NULL,
[OrganizationKPP] [nvarchar](256) NULL,
[BankAccountSeller] [nvarchar](256) NULL,
[BankAccountBIK] [nvarchar](256) NULL,
[Bank] [nvarchar](256) NULL,
[BankAccount] [nvarchar](256) NULL,
[Director] [nvarchar](256) NULL,
[Accountant] [nvarchar](256) NULL,
[LiteNDS] [bit] NOT NULL,
[Culture] [nvarchar](50) NOT NULL,
[AllCulture] [bit] NOT NULL,
CONSTRAINT [PK_mytrip_storeseller] PRIMARY KEY CLUSTERED 
(
[SellerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storesale]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storesale](
[SaleId] [int] NOT NULL,
[Sale] [int] NOT NULL,
[Title] [nvarchar](256) NULL,
[CreationDate] [datetime] NOT NULL,
[CloseDate] [datetime] NOT NULL,
[UserName] [nvarchar](256) NULL,
[StartDate] [datetime] NOT NULL,
[ManagerName] [nvarchar](256) NOT NULL,
CONSTRAINT [PK_mytrip_storesale] PRIMARY KEY CLUSTERED 
(
[SaleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeprofile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeprofile](
[ProfileId] [int] NOT NULL,
[UserName] [nvarchar](256) NOT NULL,
[UserEmail] [nvarchar](256) NOT NULL,
[Address] [nvarchar](max) NULL,
[Phone] [nvarchar](256) NULL,
[IsAnonym] [bit] NOT NULL,
[FirstName] [nvarchar](256) NOT NULL,
[LastName] [nvarchar](256) NULL,
[Organization] [nvarchar](256) NULL,
[OrganizationINN] [nvarchar](256) NULL,
[OrganizationKPP] [nvarchar](256) NULL,
[UserIP] [nvarchar](256) NOT NULL,
CONSTRAINT [PK_mytrip_storeprofile] PRIMARY KEY CLUSTERED 
(
[ProfileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeorder]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeorder](
[OrderId] [int] NOT NULL,
[Status] [int] NOT NULL,
[Culture] [nvarchar](256) NOT NULL,
[CreationDate] [datetime] NOT NULL,
[ManagerName] [nvarchar](256) NULL,
[Delivery] [decimal](18, 2) NOT NULL,
[PriceInWords] [nvarchar](256) NULL,
[NamberAccount] [int] NULL,
[DateAccount] [datetime] NULL,
[AccountPage] [nvarchar](max) NULL,
[MoneyId] [nvarchar](50) NOT NULL,
[ProfileId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_storemanageorder] PRIMARY KEY CLUSTERED 
(
[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storedepartment](
[DepartmentId] [int] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
[SeoTitle] [nvarchar](256) NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[Culture] [nvarchar](50) NOT NULL,
[AllCulture] [bit] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[CreationDate] [datetime] NOT NULL,
[Body] [nvarchar](max) NULL,
[SubDepartmentId] [int] NOT NULL,
[SaleId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_storedepartment] PRIMARY KEY CLUSTERED 
(
[DepartmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeproducer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeproducer](
[ProducerId] [int] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[SeoTitle] [nvarchar](256) NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[Body] [nvarchar](max) NULL,
[Path] [nvarchar](256) NOT NULL,
[Culture] [nvarchar](50) NOT NULL,
[AllCulture] [bit] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[CreationDate] [datetime] NOT NULL,
[SaleId] [int] NOT NULL,
CONSTRAINT [PK_mytrip_storeproducer] PRIMARY KEY CLUSTERED 
(
[ProducerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeproduct](
[ProductId] [int] NOT NULL,
[DepartmentId] [int] NOT NULL,
[ProducerId] [int] NOT NULL,
[Title] [nvarchar](256) NOT NULL,
[Path] [nvarchar](256) NOT NULL,
[SeoTitle] [nvarchar](256) NULL,
[SeoKeyword] [nvarchar](max) NULL,
[SeoDescription] [nvarchar](max) NULL,
[Details] [nvarchar](max) NULL,
[Body] [nvarchar](max) NULL,
[CreationDate] [datetime] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[Culture] [nvarchar](50) NOT NULL,
[AllCulture] [bit] NOT NULL,
[Price] [decimal](18, 2) NOT NULL,
[TotalVotes] [decimal](4, 2) NOT NULL,
[TotalCount] [int] NOT NULL,
[ViewPrice] [bit] NOT NULL,
[ViewVotes] [bit] NOT NULL,
[ViewCount] [bit] NOT NULL,
[UrlFile] [nvarchar](256) NULL,
[Packing] [nvarchar](256) NULL,
[NamberCatalog] [nvarchar](256) NULL,
[SaleId] [int] NOT NULL,
[MoneyId] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_mytrip_storeproduct] PRIMARY KEY CLUSTERED 
(
[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storevotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storevotes](
[VotesId] [int] NOT NULL,
[ProductId] [int] NOT NULL,
[Vote] [int] NOT NULL,
[UserName] [nvarchar](100) NOT NULL,
[Reviews] [nvarchar](max) NULL,
[CreationDate] [datetime] NOT NULL,
CONSTRAINT [PK_mytrip_storevotes] PRIMARY KEY CLUSTERED 
(
[VotesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mytrip_storeorderisproduct](
[ProductId] [int] NOT NULL,
[Count] [int] NOT NULL,
[OrderId] [int] NOT NULL,
[Price] [decimal](18, 2) NOT NULL,
[MoneyId] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_mytrip_storeorderisproduct] PRIMARY KEY CLUSTERED 
(
[ProductId] ASC,
[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment] FOREIGN KEY([SubDepartmentId])
REFERENCES [dbo].[mytrip_storedepartment] ([DepartmentId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment] CHECK CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storedepartment_mytrip_storesale] FOREIGN KEY([SaleId])
REFERENCES [dbo].[mytrip_storesale] ([SaleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment] CHECK CONSTRAINT [FK_mytrip_storedepartment_mytrip_storesale]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeprofile]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorder]'))
ALTER TABLE [dbo].[mytrip_storeorder]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeorder_mytrip_storeprofile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[mytrip_storeprofile] ([ProfileId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeprofile]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorder]'))
ALTER TABLE [dbo].[mytrip_storeorder] CHECK CONSTRAINT [FK_mytrip_storeorder_mytrip_storeprofile]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storemanageorder]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeorder_mytrip_storemanageorder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[mytrip_storeorder] ([OrderId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storemanageorder]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct] CHECK CONSTRAINT [FK_mytrip_storeorder_mytrip_storemanageorder]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeorder_mytrip_storeproduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[mytrip_storeproduct] ([ProductId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct] CHECK CONSTRAINT [FK_mytrip_storeorder_mytrip_storeproduct]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproducer_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproducer]'))
ALTER TABLE [dbo].[mytrip_storeproducer]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproducer_mytrip_storesale] FOREIGN KEY([SaleId])
REFERENCES [dbo].[mytrip_storesale] ([SaleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproducer_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproducer]'))
ALTER TABLE [dbo].[mytrip_storeproducer] CHECK CONSTRAINT [FK_mytrip_storeproducer_mytrip_storesale]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproduct_mytrip_storedepartment] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[mytrip_storedepartment] ([DepartmentId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] CHECK CONSTRAINT [FK_mytrip_storeproduct_mytrip_storedepartment]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storeproducer]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproduct_mytrip_storeproducer] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[mytrip_storeproducer] ([ProducerId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storeproducer]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] CHECK CONSTRAINT [FK_mytrip_storeproduct_mytrip_storeproducer]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storeproduct_mytrip_storesale] FOREIGN KEY([SaleId])
REFERENCES [dbo].[mytrip_storesale] ([SaleId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] CHECK CONSTRAINT [FK_mytrip_storeproduct_mytrip_storesale]
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storevotes_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storevotes]'))
ALTER TABLE [dbo].[mytrip_storevotes]  WITH CHECK ADD  CONSTRAINT [FK_mytrip_storevotes_mytrip_storeproduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[mytrip_storeproduct] ([ProductId])
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storevotes_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storevotes]'))
ALTER TABLE [dbo].[mytrip_storevotes] CHECK CONSTRAINT [FK_mytrip_storevotes_mytrip_storeproduct]
      