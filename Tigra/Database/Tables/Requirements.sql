IF OBJECT_ID('[Tigra].[Requirements]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Requirements]
	(
		[RequirementId] INT IDENTITY(1,1) NOT NULL,
		[Revision] INT NOT NULL,
		[ProjectId] INT NOT NULL,
		[RequirementName] VARCHAR(40) NOT NULL,
		[UserId] INT NOT NULL,
		[Created] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[Modified] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		CONSTRAINT [pk_TigraRequirements_Key] PRIMARY KEY ([RequirementId]),
		CONSTRAINT [un_TigraRequirements_Rname] UNIQUE ([ProjectId], [RequirementName]),
		CONSTRAINT [fk_TigraRequirements_Pjid] FOREIGN KEY ([ProjectId]) REFERENCES [Tigra].[Projects] ([ProjectId]),
		CONSTRAINT [fk_TigraRequirements_Uid] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Users] ([UserId])
	);

END
GO
