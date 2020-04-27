CREATE TABLE [dbo].[Notifications] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (200) NOT NULL,
    [ValidFrom] DATETIME       CONSTRAINT [DF_Notifications_ValidFrom] DEFAULT (getdate()) NOT NULL,
    [ValidTo]   DATETIME       CONSTRAINT [DF_Notifications_ValidTo] DEFAULT (dateadd(hour,(1),getdate())) NOT NULL,
    [Repeat]    INT            CONSTRAINT [DF_Notifications_Repeat] DEFAULT ((0)) NOT NULL,
    [RunAtTime] DATETIME       NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

