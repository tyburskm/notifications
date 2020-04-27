






CREATE PROCEDURE sp_GetActiveNotifications
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM Notifications 
	WHERE GETDATE() between ValidFrom and ValidTo
END
