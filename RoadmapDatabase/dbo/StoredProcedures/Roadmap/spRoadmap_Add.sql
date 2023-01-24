CREATE PROCEDURE [dbo].[spRoadmap_Add]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(20),
	@UserId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Roadmaps] (Id, Name, Description, UserId)
	VALUES (@Id, @Name, @Description, @UserId);
END
