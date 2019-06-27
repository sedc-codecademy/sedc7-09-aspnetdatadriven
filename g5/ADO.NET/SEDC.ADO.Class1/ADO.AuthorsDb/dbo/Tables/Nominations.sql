CREATE TABLE [dbo].[Nominations] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [BookID]        INT NOT NULL,
    [AwardID]       INT NOT NULL,
    [YearNominated] INT NULL,
    [IsWinner]      BIT NULL,
    CONSTRAINT [PK_Nominations] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Nominations_Awards] FOREIGN KEY ([AwardID]) REFERENCES [dbo].[Awards] ([ID]),
    CONSTRAINT [FK_Nominations_Novels] FOREIGN KEY ([BookID]) REFERENCES [dbo].[Novels] ([ID])
);

