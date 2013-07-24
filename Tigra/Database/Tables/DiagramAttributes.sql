IF OBJECT_ID('[Tigra].[DiagramAttributes]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[DiagramAttributes]
	(
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[AttributeName] VARCHAR(20) NOT NULL,
		[Description] VARCHAR(50) NULL,
		[StrongType] VARCHAR(30) NOT NULL,
		CONSTRAINT [pk_TigraDiagramAttributes_Key] PRIMARY KEY ([RequirementId], [Revision], [AttributeName]),
		CONSTRAINT [fk_TigraDiagramAttributes_Rev] FOREIGN KEY ([RequirementId], [Revision]) REFERENCES [Tigra].[RequirementRevisions] ([RequirementId], [Revision])
	);

END
GO
