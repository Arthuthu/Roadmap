CREATE PROCEDURE [dbo].[spRoadmap_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Roadmaps]
	WHERE Id = @Id;
END
