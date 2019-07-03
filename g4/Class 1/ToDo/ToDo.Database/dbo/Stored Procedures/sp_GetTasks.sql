CREATE PROCEDURE [dbo].[sp_GetTasks]
AS
	SELECT t.Id, t.Description, t.Priority, t.Status, t.Title, t.Type
	FROM Tasks t
RETURN 0