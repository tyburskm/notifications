CREATE TABLE [dbo].[NotificationInGroup] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [NotificationId] INT NOT NULL,
    [GroupId]        INT NOT NULL,
    CONSTRAINT [PK_NotificationInGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NotificationInGroup_Groups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_NotificationInGroup_Notifications] FOREIGN KEY ([NotificationId]) REFERENCES [dbo].[Notifications] ([Id])
);

