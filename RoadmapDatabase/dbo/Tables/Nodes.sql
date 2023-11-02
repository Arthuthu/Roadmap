﻿CREATE TABLE [dbo].[Nodes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(2000) NULL, 
    [RoadmapId] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [UpdatedDate] DATETIME2 NULL, 
    CONSTRAINT [FK_Nodes_ToTable] FOREIGN KEY ([RoadmapId]) REFERENCES [Roadmaps]([Id]) ON DELETE CASCADE
)
