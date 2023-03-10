CREATE PROCEDURE [dbo].[spComentario_Add]
	@Id uniqueidentifier,
	@Comentario nvarchar(1000),
	@CreatedDate datetime2,
	@UserId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Comentarios] (Id, Comentario, CreatedDate, UserId)
	VALUES (@Id, @Comentario, @CreatedDate, @UserId);
END
