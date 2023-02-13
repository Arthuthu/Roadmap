CREATE PROCEDURE [dbo].[spRoadmapVotes_GetRoadmapVoteIdByUserAndRoadmapId]
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM RoadmapVotes
	WHERE UserId = @UserId AND RoadmapId = @RoadmapId;
END