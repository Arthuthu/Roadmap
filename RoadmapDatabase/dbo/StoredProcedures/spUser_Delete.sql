CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[Users]
	WHERE Id = @Id;
END
