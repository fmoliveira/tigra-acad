IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'RequirementRevisions')
BEGIN

	CREATE TABLE [Tigra].[RequirementRevisions]
	(
		[RevisionID] BIGINT IDENTITY(1,1) NOT NULL,
		[RequirementID] INT NOT NULL,
		[RevisionNumber] SMALLINT NOT NULL,
		[RevisionDate] DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
		[UserID] INT NOT NULL,
		[Message] VARCHAR(250) NOT NULL CHECK(LEN([Message]) >= 10),
		[Title] VARCHAR(100) NOT NULL CHECK(LEN([Title]) >= 10),
		CONSTRAINT [pk_Tigra_RequirementRevisions_Key] PRIMARY KEY ([RevisionID]),
		CONSTRAINT [fk_Tigra_RequirementRevisions_Req] FOREIGN KEY ([RequirementID]) REFERENCES [Tigra].[Requirements] ([RequirementID]),
		CONSTRAINT [fk_Tigra_RequirementRevisions_User] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserID])
	);

END
GO