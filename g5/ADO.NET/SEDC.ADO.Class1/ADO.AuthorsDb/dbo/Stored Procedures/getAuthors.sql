CREATE PROCEDURE [dbo].[getAuthors]
	@authorName NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM dbo.Authors WHERE Name LIKE '%' + @authorName + '%';
END
RETURN 0
