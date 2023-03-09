CREATE PROCEDURE [dbo].[spRoadmap_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@Category nvarchar(50),
	@IsApproved nvarchar(1),
	@IsHidden nvarchar(1),
	@UpdatedDate datetime2(7),
	@UserId uniqueidentifier
AS
BEGIN
	UPDATE dbo.[Roadmaps]
	SET Name = @Name, Description = @Description, Category = @Category,
	IsApproved = @IsApproved, IsHidden = @IsHidden, UpdatedDate = @UpdatedDate, UserId = @UserId
	WHERE Id = @Id
END