SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (1, 'HGIYeMTYpqfdwXpfSw2AUvxnvsC3', 'John', 'Sanchez', '355 Main St', '(615)-553-2456', 1, 1);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (2, 'vhbgqyeqelhgkohutnoglbdohssl', 'Patricia', 'Young', '233 Washington St', '(615)-448-5521', 0, 2);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (3, 'wqhvgdjxjqkqecuridpvjtwpoacc', 'Robert', 'Brown', '145 Sixth Ave', '(615)-323-7711', 0, 3);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (4, 'exsjcqvnhydjofznqmtvecekcgno', 'Jennifer', 'Wilson', '495 Cedar Rd', '(615)-919-8944', 1, 4);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (5, 'djwoicosfnhexpmmsnukgcsbnqod', 'Michael', 'Moore', '88 Oak St', '(615)-556-7273', 1, 5);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (6, 'xiybslspeizewvkixqubnqjlwamz', 'Linda', 'Green', '53 Lake Cir', '(615)-339-4488', 0, 6);
INSERT INTO [UserProfile] (Id, FirebaseUserId, [FirstName], [LastName], [Address], PhoneNumber, IsAdmin, StudentId) VALUES (7, 'lzxmysyzqrmcwjzxsyrkbczhspup', 'William', 'Anderson', '223 Hill St', '(615)-232-6768', 1, 7);
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

