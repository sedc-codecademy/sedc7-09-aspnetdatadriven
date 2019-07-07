CREATE PROCEDURE [dbo].[sp_InsertTask]
	@Title nvarchar(100),
	@Description nvarchar(300),
	@Priority int,
	@Status int,
	@Type int,
	@Id int out
AS
	insert into dbo.Tasks (Title, Description, Priority, Status, Type)
	values (@Title, @Description, @Priority, @Status, @Type);

	select @Id = SCOPE_IDENTITY(); 
RETURN 0
