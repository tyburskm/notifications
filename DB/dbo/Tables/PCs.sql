﻿CREATE TABLE [dbo].[PCs] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [PcName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PCs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

