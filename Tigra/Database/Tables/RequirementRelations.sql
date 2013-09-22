IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementRelations')
BEGIN

	CREATE TABLE [Tigra].[RequirementRelations]
	(
		[LeftRevisionID] BIGINT NOT NULL,
		[RightRevisionID] BIGINT NOT NULL,
		CONSTRAINT [pk_Tigra_RequirementRelations_Key] PRIMARY KEY ([LeftRevisionID], [RightRevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRelations_Left] FOREIGN KEY ([LeftRevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRelations_Right] FOREIGN KEY ([RightRevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID])
	);

END
GO