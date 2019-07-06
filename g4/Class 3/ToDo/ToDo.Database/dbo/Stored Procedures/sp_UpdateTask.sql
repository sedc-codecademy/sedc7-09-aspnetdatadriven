CREATE PROCEDURE [dbo].[sp_UpdateTask]
	@Title nvarchar(100),
	@Description nvarchar(300),
	@Priority int,
	@Status int,
	@Type int,
	@Id int
AS
	update dbo.Tasks
	set Description = @Description,
		Priority = @Priority,
		Title = @Title,
		Status = @Status,
		Type = @Type
	where Id = @Id;
RETURN 0