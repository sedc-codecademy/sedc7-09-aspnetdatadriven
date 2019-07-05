CREATE TABLE [dbo].[SubTask] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (MAX) NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Status]       INT            NOT NULL,
    [ParentTaskId] INT            NULL,
    CONSTRAINT [PK_SubTask] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubTask_Tasks_ParentTaskId] FOREIGN KEY ([ParentTaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SubTask_ParentTaskId]
    ON [dbo].[SubTask]([ParentTaskId] ASC);

