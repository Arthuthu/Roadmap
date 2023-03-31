CREATE PROCEDURE [dbo].[spDenuncia_Add]
	@Id uniqueidentifier,
	@Description nvarchar(1000),
	@Type nvarchar(20),
	@AuthorId uniqueidentifier,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier,
	@CommentId uniqueidentifier,
	@CreatedDate datetime2(7)
AS
BEGIN
	INSERT INTO dbo.[Denuncias] (Id, Description, Type, AuthorId, UserId, RoadmapId, CommentId, CreatedDate)
	VALUES (@Id, @Description, @Type, @AuthorId, @UserId, @RoadmapId, @CommentId, @CreatedDate);
END
