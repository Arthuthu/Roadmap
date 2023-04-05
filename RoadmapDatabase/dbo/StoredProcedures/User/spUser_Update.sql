CREATE PROCEDURE [dbo].[spUser_Update]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Bio nvarchar(1000),
	@IsBanned nvarchar(1)
AS
BEGIN
	UPDATE dbo.[Users]
	SET Username = @Username, Password = @Password, Bio = @Bio, IsBanned = @IsBanned
	WHERE Id = @Id
END