CREATE PROCEDURE [dbo].[spUser_Add]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Password nvarchar(50),
	@PasswordHash varbinary(MAX),
	@PasswordSalt varbinary(MAX)
AS
BEGIN
	INSERT INTO dbo.[Users] (Id, Username, Password, PasswordHash, PasswordSalt)
	VALUES (@Id, @Username, @Password, @PasswordHash, @PasswordSalt);
END
