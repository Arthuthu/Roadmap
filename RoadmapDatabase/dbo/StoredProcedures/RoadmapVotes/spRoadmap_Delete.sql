CREATE PROCEDURE [dbo].[spRoadmapVotes_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE FROM dbo.[RoadmapVotes]
	WHERE Id = @Id;
END
