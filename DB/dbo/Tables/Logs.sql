CREATE TABLE [dbo].[Logs] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Source]      NVARCHAR (50)  NOT NULL,
    [Message]     NVARCHAR (MAX) NOT NULL,
    [Type]        NVARCHAR (20)  NOT NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_Logs_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

