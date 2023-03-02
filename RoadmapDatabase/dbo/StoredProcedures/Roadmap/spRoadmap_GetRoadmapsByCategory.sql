CREATE PROCEDURE [dbo].[spRoadmap_GetRoadmapsByCategory]
	@Category nvarchar(50)
AS
BEGIN
	select * from dbo.[Roadmaps]
	WHERE Category = @Category
END
