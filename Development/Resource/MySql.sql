-- Скрипт сгенерирован Devart dbForge Studio for MySQL, Версия 3.60.351.1
-- Дата: 10/29/2010 2:55:20 AM
-- Версия сервера: 5.1.50-community
-- Версия клиента: 4.1

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS = @@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS = 0 */;

SET NAMES 'utf8';
USE mytripmvc;

--
-- Описание для таблицы mytrip_articlescategory
--
CREATE TABLE IF NOT EXISTS mytrip_articlescategory(
  CategoryId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  CreateDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  UserEmail VARCHAR (100) NOT NULL,
  SeparateBlock BIT (1) NOT NULL,
  Blog BIT (1) NOT NULL,
  Views INT (11) NOT NULL,
  SubCategoryId INT (11) NOT NULL,
  Culture VARCHAR (100) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  PRIMARY KEY (CategoryId),
  INDEX IX_mytrip_ArticleCategory_mytrip_ArticleCategory USING BTREE (SubCategoryId),
  CONSTRAINT FK_mytrip_ArticleCategory_mytrip_ArticleCategory FOREIGN KEY (SubCategoryId)
  REFERENCES mytrip_articlescategory (CategoryId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articlestag
--
CREATE TABLE IF NOT EXISTS mytrip_articlestag(
  TagId INT (11) NOT NULL,
  TagName VARCHAR (256) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  PRIMARY KEY (TagId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_gismeteo
--
CREATE TABLE IF NOT EXISTS mytrip_gismeteo(
  GismeteoId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  UrlXml VARCHAR (256) NOT NULL,
  Culture VARCHAR (50) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  CreateDate DATETIME NOT NULL,
  VisibleInformer BIT (1) NOT NULL,
  PRIMARY KEY (GismeteoId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_rssparser
--
CREATE TABLE IF NOT EXISTS mytrip_rssparser(
  RssparserId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  CreateDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  Views INT (11) NOT NULL,
  Culture VARCHAR (100) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  RssUrl VARCHAR (256) NOT NULL,
  ImageUrl VARCHAR (256) DEFAULT NULL,
  PRIMARY KEY (RssparserId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeprofile
--
CREATE TABLE IF NOT EXISTS mytrip_storeprofile(
  ProfileId INT (11) NOT NULL,
  UserName VARCHAR (256) NOT NULL,
  UserEmail VARCHAR (256) NOT NULL,
  Address TEXT DEFAULT NULL,
  Phone VARCHAR (256) DEFAULT NULL,
  IsAnonym BIT (1) NOT NULL,
  FirstName VARCHAR (256) NOT NULL,
  LastName VARCHAR (256) DEFAULT NULL,
  Organization VARCHAR (256) DEFAULT NULL,
  OrganizationINN VARCHAR (256) DEFAULT NULL,
  OrganizationKPP VARCHAR (256) DEFAULT NULL,
  UserIP VARCHAR (256) NOT NULL,
  PRIMARY KEY (ProfileId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storesale
--
CREATE TABLE IF NOT EXISTS mytrip_storesale(
  SaleId INT (11) NOT NULL,
  Sale INT (11) NOT NULL,
  Title VARCHAR (256) DEFAULT NULL,
  CreationDate DATETIME NOT NULL,
  CloseDate DATETIME NOT NULL,
  UserName VARCHAR (256) DEFAULT NULL,
  StartDate DATETIME NOT NULL,
  ManagerName VARCHAR (256) NOT NULL,
  PRIMARY KEY (SaleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 5461
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeseller
--
CREATE TABLE IF NOT EXISTS mytrip_storeseller(
  SellerId INT (11) NOT NULL,
  Organization VARCHAR (256) DEFAULT NULL,
  Address TEXT DEFAULT NULL,
  Phone VARCHAR (256) DEFAULT NULL,
  Email VARCHAR (256) DEFAULT NULL,
  OrganizationINN VARCHAR (256) DEFAULT NULL,
  OrganizationKPP VARCHAR (256) DEFAULT NULL,
  BankAccountSeller VARCHAR (256) DEFAULT NULL,
  BankAccountBIK VARCHAR (256) DEFAULT NULL,
  Bank VARCHAR (256) DEFAULT NULL,
  BankAccount VARCHAR (256) DEFAULT NULL,
  Director VARCHAR (256) DEFAULT NULL,
  Accountant VARCHAR (256) DEFAULT NULL,
  LiteNDS BIT (1) NOT NULL,
  Culture VARCHAR (50) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  PRIMARY KEY (SellerId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_users
--
CREATE TABLE IF NOT EXISTS mytrip_users(
  UserId VARCHAR (50),
  UserName VARCHAR (100) NOT NULL,
  LastActivityDate DATETIME NOT NULL,
  PRIMARY KEY (UserId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_usersroles
--
CREATE TABLE IF NOT EXISTS mytrip_usersroles(
  RoleId INT (11) NOT NULL,
  RoleName VARCHAR (100) NOT NULL,
  PRIMARY KEY (RoleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 4096
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_votesquestion
--
CREATE TABLE IF NOT EXISTS mytrip_votesquestion(
  QuestionId INT (11) NOT NULL,
  Question VARCHAR (256) NOT NULL,
  TotalVotes INT (11) NOT NULL,
  Active BIT (10) NOT NULL,
  CreateDate DATETIME NOT NULL,
  CloseDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  OnlyForRegisterUser BIT (10) NOT NULL,
  Culture VARCHAR (100) NOT NULL,
  AllCulture BIT (10) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  PRIMARY KEY (QuestionId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articles
--
CREATE TABLE IF NOT EXISTS mytrip_articles(
  ArticleId INT (11) NOT NULL,
  SubArticleId INT (11) NOT NULL,
  CategoryId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Abstract TEXT DEFAULT NULL,
  Body TEXT NOT NULL,
  CreateDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  Views INT (11) NOT NULL,
  ApprovedComment BIT (1) NOT NULL,
  IncludeAnonymComment BIT (1) NOT NULL,
  ImageForAbstract VARCHAR (256) DEFAULT NULL,
  Path VARCHAR (256) NOT NULL,
  OnlyForRegisterUser BIT (1) NOT NULL,
  ApprovedVotes BIT (1) NOT NULL,
  CloseDate DATETIME NOT NULL,
  Culture VARCHAR (100) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  TotalVotes DECIMAL (4, 2) NOT NULL,
  ModerateComments BIT (1) NOT NULL,
  PRIMARY KEY (ArticleId),
  INDEX FK_mytrip_articles_mytrip_articles_ArticleId USING BTREE (SubArticleId),
  INDEX IX_mytrip_Article_mytrip_ArticleCategory USING BTREE (CategoryId),
  CONSTRAINT FK_mytrip_Article_mytrip_ArticleCategory FOREIGN KEY (CategoryId)
  REFERENCES mytrip_articlescategory (CategoryId),
  CONSTRAINT FK_mytrip_articles_mytrip_articles_ArticleId FOREIGN KEY (SubArticleId)
  REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storedepartment
--
CREATE TABLE IF NOT EXISTS mytrip_storedepartment(
  DepartmentId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  Culture VARCHAR (50) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  CreationDate DATETIME NOT NULL,
  Body TEXT DEFAULT NULL,
  SubDepartmentId INT (11) NOT NULL,
  SaleId INT (11) NOT NULL,
  PRIMARY KEY (DepartmentId),
  INDEX IX_mytrip_storedepartment USING BTREE (SubDepartmentId),
  INDEX mytrip_storedepartment_FK1 USING BTREE (SaleId),
  CONSTRAINT mytrip_storedepartment_FK1 FOREIGN KEY (SaleId)
  REFERENCES mytrip_storesale (SaleId),
  CONSTRAINT mytrip_storedepartment_FK2 FOREIGN KEY (SubDepartmentId)
  REFERENCES mytrip_storedepartment (DepartmentId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeorder
--
CREATE TABLE IF NOT EXISTS mytrip_storeorder(
  OrderId INT (11) NOT NULL,
  Status INT (11) NOT NULL,
  Culture VARCHAR (256) NOT NULL,
  CreationDate DATETIME NOT NULL,
  ManagerName VARCHAR (256) DEFAULT NULL,
  Delivery DECIMAL (18, 2) NOT NULL,
  MoneyId VARCHAR (50) NOT NULL,
  PriceInWords VARCHAR (256) DEFAULT NULL,
  NamberAccount INT (11) DEFAULT NULL,
  DateAccount DATETIME DEFAULT NULL,
  ProfileId INT (11) NOT NULL,
  AccountPage TEXT DEFAULT NULL,
  PRIMARY KEY (OrderId),
  INDEX mytrip_storeorder_FK1 USING BTREE (MoneyId),
  INDEX mytrip_storeorder_FK5 USING BTREE (ProfileId),
  CONSTRAINT mytrip_storeorder_FK5 FOREIGN KEY (ProfileId)
  REFERENCES mytrip_storeprofile (ProfileId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 9830
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeproducer
--
CREATE TABLE IF NOT EXISTS mytrip_storeproducer(
  ProducerId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Body TEXT DEFAULT NULL,
  Path VARCHAR (256) NOT NULL,
  Culture VARCHAR (50) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  CreationDate DATETIME NOT NULL,
  SaleId INT (11) NOT NULL,
  PRIMARY KEY (ProducerId),
  INDEX mytrip_storeproducer_FK1 USING BTREE (SaleId),
  CONSTRAINT mytrip_storeproducer_FK1 FOREIGN KEY (SaleId)
  REFERENCES mytrip_storesale (SaleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_usersinroles
--
CREATE TABLE IF NOT EXISTS mytrip_usersinroles(
  UserId VARCHAR (50),
  RoleId INT (11) NOT NULL,
  PRIMARY KEY (UserId, RoleId),
  INDEX FK_mytrip_usersinroles_mytrip_usersroles_RoleId USING BTREE (RoleId),
  CONSTRAINT FK_mytrip_usersinroles_mytrip_users_UserId FOREIGN KEY (UserId)
  REFERENCES mytrip_users (UserId),
  CONSTRAINT FK_mytrip_usersinroles_mytrip_usersroles_RoleId FOREIGN KEY (RoleId)
  REFERENCES mytrip_usersroles (RoleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 4096
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_usersmembership
--
CREATE TABLE IF NOT EXISTS mytrip_usersmembership(
  UserId VARCHAR (50),
  `Password` VARCHAR (100) NOT NULL,
  PasswordSalt VARCHAR (100) NOT NULL,
  Email VARCHAR (100) NOT NULL,
  IsApproved BIT (1) NOT NULL,
  CreationDate DATETIME NOT NULL,
  LastLockoutDate DATETIME NOT NULL,
  LastLoginDate DATETIME NOT NULL,
  LastPasswordChangedDate DATETIME NOT NULL,
  UserIP VARCHAR (50) NOT NULL,
  PRIMARY KEY (UserId),
  CONSTRAINT FK_mytrip_usersmembership_mytrip_users_UserId FOREIGN KEY (UserId)
  REFERENCES mytrip_users (UserId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_votesanswer
--
CREATE TABLE IF NOT EXISTS mytrip_votesanswer(
  AnswerId INT (11) NOT NULL,
  QuestionId INT (11) NOT NULL,
  Answer VARCHAR (256) NOT NULL,
  TotalVotes INT (11) NOT NULL,
  PRIMARY KEY (AnswerId),
  INDEX FK_mytrip_votesanswer_mytrip_votesquestion_QuestionId USING BTREE (QuestionId),
  CONSTRAINT FK_mytrip_votesanswer_mytrip_votesquestion_QuestionId FOREIGN KEY (QuestionId)
  REFERENCES mytrip_votesquestion (QuestionId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articlescomments
--
CREATE TABLE IF NOT EXISTS mytrip_articlescomments(
  CommentId INT (11) NOT NULL,
  ArticleId INT (11) NOT NULL,
  Body TEXT NOT NULL,
  CreateDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  UserEmail VARCHAR (100) NOT NULL,
  IsAnonym BIT (1) NOT NULL,
  IsApproved BIT (1) NOT NULL,
  PRIMARY KEY (CommentId),
  INDEX IX_mytrip_ArticleComment_mytrip_Article USING BTREE (ArticleId),
  CONSTRAINT FK_mytrip_ArticleComment_mytrip_Article FOREIGN KEY (ArticleId)
  REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articlesintags
--
CREATE TABLE IF NOT EXISTS mytrip_articlesintags(
  ArticleId INT (11) NOT NULL,
  TagId INT (11) NOT NULL,
  PRIMARY KEY (ArticleId, TagId),
  INDEX IX_mytrip_ArticlesInTags_mytrip_ArticlesTag USING BTREE (TagId),
  CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_Article FOREIGN KEY (ArticleId)
  REFERENCES mytrip_articles (ArticleId),
  CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_ArticlesTag FOREIGN KEY (TagId)
  REFERENCES mytrip_articlestag (TagId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articlessubscription
--
CREATE TABLE IF NOT EXISTS mytrip_articlessubscription(
  SubscribeId INT (11) NOT NULL,
  ArticleId INT (11) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  PRIMARY KEY (SubscribeId),
  INDEX FK_mytrip_articlessubscription_mytrip_articles_ArticleId USING BTREE (ArticleId),
  CONSTRAINT FK_mytrip_articlessubscription_mytrip_articles_ArticleId FOREIGN KEY (ArticleId)
  REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_articlesvotes
--
CREATE TABLE IF NOT EXISTS mytrip_articlesvotes(
  VotesId INT (11) NOT NULL,
  ArticleId INT (11) NOT NULL,
  Vote INT (11) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  PRIMARY KEY (VotesId),
  INDEX IX_mytrip_ArticlesVotes_mytrip_Articles USING BTREE (ArticleId),
  CONSTRAINT FK_mytrip_ArticlesVotes_mytrip_Articles FOREIGN KEY (ArticleId)
  REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeproduct
--
CREATE TABLE IF NOT EXISTS mytrip_storeproduct(
  ProductId INT (11) NOT NULL,
  DepartmentId INT (11) NOT NULL,
  ProducerId INT (11) NOT NULL,
  Title VARCHAR (256) NOT NULL,
  Path VARCHAR (256) NOT NULL,
  Details TEXT DEFAULT NULL,
  Body TEXT DEFAULT NULL,
  CreationDate DATETIME NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  Culture VARCHAR (50) NOT NULL,
  AllCulture BIT (1) NOT NULL,
  Price DECIMAL (18, 2) NOT NULL,
  TotalVotes DECIMAL (4, 2) NOT NULL,
  TotalCount INT (11) NOT NULL,
  ViewPrice BIT (1) NOT NULL,
  ViewVotes BIT (1) NOT NULL,
  ViewCount BIT (1) NOT NULL,
  UrlFile VARCHAR (256) DEFAULT NULL,
  Packing VARCHAR (256) DEFAULT NULL,
  NamberCatalog VARCHAR (256) DEFAULT NULL,
  SaleId INT (11) NOT NULL,
  MoneyId VARCHAR (50) NOT NULL,
  PRIMARY KEY (ProductId),
  INDEX IX_mytrip_storeproduct_department USING BTREE (DepartmentId),
  INDEX IX_mytrip_storeproduct_producer USING BTREE (ProducerId),
  INDEX mytrip_storeproduct_FK1 USING BTREE (SaleId),
  INDEX mytrip_storeproduct_FK2 USING BTREE (MoneyId),
  CONSTRAINT FK_mytrip_storeproduct_mytrip_storedepartment FOREIGN KEY (DepartmentId)
  REFERENCES mytrip_storedepartment (DepartmentId),
  CONSTRAINT FK_mytrip_storeproduct_mytrip_storeproducer FOREIGN KEY (ProducerId)
  REFERENCES mytrip_storeproducer (ProducerId),
  CONSTRAINT mytrip_storeproduct_FK1 FOREIGN KEY (SaleId)
  REFERENCES mytrip_storesale (SaleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 1328
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storeorderisproduct
--
CREATE TABLE IF NOT EXISTS mytrip_storeorderisproduct(
  ProductId INT (11) NOT NULL,
  Count INT (11) NOT NULL,
  OrderId INT (11) NOT NULL,
  Price DECIMAL (18, 2) NOT NULL,
  MoneyId VARCHAR (50) NOT NULL,
  PRIMARY KEY (ProductId, OrderId),
  INDEX mytrip_storeorderisproduct_FK1 USING BTREE (OrderId),
  INDEX mytrip_storeorderisproduct_FK2 USING BTREE (MoneyId),
  CONSTRAINT mytrip_storeorder_FK2 FOREIGN KEY (ProductId)
  REFERENCES mytrip_storeproduct (ProductId),
  CONSTRAINT mytrip_storeorderisproduct_FK1 FOREIGN KEY (OrderId)
  REFERENCES mytrip_storeorder (OrderId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 1489
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

--
-- Описание для таблицы mytrip_storevotes
--
CREATE TABLE IF NOT EXISTS mytrip_storevotes(
  VotesId INT (11) NOT NULL,
  ProductId INT (11) NOT NULL,
  Vote INT (11) NOT NULL,
  UserName VARCHAR (100) NOT NULL,
  Reviews TEXT DEFAULT NULL,
  CreationDate DATETIME NOT NULL,
  PRIMARY KEY (VotesId),
  INDEX IX_mytrip_storevotes USING BTREE (ProductId),
  CONSTRAINT FK_mytrip_storevotes_mytrip_storeproduct FOREIGN KEY (ProductId)
  REFERENCES mytrip_storeproduct (ProductId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 4096
CHARACTER SET cp1251
COLLATE cp1251_general_ci;

/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;

