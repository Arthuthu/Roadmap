CREATE PROCEDURE [dbo].[spUser_Update]
	@Username nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.[User] (Username, Password)
	VALUES (@Username, @Password);
END