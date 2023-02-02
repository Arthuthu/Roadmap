CREATE PROCEDURE [dbo].[spRoadmap_Add]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(20),
	@Category nvarchar(50),
	@UserId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Roadmaps] (Id, Name, Description, Category, UserId)
	VALUES (@Id, @Name, @Description, @Category, @UserId);
END
