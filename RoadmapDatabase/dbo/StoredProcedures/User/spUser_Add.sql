CREATE PROCEDURE [dbo].[spUser_Add]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Email nvarchar(50),
	@Password nvarchar(50),
	@ConfirmationCode uniqueidentifier,
	@ConfirmationCodeExpirationDate datetime2(7),
	@PasswordHash varbinary(MAX),
	@PasswordSalt varbinary(MAX),
	@CreatedDate datetime2
AS
BEGIN
	INSERT INTO dbo.[Users] (Id, Username, Email, Password, ConfirmationCode,
	ConfirmationCodeExpirationDate, PasswordHash, PasswordSalt, CreatedDate)
	VALUES (@Id, @Username, @Email, @Password, @ConfirmationCode,
	@ConfirmationCodeExpirationDate, @PasswordHash, @PasswordSalt, @CreatedDate);
END
