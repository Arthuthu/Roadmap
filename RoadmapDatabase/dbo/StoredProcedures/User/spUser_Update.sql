CREATE PROCEDURE [dbo].[spUser_Update]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Password nvarchar(50),
	@ConfirmationCode uniqueidentifier,
	@ConfirmationCodeExpirationDate datetime2(7),
	@RestorationCode uniqueidentifier,
	@RestorationCodeExpirationDate datetime2(7),
	@Bio nvarchar(1000),
	@IsBanned nvarchar(1),
	@IsConfirmed nvarchar(1),
	@UpdatedDate datetime2(7)
AS
BEGIN
	UPDATE dbo.[Users]
	SET Username = @Username, Password = @Password,
	ConfirmationCode = @ConfirmationCode, ConfirmationCodeExpirationDate = @ConfirmationCodeExpirationDate,
	RestorationCode = @RestorationCode, RestorationCodeExpirationDate = @RestorationCodeExpirationDate,
	Bio = @Bio, IsBanned = @IsBanned,IsConfirmed = @IsConfirmed, UpdatedDate = @UpdatedDate
	WHERE Id = @Id
END