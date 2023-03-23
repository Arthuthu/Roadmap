CREATE PROCEDURE [dbo].[spComentario_Update]
	@Id uniqueidentifier,
	@Description nvarchar(1000),
	@UpdatedDate datetime2,
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Comentarios]
	SET Description = @Description, UpdatedDate = @UpdatedDate, UserId = @UserId
	WHERE Id = @Id
END