IF OBJECT_ID('[Tigra].[Modules]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Modules]
	(
		[ModuleId] INT IDENTITY(1,1) NOT NULL,
		[ProjectId] INT NOT NULL,
		[ModuleName] VARCHAR(30) NOT NULL,
		[Description] VARCHAR(100) NOT NULL,
		CONSTRAINT [pk_TigraModules_Key] PRIMARY KEY ([ModuleId]),
		CONSTRAINT [un_TigraModules_Mname] UNIQUE ([ProjectId], [ModuleName]),
		CONSTRAINT [fk_TigraModules_Pjid] FOREIGN KEY ([ProjectId]) REFERENCES [Tigra].[Projects] ([ProjectId])
	);

END
GO
