CREATE PROCEDURE [dbo].[spNode_Update]
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@Description nvarchar(200),
	@UpdatedDate datetime2(7)
AS
BEGIN
	UPDATE dbo.[Nodes]
	SET Name = @Name, Description = @Description, UpdatedDate = @UpdatedDate
	WHERE Id = @Id
END