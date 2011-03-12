CREATE TABLE IF NOT EXISTS mytrip_votesquestion(
QuestionId INT(11) NOT NULL,
Question VARCHAR(256) NOT NULL,
TotalVotes INT(11) NOT NULL,
Active BIT(10) NOT NULL,
CreateDate DATETIME NOT NULL,
CloseDate DATETIME NOT NULL,
UserName VARCHAR(100) NOT NULL,
OnlyForRegisterUser BIT(10) NOT NULL,
Culture VARCHAR(100) NOT NULL,
AllCulture BIT(10) NOT NULL,
Path VARCHAR(256) NOT NULL,
PRIMARY KEY (QuestionId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET cp1251
COLLATE cp1251_general_ci;
CREATE TABLE IF NOT EXISTS mytrip_votesanswer(
AnswerId INT(11) NOT NULL,
QuestionId INT(11) NOT NULL,
Answer VARCHAR(256) NOT NULL,
TotalVotes INT(11) NOT NULL,
PRIMARY KEY (AnswerId),
INDEX IX_mytrip_VotesAnswer_mytrip_VotesQuestion (QuestionId),
CONSTRAINT FK_mytrip_VotesAnswer_mytrip_VotesQuestion FOREIGN KEY (QuestionId)
REFERENCES mytrip_votesquestion (QuestionId)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 2048
CHARACTER SET cp1251
COLLATE cp1251_general_ci;