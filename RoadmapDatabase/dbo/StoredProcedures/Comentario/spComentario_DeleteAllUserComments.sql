CREATE PROCEDURE [dbo].[spComentario_DeleteAllUserComments]
	@UserId uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Comentarios]
	WHERE UserId = @UserId
END