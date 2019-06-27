CREATE TABLE [dbo].[Novels] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (150) NOT NULL,
    [AuthorID] INT            NOT NULL,
    [IsRead]   BIT            NULL,
    CONSTRAINT [PK_HugoNovels] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HugoNovels_Authors] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Authors] ([ID])
);

