IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'UserRatings')
BEGIN

	CREATE TABLE [Tigra].[UserRatings]
	(
		[RevisionID] BIGINT NOT NULL,
		[UserID] INT NOT NULL,
		[Comments] VARCHAR(250) NOT NULL,
		[Approved] BIT NOT NULL,
		CONSTRAINT [pk_Tigra_UserRatings_Key] PRIMARY KEY ([RevisionID], [UserID]),
		CONSTRAINT [fk_Tigra_UserRatings_Rev] FOREIGN KEY ([RevisionID]) REFERENCES [Tigra].[RequirementRevisions] ([RevisionID]),
		CONSTRAINT [fk_Tigra_UserRatings_Usr] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserId])
	);

END
GO
