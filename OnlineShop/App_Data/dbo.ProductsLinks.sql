CREATE TABLE [dbo].[ProductsLinks] (
    [Pr_Id]   BIGINT NOT NULL,
    [Link_Id] BIGINT NOT NULL,
    CONSTRAINT [PK_dboProductsLinks] PRIMARY KEY CLUSTERED ([Pr_Id] ASC, [Link_Id] ASC),
    CONSTRAINT [FK_dboProductsLinks_dbo.Products_Pr_Id] FOREIGN KEY ([Pr_Id]) REFERENCES [dbo].[Products] ([Pr_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dboProductsLinks_dboLinks_Link_Id] FOREIGN KEY ([Link_Id]) REFERENCES [dbo].[Links] ([Link_Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Id]
    ON [dbo].[ProductsLinks]([Pr_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Link_Id]
    ON [dbo].[ProductsLinks]([Link_Id] ASC);

