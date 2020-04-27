CREATE TABLE [dbo].[PcInGroup] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [GroupId] INT NOT NULL,
    [PCId]    INT NOT NULL,
    CONSTRAINT [PK_PcInGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PcInGroup_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_PcInGroup_PCs] FOREIGN KEY ([PCId]) REFERENCES [dbo].[PCs] ([Id])
);

