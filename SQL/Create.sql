﻿USE MASTER
GO

IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = N'WhizKids'
)
CREATE DATABASE WhizKids
GO

USE WhizKids
GO


DROP TABLE IF EXISTS [Messages];
DROP TABLE IF EXISTS UserStudent;
DROP TABLE IF EXISTS Student;
DROP TABLE IF EXISTS [UserProfile];

CREATE TABLE [UserProfile] (
    [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
	FirebaseUserId VARCHAR(28) NOT NULL,
	[FirstName] VARCHAR(55) NOT NULL,
	[LastName] VARCHAR(55) NOT NULL,
	[Address] VARCHAR(255) NOT NULL,
	PhoneNumber VARCHAR(55) NOT NULL,
	IsAdmin INTEGER NOT NULL,
    CONSTRAINT UQ_FirebaseUserId UNIQUE(FirebaseUserId)
);

CREATE TABLE Student (
    [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
    [FirstName] VARCHAR(55) NOT NULL,
	[LastName] VARCHAR(55) NOT NULL,
    Enrolled INTEGER NOT NULL,
);

CREATE TABLE UserStudent (
    [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
	UserProfileId INTEGER NOT NULL,
	StudentId INTEGER NOT NULL,
	CONSTRAINT FK_UserStudent_UserProfile FOREIGN KEY (UserProfileId) REFERENCES [UserProfile](Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserStudent_Student FOREIGN KEY (StudentId) REFERENCES [Student](Id) ON DELETE CASCADE
);


CREATE TABLE [Messages] (
    [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
	[StudentId] INTEGER NOT NULL,
	[CreateDateTime] DATETIME NOT NULL,
    [Message] nvarchar(255) NOT NULL,
	CONSTRAINT FK_Messages_UserStudent FOREIGN KEY (StudentId) REFERENCES [UserStudent](Id),
);