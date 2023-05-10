CREATE PROCEDURE [dbo].[spRoadmapVotes_GetAll]
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[RoadmapVotes]
	WHERE UserId = @UserId AND RoadmapId = @RoadmapId
END