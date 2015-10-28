SET IDENTITY_INSERT [dbo].[webpages_Roles] ON
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (9, N'admin')
INSERT INTO [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (10, N'user')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF

SET IDENTITY_INSERT [dbo].[Organizations] ON
INSERT INTO [dbo].[Organizations] ([Id], [Name]) VALUES (453, N'FLS organization')
INSERT INTO [dbo].[Organizations] ([Id], [Name]) VALUES (454, N'Test organization')
INSERT INTO [dbo].[Organizations] ([Id], [Name]) VALUES (455, N'Some organization')
SET IDENTITY_INSERT [dbo].[Organizations] OFF

SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (305, 453, N'QA dep')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (306, 453, N'DEV dep')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (307, 453, N'HR dep')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (308, 455, N'RD dep')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (309, 454, N'department 1')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (310, 454, N'department 2')
INSERT INTO [dbo].[Departments] ([Id], [OrganizationId], [Name]) VALUES (311, 454, N'department 3')
SET IDENTITY_INSERT [dbo].[Departments] OFF

SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (179, NULL, N'admin')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (180, 306, N'Zuev')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (181, 310, N'Petrov')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (182, 306, N'Sidorov')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (183, 305, N'Zaycev')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (184, 307, N'Astahova')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (185, 307, N'Parinov')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (186, 307, N'Karpova')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (187, 309, N'Pankratov')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (188, 306, N'Stalin')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (189, NULL, N'Lenin')
INSERT INTO [dbo].[Users] ([Id], [DepartmentId], [Name]) VALUES (190, 309, N'Marks')
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (179, N'2015-10-28 19:41:45', NULL, 1, NULL, 0, N'AM2xS3fU+1DSf8WQWe/qEdpXBrynutT4DoSae3ROliyUR+SSY+hghXltRN+tiU35kA==', N'2015-10-28 19:41:45', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (180, N'2015-10-28 19:43:42', NULL, 1, NULL, 0, N'AGXDze1ICpxazzVi/cT/EqAf1oydJgMe/f8GFKndCaO1sGKTtXRhD+afIcaVfJAUhA==', N'2015-10-28 19:43:42', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (181, N'2015-10-28 19:43:59', NULL, 1, NULL, 0, N'AEOxD8F+YibG0a2CQ1dwQNZ+3hUlcbBe6Ob5HMMEfQF785m0rDYSz07kBZSd7OEfCA==', N'2015-10-28 19:43:59', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (182, N'2015-10-28 19:44:08', NULL, 1, NULL, 0, N'AHiUzSbjUfifTAhaXXJ22eRt5G9SxkRZZ7grzi+4k/kuNesEkB1s0TL11LVXUI/RmQ==', N'2015-10-28 19:44:08', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (183, N'2015-10-28 19:44:20', NULL, 1, NULL, 0, N'AIxQtwFEjDVhaQOouiB+4LTJtVOwZx9gA37zPuNr/NrMlpJVAwlzd/JU1gvOXsYX5A==', N'2015-10-28 19:44:20', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (184, N'2015-10-28 19:44:29', NULL, 1, NULL, 0, N'ANrD/CmhGSk3ITZLcUvFImSn5KJEDjulKZHklpjZS1cIRk0driB9ZK/kZ09Ik8bCSA==', N'2015-10-28 19:44:29', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (185, N'2015-10-28 19:44:39', NULL, 1, NULL, 0, N'AKl6aTC7yKRX6yEUtWHgxqEwFx+R3GeDUS5hkJnR2PH87op9ph6F6jkphoWmugYIww==', N'2015-10-28 19:44:39', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (186, N'2015-10-28 19:44:58', NULL, 1, NULL, 0, N'AMIBhuV2mDqHOUIZwWcmafPBuN7lySk0r6adbSg5A+KY/bAPf5H2nSAIjNP58QDR8g==', N'2015-10-28 19:44:58', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (187, N'2015-10-28 19:45:19', NULL, 1, NULL, 0, N'AMnSsKqbG4m+VGEscTwJMoWMLypbS/8TJRH8QJWnAOBNKCUD548ud+9a62Ud9+EZzQ==', N'2015-10-28 19:45:19', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (188, N'2015-10-28 19:45:27', NULL, 1, NULL, 0, N'AARl0mxASPRNvuj1ixNnxBqIpsqzmzic8URd4fSAoxFKXRo4lRW3fMnmmnIlhvn6Yw==', N'2015-10-28 19:45:27', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (189, N'2015-10-28 19:45:35', NULL, 1, NULL, 0, N'ALAOp8QBDq1jn97Ml1UC1fUcmcHQBZ5O6Wbar7P1/1VI4X7l1mTElsURzTsELgiA6g==', N'2015-10-28 19:45:35', N'', NULL, NULL)
INSERT INTO [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (190, N'2015-10-28 19:45:43', NULL, 1, NULL, 0, N'ACjJXXTsbMy1mXQnaYBAEBqZbUrGJsKzRNHlQGaNB2wFW+08Z8dgN3VKRYuuReBmUQ==', N'2015-10-28 19:45:43', N'', NULL, NULL)

INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (179, 9)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (180, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (181, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (182, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (183, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (184, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (185, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (186, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (187, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (188, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (189, 10)
INSERT INTO [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (190, 10)
