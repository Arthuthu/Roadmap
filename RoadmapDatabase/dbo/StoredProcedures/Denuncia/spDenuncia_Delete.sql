CREATE PROCEDURE [dbo].[spDenuncia_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Denuncias]
	WHERE Id = @Id;
END
