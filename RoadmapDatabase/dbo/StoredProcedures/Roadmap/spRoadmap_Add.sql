CREATE PROCEDURE [dbo].[spRoadmap_Add]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(20),
	@Category nvarchar(50),
	@UserId uniqueidentifier,
	@CreatedDate datetime2
AS
BEGIN
	INSERT INTO dbo.[Roadmaps] (Id, Name, Description, Category, UserId, CreatedDate)
	VALUES (@Id, @Name, @Description, @Category, @UserId, @CreatedDate);
END
