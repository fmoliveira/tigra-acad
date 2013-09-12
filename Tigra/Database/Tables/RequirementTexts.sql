IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementTexts')
BEGIN

	CREATE TABLE [Tigra].[RequirementTexts]
	(
		[RevisionID] BIGINT NOT NULL,
		[Text] TEXT NOT NULL,
		CONSTRAINT [pk_Tigra_RequirementTexts_Key] PRIMARY KEY ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementTexts_Rev] FOREIGN KEY ([RevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID])
	);

END
GO