CREATE TABLE [dbo].[Tasks] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (100) NULL,
    [Description] NVARCHAR (300) NULL,
    [Status]      INT            NOT NULL,
    [Priority]    INT            NOT NULL,
    [Type]        INT            NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

