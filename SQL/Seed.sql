USE WhizKids
GO
SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (Id, FirebaseUserId, Email, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (1, 'GC1k9Cvsbbd1aR3v0RljmeRfT2b2', 'patriciayoung@greatmail.com', 'John', 'Sanchez', '355 Main St', '(615)-553-2456', 1, 1);
INSERT INTO [UserProfile] (Id, FirebaseUserId, Email, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (2, '4u3jqWt8EYTRcb1gDfMNDsgZ9S02', 'johnsanchez@greatmail.com','Patricia', 'Young', '233 Washington St', '(615)-448-5521', 0, 1);
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Student] ON
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (1, 'Bob', 'Cool', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (2, 'Princess', 'America', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (3, 'Charkey', 'Camo', 0);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (4, 'Xyla', 'Johns', 0);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (5, 'Tilly', 'Exo', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (6, 'Swinky', 'Maine', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (7, 'Fin', 'Ragoll', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (8, 'Casper', 'Banese', 1);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (9, 'Jack', 'Selkir', 0);
INSERT INTO Student (Id, [FirstName], [LastName], Enrolled) VALUES (10, 'Binx', 'Bomb', 1);
SET IDENTITY_INSERT [Student] OFF

