CREATE PROCEDURE [dbo].[spUser_Add]
	@Id uniqueidentifier,
	@Username nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.[Users] (Id, Username, Password)
	VALUES (@Id, @Username, @Password);
END
