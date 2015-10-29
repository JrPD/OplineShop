CREATE TABLE [dbo].[ProductsProperties] (
    [Pr_Id]   BIGINT NOT NULL,
    [Prop_Id] BIGINT NOT NULL,
    CONSTRAINT [PK_dboProductsProperties] PRIMARY KEY CLUSTERED ([Pr_Id] ASC, [Prop_Id] ASC),
    CONSTRAINT [FK_dboProductsProperties_dbo.Products_Pr_Id] FOREIGN KEY ([Pr_Id]) REFERENCES [dbo].[Products] ([Pr_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dboProductsProperties_dboProperties_Prop_Id] FOREIGN KEY ([Prop_Id]) REFERENCES [dbo].[Properties] ([Prop_Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Id]
    ON [dbo].[ProductsProperties]([Pr_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Link_Id]
    ON [dbo].[ProductsProperties]([Prop_Id] ASC);

