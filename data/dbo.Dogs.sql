CREATE TABLE [dbo].[Dogs] (
    [id]       INT            NOT NULL IDENTITY,
    [name]     NVARCHAR (256) NOT NULL,
    [legs]     INT            DEFAULT ((4)) NOT NULL,
    [has_tail] BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Dogs_name]
    ON [dbo].[Dogs]([name] ASC);

