CREATE TABLE [dbo].[Companies] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (150) NOT NULL,
    [Address] NVARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

