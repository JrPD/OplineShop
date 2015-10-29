CREATE TABLE [dbo].[Categories] (
    [Cat_Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [Cat_Level]         TINYINT        NOT NULL,
    [Cat_Parent_Cat_Id] BIGINT         NOT NULL,
    [Cat_Name]          NVARCHAR (200) NOT NULL,
    [Cat_HasChild]      BIT            NOT NULL,
    [Cat_Img_Id]        BIGINT         NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED ([Cat_Id] ASC),
    CONSTRAINT [FK_Categories_Image] FOREIGN KEY ([Cat_Img_Id]) REFERENCES [dbo].[Images] ([Img_Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Cat_Name_UN]
    ON [dbo].[Categories]([Cat_Name] ASC);

