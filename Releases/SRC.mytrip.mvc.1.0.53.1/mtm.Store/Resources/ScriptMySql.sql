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
CREATE TABLE IF NOT EXISTS mytrip_storedepartment(
DepartmentId INT (11) NOT NULL,
Title VARCHAR (256) NOT NULL,
Path VARCHAR (256) NOT NULL,
SeoTitle VARCHAR (256) DEFAULT NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
Culture VARCHAR (50) NOT NULL,
AllCulture BIT (1) NOT NULL,
UserName VARCHAR (100) NOT NULL,
CreationDate DATETIME NOT NULL,
Body TEXT DEFAULT NULL,
SubDepartmentId INT (11) NOT NULL,
SaleId INT (11) NOT NULL,
PRIMARY KEY (DepartmentId),
INDEX IX_mytrip_storedepartment (SubDepartmentId),
INDEX mytrip_storedepartment_FK1 (SaleId),
CONSTRAINT mytrip_storedepartment_FK1 FOREIGN KEY (SaleId)
REFERENCES mytrip_storesale (SaleId),
CONSTRAINT mytrip_storedepartment_FK2 FOREIGN KEY (SubDepartmentId)
REFERENCES mytrip_storedepartment (DepartmentId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_storeorder(
OrderId INT (11) NOT NULL,
`Status` INT (11) NOT NULL,
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
INDEX mytrip_storeorder_FK1 (MoneyId),
INDEX mytrip_storeorder_FK5 (ProfileId),
CONSTRAINT mytrip_storeorder_FK5 FOREIGN KEY (ProfileId)
REFERENCES mytrip_storeprofile (ProfileId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 9830
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_storeproducer(
ProducerId INT (11) NOT NULL,
Title VARCHAR (256) NOT NULL,
SeoTitle VARCHAR (256) DEFAULT NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
Body TEXT DEFAULT NULL,
Path VARCHAR (256) NOT NULL,
Culture VARCHAR (50) NOT NULL,
AllCulture BIT (1) NOT NULL,
UserName VARCHAR (100) NOT NULL,
CreationDate DATETIME NOT NULL,
SaleId INT (11) NOT NULL,
PRIMARY KEY (ProducerId),
INDEX mytrip_storeproducer_FK1 (SaleId),
CONSTRAINT mytrip_storeproducer_FK1 FOREIGN KEY (SaleId)
REFERENCES mytrip_storesale (SaleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_storeproduct(
ProductId INT (11) NOT NULL,
DepartmentId INT (11) NOT NULL,
ProducerId INT (11) NOT NULL,
Title VARCHAR (256) NOT NULL,
Path VARCHAR (256) NOT NULL,
SeoTitle VARCHAR (256) DEFAULT NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
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
INDEX IX_mytrip_storeproduct_department (DepartmentId),
INDEX IX_mytrip_storeproduct_producer (ProducerId),
INDEX mytrip_storeproduct_FK1 (SaleId),
INDEX mytrip_storeproduct_FK2 (MoneyId),
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
CREATE TABLE IF NOT EXISTS mytrip_storeorderisproduct(
ProductId INT (11) NOT NULL,
Count INT (11) NOT NULL,
OrderId INT (11) NOT NULL,
Price DECIMAL (18, 2) NOT NULL,
MoneyId VARCHAR (50) NOT NULL,
PRIMARY KEY (ProductId, OrderId),
INDEX mytrip_storeorderisproduct_FK1 (OrderId),
INDEX mytrip_storeorderisproduct_FK2 (MoneyId),
CONSTRAINT mytrip_storeorder_FK2 FOREIGN KEY (ProductId)
REFERENCES mytrip_storeproduct (ProductId),
CONSTRAINT mytrip_storeorderisproduct_FK1 FOREIGN KEY (OrderId)
REFERENCES mytrip_storeorder (OrderId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 1489
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_storevotes(
VotesId INT (11) NOT NULL,
ProductId INT (11) NOT NULL,
Vote INT (11) NOT NULL,
UserName VARCHAR (100) NOT NULL,
Reviews TEXT DEFAULT NULL,
CreationDate DATETIME NOT NULL,
PRIMARY KEY (VotesId),
INDEX IX_mytrip_storevotes (ProductId),
CONSTRAINT FK_mytrip_storevotes_mytrip_storeproduct FOREIGN KEY (ProductId)
REFERENCES mytrip_storeproduct (ProductId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 4096
CHARACTER SET cp1251
COLLATE cp1251_general_ci;   