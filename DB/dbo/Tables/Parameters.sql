CREATE TABLE [dbo].[Parameters] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [NotificationId]   INT            NOT NULL,
    [Title]            NVARCHAR (200) CONSTRAINT [DF_Parameters_Title] DEFAULT (N'Info') NOT NULL,
    [NotificationText] NVARCHAR (MAX) CONSTRAINT [DF_Parameters_NotificationText] DEFAULT (N'') NOT NULL,
    [BackgroundColor]  NVARCHAR (50)  NULL,
    [BackgroundImage]  NVARCHAR (MAX) NULL,
    [TextColor]        NVARCHAR (50)  NULL,
    [Maximized]        BIT            NULL,
    [Width]            INT            NULL,
    [Height]           INT            NULL,
    [Autosize]         BIT            NULL,
    [Gradient]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Parameters_Notifications] FOREIGN KEY ([NotificationId]) REFERENCES [dbo].[Notifications] ([Id])
);

