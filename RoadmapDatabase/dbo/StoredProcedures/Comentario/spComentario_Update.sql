CREATE PROCEDURE [dbo].[spComentario_Update]
	@Id uniqueidentifier,
	@Descricao nvarchar(1000),
	@UpdatedDate datetime2,
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Comentarios]
	SET Descricao = @Descricao, UpdatedDate = @UpdatedDate, UserId = @UserId
	WHERE Id = @Id
END