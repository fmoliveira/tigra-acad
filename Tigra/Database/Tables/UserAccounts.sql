IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'UserAccounts')
BEGIN

	CREATE TABLE [Tigra].[UserAccounts]
	(
		[UserID] INT IDENTITY(1,1) NOT NULL,
		[Email] VARCHAR(80) NOT NULL,
		[Password] BINARY(32) NULL,
		[RegisterDate] DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
		CONSTRAINT [pk_Tigra_UserAccounts_Key] PRIMARY KEY ([UserID]),
		CONSTRAINT [un_Tigra_UserAccounts_Usr] UNIQUE ([Email])
	);

END
GO
