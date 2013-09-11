IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Stories')
BEGIN

	CREATE TABLE [Tigra].[Stories]
	(
		[StoryID] INT IDENTITY(1,1) NOT NULL,
		[CellID] INT NOT NULL,
		[UserID] INT NOT NULL,
		[RequestDate] DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
		[Summary] VARCHAR(100) NOT NULL,
		[Text] TEXT NOT NULL,
		CONSTRAINT [pk_Tigra_Stories_Key] PRIMARY KEY ([StoryID]),
		CONSTRAINT [fk_Tigra_Stories_Cell] FOREIGN KEY ([CellID]) REFERENCES [Tigra].[Cells] ([CellID]),
		CONSTRAINT [fk_Tigra_Stories_User] FOREIGN KEY ([UserID]) REFERENCES [Tigra].[UserAccounts] ([UserID])
	);

END
GO