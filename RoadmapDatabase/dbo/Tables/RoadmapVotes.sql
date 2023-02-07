CREATE TABLE [dbo].[RoadmapVotes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RoadmapId] UNIQUEIDENTIFIER NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_RoadmapVotes_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_RoadmapVotes_RoadmapId] FOREIGN KEY ([RoadmapId]) REFERENCES [Roadmaps]([Id]),
    UNIQUE (RoadmapId, UserId)
)
