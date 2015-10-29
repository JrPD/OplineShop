CREATE TABLE [dbo].[Images] (
    [Img_Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Img_Path] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Images] PRIMARY KEY CLUSTERED ([Img_Id] ASC)
);

