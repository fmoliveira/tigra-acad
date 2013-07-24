IF OBJECT_ID('[Tigra].[RequirementRevisions]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[RequirementRevisions]
	(
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[UserId] INT NOT NULL,
		[Modified] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
		[Comment] VARCHAR(250) NOT NULL,
		[RequirementName] VARCHAR(40) NULL,
		[RequirementType] TINYINT NULL,
		[TemplateId] INT NULL,
		CONSTRAINT [pk_TigraRequirementRevisions_Key] PRIMARY KEY ([RequirementId], [Revision]),
		CONSTRAINT [fk_TigraRequirementRevisions_Reqid] FOREIGN KEY ([RequirementId]) REFERENCES [Tigra].[Requirements] ([RequirementId]),
		CONSTRAINT [fk_TigraRequirementRevisions_Tmplid] FOREIGN KEY ([TemplateId]) REFERENCES [Tigra].[Templates] ([TemplateId]),
		CONSTRAINT [fk_TigraRequirementRevisions_Uid] FOREIGN KEY ([UserId]) REFERENCES [Tigra].[Users] ([UserId]),
	);

END
GO
