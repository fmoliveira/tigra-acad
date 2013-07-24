IF OBJECT_ID('[Tigra].[RequirementRelations]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[RequirementRelations]
	(
		[SourceReqId] INT NOT NULL,
		[DestReqId] INT NOT NULL,
		[Modified] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[UserId] INT NOT NULL,
		[RelationType] TINYINT NULL,
		CONSTRAINT [pk_RequirementRelations_Key] PRIMARY KEY ([SourceReqId], [DestReqId], [Modified]),
		CONSTRAINT [fk_RequirementRelations_Srcid] FOREIGN KEY ([SourceReqId]) REFERENCES [Tigra].[Requirements] ([RequirementId]),
		CONSTRAINT [fk_RequirementRelations_Destid] FOREIGN KEY ([DestReqId]) REFERENCES [Tigra].[Requirements] ([RequirementId]),
		CONSTRAINT [fk_RequirementRelations_Uid] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Users] ([UserId])
	);

END
GO
