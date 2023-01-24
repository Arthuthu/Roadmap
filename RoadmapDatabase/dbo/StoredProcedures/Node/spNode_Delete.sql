CREATE PROCEDURE [dbo].[spNode_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Nodes]
	WHERE Id = @Id;
END
