CREATE PROCEDURE [dbo].[spDenuncias_Update]
	@Id uniqueidentifier,
	@Description nvarchar(1000),
	@Type nvarchar(20),
	@Username nvarchar(50),
	@RoadmapId uniqueidentifier,
	@CommentId uniqueidentifier,
	@UpdatedDate datetime2(7)
AS
BEGIN
	UPDATE dbo.[Denuncias]
	SET Description = @Description, Type = @Type, Username = @Username, RoadmapId = @RoadmapId, CommentId = @CommentId, UpdatedDate = @UpdatedDate
	WHERE Id = @Id
END