IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'UserProfiles')
BEGIN

	CREATE TABLE [Tigra].[UserProfiles]
	(
		[UserID] INT NOT NULL,
		[FullName] VARCHAR(50) NULL,
		[Picture] VARBINARY(MAX) NULL,
		[Location] VARCHAR(50) NULL,
		[Biography] VARCHAR(500) NULL
		CONSTRAINT [pk_Tigra_UserProfiles_Key] PRIMARY KEY ([UserID]),
		CONSTRAINT [fk_Tigra_UserProfiles_Usr] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserID])
	);

END
GO
