CREATE PROCEDURE [dbo].[spRoadmapVotes_GetAll]
AS
BEGIN
	SELECT * FROM dbo.[RoadmapVotes];
END