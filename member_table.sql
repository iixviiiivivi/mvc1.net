DECLARE @dbName NVARCHAR(50);
SELECT @dbName=[name] FROM sys.databases
WHERE [name] = 'WebDB';

IF @dbName IS NOT NULL
	BEGIN
		USE [master]
		ALTER DATABASE [WebDB] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
		DROP DATABASE [WebDB]
	END
GO

CREATE DATABASE WebDB;
GO

USE WebDB;
GO

DROP TABLE IF EXISTS member;
GO
		
CREATE TABLE member(
	[id] INT IDENTITY,
	[name] NVARCHAR(50),
	[username] NVARCHAR(50),
	[password] NVARCHAR(50),
	[registerDate] DATETIME DEFAULT GETDATE()

	CONSTRAINT PKid PRIMARY KEY(id)
);
GO

INSERT INTO member ([name], [username], [password])
	VALUES (N'John', N'userJohn', N'passJohn'),
		   (N'Mary', N'userMary', N'passMary'),
		   (N'Joe', N'userJoe', N'passJoe'),
		   (N'Sarah', N'userSarah', N'PassSarah'),
		   (N'Tom', N'userTom', N'passTom'),
		   (N'Louis', N'userLouis', N'passLouis'),
		   (N'Grace', N'userGrace', N'passGrace'),
		   (N'Frank', N'userFrank', N'passFrank'),
		   (N'Christine', N'userChristine', N'passChristine'),
		   (N'Tony', N'userTony', N'passTony'),
		   (N'Evelyn', N'userEvelyn', N'passEvelyn'),
		   (N'Bob', N'userBob', N'passBob'),
		   (N'Amanda', N'userAmanda', N'passAmanda'),
		   (N'Cathy', N'userCathy', N'passCathy'),
		   (N'Diana', N'userDiana', N'passDiana'),
		   (N'Alice', N'userAlice', N'passAlice'),
		   (N'Steve', N'userSteve', N'passSteve'),
		   (N'Gina', N'userGina', N'passGina'),
		   (N'George', N'userGeorge', N'passGeorge'),
		   (N'Alex', N'userAlex', N'passAlex');
GO

SELECT * FROM member;
GO