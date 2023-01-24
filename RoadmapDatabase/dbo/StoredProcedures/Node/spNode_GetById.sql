CREATE PROCEDURE [dbo].[spNode_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Nodes]
	WHERE Id = @Id;
END
