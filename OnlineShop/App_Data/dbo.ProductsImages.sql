CREATE TABLE [dbo].[ProductsImages] (
    [Pr_Id]  BIGINT NOT NULL,
    [Img_Id] BIGINT NOT NULL,
    CONSTRAINT [PK_dbo.ProductsImages] PRIMARY KEY CLUSTERED ([Pr_Id] ASC, [Img_Id] ASC),
    CONSTRAINT [FK_dbo.ProductsImages_dbo.Products_Pr_Id] FOREIGN KEY ([Pr_Id]) REFERENCES [dbo].[Products] ([Pr_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ProductsImages_dbo.Images_Img_Id] FOREIGN KEY ([Img_Id]) REFERENCES [dbo].[Images] ([Img_Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Pr_Id]
    ON [dbo].[ProductsImages]([Pr_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Img_Id]
    ON [dbo].[ProductsImages]([Img_Id] ASC);

