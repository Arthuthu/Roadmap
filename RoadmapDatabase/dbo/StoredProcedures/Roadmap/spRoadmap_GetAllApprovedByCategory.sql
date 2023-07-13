CREATE PROCEDURE [dbo].[spRoadmap_GetAllApprovedByCategory]
	@Category nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.[Roadmaps]
	WHERE IsApproved = 1 AND Category = @Category
END
