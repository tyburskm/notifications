






CREATE PROCEDURE [dbo].[sp_GetNotificationParameters](@NotificationId int)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT
	   [Id]
      ,[NotificationId]
      ,[Title]
      ,[NotificationText]
      ,ISNULL([BackgroundColor],'') as [BackgroundColor]
      ,ISNULL([BackgroundImage],'') as [BackgroundImage]
      ,ISNULL([TextColor],'') as [TextColor]
      ,ISNULL([Maximized],'false') as [Maximized]
      ,ISNULL([Width],0) as [Width]
      ,ISNULL([Height],0) as [Height]
      ,ISNULL([Autosize],'false') as [Autosize]
	  ,ISNULL([Gradient],'') as [Gradient]
	FROM Parameters 
	WHERE NotificationId = @NotificationId
END
