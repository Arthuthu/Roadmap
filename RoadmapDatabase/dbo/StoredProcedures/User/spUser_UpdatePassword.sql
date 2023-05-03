CREATE PROCEDURE [dbo].[spUser_UpdatePassword]
	@Id uniqueidentifier,
	@Password nvarchar(50),
	@UpdatedDate datetime2(7)
AS
BEGIN
	UPDATE dbo.[Users]
	SET Password = @Password, UpdatedDate = @UpdatedDate
	WHERE Id = @Id
END