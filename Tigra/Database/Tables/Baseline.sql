IF OBJECT_ID('[Tigra].[Baseline]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Baseline]
	(
		[BaselineId] INT IDENTITY(1,1) NOT NULL,
		[ProjectId] INT NOT NULL,
		[ModuleId] INT NULL,
		[Version] VARCHAR(20) NULL,
		[Built] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[UserId] INT NOT NULL,
		[Comment] VARCHAR(MAX) NOT NULL,
		CONSTRAINT [pk_TigraBaseline_Key] PRIMARY KEY ([BaselineId]),
		CONSTRAINT [fk_TigraBaseline_Pjid] FOREIGN KEY ([ProjectId]) REFERENCES [Tigra].[Projects] ([ProjectId]),
		CONSTRAINT [fk_TigraBaseline_Mid] FOREIGN KEY ([ModuleId]) REFERENCES [Tigra].[Modules] ([ModuleId]),
		CONSTRAINT [un_TigraBaseline_Vers] UNIQUE ([ProjectId], [ModuleId], [Version])
	);

END
GO
