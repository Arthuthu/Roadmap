CREATE PROCEDURE [dbo].[spRoadmap_GetAllByNotApproved]
AS
BEGIN
	SELECT * FROM dbo.[Roadmaps]
	WHERE IsApproved = 0
END