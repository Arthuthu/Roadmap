CREATE PROCEDURE [dbo].[spRoadmap_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Roadmaps]
	WHERE Id = @Id;
END
