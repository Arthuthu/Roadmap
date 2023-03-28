CREATE PROCEDURE [dbo].[spComentarioVotes_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[ComentarioVotes]
	WHERE Id = @Id;
END
