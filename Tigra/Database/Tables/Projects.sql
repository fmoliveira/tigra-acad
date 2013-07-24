IF OBJECT_ID('[Tigra].[Projects]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Projects]
	(
		[ProjectId] INT IDENTITY(1,1) NOT NULL,
		[ProjectName] VARCHAR(20) NOT NULL,
		[Description] VARCHAR(100) NOT NULL,
		CONSTRAINT [pk_TigraProjects_Key] PRIMARY KEY ([ProjectId]),
		CONSTRAINT [un_TigraProjects_Pname] UNIQUE ([ProjectName])
	);

END
GO
