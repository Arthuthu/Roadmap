CREATE PROCEDURE [dbo].[spRoadmap_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@Category nvarchar(50),
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Roadmaps]
	SET Name = @Name, Description = @Description, Category = @Category, UserId = @UserId
	WHERE Id = @Id
END