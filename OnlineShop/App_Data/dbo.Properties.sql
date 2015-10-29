CREATE TABLE [dbo].[Properties] (
    [Prop_Id]      BIGINT         NOT NULL,
    [Prop_Name]    NVARCHAR (200) NOT NULL,
    [Prop_Link_Id] BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.Propertes] PRIMARY KEY CLUSTERED ([Prop_Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Prop_Link_Id]
    ON [dbo].[Properties]([Prop_Link_Id] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Prop_Name_UN]
    ON [dbo].[Properties]([Prop_Name] ASC);

