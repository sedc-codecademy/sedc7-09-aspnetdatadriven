CREATE TABLE [dbo].[Authors] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [DateOfBirth] DATE           NOT NULL,
    [DateOfDeath] DATE           NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Authors]
    ON [dbo].[Authors]([Name] ASC);

