CREATE PROCEDURE [dbo].[spNode_GetAll]
@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Nodes]
	WHERE RoadmapId = @RoadmapId
END