IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Roles')
BEGIN

	CREATE TABLE [Tigra].[Roles]
	(
		[RoleID] INT IDENTITY(1,1) NOT NULL,
		[RoleName] VARCHAR(50) NOT NULL,
		[Authorisations] VARBINARY(4096) NOT NULL DEFAULT 0,
		CONSTRAINT [pk_Tigra_Roles_Key] PRIMARY KEY ([RoleID]),
		CONSTRAINT [un_Tigra_Roles_Name] UNIQUE ([RoleName])
	);

END
GO