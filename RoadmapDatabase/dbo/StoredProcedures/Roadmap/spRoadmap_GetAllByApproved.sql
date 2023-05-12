CREATE PROCEDURE [dbo].[spRoadmap_GetAllByApproved]
AS
BEGIN
	SELECT * FROM dbo.[Roadmaps]
	WHERE IsApproved = 1
END