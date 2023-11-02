CREATE PROCEDURE [dbo].[spNode_Add]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(2000),
	@CreatedDate datetime2(7),
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Nodes] (Id, Name, Description, CreatedDate, RoadmapId)
	VALUES (@Id, @Name, @Description, @CreatedDate, @RoadmapId);
END
