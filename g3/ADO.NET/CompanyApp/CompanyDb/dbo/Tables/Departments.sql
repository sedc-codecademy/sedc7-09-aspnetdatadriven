CREATE TABLE [dbo].[Departments] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (150) NOT NULL,
    [CompanyId] INT            NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Departments_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id])
);

