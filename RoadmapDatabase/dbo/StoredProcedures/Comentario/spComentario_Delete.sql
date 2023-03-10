CREATE PROCEDURE [dbo].[spComentario_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Comentarios]
	WHERE Id = @Id;
END
