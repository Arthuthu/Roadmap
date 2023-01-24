CREATE PROCEDURE [dbo].[spNode_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@RoadmapId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Nodes]
	SET Name = @Name, Description = @Description, RoadmapId = @RoadmapId
	WHERE Id = @Id
END