CREATE PROCEDURE [dbo].[spRoadmap_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Roadmaps]
	SET Name = @Name, Description = @Description, UserId = @UserId
	WHERE Id = @Id
END