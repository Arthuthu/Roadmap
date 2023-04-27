CREATE PROCEDURE [dbo].[spUser_UpdateEmailConfirmation]
	@Id uniqueidentifier,
	@ConfirmationCode uniqueidentifier,
	@ConfirmationCodeExpirationDate datetime2(7),
	@IsConfirmed nvarchar(1),
	@UpdatedDate datetime2(7)
AS
BEGIN
	UPDATE dbo.[Users]
	SET ConfirmationCode = @ConfirmationCode, ConfirmationCodeExpirationDate = @ConfirmationCodeExpirationDate,
	IsConfirmed = @IsConfirmed, UpdatedDate = @UpdatedDate
	WHERE Id = @Id
END