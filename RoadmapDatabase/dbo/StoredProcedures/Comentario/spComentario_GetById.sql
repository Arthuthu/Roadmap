CREATE PROCEDURE [dbo].[spComentario_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Comentarios]
	WHERE Id = @Id;
END
