CREATE PROCEDURE [dbo].[spUser_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE Id = @Id;
END
