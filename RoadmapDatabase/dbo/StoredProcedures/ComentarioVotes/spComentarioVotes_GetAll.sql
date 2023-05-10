CREATE PROCEDURE [dbo].[spComentarioVotes_GetAll]
	@Id uniqueidentifier,
	@UserId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[ComentarioVotes]
	WHERE Id = @Id AND UserId = @UserId
END