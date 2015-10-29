CREATE TABLE [dbo].[Descriptions] (
    [Desc_Id]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Desc_Path] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Descriptions] PRIMARY KEY CLUSTERED ([Desc_Id] ASC)
);

