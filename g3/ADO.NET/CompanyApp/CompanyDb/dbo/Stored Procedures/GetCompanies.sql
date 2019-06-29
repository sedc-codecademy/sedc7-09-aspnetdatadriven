CREATE PROCEDURE dbo.GetCompanies
	@companyName NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Name, Address From dbo.Companies Where [Name] Like '%' + @companyName + '%'
END