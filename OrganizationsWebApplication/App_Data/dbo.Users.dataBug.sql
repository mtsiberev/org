SET IDENTITY_INSERT [dbo].[webpages_Roles] ON
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (9, N'admin')
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (10, N'user')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (179, NULL, N'admin')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (1, NULL, N'1')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (11, NULL, N'11')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (111, NULL, N'111')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (1111, NULL, N'1111')
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (179, 9)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (11, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (111, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1111, 10)

INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (179, N'2015-10-28 19:41:45', NULL, 1, NULL, 0, N'AM2xS3fU+1DSf8WQWe/qEdpXBrynutT4DoSae3ROliyUR+SSY+hghXltRN+tiU35kA==', N'2015-10-28 19:41:45', N'', NULL, NULL)
