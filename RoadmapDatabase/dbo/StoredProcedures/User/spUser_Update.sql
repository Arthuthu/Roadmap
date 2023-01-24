CREATE PROCEDURE [dbo].[spUser_Update]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	UPDATE dbo.[Users]
	SET Username = @Username, Password = @Password
	WHERE Id = @Id
END