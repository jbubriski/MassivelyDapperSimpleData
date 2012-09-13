CREATE TABLE [dbo].[Posts] (
    [Id]           INT            NOT NULL IDENTITY(1, 1),
    [DateCreated]  DATETIME       NOT NULL,
    [DateModified] DATETIME       NOT NULL,
    [Title]        NVARCHAR (200) NOT NULL,
    [Content]      NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

