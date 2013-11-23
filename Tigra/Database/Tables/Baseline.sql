IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE schema_id = SCHEMA_ID(N'Tigra') AND name = N'Baseline')
BEGIN

	CREATE TABLE [Tigra].[Baseline]
	(
		[BaselineID] INT IDENTITY(1,1) NOT NULL,
		[CellID] INT NOT NULL,
		[UserID] INT NOT NULL,
		[SetDate] DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
		[Message] VARCHAR(20) NOT NULL,
		CONSTRAINT [pk_Tigra_Baseline_Key] PRIMARY KEY ([BaselineID])
	);

END
GO