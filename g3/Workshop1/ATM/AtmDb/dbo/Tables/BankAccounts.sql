CREATE TABLE [dbo].[BankAccounts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Number]    NVARCHAR (50) NOT NULL,
    [Pin]       NVARCHAR (4)  NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Balance]   INT           NOT NULL,
    CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);

