CREATE TABLE [dbo].[Transactions] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [BankAccountId] INT NOT NULL,
    [MoneyWithdraw] INT NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transactions_BankAccounts] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[BankAccounts] ([Id])
);

