CREATE PROCEDURE [dbo].[spUser_GetById]
	@Id int
AS
BEGIN
	SELECT * FROM dbo.[User]
	WHERE Id = @Id;
END
