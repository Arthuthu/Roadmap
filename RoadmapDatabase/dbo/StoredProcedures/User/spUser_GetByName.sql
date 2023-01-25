CREATE PROCEDURE [dbo].[spUser_GetByName]
	@Username nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE Username = @Username;
END
