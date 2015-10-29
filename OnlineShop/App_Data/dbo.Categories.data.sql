SET IDENTITY_INSERT [dbo].Images ON
INSERT INTO [dbo].Images([Img_Id],[Img_Path] ) VALUES (1, 'path')
SET IDENTITY_INSERT [dbo].Images off


SET IDENTITY_INSERT [dbo].[Categories] ON
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (3, 1, -1, N'1_1', 1, 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (5, 1, -1, N'2_1', 1, 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (6, 2, 3, N'1_2', 1, 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (7, 2, 5, N'2_2', 1, 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (8, 3, 6, N'1_3', 0, 1)
INSERT INTO [dbo].[Categories] ([Cat_Id], [Cat_Level], [Cat_Parent_Cat_Id], [Cat_Name], [Cat_HasChild], [Image_Img_Id]) VALUES (9, 3, 7, N'2_3', 0, 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF

