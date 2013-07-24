IF OBJECT_ID('[Tigra].[TemplateFields]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[TemplateFields]
	(
		[TemplateId] INT IDENTITY(1,1) NOT NULL,
		[FieldIndex] TINYINT NOT NULL,
		[FieldName] VARCHAR(20) NOT NULL,
		[FieldType] TINYINT NOT NULL,
		[Format] VARCHAR(20) NOT NULL,
		[Hint] VARCHAR(50) NULL,
		[URI] VARCHAR(100) NULL,
		CONSTRAINT [pk_TigraTemplateFields_Key] PRIMARY KEY ([TemplateId], [FieldIndex]),
		CONSTRAINT [un_TigraTemplateFields_Fname] UNIQUE ([TemplateId], [FieldName]),
		CONSTRAINT [fk_TigraTemplateFields_Templ] FOREIGN KEY ([TemplateId]) REFERENCES [Tigra].[Templates] ([TemplateId])
	);

END
GO
