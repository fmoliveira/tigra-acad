IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementRevisions')
BEGIN

	CREATE TABLE [Tigra].[RequirementRevisions]
	(
		[RevisionID] BIGINT IDENTITY(1,1) NOT NULL,
		[RequirementID] INT NOT NULL,
		[UserID] INT NOT NULL,
		[Modified] DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
		[Message] VARCHAR(250) NOT NULL CHECK(LEN([Message]) >= 10),
		[Status] TINYINT NOT NULL,
		CONSTRAINT [pk_Tigra_RequirementRevisions_Key] PRIMARY KEY ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRevisions_Req] FOREIGN KEY ([RequirementID]) REFERENCES [Tigra].[Requirements] ([RequirementID]),
		CONSTRAINT [fk_Tigra_RequirementRevisions_User] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserID])
	);

END
GO