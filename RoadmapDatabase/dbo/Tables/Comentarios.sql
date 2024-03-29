﻿CREATE TABLE [dbo].[Comentarios]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Description] NVARCHAR(1000) NOT NULL, 
    [AuthorName] nvarchar(50) NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoadmapId] UNIQUEIDENTIFIER NULL,
    [CreatedDate] DATETIME2 NULL, 
    [UpdatedDate] DATETIME2 NULL,
    FOREIGN KEY (RoadmapId) REFERENCES Roadmaps(Id) ON DELETE CASCADE,

)
