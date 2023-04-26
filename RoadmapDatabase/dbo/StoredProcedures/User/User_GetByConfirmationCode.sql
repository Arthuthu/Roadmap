CREATE PROCEDURE [dbo].[spUser_GetByConfirmationCode]
	@ConfirmationCode uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE ConfirmationCode = @ConfirmationCode;
END
