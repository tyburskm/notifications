CREATE TABLE [dbo].[Groups] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [GroupName]   NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [Active]      BIT            NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

