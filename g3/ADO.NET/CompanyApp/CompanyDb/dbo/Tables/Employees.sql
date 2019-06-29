CREATE TABLE [dbo].[Employees] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (150) NOT NULL,
    [DateOfBirth]  DATE           NOT NULL,
    [DepartmentId] INT            NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_Departments] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id])
);

