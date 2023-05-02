CREATE PROCEDURE [dbo].[spUser_GetByRestorationCode]
	@RestorationCode uniqueidentifier
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE RestorationCode = @RestorationCode;
END
