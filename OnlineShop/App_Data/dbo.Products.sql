CREATE TABLE [dbo].[Products] (
    [Pr_Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Pr_Cat_Id]      BIGINT         NOT NULL,
    [Pr_Descr_Id]    BIGINT         NULL,
    [Pr_Name]        NVARCHAR (200) NOT NULL,
    [Pr_Price]       FLOAT (53)     NOT NULL,
    [Pr_IsAvailable] BIT            NOT NULL,
    [Pr_Count]       INT            NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Pr_Id] ASC),
    CONSTRAINT [FK_dbo.Products_dbo.Categories_Pr_Cat_Id] FOREIGN KEY ([Pr_Cat_Id]) REFERENCES [dbo].[Categories] ([Cat_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Products_dbo.Descriptions_Pr_Descr_Id] FOREIGN KEY ([Pr_Descr_Id]) REFERENCES [dbo].[Descriptions] ([Desc_Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Cat_Id]
    ON [dbo].[Products]([Pr_Cat_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Descr_Id]
    ON [dbo].[Products]([Pr_Descr_Id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Pr_Name_UN]
    ON [dbo].[Products]([Pr_Name] ASC);

