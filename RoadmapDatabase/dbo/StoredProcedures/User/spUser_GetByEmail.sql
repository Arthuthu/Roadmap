CREATE PROCEDURE [dbo].[spUser_GetByEmail]
	@Email nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE Email = @Email;
END
