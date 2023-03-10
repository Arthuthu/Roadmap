CREATE PROCEDURE [dbo].[spComentario_Update]
	@Id uniqueidentifier,
	@Comentario nvarchar(1000),
	@UpdatedDate datetime2,
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Comentarios]
	SET Comentario = @Comentario, UpdatedDate = @UpdatedDate, UserId = @UserId
	WHERE Id = @Id
END