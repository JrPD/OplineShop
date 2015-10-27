SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (3, 1, -1, N'1_1', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (5, 1, -1, N'2_1', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (6, 1, -1, N'3_1', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (7, 2, 3, N'1_2', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (8, 2, 3, N'2_2', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (9, 2, 3, N'3_2', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (10, 2, 5, N'4_2', 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [IsAvailable]) VALUES (11, 3, 7, N'1_3', 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
