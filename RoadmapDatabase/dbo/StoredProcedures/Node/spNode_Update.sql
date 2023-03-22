CREATE PROCEDURE [dbo].[spNode_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@UpdateDate datetime2(7),
	@RoadmapId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Nodes]
	SET Name = @Name, Description = @Description, RoadmapId = @RoadmapId, UpdatedDate = @UpdateDate
	WHERE Id = @Id
END