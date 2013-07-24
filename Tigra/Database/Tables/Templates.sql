IF OBJECT_ID('[Tigra].[Templates]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Templates]
	(
		[TemplateId] INT IDENTITY(1,1) NOT NULL,
		[ProjectId] INT NOT NULL,
		[TemplateName] VARCHAR(30),
		CONSTRAINT [pk_TigraTemplates_Key] PRIMARY KEY ([TemplateId]),
		CONSTRAINT [un_TigraTemplates_Tname] UNIQUE ([ProjectId], [TemplateName]),
		CONSTRAINT [fk_TigraTemplates_Pjid] FOREIGN KEY ([ProjectId]) REFERENCES [Tigra].[Projects] ([ProjectId])
	);

END
GO
