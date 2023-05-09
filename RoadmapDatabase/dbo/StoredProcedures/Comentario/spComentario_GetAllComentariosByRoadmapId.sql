CREATE PROCEDURE [dbo].[spComentario_GetAllComentariosByRoadmapId]
	@RoadmapId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Comentarios]
	WHERE RoadmapId = @RoadmapId
END