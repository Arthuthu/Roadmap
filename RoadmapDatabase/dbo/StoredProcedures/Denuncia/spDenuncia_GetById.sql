CREATE PROCEDURE [dbo].[spDenuncia_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Denuncias]
	WHERE Id = @Id;
END
