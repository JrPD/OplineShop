CREATE TABLE [dbo].[ProductsCarts] (
    [Pr_Id]   BIGINT NOT NULL,
    [Cart_Id] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.ProductsCarts] PRIMARY KEY CLUSTERED ([Pr_Id] ASC, [Cart_Id] ASC),
    CONSTRAINT [FK_dbo.ProductsCarts_dbo.Products_Pr_Id] FOREIGN KEY ([Pr_Id]) REFERENCES [dbo].[Products] ([Pr_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ProductsCarts_dbo.Carts_Cart_Id] FOREIGN KEY ([Cart_Id]) REFERENCES [dbo].[Carts] ([Cart_Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Id]
    ON [dbo].[ProductsCarts]([Pr_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Cart_Id]
    ON [dbo].[ProductsCarts]([Cart_Id] ASC);

