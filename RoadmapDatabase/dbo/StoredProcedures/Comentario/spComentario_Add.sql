CREATE PROCEDURE [dbo].[spComentario_Add]
	@Id uniqueidentifier,
	@Description nvarchar(1000),
	@AuthorName nvarchar(50),
	@CreatedDate datetime2,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Comentarios] (Id, Description, AuthorName, CreatedDate, UserId, RoadmapId)
	VALUES (@Id, @Description, @AuthorName, @CreatedDate, @UserId, @RoadmapId);
END
