IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementRatings')
BEGIN

	CREATE TABLE [Tigra].[RequirementRatings]
	(
		[RevisionID] BIGINT NOT NULL,
		[Approved] BIT NOT NULL,
		CONSTRAINT [pk_Tigra_RequirementRatings_Key] PRIMARY KEY ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRatings_Rev] FOREIGN KEY ([RevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID])
	);

END
GO
