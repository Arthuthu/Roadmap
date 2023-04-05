CREATE PROCEDURE [dbo].[spRoadmap_DeleteAllUserRoadmaps]
	@UserId uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Roadmaps]
	WHERE UserId = @UserId;
END
