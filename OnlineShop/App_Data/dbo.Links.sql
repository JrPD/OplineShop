CREATE TABLE [dbo].[Links] (
    [Link_Id]   BIGINT         NOT NULL,
    [Link_Name] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_dbo.Links] PRIMARY KEY CLUSTERED ([Link_Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Pr_Name_UN]
    ON [dbo].[Links]([Link_Name] ASC);

