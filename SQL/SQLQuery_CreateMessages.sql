USE WhizKids
CREATE TABLE [Messages] (
    [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
	[StudentId] INTEGER  NOT NULL,
	[UserProfileId] INTEGER NOT NULL,
	[CreateDateTime] DATETIME NOT NULL,
    [Body] varchar(55) NOT NULL,
	CONSTRAINT FK_Messages_UserProfile FOREIGN KEY (UserProfileId) REFERENCES [UserProfile](Id) ON DELETE CASCADE,
	CONSTRAINT FK_Messages_Student FOREIGN KEY (StudentId) REFERENCES [Student](Id) ON DELETE CASCADE
);