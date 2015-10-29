CREATE TABLE [dbo].[Carts] (
    [Cart_Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Cart_Pr_Id]        BIGINT         NOT NULL,
    [Cart_Count]        TINYINT        NOT NULL,
    [Cart_DataCreation] DATETIME       NULL,
    [User]              NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Carts] PRIMARY KEY CLUSTERED ([Cart_Id] ASC)
);

