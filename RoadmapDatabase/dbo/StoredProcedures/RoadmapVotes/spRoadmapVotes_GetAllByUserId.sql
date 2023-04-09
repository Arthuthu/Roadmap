CREATE PROCEDURE [dbo].[spRoadmapVotes_GetAllByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SELECT * FROM [RoadmapVotes]
	WHERE UserId = @UserId
END
