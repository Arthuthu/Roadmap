CREATE PROCEDURE [dbo].[spComentarioVotes_Add]
	@Id uniqueidentifier,
	@UserId uniqueidentifier,
	@ComentarioId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[ComentarioVotes] (Id, UserId, ComentarioId)
	VALUES (@Id, @UserId, @ComentarioId);
END
