CREATE PROCEDURE [dbo].[createAuthor]
	@AuthorName NVARCHAR(100),
	@DateOfBirth DATE,
	@DateOfDeath DATE = NULL,
	@ID INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Authors
	VALUES
	(
		@AuthorName,
		@DateOfBirth,
		@DateOfDeath
	);
	SELECT @ID = SCOPE_IDENTITY();
END