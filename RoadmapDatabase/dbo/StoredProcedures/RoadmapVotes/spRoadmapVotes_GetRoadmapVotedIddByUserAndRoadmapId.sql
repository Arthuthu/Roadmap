CREATE PROCEDURE [dbo].[spRoadmapVotes_GetRoadmapVotedIdByUserAndRoadmapId]
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM RoadmapVotes
	WHERE UserId = @UserId AND RoadmapId = @RoadmapId;
END