CREATE PROCEDURE [dbo].[spNode_Add]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(20),
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Nodes] (Id, Name, Description, RoadmapId)
	VALUES (@Id, @Name, @Description, @RoadmapId);
END
