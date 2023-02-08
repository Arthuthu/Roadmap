CREATE PROCEDURE [dbo].[spRoadmapVotes_GetRoadmapVoteIdByUserAndRoadmapId]
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM RoadmapVotes
	WHERE RoadmapVotes.UserId = @UserId AND RoadmapVotes.RoadmapId = RoadmapId;
END