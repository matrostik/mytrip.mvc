CREATE TABLE IF NOT EXISTS mytrip_articlescategory(
CategoryId INT(11) NOT NULL,
Title VARCHAR(256) NOT NULL,
Path VARCHAR(256) NOT NULL,
CreateDate DATETIME NOT NULL,
UserName VARCHAR(100) NOT NULL,
SeoTitle VARCHAR(256) NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
UserEmail VARCHAR(100) NOT NULL,
SeparateBlock BIT(1) NOT NULL,
Blog BIT(1) NOT NULL,
Views INT(11) NOT NULL,
SubCategoryId INT(11) NOT NULL,
Culture VARCHAR(100) NOT NULL,
AllCulture BIT(1) NOT NULL,
PRIMARY KEY (CategoryId),
INDEX IX_mytrip_ArticleCategory_mytrip_ArticleCategory (SubCategoryId),
CONSTRAINT FK_mytrip_ArticleCategory_mytrip_ArticleCategory FOREIGN KEY (SubCategoryId)
REFERENCES mytrip_articlescategory (CategoryId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 2048
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articlestag(
TagId INT(11) NOT NULL,
TagName VARCHAR(256) NOT NULL,
Path VARCHAR(256) NOT NULL,
PRIMARY KEY (TagId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articles(
ArticleId INT(11) NOT NULL,
SubArticleId INT(11) NOT NULL,
CategoryId INT(11) NOT NULL,
Title VARCHAR(256) NOT NULL,
Abstract TEXT DEFAULT NULL,
Body TEXT NOT NULL,
CreateDate DATETIME NOT NULL,
UserName VARCHAR(100) NOT NULL,
Views INT(11) NOT NULL,
ApprovedComment BIT(1) NOT NULL,
SeoTitle VARCHAR(256) NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
IncludeAnonymComment BIT(1) NOT NULL,
ImageForAbstract VARCHAR(256) DEFAULT NULL,
Path VARCHAR(256) NOT NULL,
OnlyForRegisterUser BIT(1) NOT NULL,
ApprovedVotes BIT(1) NOT NULL,
CloseDate DATETIME NOT NULL,
Culture VARCHAR(100) NOT NULL,
AllCulture BIT(1) NOT NULL,
TotalVotes DECIMAL (4, 2) NOT NULL,
ModerateComments BIT(1) NOT NULL,
CommentVotes BIT(1) NOT NULL,
PRIMARY KEY (ArticleId),
INDEX IX_mytrip_articles_mytrip_articles (SubArticleId),
INDEX IX_mytrip_Article_mytrip_ArticleCategory (CategoryId),
CONSTRAINT FK_mytrip_Article_mytrip_ArticleCategory FOREIGN KEY (CategoryId)
REFERENCES mytrip_articlescategory (CategoryId),
CONSTRAINT FK_mytrip_articles_mytrip_articles FOREIGN KEY (SubArticleId)
REFERENCES mytrip_articles(ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 1365
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articlescomments(
CommentId INT(11) NOT NULL,
ArticleId INT(11) NOT NULL,
Body TEXT NOT NULL,
CreateDate DATETIME NOT NULL,
UserName VARCHAR(100) NOT NULL,
UserEmail VARCHAR(100) NOT NULL,
IsAnonym BIT(1) NOT NULL,
IsApproved BIT(1) NOT NULL,
Votes INT(11) NOT NULL,
PRIMARY KEY (CommentId),
INDEX IX_mytrip_ArticleComment_mytrip_Article (ArticleId),
CONSTRAINT FK_mytrip_ArticleComment_mytrip_Article FOREIGN KEY (ArticleId)
REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 5461
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articlesintags(
ArticleId INT(11) NOT NULL,
TagId INT(11) NOT NULL,
PRIMARY KEY (ArticleId, TagId),
INDEX IX_mytrip_ArticlesInTags_mytrip_ArticlesTag (TagId),
CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_Article FOREIGN KEY (ArticleId)
REFERENCES mytrip_articles (ArticleId),
CONSTRAINT FK_mytrip_ArticlesInTags_mytrip_ArticlesTag FOREIGN KEY (TagId)
REFERENCES mytrip_articlestag (TagId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articlesvotes(
VotesId INT(11) NOT NULL,
ArticleId INT(11) NOT NULL,
Vote INT(11) NOT NULL,
UserName VARCHAR(100) NOT NULL,
PRIMARY KEY (VotesId),
INDEX IX_mytrip_ArticlesVotes_mytrip_Articles (ArticleId),
CONSTRAINT FK_mytrip_ArticlesVotes_mytrip_Articles FOREIGN KEY (ArticleId)
REFERENCES mytrip_articles (ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_articlessubscription (
SubscribeId INT(11) NOT NULL,
ArticleId INT(11) NOT NULL,
UserName VARCHAR(100) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
PRIMARY KEY (SubscribeId),
INDEX FK_mytrip_articlessubscription_mytrip_articles_ArticleId (ArticleId),
CONSTRAINT FK_mytrip_articlessubscription_mytrip_articles_ArticleId FOREIGN KEY (ArticleId)
REFERENCES mytrip_articles(ArticleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_commentvotes (
Id INT(11) NOT NULL,
CommentId INT(11) NOT NULL,
UserName VARCHAR(100) NOT NULL,
PRIMARY KEY (Id),
INDEX FK_mytrip_commentvotes_mytrip_articlescomments_CommentId (CommentId),
CONSTRAINT FK_mytrip_commentvotes_mytrip_articlescomments_CommentId FOREIGN KEY (CommentId)
REFERENCES mytrip_articlescomments(CommentId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;