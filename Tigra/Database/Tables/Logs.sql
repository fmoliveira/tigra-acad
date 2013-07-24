IF OBJECT_ID('[Tigra].[Logs]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Logs]
	(
		[EventTime] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[ActionType] TINYINT NOT NULL,
		[LeaveTime] DATETIME2 NULL,
		[UserId] INT NOT NULL,
		[ProjectId] INT NULL,
		[RequirementId] INT NULL,
		[Revision] INT NULL,
		CONSTRAINT [pk_TigraLogs_Key] PRIMARY KEY ([EventTime], [ActionType], [UserId])
	);

END
GO
