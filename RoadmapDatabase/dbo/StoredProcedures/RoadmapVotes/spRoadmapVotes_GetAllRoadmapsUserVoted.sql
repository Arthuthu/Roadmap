CREATE PROCEDURE [dbo].[spRoadmapVotes_GetAllRoadmapsUserVoted]
	@UserId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[RoadmapVotes]
	WHERE UserId = @UserId;
END