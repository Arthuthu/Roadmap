CREATE PROCEDURE [dbo].[spComentario_Add]
	@Id uniqueidentifier,
	@Descricao nvarchar(1000),
	@CreatedDate datetime2,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Comentarios] (Id, Descricao, CreatedDate, UserId, RoadmapId)
	VALUES (@Id, @Descricao, @CreatedDate, @UserId, @RoadmapId);
END
