CREATE PROCEDURE [dbo].[createNovel]
	@Title NVARCHAR(150),
	@AuthorID INT,
	@IsRead BIT,
	@ID INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Novels
	VALUES
	(
		@Title,
		@AuthorID,
		@IsRead
	);
	SELECT @ID = SCOPE_IDENTITY();
END
