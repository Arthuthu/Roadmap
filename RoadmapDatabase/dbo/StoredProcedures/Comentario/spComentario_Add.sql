CREATE PROCEDURE [dbo].[spComentario_Add]
	@Id uniqueidentifier,
	@Comentario nvarchar(1000),
	@CreatedDate datetime2,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Comentarios] (Id, Comentario, CreatedDate, UserId, RoadmapId)
	VALUES (@Id, @Comentario, @CreatedDate, @UserId, @RoadmapId);
END
