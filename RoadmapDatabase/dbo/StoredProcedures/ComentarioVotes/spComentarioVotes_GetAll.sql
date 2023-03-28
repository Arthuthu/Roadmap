CREATE PROCEDURE [dbo].[spComentarioVotes_GetAll]
AS
BEGIN
	SELECT * FROM dbo.[ComentarioVotes];
END