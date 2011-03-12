CREATE TABLE IF NOT EXISTS mytrip_users(
UserId VARCHAR(50),
UserName VARCHAR(100) NOT NULL,
LastActivityDate DATETIME NOT NULL,
PRIMARY KEY (UserId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 2340
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_usersroles(
RoleId INT(11) NOT NULL,
RoleName VARCHAR(100) NOT NULL,
PRIMARY KEY (RoleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 4096
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_usersinroles(
UserId VARCHAR(50),
RoleId INT(11) NOT NULL,
PRIMARY KEY (UserId, RoleId),
INDEX IX_mytrip_UsersInRoles_mytrip_Users (RoleId),
CONSTRAINT FK_mytrip_UsersInRoles_mytrip_Users FOREIGN KEY (UserId)
REFERENCES mytrip_users (UserId),
CONSTRAINT FK_mytrip_UsersInRoles_mytrip_Roles FOREIGN KEY (RoleId)
REFERENCES mytrip_usersroles (RoleId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_usersmembership(
UserId VARCHAR(50),
`Password` VARCHAR(100) NOT NULL,
PasswordSalt VARCHAR(100) NOT NULL,
Email VARCHAR(100) NOT NULL,
IsApproved BIT(1) NOT NULL,
CreationDate DATETIME NOT NULL,
LastLockoutDate DATETIME NOT NULL,
LastLoginDate DATETIME NOT NULL,
LastPasswordChangedDate DATETIME NOT NULL,
UserIP VARCHAR(50) NOT NULL,
PRIMARY KEY (UserId),
CONSTRAINT FK_mytrip_Membership_mytrip_Users FOREIGN KEY (UserId)
REFERENCES mytrip_users (UserId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 2340
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_corepages(
PageId INT (11) NOT NULL,
Title VARCHAR (256) DEFAULT NULL,
Path VARCHAR (256) DEFAULT NULL,
SeoTitle VARCHAR (256) DEFAULT NULL,
SeoKeyword TEXT DEFAULT NULL,
SeoDescription TEXT DEFAULT NULL,
Body TEXT DEFAULT NULL,
ViewOnlyHomePage BIT (1) NOT NULL,
SideBar BIT (1) NOT NULL,
EmailForm BIT (1) NOT NULL,
AddMenu BIT (1) NOT NULL,
AddHomePage BIT (1) NOT NULL,
Culture VARCHAR (100) NOT NULL,
AllCulture BIT (1) NOT NULL,
SubPagesId INT (11) NOT NULL,
PRIMARY KEY (PageId),
INDEX mytrip_corepages_FK1 USING BTREE (SubPagesId),
CONSTRAINT mytrip_corepages_FK1 FOREIGN KEY (SubPagesId)
REFERENCES mytrip_corepages (PageId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_geocountry(
CountryId INT (11) NOT NULL,
`Code` VARCHAR (50) NOT NULL,
`Name` VARCHAR (256) NOT NULL,
Longitude DECIMAL (18, 8) DEFAULT NULL,
Latitude DECIMAL (18, 8) DEFAULT NULL,
EditAdmin BIT (1) NOT NULL,
EditUser BIT (1) NOT NULL,
PRIMARY KEY (CountryId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_georegion(
RegionId INT (11) NOT NULL,
CountryId INT (11) NOT NULL,
`Code` VARCHAR (50) NOT NULL,
`Name` VARCHAR (256) NOT NULL,
Longitude DECIMAL (18, 8) DEFAULT NULL,
Latitude DECIMAL (18, 8) DEFAULT NULL,
EditAdmin BIT (1) NOT NULL,
EditUser BIT (1) NOT NULL,
PRIMARY KEY (RegionId),
INDEX mytrip_georegion_FK1 USING BTREE (CountryId),
CONSTRAINT mytrip_georegion_FK1 FOREIGN KEY (CountryId)
REFERENCES mytrip_geocountry (CountryId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_geocity(
CityId INT (11) NOT NULL,
RegionId INT (11) NOT NULL,
`Name` VARCHAR (256) NOT NULL,
Longitude DECIMAL (18, 8) DEFAULT NULL,
Latitude DECIMAL (18, 8) DEFAULT NULL,
EditAdmin BIT (1) NOT NULL,
EditUser BIT (1) NOT NULL,
PRIMARY KEY (CityId),
INDEX mytrip_geocity_FK1 USING BTREE (RegionId),
CONSTRAINT mytrip_geocity_FK1 FOREIGN KEY (RegionId)
REFERENCES mytrip_georegion (RegionId)
)
ENGINE = INNODB
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_corestatistic(
StatisticId INT (11) NOT NULL,
UserIP TEXT DEFAULT NULL,
`Date` DATETIME NOT NULL,
`Page` TEXT DEFAULT NULL,
Referrer TEXT DEFAULT NULL,
Browser TEXT DEFAULT NULL,
OS TEXT DEFAULT NULL,
CityId INT (11) NOT NULL,
UserCount INT (11) NOT NULL,
ViewCount INT (11) NOT NULL,
`Day` BIT (1) NOT NULL,
`Month` BIT (1) NOT NULL,
`Year` BIT (1) NOT NULL,
`Time` INT (11) DEFAULT NULL,
PRIMARY KEY (StatisticId),
INDEX mytrip_corestatistic_FK1 USING BTREE (CityId),
CONSTRAINT mytrip_corestatistic_FK1 FOREIGN KEY (CityId)
REFERENCES mytrip_geocity (CityId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_usersprofile(
UserId VARCHAR (50),
FirstName VARCHAR (256) DEFAULT NULL,
LastName VARCHAR (256) DEFAULT NULL,
Phone VARCHAR (256) DEFAULT NULL,
Longitude DECIMAL (18, 8) DEFAULT NULL,
Latitude DECIMAL (18, 8) DEFAULT NULL,
Site VARCHAR (256) DEFAULT NULL,
Description TEXT DEFAULT NULL,
icq VARCHAR (100) DEFAULT NULL,
skype VARCHAR (100) DEFAULT NULL,
CityId INT (11) NOT NULL,
ProfileClose BIT (1) NOT NULL,
PRIMARY KEY (UserId),
INDEX mytrip_usersprofile_FK1 USING BTREE (CityId),
CONSTRAINT mytrip_usersprofile_FK1 FOREIGN KEY (CityId)
REFERENCES mytrip_geocity (CityId),
CONSTRAINT mytrip_usersprofile_FK2 FOREIGN KEY (UserId)
REFERENCES mytrip_users (UserId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 16384
CHARACTER SET cp1251
COLLATE cp1251_general_ci;