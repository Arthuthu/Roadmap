CREATE PROCEDURE [dbo].[spRoadmapVotes_Add]
	@Id uniqueidentifier,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[RoadmapVotes] (Id, UserId, RoadmapId)
	VALUES (@Id, @UserId, @RoadmapId);
END
