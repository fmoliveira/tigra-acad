IF OBJECT_ID('[Tigra].[Roles]') IS NULL
BEGIN

	CREATE TABLE [Tigra].[Roles]
	(
		[RoleId] INT IDENTITY(1,1) NOT NULL,
		[RoleName] VARCHAR(20) NOT NULL,
		[Description] VARCHAR(50) NOT NULL,
		[Flags] BINARY(4) NOT NULL DEFAULT 0x00000000,
		CONSTRAINT [pk_TigraRoles_Key] PRIMARY KEY ([RoleId]),
		CONSTRAINT [un_TigraRoles_Rname] UNIQUE ([RoleName])
	);

END
GO
