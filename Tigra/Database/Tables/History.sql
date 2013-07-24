IF OBJECT_ID('[Tigra].[History]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[History]
	(
		[BaselineId] INT NOT NULL,
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		CONSTRAINT [pk_TigraHistory_Key] PRIMARY KEY ([BaselineId], [RequirementId]),
		CONSTRAINT [fk_TigraHistory_Bsid] FOREIGN KEY ([BaselineId]) REFERENCES [Tigra].[Baseline] ([BaselineId]),
		CONSTRAINT [fk_TigraHistory_Rev] FOREIGN KEY ([RequirementId], [Revision]) REFERENCES [Tigra].[RequirementRevisions] ([RequirementId], [Revision])
	);

END
GO
