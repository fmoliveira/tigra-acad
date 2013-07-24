IF OBJECT_ID('[Tigra].[UseCases]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[UseCases]
	(
		[RequirementId] INT NOT NULL,
		[Revision] INT NOT NULL,
		[Summary] VARCHAR(120) NOT NULL,
		[Actors] VARCHAR(250) NOT NULL,
		[PreConditions] VARCHAR(250) NOT NULL,
		[PostConditions] VARCHAR(250) NOT NULL,
		CONSTRAINT [pk_TigraUseCases_Key] PRIMARY KEY ([RequirementId], [Revision]),
		CONSTRAINT [fk_TigraUseCases_Rev] FOREIGN KEY ([RequirementId], [Revision]) REFERENCES [Tigra].[RequirementRevisions] ([RequirementId], [Revision])
	);

END
GO
