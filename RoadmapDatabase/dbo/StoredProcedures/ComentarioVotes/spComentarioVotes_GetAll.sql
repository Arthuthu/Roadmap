CREATE PROCEDURE [dbo].[spComentarioVotes_GetAll]
	@UserId uniqueidentifier,
	@ComentarioId uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[ComentarioVotes]
	WHERE UserId = @UserId AND ComentarioId = @ComentarioId
END