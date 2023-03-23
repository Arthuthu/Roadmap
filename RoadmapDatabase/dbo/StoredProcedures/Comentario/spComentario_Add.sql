﻿CREATE PROCEDURE [dbo].[spComentario_Add]
	@Id uniqueidentifier,
	@Description nvarchar(1000),
	@CreatedDate datetime2,
	@UserId uniqueidentifier,
	@RoadmapId uniqueidentifier
AS
BEGIN
	INSERT INTO dbo.[Comentarios] (Id, Description, CreatedDate, UserId, RoadmapId)
	VALUES (@Id, @Description, @CreatedDate, @UserId, @RoadmapId);
END
