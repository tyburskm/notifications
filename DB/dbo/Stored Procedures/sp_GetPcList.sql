CREATE PROCEDURE [dbo].[sp_GetPcList](@NotificationId int)
AS
BEGIN

	if exists(select * from NotificationInGroup a
	join Groups b on a.GroupId = b.Id where NotificationId = @NotificationId and active = 'true')
	begin
		SELECT a.PcName FROM PCs a
		JOIN PcInGroup b on a.Id = b.PCId
		JOIN NotificationInGroup c on b.GroupId = c.GroupId
		JOIN Groups d on b.GroupId = d.Id
		WHERE c.NotificationId = @NotificationId
		AND d.Active = 'true'
	end
	else
		select '*' as PcName
	
END