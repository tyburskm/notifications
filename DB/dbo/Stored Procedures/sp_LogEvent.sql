






CREATE PROCEDURE [dbo].[sp_LogEvent](@Source nvarchar(50), @Message nvarchar(max), @Type varchar(20))
AS
BEGIN

	INSERT INTO Logs 
	(Source, Message, Type)
	VALUES
	(@Source, @Message, @Type)
END
