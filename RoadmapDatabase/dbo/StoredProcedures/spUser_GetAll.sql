CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN
	SELECT * FROM dbo.[Users];
END