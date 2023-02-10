CREATE PROCEDURE [dbo].[spRoadmapVotes_GetRoadmapVotesByRoadmapId]
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM RoadmapVotes
	WHERE RoadmapId = @RoadmapId
END
