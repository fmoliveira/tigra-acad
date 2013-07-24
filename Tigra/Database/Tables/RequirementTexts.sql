IF OBJECT_ID('[Tigra].[RequirementTexts]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[RequirementTexts]
	(
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[TemplateId] INT NOT NULL,
		[FieldIndex] TINYINT NOT NULL,
		[FieldValue] VARCHAR(MAX) NOT NULL,
		CONSTRAINT [pk_TigraRequirementTexts_Key] PRIMARY KEY ([RequirementId], [Revision], [FieldIndex]),
		CONSTRAINT [fk_TigraRequirements_Rev] FOREIGN KEY ([RequirementId], [Revision]) REFERENCES [Tigra].[RequirementRevisions] ([RequirementId], [Revision]),
		CONSTRAINT [fk_TigraRequirements_Tmplid] FOREIGN KEY ([TemplateId]) REFERENCES [Tigra].[Templates] ([TemplateId]),
		CONSTRAINT [fk_TigraRequirements_Field] FOREIGN KEY ([TemplateId], [FieldIndex]) REFERENCES [Tigra].[TemplateFields] ([TemplateId], [FieldIndex])
	);

END
GO
