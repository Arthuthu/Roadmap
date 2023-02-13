CREATE PROCEDURE [dbo].[spRoadmap_GetRoadmapsByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Roadmaps]
	WHERE UserId = @UserId;
END
